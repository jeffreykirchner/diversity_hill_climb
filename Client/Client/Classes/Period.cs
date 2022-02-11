using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Client
{
    public class Period
    {
        public CirclePoint[] circlePoints;
        
        public int periodNumber;                       //period number
        public int startLocation;                      //index of start location

        public double maxValue;                               //max possible value this period 
        public int[] maxValueLocations = new int[1000];       //locations of max value
        public int maxValueLocationCount = 0;                 //number of locations that have max value

        public void fromString(ref string[] msgtokens, ref int nextToken)
        {
            try
            {                
                periodNumber = int.Parse(msgtokens[nextToken++]);
                startLocation = int.Parse(msgtokens[nextToken++]);

                maxValue = int.Parse(msgtokens[nextToken++]);
                maxValueLocationCount = int.Parse(msgtokens[nextToken++]);

                for (int i = 1; i <= maxValueLocationCount; i++)
                {
                    maxValueLocations[i] = int.Parse(msgtokens[nextToken++]);
                }

                circlePoints = new CirclePoint[Common.circlePointCount + 1];

                for (int i=1;i<=Common.circlePointCount;i++)
                {
                    circlePoints[i] = new CirclePoint();
                    circlePoints[i].fromString(ref msgtokens,ref nextToken);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void draw(Graphics g)
        {
            try
            {

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
