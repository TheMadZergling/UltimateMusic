using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateMusic
{
    public partial class PlayerForm : Form
    {
        public PlayerForm(Form TheMainForm, List<string> Settngs,List<string> Playlist)
        {
            InitializeComponent();
            //Golbális Változók értékének megadása
            Settings = Settngs;
            MainForm = TheMainForm;

            //zenék beolvasása;
            if (!Playlist.Contains(Settings[5]))
            {
                Playlist.Add(Settings[5]);
            }

            //playlist-hez adás
            foreach (var item in Playlist)
            {
                axVLCPlugin21.playlist.add("file:///"+item);
               
            }
            axVLCPlugin21.playlist.playItem(1);

            //timer bekapcs
            timer1.Enabled = true;
        }

        //globális változók
        List<string> Settings;
        Form MainForm;
        List<string> Playlist;

        //dll import       
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        //Billentyű ellenörzés
        public void ReadKey()
        {
            try
            {
                foreach (int i in Enum.GetValues(typeof(Keys)))
                {
                    if (GetAsyncKeyState(i) == -32767)
                    {
                        UseKey((Enum.GetName(typeof(Keys), i)));
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        //Billentyű esemény 
        public void UseKey(string Key)
        {
            //swich csak statikus elemeket enged használni if-re vagyok kényszerülve
            if (Key == Settings[0]) { StartorStop(); }
            if (Key == Settings[1]) { Pause(); }
            if (Key == Settings[2]) { Next(); }
            if (Key == Settings[3]) { Prev(); }
            if (Key == Settings[4]) { Ultimate(); }
        }
       
        public void StartorStop()
        {
            if (axVLCPlugin21.playlist.isPlaying)
            {
                axVLCPlugin21.playlist.stop();
            }
            else
            {
                axVLCPlugin21.playlist.play();
            }

        }

        private void Ultimate()
        {
            //Előkészület
            int wherewasi = axVLCPlugin21.playlist.currentItem;
            Playlist.Remove(Settings[5]);
            axVLCPlugin21.playlist.clear();
            List<string> RePlaylist = new List<string>();
            
            //Egy új playlist létrehozása amelyben a zeneszámok sorrendje nem válzotik és a Settings[5] kerül elörre
            RePlaylist.Add(Settings[5]);
            for (int i = wherewasi; i < Playlist.Count; i++)
            {
                RePlaylist.Add(Playlist[i]);
            }
           for (int i = 0; i < wherewasi; i++)
            {
                RePlaylist.Add(Playlist[i]);
            }
            Playlist = RePlaylist;

            //A playlist újra írása
            foreach (var item in Playlist)
            {
                axVLCPlugin21.playlist.add("file:///"+item);
            }
            //lejátszás
            axVLCPlugin21.playlist.playItem(0);
            axVLCPlugin21.input.time = (1000*(int.Parse(Settings[6].Split(':')[0])*60+ int.Parse(Settings[6].Split(':')[1])));
        }

        private void Prev()
        {
            axVLCPlugin21.playlist.prev();
        }

        private void Next()
        {
            axVLCPlugin21.playlist.next();
        }

        private void Pause()
        {
            axVLCPlugin21.playlist.togglePause();
        }

        private void PlayerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ReadKey();
        }
    }
}
