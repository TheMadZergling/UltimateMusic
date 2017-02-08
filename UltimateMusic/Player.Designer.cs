namespace UltimateMusic
{
    partial class PlayerForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.VlcBackground = new AxAXVLC.AxVLCPlugin2();
            this.UltimateMusic = new AxAXVLC.AxVLCPlugin2();
            ((System.ComponentModel.ISupportInitialize)(this.VlcBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltimateMusic)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // VlcBackground
            // 
            this.VlcBackground.CausesValidation = false;
            this.VlcBackground.Enabled = true;
            this.VlcBackground.Location = new System.Drawing.Point(2, 3);
            this.VlcBackground.Name = "VlcBackground";
            this.VlcBackground.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("VlcBackground.OcxState")));
            this.VlcBackground.Size = new System.Drawing.Size(306, 227);
            this.VlcBackground.TabIndex = 0;
            // 
            // UltimateMusic
            // 
            this.UltimateMusic.Enabled = true;
            this.UltimateMusic.Location = new System.Drawing.Point(314, 3);
            this.UltimateMusic.Name = "UltimateMusic";
            this.UltimateMusic.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("UltimateMusic.OcxState")));
            this.UltimateMusic.Size = new System.Drawing.Size(308, 227);
            this.UltimateMusic.TabIndex = 1;
            // 
            // PlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 180);
            this.Controls.Add(this.UltimateMusic);
            this.Controls.Add(this.VlcBackground);
            this.MaximumSize = new System.Drawing.Size(635, 219);
            this.MinimumSize = new System.Drawing.Size(635, 219);
            this.Name = "PlayerForm";
            this.Text = "UltimateMusicPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlayerForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.VlcBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltimateMusic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private AxAXVLC.AxVLCPlugin2 VlcBackground;
        private AxAXVLC.AxVLCPlugin2 UltimateMusic;
    }
}