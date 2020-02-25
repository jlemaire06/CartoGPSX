namespace CartoGpsx
{
    partial class FormCartoGpsx
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnChangeMap = new System.Windows.Forms.Button();
            this.sst = new System.Windows.Forms.StatusStrip();
            this.tsslIco = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslGps = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLon = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLat = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslAlt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSat = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslHDOP = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslGpx = new System.Windows.Forms.ToolStripStatusLabel();
            this.port = new System.IO.Ports.SerialPort(this.components);
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.sst.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnChangeMap
            // 
            this.btnChangeMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeMap.Location = new System.Drawing.Point(713, 389);
            this.btnChangeMap.Name = "btnChangeMap";
            this.btnChangeMap.Size = new System.Drawing.Size(75, 23);
            this.btnChangeMap.TabIndex = 1;
            this.btnChangeMap.Text = "Change";
            this.btnChangeMap.UseVisualStyleBackColor = true;
            this.btnChangeMap.Click += new System.EventHandler(this.BtnChangeMap_Click);
            // 
            // sst
            // 
            this.sst.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslIco,
            this.tsslGps,
            this.tsslLon,
            this.tsslLat,
            this.tsslAlt,
            this.tsslSat,
            this.tsslHDOP,
            this.tsslGpx});
            this.sst.Location = new System.Drawing.Point(0, 428);
            this.sst.Name = "sst";
            this.sst.Size = new System.Drawing.Size(800, 22);
            this.sst.TabIndex = 2;
            // 
            // tsslIco
            // 
            this.tsslIco.Image = global::CartoGpsx.Resource.ko;
            this.tsslIco.Name = "tsslIco";
            this.tsslIco.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tsslIco.Size = new System.Drawing.Size(26, 17);
            // 
            // tsslGps
            // 
            this.tsslGps.Image = global::CartoGpsx.Resource.GPS;
            this.tsslGps.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsslGps.Name = "tsslGps";
            this.tsslGps.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.tsslGps.Size = new System.Drawing.Size(60, 17);
            this.tsslGps.Click += new System.EventHandler(this.TsslGps_Click);
            // 
            // tsslLon
            // 
            this.tsslLon.Name = "tsslLon";
            this.tsslLon.Size = new System.Drawing.Size(27, 17);
            this.tsslLon.Text = "Lon";
            // 
            // tsslLat
            // 
            this.tsslLat.Name = "tsslLat";
            this.tsslLat.Size = new System.Drawing.Size(23, 17);
            this.tsslLat.Text = "Lat";
            // 
            // tsslAlt
            // 
            this.tsslAlt.Name = "tsslAlt";
            this.tsslAlt.Size = new System.Drawing.Size(22, 17);
            this.tsslAlt.Text = "Alt";
            // 
            // tsslSat
            // 
            this.tsslSat.Name = "tsslSat";
            this.tsslSat.Size = new System.Drawing.Size(23, 17);
            this.tsslSat.Text = "Sat";
            // 
            // tsslHDOP
            // 
            this.tsslHDOP.Name = "tsslHDOP";
            this.tsslHDOP.Size = new System.Drawing.Size(40, 17);
            this.tsslHDOP.Text = "HDOP";
            // 
            // tsslGpx
            // 
            this.tsslGpx.Image = global::CartoGpsx.Resource.GPX;
            this.tsslGpx.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsslGpx.Name = "tsslGpx";
            this.tsslGpx.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.tsslGpx.Size = new System.Drawing.Size(60, 17);
            this.tsslGpx.Click += new System.EventHandler(this.TsslGpx_Click);
            // 
            // port
            // 
            this.port.PortName = "COM27";
            this.port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.Port_DataReceived);
            // 
            // gmap
            // 
            this.gmap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(0, 1);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 20;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(800, 424);
            this.gmap.TabIndex = 3;
            this.gmap.Zoom = 0D;
            // 
            // FormCartoGpsx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.sst);
            this.Controls.Add(this.btnChangeMap);
            this.Controls.Add(this.gmap);
            this.Name = "FormCartoGpsx";
            this.Text = "CartoGPSX";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCartoGpsx_FormClosing);
            this.Load += new System.EventHandler(this.FormCartoGpsx_Load);
            this.sst.ResumeLayout(false);
            this.sst.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChangeMap;
        private System.Windows.Forms.StatusStrip sst;
        private System.Windows.Forms.ToolStripStatusLabel tsslIco;
        private System.IO.Ports.SerialPort port;
        private System.Windows.Forms.ToolStripStatusLabel tsslGpx;
        private System.Windows.Forms.ToolStripStatusLabel tsslGps;
        private System.Windows.Forms.ToolStripStatusLabel tsslLon;
        private System.Windows.Forms.ToolStripStatusLabel tsslLat;
        private System.Windows.Forms.ToolStripStatusLabel tsslAlt;
        private System.Windows.Forms.ToolStripStatusLabel tsslSat;
        private System.Windows.Forms.ToolStripStatusLabel tsslHDOP;
        private GMap.NET.WindowsForms.GMapControl gmap;
    }
}