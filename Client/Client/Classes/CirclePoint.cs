using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Client
{
    public class CirclePoint
    {
        public float value = -1;
        public bool controlPoint = false;

        public PointF location= new PointF(0,0);        
        public RectangleF boundingBox = new RectangleF();

        public float degree = new float();       //degree around the circle
        
       

        public void fromString(ref string[] msgtokens,ref int nextToken)
        {
            try
            {
                value = float.Parse(msgtokens[nextToken++]);
                controlPoint = bool.Parse(msgtokens[nextToken++]);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void setup(float tempD)
        {
            try
            {
                degree = tempD;                               

                location = Common.returnLocation(tempD, value);

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
