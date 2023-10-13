using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

using System.IO;


namespace Client
{
    public partial class frm1 : Form
    {
        public Screen mainScreen;

        public Font f6 = new Font("Microsoft Sans Serif", 6, System.Drawing.FontStyle.Bold);
        public Font f8 = new Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold);
        public Font f10 = new Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold);

        public StringFormat fmt = new StringFormat(); //center alignment
        public StringFormat fmtL = new StringFormat(); //center alignment
        public StringFormat fmtR = new StringFormat(); //center alignment

        public Pen p3Blue;
        public Pen p3Gold;
        public Pen p3Black;
        public Pen p10BackArrow;
        public Pen p3StartColor;
        public Pen p3EndColor;
        public Pen p1BlackDash;

        public float circleDiameter;
        public float circleRadius;
        public float circleOffset = 25;
        public float circleDataArea = 250;
        public float tempDegree = 360;
        public PointF zeroLine;

        public PointF selectionPoint = new Point(0, 0);
        public Color selectionColor = Color.Green;
        public int selectionStartCirclePoint = 1;
        public int selectionEndCirclePoint = 1;
        public int selectionTurn = 0;
        public int selectionMove = 0;
        public int selectionIndex = 0;
        public int selectionRound = 0;

        public Color startColor = Color.CornflowerBlue;
        public Color endColor = Color.Crimson;
        public Color moveColor = Color.LightBlue;

        public string[] lastMoveDirections = new string[100];
        public string[] lastMoveDistances = new string[100];

        public frm1()
        {
            InitializeComponent();
        }

        private void frm1_Load(object sender, EventArgs e)
        {
            try
            {
                mainScreen = new Screen(pnlMain, new Rectangle(0, 0, pnlMain.Width, pnlMain.Height));

                fmt.Alignment = StringAlignment.Center;
                fmtL.Alignment = StringAlignment.Near;
                fmtR.Alignment = StringAlignment.Far;

                p1BlackDash = new Pen(new SolidBrush(Color.FromArgb(75,Color.Black)) ,(float) 0.5);
                p1BlackDash.DashStyle = DashStyle.Dot;
                p1BlackDash.Alignment = PenAlignment.Center;

                p3Blue = new Pen(Brushes.CornflowerBlue, 3);
                p3Blue.Alignment = PenAlignment.Center;

                p3Black = new Pen(Brushes.Black, 3);
                p3Black.Alignment = PenAlignment.Center;

                p3Gold = new Pen(Brushes.Gold, 3);
                p3Gold.Alignment = PenAlignment.Center;

                p3StartColor = new Pen(startColor, 3);
                p3StartColor.Alignment = PenAlignment.Center;
                p3StartColor.EndCap = LineCap.Triangle;
                p3StartColor.StartCap = LineCap.Triangle;

                p3EndColor = new Pen(endColor, 3);
                p3EndColor.Alignment = PenAlignment.Center;
                p3EndColor.EndCap = LineCap.Triangle;
                p3EndColor.StartCap = LineCap.Triangle;

                p10BackArrow = new Pen(Brushes.Black, 10);
                p10BackArrow.Alignment = PenAlignment.Center;
                p10BackArrow.EndCap = LineCap.ArrowAnchor;

                circleDiameter = Math.Min(pnlMain.Height,pnlMain.Width) - circleOffset*2-circleDataArea*2;
                circleRadius = circleDiameter / 2;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        /// <summary>Start game loop</summary> 
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                refreshScreen();
            }               
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void refreshScreen()
        {
            try
            {
                Period p = Common.periodList[Common.currentPeriod];
                PeriodGroup pg = Common.periodGroups[Common.currentPeriod];               

                if (mainScreen == null) return;

                mainScreen.erase1();
                Graphics g = mainScreen.GetGraphics();

                g.SmoothingMode = SmoothingMode.AntiAlias;

                //legend
                g.FillRectangle(new SolidBrush(startColor), new RectangleF(pnlMain.Width - 170, pnlMain.Height-115 +  15, 20, 20));
                g.DrawString("Starting Location", f10, Brushes.Black, pnlMain.Width - 140, pnlMain.Height - 115 + 18, fmtL);

                g.FillRectangle(new SolidBrush(moveColor), new RectangleF(pnlMain.Width - 170, pnlMain.Height - 115 + 45, 20, 20));
                g.DrawString("Moves", f10, Brushes.Black, pnlMain.Width - 140, pnlMain.Height - 115 + 48, fmtL);

                g.FillRectangle(new SolidBrush(endColor), new RectangleF(pnlMain.Width - 170, pnlMain.Height - 115 + 75, 20, 20));
                g.DrawString("Ending Location", f10, Brushes.Black, pnlMain.Width - 140, pnlMain.Height - 115 + 78, fmtL);

                //tick marks

                float degreeCounter = -90;

                g.TranslateTransform((float)pnlMain.Width / (float)2, (float)pnlMain.Height / (float)2);

                //main circle
                g.DrawEllipse(p3Black, new RectangleF(new PointF(-circleDiameter/(float)2, -circleDiameter/(float)2), new SizeF(circleDiameter, circleDiameter)));

                for (int i=1;i<=Common.circlePointCount;i+=10)
                {
                    double tempLength = 10;

                    if (i % 100 == 1)
                        tempLength = 20;

                    g.DrawLine(Pens.Black,
                          new PointF((float)circleRadius * (float) Math.Cos(degreeCounter * (Math.PI / 180)),
                                    (float) circleRadius * (float) Math.Sin(degreeCounter * (Math.PI / 180))),
                          new PointF((float)(circleRadius-tempLength) * (float) Math.Cos(degreeCounter * (Math.PI / 180)),
                                    (float)(circleRadius-tempLength) * (float) Math.Sin(degreeCounter * (Math.PI / 180))));

                    if(tempLength==20)
                        g.DrawString(i.ToString(),
                                 f6,
                                 Brushes.Black,
                                 new PointF((float)(circleRadius - tempLength - 10) * (float)Math.Cos(degreeCounter * (Math.PI / 180)),
                                            (float)(circleRadius - tempLength - 10) * (float)Math.Sin(degreeCounter * (Math.PI / 180))-3),
                                 fmt);

                    degreeCounter += tempDegree*10;
                }              

                 //zero line
                g.DrawEllipse(p1BlackDash, new RectangleF(-zeroLine.X, -zeroLine.X, zeroLine.X * 2, zeroLine.X * 2));
                g.DrawString("Zero", f8, Brushes.DimGray, new PointF(0, -zeroLine.X),fmt);

                if (Common.drawFullCircle &&( Common.gamePhase=="results" || Common.showInstructions))
                {
                   

                    //draw line
                    for (int i = 1; i < Common.circlePointCount; i++)
                    {
                        g.DrawLine(Pens.LightGray, p.circlePoints[i].location, p.circlePoints[i + 1].location);
                    }

                    g.DrawLine(Pens.LightGray, p.circlePoints[1].location, p.circlePoints[Common.circlePointCount].location);

                    //mark control points
                    //for (int i = 1; i <= Common.circlePointCount; i++)
                    //{
                    //    if (p.circlePoints[i].controlPoint)
                    //        g.FillEllipse(Brushes.Crimson, new RectangleF(p.circlePoints[i].location.X - 2, p.circlePoints[i].location.Y - 2, 4, 4));
                    //}
                }

                //draw results
                pg.draw(g);

                if(Common.gamePhase =="results" && Common.showProgramResults)
                {
                    //draw selection
                    g.DrawLine(p3Gold, new Point(0, 0), selectionPoint);
                    g.DrawLine(new Pen(selectionColor), new Point(0, 0), selectionPoint);
                }

                g.FillEllipse(Brushes.Black, new RectangleF(-5, -5, 10, 10));                

                //arrow from start to current location
                if (selectionStartCirclePoint != selectionEndCirclePoint && Common.gamePhase == "results"  && Common.showProgramResults)
                {
                    float a1 = circlePointToAngle(selectionStartCirclePoint);
                    float a2 = circlePointToAngle(selectionEndCirclePoint);

                    float sweep;

                    if(a1>=a2)
                    {
                        if ((360 - a1 + a2) < a1 - a2)
                            sweep = 360 - a1 + a2;
                        else
                            sweep = a2 - a1;
                    }
                    else
                    {
                        if ((360 - a2 + a1) < a2 - a1)
                            sweep = -(360 - a2 + a1);
                        else
                            sweep = a2 - a1;
                    }

                    

                    g.DrawArc(p10BackArrow,
                              new RectangleF(-circleDiameter / 4, -circleDiameter / 4, circleDiameter / 2, circleDiameter / 2),
                              a1 - (float)90,
                              sweep);
                }

                //pg.drawLabels(g);


                g.ResetTransform();

                mainScreen.flip();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public float circlePointToAngle(int tempCirclePoint)
        {
            try
            {
                float f = 0;

                float d1 = 360 / (float)Common.circlePointCount;

                f = (d1 * tempCirclePoint);

                return f;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }
        }

        ///<summary>If ALT+K are pressed kill the client,if ALT+Q are pressed bring up connection box</summary>          
        private void frm1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { 

                if (e.Alt == true)
                {
                    if (Convert.ToInt32(e.KeyValue) == Convert.ToInt32(Keys.K))
                    {
                        if (MessageBox.Show("Close Program?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;

                        Common.closeClient();
                    }
                    else if (Convert.ToInt32(e.KeyValue) == Convert.ToInt32(Keys.Q))
                    {
                        Common.FrmConnect.Show();
                    }
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Common.FrmNames != null)
                {
                    if (Common.FrmNames.Visible)
                    {
                        int tempN = Rand.rand(20, 5);
                        string s = "";
                        for (int i = 1; (i <= tempN); i++)
                        {

                            s += (char)Rand.rand(122, 60);
                        }

                        Common.FrmNames.txtName.Text = s;

                        Common.FrmNames.txtIDNumber.Text = Rand.rand(100000, 1).ToString();

                        Common.FrmNames.cmdSubmit.PerformClick();

                        Common.testMode = false;
                        timer2.Enabled = false;
                        return;
                    }
                }

                
                if(Common.FrmInstructions!= null && Common.FrmInstructions.Visible)
                {
                    if (Common.FrmInstructions.pagesDone[Common.FrmInstructions.currentInstruction])
                    {
                        Common.FrmInstructions.cmdNext.PerformClick();
                        return;
                    }
                    else
                    {
                        switch (Common.FrmInstructions.currentInstruction)
                        {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                if (dgMoves[2, 3].Value.ToString() == "")
                                {
                                    dgMoves[1, 0].Value = "Clockwise →";
                                    dgMoves[1, 1].Value = "← Counter Clockwise";
                                    dgMoves[1, 2].Value = "Clockwise →";
                                    dgMoves[1, 3].Value = "← Counter Clockwise";

                                    dgMoves[2, 0].Value = "145";
                                    dgMoves[2, 1].Value = "15";
                                    dgMoves[2, 2].Value = "90";
                                    dgMoves[2, 3].Value = "25";
                                }
                                else
                                {
                                    cmdSubmit.PerformClick();
                                }
                                break;
                            case 5:
                                Common.Frm1.dgResults.Rows[Common.Frm1.dgResults.RowCount-1].Selected = true;
                                Common.Frm1.dgResults_selectionChangedAction(Common.Frm1.dgResults.RowCount - 1);
                                break;
                            case 6:
                                Common.FrmInstructions.cmdStart.PerformClick();
                                break;
                            case 7:
                                if (dgMoves[2, 3].Value.ToString() == "")
                                {
                                    fillRandomProgram();
                                }
                                else
                                {
                                    cmdSubmit.PerformClick();
                                }
                                break;
                            case 8:
                                if(cmdReady.Visible)
                                {
                                    cmdReady.PerformClick();
                                }
                                break;

                        }
                    }

                    return;
                }

                if (cmdSubmit.Visible)
                {
                    if(dgMoves[2, 1].Value.ToString() == "")
                    {

                        fillRandomProgram();
                    }
                    else
                    {
                        cmdSubmit.PerformClick();
                    }
                }
                else if( cmdReady.Visible)
                {
                    
                    cmdReady.PerformClick();
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void fillRandomProgram()
        {
            try
            {
                for (int i = 1; i <= dgMoves.RowCount; i++)
                {
                    DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)dgMoves.Columns[1];
                    if (Rand.rand(2, 1) == 1)
                        dgMoves[1, i - 1].Value = col.Items[0].ToString();
                    else
                        dgMoves[1, i - 1].Value = col.Items[1].ToString();

                    dgMoves[2, i - 1].Value = Rand.rand(Common.maxDistancePerTurn, 1);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void frm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!Common.clientClosing) e.Cancel = true;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                cmdSubmitAction(false);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void cmdSubmitAction(bool autoSubmit)
        {
            try
            {
                if (!cmdSubmit.Visible) return;

                string str = "";

                for (int i = 1; i <= Common.movesPerTurn; i++)
                {
                    //check valid
                    if (dgMoves[1, i - 1].Value == null || dgMoves[2, i - 1].Value == null)
                    {
                        lblError.Visible = true;
                        lblError.Text = "Invalid entry, fill all moves.";
                        return;
                    }
                    else if (!Common.validateNumber(dgMoves[2, i - 1].Value.ToString(), false, false))
                    {
                        lblError.Visible = true;
                        lblError.Text = "Invalid Distance.";
                        return;
                    }
                    else if (int.TryParse(dgMoves[2, i - 1].Value.ToString(), out int v))
                    {
                        if (v <= 0 || v > Common.maxDistancePerTurn)
                        {
                            lblError.Visible = true;
                            lblError.Text = $"Invalid Distance, must be 1 to {Common.maxDistancePerTurn}";
                            return;
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Invalid Entry.";
                        return;
                    }

                    str += dgMoves[1, i - 1].Value + ";";
                    str += dgMoves[2, i - 1].Value + ";";

                    lastMoveDirections[i - 1] = dgMoves[1, i - 1].Value.ToString();
                    lastMoveDistances[i - 1] = dgMoves[2, i - 1].Value.ToString();
                }

                lblError.Visible = false;

                if (Common.currentPeriod == 0)
                {
                    bool fail = false;

                    if (dgMoves[1, 0].Value.ToString() != "Clockwise →" && Common.exampleDirection1=="CW" ||
                        dgMoves[1, 0].Value.ToString() != "← Counter Clockwise" && Common.exampleDirection1 == "CCW") fail = true;

                    if (dgMoves[1, 1].Value.ToString() != "Clockwise →" && Common.exampleDirection2 == "CW" ||
                        dgMoves[1, 1].Value.ToString() != "← Counter Clockwise" && Common.exampleDirection2 == "CCW") fail = true;

                    if (dgMoves[1, 2].Value.ToString() != "Clockwise →" && Common.exampleDirection3 == "CW" ||
                        dgMoves[1, 2].Value.ToString() != "← Counter Clockwise" && Common.exampleDirection3 == "CCW") fail = true;

                    if (dgMoves[1, 3].Value.ToString() != "Clockwise →" && Common.exampleDirection4 == "CW" ||
                        dgMoves[1, 3].Value.ToString() != "← Counter Clockwise" && Common.exampleDirection4 == "CCW") fail = true;

                    //if (dgMoves[1, 1].Value.ToString() != "← Counter Clockwise") fail = true;
                    //if (dgMoves[1, 2].Value.ToString() != "Clockwise →") fail = true;
                    //if (dgMoves[1, 3].Value.ToString() != "← Counter Clockwise") fail = true;

                    if (dgMoves[2, 0].Value.ToString() != Common.exampleDistance1.ToString()) fail = true;
                    if (dgMoves[2, 1].Value.ToString() != Common.exampleDistance2.ToString()) fail = true;
                    if (dgMoves[2, 2].Value.ToString() != Common.exampleDistance3.ToString()) fail = true;
                    if (dgMoves[2, 3].Value.ToString() != Common.exampleDistance4.ToString()) fail = true;

                    if (fail)
                    {
                        MessageBox.Show("Please enter the specified program.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Common.FrmInstructions.pagesDone[4] = true;
                }
                else
                {
                    timer4.Enabled = false;
                    Common.Frm1.txtTimeRemaining.Text = "-";
                    txtMessages.Text = "Waiting for Others.";
                }

                cmdSubmit.Visible = false;
                dgMoves.Columns[1].ReadOnly = true;
                dgMoves.Columns[2].ReadOnly = true;

                str += autoSubmit + ";";

                Common.FrmClient.SC.sendMessage("01", str);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdReady_Click(object sender, EventArgs e)
        {
            try
            {
                if (!cmdReady.Visible) return;

                timer4.Enabled = false;
                Common.Frm1.txtTimeRemaining.Text = "-";

                string str = "";

                txtMessages.Text = "Waiting for others.";
                cmdReady.Visible = false;

                Common.FrmClient.SC.sendMessage("02", str);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void dgResults_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                dgResults_selectionChangedAction(dgResults.CurrentRow.Index);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void dgResults_selectionChangedAction(int r)
        {
            try
            {               

                if (dgResults[4, r].Value == null) return;

                if (dgResults[4, r].Value.ToString() == "Start")
                {
                    if (Common.periodGroups[Common.currentPeriod].startingLocation == 0) return;
                    selectionPoint = Common.periodList[Common.currentPeriod].circlePoints[Common.periodGroups[Common.currentPeriod].startingLocation].location;
                    selectionColor = startColor;
                    selectionStartCirclePoint = Common.periodGroups[Common.currentPeriod].startingLocation;
                    selectionEndCirclePoint = Common.periodGroups[Common.currentPeriod].startingLocation;

                    selectionTurn = 0;
                    selectionMove = 0;
                    selectionIndex = 0;
                    selectionRound = 0;
                }
                else if (dgResults[4, r].Value.ToString() == "End")
                {
                    if (Common.periodGroups[Common.currentPeriod].endingLocation == 0) return;
                    selectionPoint = Common.periodList[Common.currentPeriod].circlePoints[Common.periodGroups[Common.currentPeriod].endingLocation].location;
                    selectionColor = endColor;

                    selectionStartCirclePoint = Common.periodGroups[Common.currentPeriod].startingLocation;
                    selectionEndCirclePoint = Common.periodGroups[Common.currentPeriod].startingLocation;

                    selectionTurn = 10000;
                    selectionMove = 10000;
                    selectionIndex = 10000;
                    selectionRound = 10000;

                    if(Common.currentPeriod == 0)
                    {
                        Common.FrmInstructions.pagesDone[5] = true;
                    }
                }
                else
                {
                    int tempTurn = int.Parse(dgResults[2, r].Value.ToString());
                    int tempMove = int.Parse(dgResults[3, r].Value.ToString());
                    int tempRound = int.Parse(dgResults[0, r].Value.ToString()); ;
                    int tempPlayer;

                    if (dgResults[1, r].Value.ToString() == "You")
                    {
                        tempPlayer = Common.inumber;
                    }
                    else
                    {
                        tempPlayer = int.Parse(dgResults[1, r].Value.ToString());
                    }


                    selectionPoint = Common.periodGroups[Common.currentPeriod].getMoveLocation(tempPlayer, tempTurn, tempMove, tempRound);
                    selectionColor = moveColor;

                    selectionStartCirclePoint = Common.periodGroups[Common.currentPeriod].getStartingCirclePoint(tempPlayer, tempTurn, tempRound);
                    selectionEndCirclePoint = Common.periodGroups[Common.currentPeriod].getMoveCirclePoint(tempPlayer, tempTurn, tempMove, tempRound);

                    selectionTurn = tempTurn;
                    selectionMove = tempMove;
                    selectionIndex = Common.periodGroups[Common.currentPeriod].getPlayerIndex(tempPlayer);
                    selectionRound = tempRound;
                }

                refreshScreen();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void clearMouseOver()
        {
            try
            {
                for(int i=1;i<=dgGroup.RowCount;i++)
                {
                    if (dgGroup[2, i - 1].Style.BackColor == Color.LightGray) 
                        dgGroup[2, i - 1].Style.BackColor = Color.White;

                    if (dgGroup[3, i - 1].Style.BackColor == Color.LightGray) 
                        dgGroup[3, i - 1].Style.BackColor = Color.White;
                }

                Common.periodGroups[Common.currentPeriod].labelStart.mouseIsOver = false;
                Common.periodGroups[Common.currentPeriod].labelEnd.mouseIsOver = false;

                //for (int i = Common.periodGroups[Common.currentPeriod].periodGroupPlayerCount; i >=1; i--)
                //{
                //    Common.periodGroups[Common.currentPeriod].periodGroupPlayers[i].labelBest.mouseIsOver = false;
                //    Common.periodGroups[Common.currentPeriod].periodGroupPlayers[i].labelStart.mouseIsOver = false;
                //    Common.periodGroups[Common.currentPeriod].periodGroupPlayers[i].labelEnd.mouseIsOver = false;
                //}
            }
             catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //if (Common.gamePhase == "results")
                //{

                //    clearMouseOver();                   

                //    //check over end label
                //    if (Common.periodGroups[Common.currentPeriod].labelEnd.isOver(mainScreen.GetGraphics(), e.Location))
                //    {
                //        Common.periodGroups[Common.currentPeriod].labelEnd.mouseIsOver = true;
                //        refreshScreen();
                //        return;
                //    }
                               
                //    //check over turn labels
                //    for (int i = Common.periodGroups[Common.currentPeriod].periodGroupPlayerCount; i >= 1; i--)
                //    {
                //        if (Common.periodGroups[Common.currentPeriod].periodGroupPlayers[i].labelStart.isOver(mainScreen.GetGraphics(), e.Location))
                //        {
                //            Common.periodGroups[Common.currentPeriod].periodGroupPlayers[i].labelStart.mouseIsOver = true;

                //            if (Common.Frm1.gbGroups.Visible)
                //            {
                //                dgGroup[2, i - 1].Style.BackColor = Color.LightGray;
                //                dgGroup.Refresh();
                //            }

                //            refreshScreen();
                                
                //            return;
                //        }

                //        if (Common.periodGroups[Common.currentPeriod].periodGroupPlayers[i].labelEnd.isOver(mainScreen.GetGraphics(), e.Location))
                //        {
                //            Common.periodGroups[Common.currentPeriod].periodGroupPlayers[i].labelEnd.mouseIsOver = true;
                //            if (Common.Frm1.gbGroups.Visible)
                //            {
                //                dgGroup[3, i - 1].Style.BackColor = Color.LightGray;
                //                dgGroup.Refresh();
                //            }
                //            refreshScreen();
                //            return;
                //        }
                //    }
                    
                //    //check over start labels
                //    if (Common.periodGroups[Common.currentPeriod].labelStart.isOver(mainScreen.GetGraphics(), e.Location))
                //    {
                //        Common.periodGroups[Common.currentPeriod].labelStart.mouseIsOver = true;
                //        refreshScreen();
                //        return;
                //    }

                //}
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void dgGroup_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                //if (Common.gamePhase == "results")
                //{
                //    if (e.RowIndex == -1) return;

                //    if (e.ColumnIndex == 2)
                //    {
                //        clearMouseOver();
                //        Common.periodGroups[Common.currentPeriod].periodGroupPlayers[e.RowIndex+1].labelStart.mouseIsOver = true;
                //        dgGroup[2, e.RowIndex].Style.BackColor = Color.LightGray;
                //        dgGroup.Refresh();
                //        refreshScreen();
                //    }
                //    else if (e.ColumnIndex == 3)
                //    {
                //        clearMouseOver();
                //        Common.periodGroups[Common.currentPeriod].periodGroupPlayers[e.RowIndex + 1].labelEnd.mouseIsOver = true;
                //        dgGroup[3, e.RowIndex].Style.BackColor = Color.LightGray;
                //        dgGroup.Refresh();
                //        refreshScreen();
                //    }
                //}
            }            
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                if(cmdReady.BackColor == Color.PaleGreen)
                {
                    cmdReady.BackColor = SystemColors.Control;
                    cmdSubmit.BackColor = SystemColors.Control;
                }
                else
                {
                    cmdReady.BackColor = Color.PaleGreen;
                    cmdSubmit.BackColor = Color.PaleGreen;
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            try
            {               

                if(Common.gamePhase== "submit" && Common.timeRemaining <= 0)
                {
                    if (Common.Frm1.cmdSubmit.Visible)
                    {
                        dgMoves.Columns[1].ReadOnly = true;
                        dgMoves.Columns[2].ReadOnly = true;

                        for (int i=1;i<=Common.movesPerTurn;i++)
                        {
                            dgMoves[1, i - 1].Value = lastMoveDirections[i - 1];
                            dgMoves[2, i - 1].Value = lastMoveDistances[i - 1];
                        }

                        Common.Frm1.cmdSubmitAction(true);
                    }

                    timer4.Enabled = false;
                }
                else if(Common.gamePhase == "results" && Common.timeRemaining <= 0)
                {
                    if (Common.Frm1.cmdReady.Visible)
                        Common.Frm1.cmdReady.PerformClick();

                    timer4.Enabled = false;
                }
                else
                {
                    Common.timeRemaining--;
                }

                Common.Frm1.txtTimeRemaining.Text = Common.timeConversion(Common.timeRemaining);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
