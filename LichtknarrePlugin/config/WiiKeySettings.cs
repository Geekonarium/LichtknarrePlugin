using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LichtpistolePlugin.config
{
    class WiiKeySetting
    {
        public KeyBindAction action;

        //Additional settings for selected actions here:
        public String executionInfo = ""; //When needed for processes
    }

    class WiiKeySettings
    {
        //WiiMote
        WiiKeySetting home;

        WiiKeySetting one;
        WiiKeySetting two;

        WiiKeySetting minus;
        WiiKeySetting plus;

        WiiKeySetting left;
        WiiKeySetting right;
        WiiKeySetting up;
        WiiKeySetting down;

        WiiKeySetting A;
        WiiKeySetting B;

        //nunchuck
        WiiKeySetting C;
        WiiKeySetting Z;
    }
}
