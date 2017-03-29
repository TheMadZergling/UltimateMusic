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

            //playlist-hez adás
            foreach (var item in Playlist)
            {
                VlcBackground.playlist.add("file:///"+item);
               
            }
            if (Playlist.Count()!=0)
            {
                VlcBackground.playlist.playItem(0);
            }
            UltimateMusic.playlist.add("file:///"+Settings[5]); 
            UltimateMusic.MediaPlayerEndReached+= UltimateMusic_EndofSong;
            VlcBackground.MediaPlayerEndReached += VlcBackground_EndofPlaylist;


            //timer bekapcs
            timer1.Enabled = true;
        }

        private void VlcBackground_EndofPlaylist(object sender, EventArgs e)
        {
            VlcBackground.playlist.playItem(0);
        }

        private void UltimateMusic_EndofSong(object sender, EventArgs e)
        {
            VlcBackground.playlist.next();
            VlcBackground.playlist.togglePause();
        }

        //globális változók
        List<string> Settings;
        Form MainForm;

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
            if (!UltimateMusic.playlist.isPlaying)
            {
                if (Key == Settings[0]) { StartorStop(); }
                if (Key == Settings[1]) { Pause(); }
                if (Key == Settings[2]) { Next(); }
                if (Key == Settings[3]) { Prev(); }
                if (Key == Settings[4]) { Ultimate(); }
            }
            else for (int i = 0; i < 4; i++)
                {
                    if (Key==Settings[i])
                    {
                        VlcBackground.Visible = true;
                        VlcBackground.playlist.next();
                        VlcBackground.playlist.togglePause();
                        UltimateMusic.playlist.stop();
                        UltimateMusic.Visible = false;
                    }
                }
        }
       
        public void StartorStop()
        {
            if (VlcBackground.playlist.isPlaying)
            {
                VlcBackground.playlist.stop();
            }
            else
            {
                VlcBackground.playlist.play();
            }
        }

        private void Ultimate()
        {
            if (!UltimateMusic.playlist.isPlaying && Settings[5]!="")
            {
                UltimateMusic.Visible = true;
                UltimateMusic.playlist.playItem(0);
                UltimateMusic.input.time = (1000 * (int.Parse(Settings[6].Split(':')[0]) * 60 + int.Parse(Settings[6].Split(':')[1])));
                VlcBackground.playlist.togglePause();
                VlcBackground.Visible = false;
            }
        }



        private void Prev()
        {
            VlcBackground.playlist.prev();
        }

        private void Next()
        {
            VlcBackground.playlist.next();
        }

        private void Pause()
        {
            VlcBackground.playlist.togglePause();
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
    