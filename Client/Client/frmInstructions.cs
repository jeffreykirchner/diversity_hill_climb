using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class frmInstructions : Form
    {
       
        public int numberOfPages = 6;

        public bool startPressed = false;
        public bool[] pagesDone = new bool[101];
        public int currentInstruction = 1;


        public frmInstructions()
        {
            InitializeComponent();
        }

        private void frmInstructions_Load(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i <= numberOfPages; i++)
                {
                    pagesDone[i] =false;
                }

                startPressed = false;
                currentInstruction = 1;

                nextInstruction();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSubmitQuiz_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtQuiz.Text.Trim())) return;

                checkQuiz();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            try
            {
                cmdNext.Visible = true;

                //previous page of instructions
                if (currentInstruction == 1)
                    return;

                currentInstruction -= 1;

                if (currentInstruction == 1) cmdBack.Visible = false;

                nextInstruction();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            try
            {
                pagesDone[6] = true;

                cmdStart.Visible = false;
                startPressed = true;

                string outstr = "";

                Common.FrmClient.SC.sendMessage("FINSHED_INSTRUCTIONS", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            try
            {

                if (!pagesDone[currentInstruction])
                {
                    MessageBox.Show("Please take the requested action before continuing.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                cmdBack.Visible = true;

                if (currentInstruction == numberOfPages)
                    return;

                currentInstruction += 1;

                if (currentInstruction == numberOfPages & !startPressed)
                {
                    cmdStart.Visible = true;
                }

                if (currentInstruction == numberOfPages)
                {
                    cmdNext.Visible = false;
                }

                nextInstruction();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void nextInstruction()
        {
            try
            {
                string folder = "";

                RichTextBox1.LoadFile(Application.StartupPath + "\\instructions\\" + folder + "\\page" + currentInstruction + ".rtf");

                variables();

                RichTextBox1.SelectionStart = 1;
                RichTextBox1.ScrollToCaret();

                

                if (Common.currentPeriod == 0)
                {
                    if (!startPressed) Common.FrmClient.SC.sendMessage("INSTRUCTION_PAGE", currentInstruction + ";");
                    Text = "Instructions, Page " + currentInstruction + "/" + numberOfPages;
                }
                else
                {
                    Text = "Instructions";
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        void variables()
        {
            try
            {
                RepRTBfield2("minValue", Common.minControlPointValue.ToString());
                RepRTBfield2("maxValue", Common.maxControlPointValue.ToString());
                RepRTBfield2("circlePointCount", Common.circlePointCount.ToString());
                RepRTBfield2("moveCount", Common.movesPerTurn.ToString());
                RepRTBfield2("maxTurns", Common.maxTurnsPerPeriod.ToString());
                RepRTBfield2("groupSize", Common.groupSize.ToString());
                RepRTBfield2("roundCount", Common.maxRoundsPerPeriod.ToString());
                RepRTBfield2("periodLength", Common.periodLength.ToString());
                RepRTBfield2("readyToGoOnLength", Common.readyToGoOnLength.ToString());

                switch (currentInstruction)
                {
                    case 1:
                        if (!pagesDone[currentInstruction])
                        {
                            pagesDone[currentInstruction] = true;
                        }
                        break;
                    case 2:
                        if (!pagesDone[currentInstruction])
                        {
                            pagesDone[currentInstruction] = true;
                        }
                        break;
                    case 3:
                        if (!pagesDone[currentInstruction])
                        {
                            pagesDone[currentInstruction] = true;
                        }
                        break;
                    case 4:
                        if (!pagesDone[currentInstruction])
                        {
                            Common.Frm1.cmdSubmit.Visible = true;
                            Common.showProgramResults = true;
                        }
                        break;
                    case 5:
                        if (!pagesDone[currentInstruction])
                        {
                            //pagesDone[currentInstruction] = true;
                            this.Location = new System.Drawing.Point(10, Common.instructionY);
                            Common.Frm1.dgResults.Rows[0].Selected = true;
                            Common.Frm1.dgResults_selectionChangedAction(0);
                        }
                        break;
                    case 6:
                        if (!pagesDone[currentInstruction])
                        {
                            // pagesDone[currentInstruction] = true;
                            //Common.showProgramResults = false;
                            //Common.showFullCircle = false;
                            //Common.Frm1.Width = 1325;
                        }
                        break;
                    case 7:
                        if (!pagesDone[currentInstruction])
                        {
                           
                        }
                        break;
                    case 8:
                        if (!pagesDone[currentInstruction])
                        {

                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        bool RepRTBfield(string sField, string sValue)
        {
            try
            {
                //when the instructions are loaded into the rich text box control this function will
                //replace the variable place holders with variables.

                if (RichTextBox1.Find("#" + sField + "#") == -1)
                {
                    RichTextBox1.DeselectAll();
                    return false;
                }

                RichTextBox1.SelectedText = sValue;

                return true;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return false;
            }
        }

        public void RepRTBfield2(string sField, string sValue)
        {
            try
            {

                while ((RepRTBfield(sField, sValue)))
                {
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        int RepRTBfieldColor(string sField, Color c, int start)
        {
            try
            {
                //when the instructions are loaded into the rich text box control this function will
                //color the specified text the specified color

                if (RichTextBox1.Find(sField, start, RichTextBoxFinds.None) == -1)
                {
                    RichTextBox1.DeselectAll();
                    return 0;
                }

                RichTextBox1.SelectionColor = c;

                return RichTextBox1.SelectionStart;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }
        }

        void RepRTBfield2Color(string sField, Color c)
        {

            try
            {
                int start = (RepRTBfieldColor(sField, c, 1));

                bool go = false;

                if (start == 0)
                {
                    go = false;
                }
                else
                {
                    go = true;
                    start += 1;
                }

                while (go)
                {
                    start = (RepRTBfieldColor(sField, c, start));

                    if (start == 0)
                    {
                        go = false;
                    }
                    else
                    {
                        start += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //quiz functions
        public void checkQuiz()
        {
            try
            {
                pagesDone[currentInstruction] = true;
                nextInstruction();
                gbQuiz.Visible = false;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void txtQuiz_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AcceptButton = cmdSubmitQuiz;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
