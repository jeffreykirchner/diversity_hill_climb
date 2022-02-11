using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{
    public class Screen
    {
        Graphics g = null;
        Image imageOffScreen = null;
        Graphics graphicsOffScreen = null;
        int screenX = 0;
        int screenY = 0;
        int screenWidth = 0;
        int screenHeight = 0;

        public Screen(Panel p, Rectangle r)
        {
            try
            {
                g = p.CreateGraphics();
                screenX = r.X;
                screenY = r.Y;
                screenWidth = r.Width;
                screenHeight = r.Height;
                //  get offscreen buffer context
                imageOffScreen = new Bitmap(screenWidth, screenHeight);
                graphicsOffScreen = Graphics.FromImage(imageOffScreen);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public Graphics GetGraphics()
        {
            try
            {
                return graphicsOffScreen;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return graphicsOffScreen;
            }

        }

        SolidBrush blackBrush = new SolidBrush(Color.White);
        // erase
        public void erase1()
        {
            try
            {
                //  erase all content in back buffer by using background color
                if (!isValidGraphics())
                {
                    return;
                }

                graphicsOffScreen.FillRectangle(blackBrush, 0, 0, screenWidth, screenHeight);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }

        }

        public void flip()
        {
            try
            {
                //  flip buffers for smooth animation
                g.DrawImage(imageOffScreen, screenX, screenY);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }

        }

        // flip
        public bool isValidGraphics()
        {
            try
            {
                if ((!(g == null)
                            && !(graphicsOffScreen == null)))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return false;
            }

        }
    }
}
