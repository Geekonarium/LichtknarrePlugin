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


        public Konfigurator(int playerIndex)
        {
            this.playerIndex = playerIndex;

            String directory = System.AppDomain.CurrentDomain.BaseDirectory;

            settings = new IniFile(directory + "Plugins\\LichtknarrePlugin.ini");

            //<usePresentationMode>
            if (!settings.KeyExists("usePresentationMode" + playerIndex.ToString(), "commonSettings"))
            {
                settings.Write("usePresentationMode" + playerIndex.ToString(), "1", "commonSettings");
            }

            usePresentationModeMemory = settings.Read("usePresentationMode" + playerIndex.ToString(), "commonSettings") == "1";
            //</usePresentationMode>

            //<inputPerSecond>
            if (!settings.KeyExists("inputPerSecond" + playerIndex.ToString(), "commonSettings"))
            {
                settings.Write("inputPerSecond" + playerIndex.ToString(), intputPerSecondDefaultValue.ToString(), "commonSettings");
            }

            inputPerSecondMemory = Int32.Parse(settings.Read("inputPerSecond" + playerIndex.ToString(), "commonSettings"));
            //</inputPerSecond>
        }

        //<inputPerSecond>
        public int intputPerSecondDefaultValue = 50;
        private int inputPerSecondMemory = 50;
        public int inputPerSecond
        {
            set
            {
                if (value != inputPerSecondMemory)
                {
                    inputPerSecondMemory = value;

                    settings.Write("inputPerSecond" + playerIndex.ToString(), value.ToString(), "commonSettings");

                }
            }
            get
            {
                return inputPerSecondMemory;
            }
        }
        //</inputPerSecond>

        //<usePresentationMode>
        private bool usePresentationModeMemory = false;
        public bool usePresentationMode
        {
            set
            {
                if (value != usePresentationMode)
                {
                    usePresentationModeMemory = value;
                    if (value)
                    {
                        settings.Write("usePresentationMode" + playerIndex.ToString(), "1", "commonSettings");
                    }
                    else
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
        //</usePresentationMode>

    }
}
