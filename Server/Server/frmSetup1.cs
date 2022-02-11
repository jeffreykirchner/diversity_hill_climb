using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class frmSetup1 : Form
    {
        public frmSetup1()
        {
            InitializeComponent();
        }

        private void frmSetup1_Load(object sender, EventArgs e)
        {
            try
            {
                txtNumberOfPlayers.Text = INI.getINI(Common.sfile, "gameSettings", "numberOfPlayers");
                txtNumberOfPeriods.Text = INI.getINI(Common.sfile, "gameSettings", "numberOfPeriods");
                txtPortNumber.Text = INI.getINI(Common.sfile, "gameSettings", "port");
                txtInstructionX.Text = INI.getINI(Common.sfile, "gameSettings", "instructionX");
                txtInstructionY.Text = INI.getINI(Common.sfile, "gameSettings", "instructionY");
                txtWindowX.Text = INI.getINI(Common.sfile, "gameSettings", "windowX");
                txtWindowY.Text = INI.getINI(Common.sfile, "gameSettings", "windowY");

                cbShowInstructions.Checked =bool.Parse(INI.getINI(Common.sfile, "gameSettings", "showInstructions"));
                cbTestMode.Checked = bool.Parse(INI.getINI(Common.sfile, "gameSettings", "testMode"));
                cbShowFullCircle.Checked = bool.Parse(INI.getINI(Common.sfile, "gameSettings", "showFullCircle"));
                                
                txtGroupSize.Text = INI.getINI(Common.sfile, "gameSettings", "groupSize");
                txtCirclePointCount.Text = INI.getINI(Common.sfile, "gameSettings", "circlePointCount");
                txtCircleControlPointLow.Text = INI.getINI(Common.sfile, "gameSettings", "circleControlPointLow");
                txtCircleControlPointHigh.Text = INI.getINI(Common.sfile, "gameSettings", "circleControlPointHigh");
                txtMinControlPointValue.Text = INI.getINI(Common.sfile, "gameSettings", "minControlPointValue");
                txtMaxControlPointValue.Text = INI.getINI(Common.sfile, "gameSettings", "maxControlPointValue");
                txtMovesPerTurn.Text = INI.getINI(Common.sfile, "gameSettings", "movesPerTurn");
                txtMaxTurnsPerPeriod.Text = INI.getINI(Common.sfile, "gameSettings", "maxTurnsPerPeriod");                
                txtMaxRoundsPerPeriod.Text = INI.getINI(Common.sfile, "gameSettings", "maxRoundsPerPeriod");

                txtPeriodLength.Text = INI.getINI(Common.sfile, "gameSettings", "periodLength");
                txtReadyToGoOnLength.Text = INI.getINI(Common.sfile, "gameSettings", "readyToGoOnLength");
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSaveAndClose_Click(object sender, EventArgs e)
        {
            try
            {

                INI.writeINI(Common.sfile, "gameSettings", "numberOfPlayers", txtNumberOfPlayers.Text);
                INI.writeINI(Common.sfile, "gameSettings", "numberOfPeriods", txtNumberOfPeriods.Text);
                INI.writeINI(Common.sfile, "gameSettings", "port", txtPortNumber.Text);
                INI.writeINI(Common.sfile, "gameSettings", "instructionX", txtInstructionX.Text);
                INI.writeINI(Common.sfile, "gameSettings", "instructionY", txtInstructionY.Text);
                INI.writeINI(Common.sfile, "gameSettings", "windowX", txtWindowX.Text);
                INI.writeINI(Common.sfile, "gameSettings", "windowY", txtWindowY.Text);

                INI.writeINI(Common.sfile, "gameSettings", "showInstructions", cbShowInstructions.Checked.ToString());
                INI.writeINI(Common.sfile, "gameSettings", "testMode", cbTestMode.Checked.ToString());
                INI.writeINI(Common.sfile, "gameSettings", "showFullCircle", cbShowFullCircle.Checked.ToString());

                INI.writeINI(Common.sfile, "gameSettings", "groupSize", txtGroupSize.Text);

                INI.writeINI(Common.sfile, "gameSettings", "circlePointCount", txtCirclePointCount.Text);
                INI.writeINI(Common.sfile, "gameSettings", "circleControlPointLow", txtCircleControlPointLow.Text);
                INI.writeINI(Common.sfile, "gameSettings", "circleControlPointHigh", txtCircleControlPointHigh.Text);
                INI.writeINI(Common.sfile, "gameSettings", "minControlPointValue", txtMinControlPointValue.Text);
                INI.writeINI(Common.sfile, "gameSettings", "maxControlPointValue", txtMaxControlPointValue.Text);
                INI.writeINI(Common.sfile, "gameSettings", "movesPerTurn", txtMovesPerTurn.Text);
                INI.writeINI(Common.sfile, "gameSettings", "maxTurnsPerPeriod", txtMaxTurnsPerPeriod.Text);                
                INI.writeINI(Common.sfile, "gameSettings", "maxRoundsPerPeriod", txtMaxRoundsPerPeriod.Text);

                INI.writeINI(Common.sfile, "gameSettings", "periodLength", txtPeriodLength.Text);
                INI.writeINI(Common.sfile, "gameSettings", "readyToGoOnLength", txtReadyToGoOnLength.Text);

                Common.loadParameters();
                this.Close();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
