using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateMusic
{
    public partial class UltimateMusic : Form
    {
        public UltimateMusic()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReloadSettings();
        }

        public void ReloadSettings()
        { 
            SC= new SettingsController();
            SC.FilesVaildate();
            Settings = SC.GetAllOptionValue();
            Playlist = SC.PlaylistLoad();
        }
        SettingsController SC;
        List<string> Settings;
        List<string> Playlist;

        private void button2_Click(object sender, EventArgs e)
        {
            PlayerForm Player = new PlayerForm(this, Settings, Playlist);
            Player.Show();
            this.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings_Form Settings = new Settings_Form(this);
            Settings.Show();
            this.Enabled = false;
        }

        private void UltimateMusic_EnabledChanged(object sender, EventArgs e)
        {
            ReloadSettings();
        }
    }
}
