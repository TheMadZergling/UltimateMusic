using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateMusic
{
    public partial class Settings_Form : Form
    {
        public Settings_Form(Form Main)
        {
            InitializeComponent();
            MainFrom=Main;
        }   
        Form MainFrom;
        SettingsController _controller= new SettingsController();

        private void Settings_Form_Load(object sender, EventArgs e)
        {
            //Betölti a válastási lehetőséget
            foreach (string item in _controller.GetKeyOptions())
            {
                comboBox1.Items.Add(item);
                comboBox2.Items.Add(item);
                comboBox3.Items.Add(item);
                comboBox4.Items.Add(item);
                comboBox5.Items.Add(item);
            }
            //FilePath helyett file névet ír ki
            foreach (var item in _controller.PlaylistLoad())
            {
                listBox1.Items.Add(item.Split('\\')[item.Split('\\').Length - 1]);
            }
            comboBox1.SelectedItem = _controller.GetSettingValue(0);
            comboBox2.SelectedItem = _controller.GetSettingValue(1);
            comboBox3.SelectedItem = _controller.GetSettingValue(2);
            comboBox4.SelectedItem = _controller.GetSettingValue(3);
            comboBox5.SelectedItem = _controller.GetSettingValue(4);

            if (_controller.GetSettingValue(5) == "")
            {
                button1.BackColor = Color.Red;
                MessageBox.Show("The ultimate's music file is not set yet. It is recomended to set it now.");
            }
            //Felveszem Változóként hogy a Value Változás ne írja át 0-ra a "numeric2"-t
            string[] asd = _controller.GetSettingValue(6).Split(':');
            numeric1.Value = int.Parse(asd[0]);
            numeric2.Value = int.Parse(asd[1]);


        }
        
        private void Settings_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            _controller.FilesVaildate();
            MainFrom.Enabled = true;
        }

        private void comboBox_SelectedValueChanged(object sender, EventArgs e)
        {      
            _controller.ChangeSettings(int.Parse((string)((ComboBox)sender).Tag), ((ComboBox)sender).Text);
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _controller.ChangeSettings(int.Parse((string)((NumericUpDown)sender).Tag), numeric1.Value+":"+numeric2.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = "C:\\";
            fd.RestoreDirectory = true;
            if (fd.ShowDialog()==DialogResult.OK)
            {
                _controller.ChangeSettings(5, Path.GetFullPath(fd.FileName));
                button1.BackColor = Color.White;
            }
        }

        private void chngPlaylist_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = "C:\\";
            fd.Multiselect = true;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                _controller.ModiflyPlaylist(fd.FileNames.ToList<string>());
                listBox1.DataSource = fd.SafeFileNames;
            }

        }

        private void EmptyButton_Click(object sender, EventArgs e)
        {
            _controller.ModiflyPlaylist(new List<string>());
            listBox1.DataSource = _controller.PlaylistLoad();
        }
    }
}
