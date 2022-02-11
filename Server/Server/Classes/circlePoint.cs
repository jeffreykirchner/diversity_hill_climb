using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Drawing2D;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Server
{
    public class CirclePoint
    {

        public float value = -1;
        public bool controlPoint = false;
        public double slope = 0;

        public PointF location = new PointF(0, 0);
        public float degree = new float();       //degree around the circle

        public string toString()
        {
            try
            {

          
            string str = "";

            str = value + ";";
            str += controlPoint + ";";

            return str;

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return "";
            }
        }

        public void setup(float tempD)
        {
            try
            {
                degree = tempD;

                location = Common.returnLocation(tempD, value);

                //float tempDistance = Common.FrmServer.circleRadius + Common.dataUnit * ((float)value - (float)Common.minControlPointValue);

                //location = new PointF(tempDistance * (float)Math.Cos(degree * (Math.PI / 180)),
                //                      tempDistance * (float)Math.Sin(degree * (Math.PI / 180)));

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public JProperty getJson(int index)
        {
            try
            {
                JProperty jp = new JProperty(index.ToString(),
                    new JObject(
                        new JProperty("Value", value),
                        new JProperty("Control Point", controlPoint),
                        new JProperty("Slope", slope),
                        new JProperty("Location X", location.X),
                        new JProperty("Location Y", location.Y),
                        new JProperty("Degree", degree))
                    );

                return jp;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return null;
            }
        }

        public void fromJSON(JProperty jp)
        {
            try
            {
                JObject jo = (JObject)jp.Value;

                value = (float)jo["Value"];
                controlPoint = (bool)jo["Control Point"];
                slope = (double)jo["Slope"];
                location.X = (float)jo["Location X"];
                location.Y = (float)jo["Location Y"];
                degree = (float)jo["Degree"];
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                
            }
        }

    }
}
