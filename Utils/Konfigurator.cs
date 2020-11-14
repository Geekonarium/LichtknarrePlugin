using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LichtknarrePlugin.Utils
{
    public class Konfigurator
    {
        int playerIndex;
        IniFile settings;
        public bool usePresentationMode {
            set {
                if (value != usePresentationMode)
                {
                    usePresentationModeMemory = value;
                    if (value) { 
                        settings.Write("usePresentationMode" + playerIndex.ToString(), "1", "commonSettings");
                    } else
                    {
                        settings.Write("usePresentationMode" + playerIndex.ToString(), "0", "commonSettings");
                    }
                }
            }
            get
            {
                return usePresentationModeMemory;
            }
        }

        private bool usePresentationModeMemory = false;

        public Konfigurator(int playerIndex)
        {
            this.playerIndex = playerIndex;

            String directory = System.AppDomain.CurrentDomain.BaseDirectory;

            settings = new IniFile(directory + "Plugins\\LichtknarrePlugin.ini");

            if (!settings.KeyExists("usePresentationMode" + playerIndex.ToString(), "commonSettings"))
            {
                settings.Write("usePresentationMode" + playerIndex.ToString(), "1", "commonSettings");
            }

            usePresentationModeMemory = settings.Read("usePresentationMode" + playerIndex.ToString(), "commonSettings") == "1";
        }
    }
}
