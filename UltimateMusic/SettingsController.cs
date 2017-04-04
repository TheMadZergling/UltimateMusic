using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace UltimateMusic
{
    class SettingsController
    {
        //ellenörzi/ hiba esetén újraírja a settings/playlist file-t/okat
        public bool FilesVaildate()
        {
            #region Config.txt
            List<string> TheFile = ConfigFileLoad();

            if (TheFile.Count != 7)
            {
                ReWriteFile();
                return false;

            }

            for (int i = 0; i < 5; i++)
            {
                if (TheFile[i].Split(':').Length != 2)
                {
                    ReWriteFile();
                    return false;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (!IsKey(TheFile[i].Split(':')[1]))
                {
                    ReWriteFile();
                    return false;
                }

            }
            //Mert A ezelőtt "File" nevű változót használtam a fordító váltózóként látja :/
            if (!System.IO.File.Exists(removeFirstPart(TheFile[5])) && TheFile[5].Split(':')[1] != "")
            {
                ReWriteFile();
                return false;
            }

            string[] split = TheFile[6].Split(':');
            int number;

            if (!((split.Length == 3 && int.TryParse(split[1], out number) && int.TryParse(split[2], out number) && split[1] != "")))
            {
                ReWriteFile();
                return false;
            }

            if (int.Parse(split[2])>60)
            {
                return false;
            }
            #endregion

            #region Songlist
            List<string> File=PlaylistLoad();
            #endregion

            return true;
        }

        //beolvassa a Config file-t
        private List<string> ConfigFileLoad()
        {
            List<string> TheFile = new List<string>();
            try
            {
                StreamReader FL = new StreamReader("Config.txt", Encoding.Default);
                while (!FL.EndOfStream)
                {
                    TheFile.Add(FL.ReadLine());
                }
                FL.Close();
                return TheFile;
            }
            catch (Exception)
            {

                ReWriteFile();
                return ConfigFileLoad();
            }
        }

        //Csak az érvényes adatokat olvassa ki a file-ból
        public List<string> PlaylistLoad()
        {
            string line;
            List<string> TheFile = new List<string>();
            try
            {
                StreamReader FL = new StreamReader("Playlist.txt", Encoding.Default);
                while (!FL.EndOfStream)
                {
                    line = FL.ReadLine();
                    if (File.Exists(line))
                    {
                        TheFile.Add(line);
                    }
                }
                FL.Close();
                return TheFile;
            }
            catch (Exception)
            {
                StreamWriter reWriter = new StreamWriter("Playlist.txt");
                reWriter.Close();
                return PlaylistLoad();
            }
        }
        
        //A playlistet tárolja
        public void ModiflyPlaylist(List<string> Playlist)
        {
            StreamWriter SW = new StreamWriter("Playlist.txt");
            foreach (var item in Playlist)
            {
                    SW.WriteLine(item);
            }
            SW.Close();
        }
        
        //újra írja a Configfile-t
        private void ReWriteFile()
        {
            StreamWriter reWriter = new StreamWriter("Config.txt");
            reWriter.WriteLine("Start/Stop:K");
            reWriter.WriteLine("Pause:I");
            reWriter.WriteLine("Next:L");
            reWriter.WriteLine("Prev:J");
            reWriter.WriteLine("UltimateKey:R");
            reWriter.WriteLine("UltimateLink:");
            reWriter.WriteLine("UltimateStartTime:0:00");
            reWriter.Close();
            MessageBox.Show("A Config Fájl sérült vagy hibásan kitöltött volt. A program újraírja az...");
        }

        //egy adott adatok átír a Configfile-ban
        public void ChangeSettings(int line, string Data)
        {
            List<string> TheFile = ConfigFileLoad();
            StreamWriter reWriter = new StreamWriter("config.txt");
            for (int i = 0; i < 7; i++)
            {
                if (line == i)
                {
                    reWriter.WriteLine(TheFile[i].Split(':')[0] + ":" + Data);
                }
                else
                {
                    reWriter.WriteLine(TheFile[i]);
                }
            }
            reWriter.Close();
        }

        //betölti egy adott preset tartalmás és azt ellenörzi
        public List<string> LoadPreset(string presetname)
        {
            try
            {
                StreamReader SR = new StreamReader("presets/" + presetname + ".txt");
                StreamWriter SW = new StreamWriter("config.txt");
                while (!SR.EndOfStream)
                {
                    SW.WriteLine(SR.ReadLine());
                }
                SR.Close();
                SW.Close();
                FilesVaildate();
                return GetAllOptionValue();
            }
            catch (Exception)
            {
                MessageBox.Show("A file már nem létezik");
                throw;
            }
            
        }
        
        //új presetet hozz létre
        public void AddPreset(string presetname)
        {
            StreamReader SR = new StreamReader("config.txt");
            if (!Directory.Exists("presets"))
            {
                Directory.CreateDirectory("presets");
            }
            StreamWriter SW = new StreamWriter("presets/" + presetname + ".txt");
            while (!SR.EndOfStream)
            {
                SW.WriteLine(SR.ReadLine());
            }
            SR.Close();
            SW.Close();
        }

        //ellenőrzi hogy billyenyű-e a string
        public bool IsKey(string Key)
        {
            string[] Keys = { "LButton", "RButton", "Cancel", "MButton", "XButton1", "XButton2", "Back", "Tab", "LineFeed", "Clear", "Enter", "Enter", "ShiftKey", "ControlKey", "Menu", "Pause", "CapsLock", "CapsLock", "HanguelMode", "HanguelMode", "HanguelMode", "JunjaMode", "FinalMode", "KanjiMode", "KanjiMode", "Escape", "IMEConvert", "IMENonconvert", "IMEAccept", "IMEAccept", "IMEModeChange", "Space", "Prior", "Prior", "PageDown", "PageDown", "End", "Home", "Left", "Up", "Right", "Down", "Select", "Print", "Execute", "PrintScreen", "PrintScreen", "Insert", "Delete", "Help", "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "LWin", "RWin", "Apps", "Sleep", "NumPad0", "NumPad1", "NumPad2", "NumPad3", "NumPad4", "NumPad5", "NumPad6", "NumPad7", "NumPad8", "NumPad9", "Multiply", "Add", "Separator", "Subtract", "Decimal", "Divide", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "F13", "F14", "F15", "F16", "F17", "F18", "F19", "F20", "F21", "F22", "F23", "F24", "NumLock", "Scroll", "LShiftKey", "RShiftKey", "LControlKey", "RControlKey", "LMenu", "RMenu", "BrowserBack", "BrowserForward", "BrowserRefresh", "BrowserStop", "BrowserSearch", "BrowserFavorites", "BrowserHome", "VolumeMute", "VolumeDown", "VolumeUp", "MediaNextTrack", "MediaPreviousTrack", "MediaStop", "MediaPlayPause", "LaunchMail", "SelectMedia", "LaunchApplication1", "LaunchApplication2", "OemSemicolon", "OemSemicolon", "Oemplus", "Oemcomma", "OemMinus", "OemPeriod", "Oem2", "Oem2", "Oemtilde", "Oemtilde", "Oem4", "Oem4", "OemPipe", "OemPipe", "Oem6", "Oem6", "OemQuotes", "OemQuotes", "Oem8", "Oem102", "Oem102", "ProcessKey", "Packet", "Attn", "Crsel", "Exsel", "EraseEof", "Play", "Zoom", "NoName", "Pa1", "OemClear", "KeyCode", "Shift", "Control", "Alt", "Modifiers" };
            return Keys.Contains(Key);
        }

        //A billenytűlehetőségeket vissza adja
        public List<string> GetKeyOptions()
        {
            List<string> Keys = new List<string> { "LButton", "RButton", "Cancel", "MButton", "XButton1", "XButton2", "Back", "Tab", "LineFeed", "Clear", "Enter", "Enter", "ShiftKey", "ControlKey", "Menu", "Pause", "CapsLock", "CapsLock", "HanguelMode", "HanguelMode", "HanguelMode", "JunjaMode", "FinalMode", "KanjiMode", "KanjiMode", "Escape", "IMEConvert", "IMENonconvert", "IMEAccept", "IMEAccept", "IMEModeChange", "Space", "Prior", "Prior", "PageDown", "PageDown", "End", "Home", "Left", "Up", "Right", "Down", "Select", "Print", "Execute", "PrintScreen", "PrintScreen", "Insert", "Delete", "Help", "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "LWin", "RWin", "Apps", "Sleep", "NumPad0", "NumPad1", "NumPad2", "NumPad3", "NumPad4", "NumPad5", "NumPad6", "NumPad7", "NumPad8", "NumPad9", "Multiply", "Add", "Separator", "Subtract", "Decimal", "Divide", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "F13", "F14", "F15", "F16", "F17", "F18", "F19", "F20", "F21", "F22", "F23", "F24", "NumLock", "Scroll", "LShiftKey", "RShiftKey", "LControlKey", "RControlKey", "LMenu", "RMenu", "BrowserBack", "BrowserForward", "BrowserRefresh", "BrowserStop", "BrowserSearch", "BrowserFavorites", "BrowserHome", "VolumeMute", "VolumeDown", "VolumeUp", "MediaNextTrack", "MediaPreviousTrack", "MediaStop", "MediaPlayPause", "LaunchMail", "SelectMedia", "LaunchApplication1", "LaunchApplication2", "OemSemicolon", "OemSemicolon", "Oemplus", "Oemcomma", "OemMinus", "OemPeriod", "Oem2", "Oem2", "Oemtilde", "Oemtilde", "Oem4", "Oem4", "OemPipe", "OemPipe", "Oem6", "Oem6", "OemQuotes", "OemQuotes", "Oem8", "Oem102", "Oem102", "ProcessKey", "Packet", "Attn", "Crsel", "Exsel", "EraseEof", "Play", "Zoom", "NoName", "Pa1", "OemClear", "KeyCode", "Shift", "Control", "Alt", "Modifiers" };
            return Keys;
        }

        //egy adott sornak a beálított értékét adja vissza
        public string GetSettingValue(int m)
        {
            return removeFirstPart(ConfigFileLoad()[m]);
        }

        //a sor beálítási értékét adja vissza
        private string removeFirstPart(string stri)
        {
            bool FirstPart=true;
            string ret="";
            foreach (var item in stri)
            {
                if (FirstPart)
                {
                    if (item==':')
                    {
                        FirstPart = false;
                    }
                }
                else
                {
                    ret += item;
                }
            }
            return ret;
        }

        //Csak a beálított adatokat adja vissza
        public List<string> GetAllOptionValue()
        {
            List<string> ret= ConfigFileLoad();
            for (int i = 0; i < ret.Count; i++)
            {
                ret[i] = removeFirstPart(ret[i]);
            }
            return ret;

        }
        
    }
}
