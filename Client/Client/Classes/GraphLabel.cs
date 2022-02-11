using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Client
{
    public class GraphLabel
    {
        public string label;
        public PointF location = new PointF(0, 0);       
        public RectangleF boundingBox;
        public RectangleF outlineBox;
        public float degree;

        public bool mouseIsOver = false;

        public void draw(Graphics g)
        {
            try
            {
                Matrix gt = g.Transform;
                SmoothingMode sm = g.SmoothingMode;
                                
                g.TranslateTransform(location.X, location.Y);

                //g.DrawRectangle(Pens.Black, -boundingBox.Width / 2, 0, boundingBox.Width, boundingBox.Height);

                if (degree > 0  && degree < 180)
                    g.RotateTransform(degree -90);
                else
                    g.RotateTransform(degree+90);

                if(mouseIsOver)
                    g.FillRectangle(Brushes.LightGray, -outlineBox.Width / 2, 0, outlineBox.Width, outlineBox.Height);
                else
                    g.FillRectangle(Brushes.White, -outlineBox.Width/2, 0, outlineBox.Width, outlineBox.Height);

                g.DrawRectangle(Pens.Black, -outlineBox.Width/2, 0, outlineBox.Width, outlineBox.Height);

                g.SmoothingMode = SmoothingMode.None;
                g.DrawString(label,
                            Common.Frm1.f10,
                            Brushes.Black,
                            new Point(0,2),
                            Common.Frm1.fmt);
                g.SmoothingMode = sm;

                               
                g.Transform = gt;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public bool isOver(Graphics g,Point pt)
        {
            try
            {
                Point[] pts = new Point[1];
                pts[0] = pt;

                Matrix gt = g.Transform;

                g.TranslateTransform((float)Common.Frm1.pnlMain.Width / (float)2, (float)Common.Frm1.pnlMain.Height / (float)2);
                g.TranslateTransform(location.X, location.Y);               

                if (degree > 0 && degree < 180)
                    g.RotateTransform(degree - 90);
                else
                    g.RotateTransform(degree + 90);

                g.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, pts);
               
                g.Transform = gt;

                if ((float)pts[0].X >= -outlineBox.Width / 2 && (float)pts[0].X<= (float)outlineBox.Width / 2  && (float)pts[0].Y>=0 && pts[0].Y <= outlineBox.Height)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return false;
            }
        }

        public void setupBoundingBox(RectangleF[] r, Graphics g, string label, float value, float degree)
        {
            try
            {
                int counter = -1;

                              
                this.degree = degree;
                this.label = label;

                setupBoundingBox2(counter, g, value);

                if (r != null)
                {

                    bool go = true;

                    while (counter > -Common.Frm1.circleDiameter && go)
                    {
                        go = false;

                        for (int i = 1; i <= r.Length - 1; i++)
                        {
                            if (r[i] != null)
                            {
                                if (r[i].IntersectsWith(boundingBox))
                                {
                                    setupBoundingBox2(counter, g, value);
                                    go = true;
                                }
                            }
                        }

                        counter--;
                    }

                    //try to stagger if no location found
                    //int counter2 = -1;
                    //if(go)
                    //{
                    //    while (counter2 < 100 && go)
                    //    {
                    //        go = false;

                    //        for (int i = 1; i <= r.Length - 1; i++)
                    //        {
                    //            if (r[i] != null)
                    //            {
                    //                if (r[i].IntersectsWith(boundingBox))
                    //                {
                    //                    setupBoundingBox2(counter, g, label, value, degree + counter2, ref boundingBox, ref locationLabel);
                    //                    go = true;
                    //                }
                    //            }
                    //        }

                    //        counter2++;
                    //    }
                    //}
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void setupBoundingBox2(int counter, Graphics g, float value)
        {
            try
            {
                float tempDistance2 = Common.Frm1.circleRadius + counter + Common.dataUnit * ((float)value - (float)Common.minControlPointValue);

                location = new PointF(tempDistance2 * (float)Math.Cos(degree * (Math.PI / 180)),
                                           tempDistance2 * (float)Math.Sin(degree * (Math.PI / 180)) - 7);

                SizeF sf = g.MeasureString(label, Common.Frm1.f10, Common.Frm1.pnlMain.Width, Common.Frm1.fmt);
                sf.Width += 2;
                sf.Height += 2;

                boundingBox = new RectangleF(location.X - sf.Height / (float)2.0, location.Y - 1, sf.Height,sf.Height);
                outlineBox = new RectangleF(new PointF(location.X - 1 - sf.Width / (float)2.0, location.Y - 1), sf);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

    }
}
