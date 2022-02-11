using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Server
{
    public class Period
    {
        public CirclePoint[] circlePoints;        
        public int periodNumber;                       //period number 
        public int startLocation;                      //index of start location

        public double maxValue;                               //max possible value this period 
        public int[] maxValueLocations = new int[1000];       //locations of max value
        public int maxValueLocationCount = 0;                 //number of locations that have max value

        public PeriodGroup[] periodGroups;             //group or induvidal playing a game 
        public int periodGroupCount;

        public void setup(int periodNumber)
        {
            try
            {
                this.periodNumber = periodNumber;

                if (periodNumber == 0)
                {
                    //setup as instruction period

                    periodGroupCount = Common.numberOfPlayers;
                    startLocation = int.Parse(INI.getINI(Common.sfile, "instructions", "startingLocation"));
                    Common.setupCirclePointInstructions(ref circlePoints, ref maxValue, ref maxValueLocationCount, ref maxValueLocations, periodNumber);
                }
                else
                {
                    //normal period
                    startLocation = Rand.rand(Common.circlePointCount, 1);

                    Common.setupCirclePoints(ref circlePoints, ref maxValue, ref maxValueLocationCount, ref maxValueLocations, periodNumber);

                    periodGroupCount = 0;
                    for (int i = 1; i <= Common.numberOfPlayers; i++)
                    {
                        if (Common.playerlist[i].groupNumber[Common.currentPeriod] > periodGroupCount)
                            periodGroupCount = Common.playerlist[i].groupNumber[Common.currentPeriod];
                    }
                }
                

                //load parameters
                //string[] msgtokens = INI.getINI(Common.sfile, "periods", periodNumber.ToString()).Split(';');
                //int nextToken = 0;

                //groupAssignment = msgtokens[nextToken++];

                //random start location
               

                 //setup period groups
                 //if (groupAssignment == "Individual")
                 //{
                 //    periodGroupCount = Common.numberOfPlayers;
                 //}
                 //else
                 //{
                 //    periodGroupCount = Common.numberOfGroups;
                 //}

                

                //setup circle point locations
                float degreeCounter = -90;

                for (int j = 1; j <= Common.circlePointCount; j++)
                {
                    circlePoints[j].setup(degreeCounter);
                    degreeCounter += Common.FrmServer.tempDegree;
                }               

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }

        }

        public string toString()
        {
            try
            {
                string str = "";

                //str = groupAssignment + ";";
                str = periodNumber + ";";
                str += startLocation + ";";
                str += maxValue + ";";
                str += maxValueLocationCount + ";";

                for (int i = 1; i <= maxValueLocationCount; i++)
                {
                    str += maxValueLocations[i] + ";";
                }

                for (int i = 1; i <= Common.circlePointCount; i++)
                {
                    str += circlePoints[i].toString();
                }

                return str;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return "";
            }
        }

        public void doPeriod(int index)
        {
            try
            {

                if (index == -1)
                {
                    //normal periods
                    for (int i = 1; i <= periodGroupCount; i++)
                    {
                        periodGroups[i].doPeriodGroup(startLocation);
                    }

                    writeSummaryData();
                }
                else
                {
                    //during instructions do each player individually
                    periodGroups[index].doPeriodGroup(startLocation);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void setupPeriod()
        {
            try
            {
                //calc number of period groups
                periodGroupCount = 0;

                for (int i=1;i<=Common.numberOfPlayers;i++)
                {
                    if(Common.playerlist[i].groupNumber[periodNumber] > periodGroupCount)
                    {
                        periodGroupCount = Common.playerlist[i].groupNumber[periodNumber];
                    }
                }

                //setup period groups
                periodGroups = new PeriodGroup[periodGroupCount + 1];

                for (int i = 1; i <= periodGroupCount; i++)
                {
                    periodGroups[i] = new PeriodGroup();
                    periodGroups[i].setup(i);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public string getGroupResult(int group)
        {
            try
            {
                return periodGroups[group].toString();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return "";
            }
        }

        public void writeSummaryData()
        {
            try
            {
                for(int i=1;i<=periodGroupCount;i++)
                {
                    periodGroups[i].writeSummaryData();
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);                
            }
        }

        public void writeReplayData()
        {
            try
            {
                for (int i = 1; i <= periodGroupCount; i++)
                {
                    periodGroups[i].writeReplayData();
                }
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
                JObject jo = new JObject();

                jo.Add(new JProperty("Period Number", periodNumber));
                jo.Add(new JProperty("Start Location", startLocation));
                jo.Add(new JProperty("Max Value", maxValue));
                jo.Add(new JProperty("Max Value Location Count", maxValueLocationCount));


                JObject joMaxValueLocations = new JObject();

                for(int i=1;i<=maxValueLocationCount;i++)
                {
                    joMaxValueLocations.Add(new JProperty(i.ToString(), maxValueLocations[i]));
                }

                jo.Add(new JProperty("Max Value Locations", joMaxValueLocations));

                JObject joCirclePoints = new JObject();
                
                for (int i=1;i<=Common.circlePointCount;i++)
                {
                    joCirclePoints.Add(circlePoints[i].getJson(i));
                }

                jo.Add(new JProperty("Circle Points", joCirclePoints));

                jo.Add(new JProperty("Period Group Count", periodGroupCount));

                JObject joPeriodGroups = new JObject();
                for (int i = 1; i <= periodGroupCount; i++)
                {
                    joPeriodGroups.Add(periodGroups[i].getJson(i));
                }

                jo.Add(new JProperty("Period Groups", joPeriodGroups));

                //Common.periodsDf.WriteLine(jo.ToString());

                //using(JsonTextWriter writer = new JsonTextWriter(Common.periodsDf))
                // {
                //     test.WriteTo(writer);
                // }

                return new JProperty(index.ToString(),jo);
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

                periodNumber = (int)jo["Period Number"];
                startLocation = (int)jo["Start Location"];
                maxValue = (int)jo["Max Value"];
                maxValueLocationCount = (int)jo["Max Value Location Count"];

                //max value locations
                JObject joMaxValueLocations = new JObject((JObject)jo["Max Value Locations"]);

                maxValueLocations = new int[maxValueLocationCount + 1];

                for(int i=1;i<=maxValueLocationCount;i++)
                {
                    maxValueLocations[i] = (int)joMaxValueLocations[i.ToString()];
                }
                
                //circle points
                circlePoints = new CirclePoint[Common.circlePointCount + 1];

                JObject joCirclePoints = new JObject((JObject)jo["Circle Points"]);

                for (int i=1;i<=Common.circlePointCount;i++)
                {
                    circlePoints[i] = new CirclePoint();
                    circlePoints[i].fromJSON(joCirclePoints.Property(i.ToString()));
                }

                //period groups
                periodGroupCount = (int)jo["Period Group Count"];
                periodGroups = new PeriodGroup[periodGroupCount + 1];
                JObject joPeriodGroups = new JObject((JObject)jo["Period Groups"]);

                for (int i=1;i<= periodGroupCount;i++)
                {
                    periodGroups[i] = new PeriodGroup();
                    periodGroups[i].fromJSON(joPeriodGroups.Property(i.ToString()));
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}