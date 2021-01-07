using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LichtpistolePlugin.config
{
    enum KeyBindProcessAction
    {
        start,
        kill,
        toggle
    }

    enum KeyBindMouseAction
    {
        leftButton,
        rightButton,
        middleButton
    }
    enum KeyBindActionType
    {
        mouse,
        keyboard,
        vjoy,
        executeProcess,
        toggleProcess,
        killProcess
    }
    class KeyBindAction
    {
        public KeyBindActionType type;
        public String title;

        public int? keyCode;
        public KeyBindMouseAction? mouseAction;
        public KeyBindProcessAction? processType;

        public KeyBindAction(KeyBindActionType type, String title, KeyBindProcessAction processType)
        {
            this.type = type;
            this.title = title;
            this.processType = processType;
        }

        public KeyBindAction(KeyBindActionType type, String title, int keyCode)
        {
            this.type = type;
            this.title = title;
            this.keyCode = keyCode;
        }

        public KeyBindAction(KeyBindActionType type, String title, KeyBindMouseAction? mouseAction)
        {
            this.type = type;
            this.title = title;
            this.mouseAction = mouseAction;
        }
    }
    class PossibleKeyBindActions
    {
        public List<KeyBindAction> list = new List<KeyBindAction>();

        public PossibleKeyBindActions()
        {
            list.Add(
                new KeyBindAction(KeyBindActionType.mouse, "left mouse button", KeyBindMouseAction.leftButton)
            );

            list.Add(
                new KeyBindAction(KeyBindActionType.mouse, "right mouse button", KeyBindMouseAction.rightButton)
            );

            list.Add(
                new KeyBindAction(KeyBindActionType.mouse, "middle mouse button", KeyBindMouseAction.middleButton)
            );

            foreach (var enumValue in Enum.GetValues(typeof(KeyBindProcessAction)))
            {
                list.Add(
                    new KeyBindAction(KeyBindActionType.executeProcess, enumValue + " process", (KeyBindProcessAction)enumValue)
                );
            }

            foreach (var enumValue in Enum.GetValues(typeof(Keys)))
            {
                list.Add(
                    new KeyBindAction(KeyBindActionType.keyboard, "Keyboard button:" + enumValue, (int)enumValue)
                );
            }

            //vjoy comming soon

        }
    }
}
