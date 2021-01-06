using WiimoteLib;
using System.Windows.Forms;
using LichtknarrePlugin.Utils;
using System.Threading;
using System.Diagnostics;
using System;
using Newtonsoft.Json;
using System.Text;
using System.Runtime.InteropServices;
using LichtpistolePlugin.config;

namespace LichtknarrePlugin
{
    public class LichtknarrePlugin
    {
        [DllImport("User32.Dll", SetLastError = true)]
        public static extern bool SetCursorPos(int x, int y);


        bool isRumbling = false;
        Konfigurator konfigurator;
        private void generateKeyPressThread(Keys key, int steps, int sleepBeforeTime)
        {
            PressKey keyFunc = new PressKey();
            keyFunc.sleepBeforeTime = sleepBeforeTime;
            keyFunc.key = key;
            keyFunc.steps = steps;
            Thread workerThread = new Thread(new ThreadStart(keyFunc.Press));
            workerThread.IsBackground = true;
            //workerThread.Priority = ThreadPriority.Highest;
            workerThread.Start();
        }

        public void startHoldKeyPressThread(Keys key, int sleepBeforeTime)
        {
            PressKey keyFunc = new PressKey();
            keyFunc.key = key;
            keyFunc.sleepBeforeTime = sleepBeforeTime;
            Thread workerThread = new Thread(new ThreadStart(keyFunc.hold));
            workerThread.IsBackground = true;
            //workerThread.Priority = ThreadPriority.Highest;
            workerThread.Start();
        }

        public void releaseHoldKeyPressThread(Keys key, int sleepBeforeTime)
        {
            PressKey keyFunc = new PressKey();
            keyFunc.sleepBeforeTime = sleepBeforeTime;
            keyFunc.key = key;
            Thread workerThread = new Thread(new ThreadStart(keyFunc.release));
            workerThread.IsBackground = true;
            //workerThread.Priority = ThreadPriority.Highest;
            workerThread.Start();
        }
      

        private bool AButton = false;
        private bool BButton = false;
        private bool home = false;
        private bool one = false;
        private int plusLag = 0;
        private int minusLag = 0;

        private int playerIndex;
        
        public string getDescription()
        {
            if (playerIndex != 1)
            {
                return "This Lightgun will be ignored, because its not Player 1. Pluginsource will become or is opensource. You can programm your own plugin for mame :D.";
            } else
            {
                return "In fact this is a singleplayer Plugin. Only first player can control the mouse :) Disable presentation mode when you play games with this Plugin.";
            }
        }

        public void onInit(int playerIndex)
        {
            this.playerIndex = playerIndex;

            konfigurator = new Konfigurator(playerIndex);

            System.Diagnostics.Process myProcess = System.Diagnostics.Process.GetCurrentProcess();
            myProcess.PriorityClass = System.Diagnostics.ProcessPriorityClass.High;
        }

        public void onClose()
        {
            
        }

        System.Drawing.PointF? oldPosition = null;

        //FPS
        double time1 = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        double time2 = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        double fps = 0;

        public void targetUpdate(float percentX, float percentY)
        {
            //PLAYER
            if (playerIndex != 1) return;

            //FPS
            time2 = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            double deltaTime = time2 - time1;

            fps = 1 / deltaTime * 1000;

            if (konfigurator.inputPerSecond != -1 && fps > konfigurator.inputPerSecond) { return; } //fps cuttoff

            time1 = time2;

            //APPLY MOUSE
            System.Drawing.PointF position = new System.Drawing.PointF(
                ((float)Screen.PrimaryScreen.Bounds.Width / 100F * percentX),
                ((float)Screen.PrimaryScreen.Bounds.Height / 100F * percentY)
            );

            if (konfigurator.usePresentationMode)
            {
                if (oldPosition != null)
                {
                    float percentStepX = (100.0F / (float)Screen.PrimaryScreen.Bounds.Width * Math.Abs(position.X - oldPosition.Value.X));
                    float percentStepY = (100.0F / (float)Screen.PrimaryScreen.Bounds.Height * Math.Abs(position.Y - oldPosition.Value.Y));

                    percentStepX = percentStepX * 3.5F;
                    percentStepY = percentStepY * 3.5F;

                    if (percentStepX < 1.5) { percentStepX = 0; }
                    if (percentStepY < 1.5) { percentStepY = 0; }
                    
                    if (percentStepX > 100) { percentStepX = 100; }
                    if (percentStepY > 100) { percentStepY = 100; }

                    float stepX = ( (position.X - oldPosition.Value.X) / 100.0F * percentStepX);
                    float stepY = ( (position.Y - oldPosition.Value.Y) / 100.0F * percentStepY);


                    float x = oldPosition.Value.X + stepX;
                    float y = oldPosition.Value.Y + stepY;


                    SetCursorPos((int)x, (int)y);
                    oldPosition = new System.Drawing.PointF(x,y);
                }
                else
                {
                    SetCursorPos((int)position.X, (int)position.Y);
                    oldPosition = position;
                }
            }
            else
            {
                SetCursorPos((int)position.X, (int)position.Y);
            }
        }

        public void wiimoteChanged(String wsJSON)
        {
            if (playerIndex != 1) return;

            WiimoteState ws = JsonConvert.DeserializeObject<WiimoteState>(wsJSON);

            MouseOperations mouseOperations = new MouseOperations();
            MouseOperations.MouseEventFlags leftUpMouse = MouseOperations.MouseEventFlags.LeftUp;
            MouseOperations.MouseEventFlags leftDownMouse = MouseOperations.MouseEventFlags.LeftDown;
            MouseOperations.MouseEventFlags rightUpMouse = MouseOperations.MouseEventFlags.RightUp;
            MouseOperations.MouseEventFlags rightDownMouse = MouseOperations.MouseEventFlags.RightDown;

            if (ws.ButtonState.B == true)
            {
                if (this.BButton == false)
                {
                    MouseOperations.MouseEvent(rightDownMouse);
                    this.BButton = true;
                    isRumbling = true;
                    Delay.executeAfter(50, () =>
                    {
                        isRumbling = false;
                    });
                }
            }
            else
            {

                if (this.BButton == true)
                {
                    MouseOperations.MouseEvent(rightUpMouse);
                    this.BButton = false;
                    isRumbling = false;
                }
            }


            if (ws.ButtonState.A == true)
            {
                if (this.AButton == false)
                {
                    MouseOperations.MouseEvent(leftDownMouse);
                    this.AButton = true;
                }
            }
            else
            {

                if (this.AButton == true)
                {
                    MouseOperations.MouseEvent(leftUpMouse);
                    this.AButton = false;
                }
            }

            if (ws.ButtonState.One == true)
            {
                generateKeyPressThread(Keys.A, 0, 0);
            }

            if (ws.ButtonState.Up == true)
            {
                generateKeyPressThread(Keys.Up, 0, 0);
            }

            if (ws.ButtonState.Down == true)
            {
                generateKeyPressThread(Keys.Down, 0, 0);
            }

            if (ws.ButtonState.Left == true)
            {
                generateKeyPressThread(Keys.Left, 0, 0);
            }

            if (ws.ButtonState.Right== true)
            {
                generateKeyPressThread(Keys.Right, 0, 0);
            }

            
            if (ws.ButtonState.Plus == true) {
                if (plusLag > 4)
                {

                    generateKeyPressThread(Keys.VolumeUp, 0, 0);
                    plusLag = 0;
                }
                plusLag++;
            }

            if (ws.ButtonState.Minus == true)
            {
                if (minusLag > 4) { 
                    generateKeyPressThread(Keys.VolumeDown, 0, 0);
                    minusLag = 0;
                }
                minusLag++;
            }

            if (ws.ButtonState.Home == true)
            {
                if (this.home == false)
                {
                    startHoldKeyPressThread(Keys.VolumeMute, 0);
                    this.home = true;
                }
            }
            else
            {

                if (this.home == true)
                {
                    releaseHoldKeyPressThread(Keys.VolumeMute, 0);
                    this.home = false;
                }
            }

            if (ws.ButtonState.One == true)
            {
                if (this.one == false)
                {
                    //generateKeyPressThread(MouseButtons.Left, 250,0);
                    //System.Diagnostics.Process.Start("TabTip.exe");
                    
                    this.one = true;
                }
            }
            else
            {

                if (this.one == true)
                {
                    this.one = false;
                }
            }
        }

        public void wiimoteExtensionChanged(WiimoteExtensionChangedEventArgs args)
        {

        }

        
        public Form createSetupForm()
        {
            Form form = new PluginCalibration(konfigurator);
            form.Show();
            return form;
        }

        public bool doRumble()
        {
            return isRumbling;
        }
    }
}
