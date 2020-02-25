// FormCartoGpsx.cs

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Data;
using System.Linq;
using ORMi;

namespace CartoGpsx
{
    [WMIClass("Win32_PnPEntity")]
    public class Device
    {
        // Pour GetPortName()
        public string Name { get; set; }
    }

    public partial class FormCartoGpsx : Form
    {
        //***************************************************************************************************
        // Variables globales
        //***************************************************************************************************

        private int numMap;                // Numéro de carte affichée : 0 => Google sat, 1 => OpenTopoMap
        private bool okGps;                // Vrai si position GPS à afficher
        private bool okGpx;                // Vrai si route GPS à afficher
        private String gpxFileName;        // Nom du fichier Gpx à afficher

        //***************************************************************************************************
        // Init et End
        //***************************************************************************************************

        public FormCartoGpsx()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void FormCartoGpsx_Load(object sender, EventArgs e)
        {
            // Port série
            try
            {
                port.PortName = GetPortName();
            }
            catch { }
            tsslIco.Image = Resource.ko;
            PortOpen();

            // Carte
            numMap = Set.Default.numMap;
            SetMap(numMap);
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gmap.CacheLocation = @".\cache\";
            gmap.MapScaleInfoEnabled = true;
            gmap.Zoom = Set.Default.zoomMap;
            gmap.Size = new Size(this.Width, this.Height);
            gmap.Position = new PointLatLng(Set.Default.latCenterMap, Set.Default.lngCenterMap);
            gmap.ShowCenter = false;

            // Overlay avec marqueur et route
            GMapOverlay ovl = new GMapOverlay("ovl");
            gmap.Overlays.Add(ovl);
            GMapMarker marker = new GMarkerGoogle(new PointLatLng(), GMarkerGoogleType.red_dot);
            marker.IsVisible = false;
            ovl.Markers.Add(marker);
            GMapRoute route = new GMapRoute(new List<PointLatLng>(), "");
            route.Stroke = new Pen(Color.Red, 4);
            route.IsVisible = false;
            ovl.Routes.Add(route);
            okGps = Set.Default.okGps;
            okGpx = Set.Default.okGpx;
            if (!okGps) EraseGps();
            if (okGpx)
            {
                gpxFileName = Set.Default.gpxFileName;
                SetGpx(gpxFileName);
            }
        }

        private void FormCartoGpsx_FormClosing(object sender, FormClosingEventArgs e)
        {
            Set.Default.numMap = numMap;
            Set.Default.zoomMap = gmap.Zoom;
            Set.Default.latCenterMap = gmap.Position.Lat;
            Set.Default.lngCenterMap = gmap.Position.Lng;
            Set.Default.okGps = okGps;
            Set.Default.okGpx = okGpx;
            Set.Default.gpxFileName = gpxFileName;
            Set.Default.Save();
        }


        //***************************************************************************************************
        // Map
        //***************************************************************************************************  

        void SetMap(int numMap)
        {
            switch (numMap)
            {
                case 0:
                    gmap.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
                    Text = "CartoGPSX : Google Satellite";
                    break;
                case 1:
                    gmap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
                    Text = "CartoGPSX : Open Topo Map";
                    break;
            }
        }

        private void BtnChangeMap_Click(object sender, EventArgs e)
        {
            numMap = 1 - numMap;
            SetMap(numMap);
        }

        //***************************************************************************************************
        // Gps
        //***************************************************************************************************  

        double Degree(string str)
        {
            double a = double.Parse(str.Replace('.', ',')) / 100;
            double e = Math.Truncate(a);
            return e + (a - e) / 0.6;
        }

        void SetGps(double lon, double lat, float alt, int sat, float hdop)
        {
            tsslLon.Text = "Lon : " + lon.ToString("#.####0°");
            tsslLat.Text = "Lat : " + lat.ToString("#.####0°");
            tsslAlt.Text = "Alt : " + alt.ToString("###m");
            tsslSat.Text = "Sat : " + sat.ToString("#");
            tsslHDOP.Text = "HDOP : " + hdop.ToString("0.#0");
            gmap.Overlays[0].Markers[0].Position = new PointLatLng(lat, lon);
            gmap.Overlays[0].Markers[0].IsVisible = true;
        }

        void EraseGps()
        {
            try { gmap.Overlays[0].Markers[0].IsVisible = false; }
            catch { }
            tsslLon.Text = "";
            tsslLat.Text = "";
            tsslAlt.Text = "";
            tsslSat.Text = "";
            tsslHDOP.Text = "";
        }

        private void TsslGps_Click(object sender, EventArgs e)
        {
            if (!port.IsOpen) return;
            okGps = !okGps;
            if (!okGps) EraseGps();
        }

        
        //***************************************************************************************************
        // Gpx
        //***************************************************************************************************  

        private void SetGpx(String strFileName)
        {
            try
            {
                gpxType gpx = gmap.Manager.DeserializeGPX(File.ReadAllText(strFileName));
                if (gpx != null)
                {
                    if (gpx.trk.Length > 0)
                    {
                        gmap.Overlays[0].Routes[0].Points.Clear();
                        foreach (var trk in gpx.trk)
                        {
                            List<PointLatLng> points = new List<PointLatLng>();
                            foreach (var seg in trk.trkseg)
                            {
                                foreach (var p in seg.trkpt)
                                {
                                    points.Add(new PointLatLng((double)p.lat, (double)p.lon));
                                }
                            }
                            gmap.Overlays[0].Routes[0].Points.AddRange(points);
                        }
                        tsslGpx.Text = Path.GetFileName(strFileName);
                        gmap.Overlays[0].Routes[0].IsVisible = true;
                        // gmap.ZoomAndCenterRoutes(null);
                        okGpx = true;
                        gpxFileName = strFileName;
                    }
                }
            }
            catch
            {
            }
        }

        private void TsslGpx_Click(object sender, EventArgs e)
        {
            okGpx = !okGpx;
            switch (okGpx)
            {
                case true:
                    using (FileDialog dlg = new OpenFileDialog())
                    {
                        dlg.CheckPathExists = true;
                        dlg.CheckFileExists = false;
                        dlg.AddExtension = true;
                        dlg.DefaultExt = "gpx";
                        dlg.ValidateNames = true;
                        dlg.Title = "Afficher une route .Gpx";
                        dlg.Filter = "gpx files (*.gpx)|*.gpx";
                        dlg.FilterIndex = 1;
                        dlg.RestoreDirectory = true;
                        if (dlg.ShowDialog() == DialogResult.OK) SetGpx(dlg.FileName);
                    }
                    break;
                case false:
                    gmap.Overlays[0].Routes[0].IsVisible = false;
                    tsslGpx.Text = "";
                    break;
            }
        }

        //***************************************************************************************************
        // Port série
        //***************************************************************************************************
        private String GetPortName()
        {
            String str = "";
            WMIHelper helper = new WMIHelper("root\\CimV2");
            Device device = helper.Query<Device>().ToList()
                .Where(p => (p.Name ?? "")
                .Contains("u-blox GNSS Receiver")).SingleOrDefault();
            if (device != null)
            {
                str = device.Name;
                int i = str.IndexOf("(");
                int j = str.LastIndexOf(")");
                str = str.Substring(i + 1, j - i - 1);
            }
            return str;
        }

        private void PortOpen()
        {
            try
            {
                port.Open();
                tsslIco.Image = Resource.ok;
                okGps = true;
            }
            catch
            {
                EraseGps();
            }
        }

        private void PortClose()
        {
            try
            {
                if (port.IsOpen) port.Close();
                tsslIco.Image = Resource.ko;
                EraseGps();
            }
            catch
            {
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_DEVICECHANGE = 0x0219;
            const int DBT_DEVICEARRIVAL = 0x8000;
            const int DBT_DEVICEREMOVECOMPLETE = 0x8004;

            if (m.Msg == WM_DEVICECHANGE)
            {
                switch (m.WParam.ToInt32())
                {
                    case DBT_DEVICEARRIVAL:
                        PortOpen();
                        break;
                    case DBT_DEVICEREMOVECOMPLETE:
                        PortClose();
                        break;
                }
            }
            base.WndProc(ref m);
        }

        private void Port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                if (port.BytesToRead < 90) return;
                string str = port.ReadExisting();
                if (!okGps) return;
                int deb = str.IndexOf("$GNGGA");
                if (deb >= 0)
                {
                    int fin = str.IndexOf('\n', deb);
                    if (fin > deb)
                    {
                        str = str.Substring(deb, fin - deb + 1);
                        string[] tabStr = str.Split(',');
                        double lon = Degree(tabStr[4]);
                        double lat = Degree(tabStr[2]);
                        float alt = float.Parse(tabStr[9].Replace('.', ','));
                        int sat = int.Parse(tabStr[7]);
                        float hdop = float.Parse(tabStr[8].Replace('.', ','));
                        if (okGps) SetGps(lon, lat, alt, sat, hdop);
                    }
                }
            }
            catch
            {
            }
        }
    }
}
