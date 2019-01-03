using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using DNP3Lib;
using DNP3Lib.Encoder;
//using System.Threading.Timer;

namespace BATMAN
{
    public partial class Form1 : Form
    {
        const int serialDataReceivedIntervel = 100;    // timer off delay for receiving data from serialport
        const int waitResponseInterval = 5000;
        bool uploading = false;
        enum State : byte { masterAddress = 1, rtuAddress , preDelay , postDelay , resetLink};
        byte uploadState = (byte)State.masterAddress;
        private List<byte> inputBuffer = new List<byte>();
        byte[] dataRx = new byte[50];
      

        public Form1()
        {
            InitializeComponent();
            CRC.init_crcdnp_tab();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                toolStripComboBox1.Items.Add(s);
            }
            if (toolStripComboBox1.Items.Count > 0)
            {
                this.toolStripComboBox1.SelectedIndex = 0;
                this.serialPort1.PortName = toolStripComboBox1.SelectedItem.ToString();
            }

            this.toolStripStatusLabel1.Text = "";
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            //this.serialPort1.Close();
            toolStripComboBox1.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                toolStripComboBox1.Items.Add(s);
            }
        }

        private void toolStripComboBox1_DropDownClosed(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Items.Count > 0)
            {
                try
                {
                    this.serialPort1.PortName = toolStripComboBox1.SelectedItem.ToString();
                    //toolStripButton1.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Please select any port.");
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.serialPort1.IsOpen)
            {
                this.serialPort1.Close();
                //this.toolStripButton1.Text = "Connect";
                //this.toolStripStatusLabel1.Text = "Offline";
                //this.toolStripStatusLabel1.BackColor = Color.Red;
                this.toolStripComboBox1.Enabled = true;
            }
            else
            {
                try
                {
                    this.serialPort1.Open();
                    this.toolStripComboBox1.Enabled = false;
                    //this.toolStripButton1.Text = "Disconnect";
                    //this.toolStripStatusLabel1.Text = "Online";
                    //this.toolStripStatusLabel1.BackColor = Color.FromArgb(0, 255, 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //this.toolStripButton1.Text = "Connect";
                    if (toolStripComboBox1.Items.Count > 0)
                    {
                        this.toolStripComboBox1.SelectedIndex = 0;
                        this.serialPort1.PortName = toolStripComboBox1.SelectedItem.ToString();
                        this.toolStripComboBox1.Enabled = true;
                    }
                    //else toolStripButton1.Enabled = false;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show
            (" Are you really want to exit?", "Confirm Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (exit == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (e.KeyChar == (char)Keys.Enter)
            {
                //MessageBox.Show("xxx");
                //this.textBox1.c
                this.textBox1.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(this.textBox1.Text.ToString());
                if (a > 65535)
                {
                    textBox1.Text = "65535";
                }
            }
            catch
            {
                textBox1.Text = "";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(this.textBox2.Text.ToString());
                if (a > 65535)
                {
                    textBox2.Text = "65535";
                }
            }
            catch
            {
                textBox2.Text = "";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(this.textBox4.Text.ToString());
                if (a > 100)
                {
                    textBox4.Text = "100";
                }
            }
            catch
            {
                textBox4.Text = "";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(this.textBox3.Text.ToString());
                if (a > 100)
                {
                    textBox3.Text = "100";
                }
            }
            catch
            {
                textBox3.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Open Serial Port 
            //byte[] dataTx;
            //byte[] dataRx = new byte[50];

            uint masterAddress = Convert.ToUInt32(this.textBox1.Text.ToString());
            uint rtuAddress = Convert.ToUInt32(this.textBox2.Text.ToString());
            uint preDelay = Convert.ToUInt32(this.textBox3.Text.ToString());
            uint postDelay = Convert.ToUInt32(this.textBox4.Text.ToString());

            if (masterAddress == rtuAddress)
            {
                MessageBox.Show("Master Address can't be similar to RTU Address.", "Please Wait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                this.serialPort1.Open();
                this.button1.Enabled = false;
                this.button2.Enabled = false;
                this.toolStripComboBox1.Enabled = false;
                this.textBox1.Enabled = false;
                this.textBox2.Enabled = false;
                this.textBox3.Enabled = false;
                this.textBox4.Enabled = false;
                this.uploadState = (byte)State.masterAddress;
                //this.toolStripComboBox1.Enabled = false;
                //this.toolStripButton1.Text = "Disconnect";
                //this.toolStripStatusLabel1.Text = "Online";
                //this.toolStripStatusLabel1.BackColor = Color.FromArgb(0, 255, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


            
            //sendBCD
            try
            {
                this.sendBCD(1, masterAddress);
            }
            catch { }
            //start timer
            this.timer1.Interval = waitResponseInterval;
            this.timer1.Start();
            //progress bar
            toolStripStatusLabel1.Text = "Uploading ";
            toolStripStatusLabel2.Visible = true;
            toolStripStatusLabel2.Text = "Writing master address";
            this.toolStripProgressBar1.Visible = true;
            this.toolStripProgressBar1.Value = 13;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Stop();
            this.serialPort1.Close();
            this.button1.Enabled = true;
            this.button2.Enabled = true;
            this.toolStripComboBox1.Enabled = true;
            this.textBox1.Enabled = true;
            this.textBox2.Enabled = true;
            this.textBox3.Enabled = true;
            this.textBox4.Enabled = true;
            this.toolStripStatusLabel2.Text = "";
            this.toolStripProgressBar1.Value = 0;
            this.toolStripProgressBar1.Visible = false;
            if (uploadState == (byte)State.resetLink)
            {
                this.toolStripStatusLabel1.Text = "No Connection.";
                MessageBox.Show("There is no responsing from your device.", "No Responsing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.uploadState = (byte)State.masterAddress;
                return;
            }
            this.toolStripStatusLabel1.Text = "Failed.";
            MessageBox.Show("Please check your communication port.", "Uploading Failed",MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.uploadState = (byte)State.masterAddress;
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytes = serialPort1.BytesToRead;

            if (bytes <= 0)
            {
                return;
            }
            byte[] rawdata;
            rawdata = new byte[bytes];

            serialPort1.Read(rawdata, 0, bytes);
            // put data into the q and wait for scanning
            for (int i = 0; i < bytes; i++)
            {
                this.inputBuffer.Add(rawdata[i]);
            }

            this.Invoke(new EventHandler(delegate
            {
                this.timer2.Enabled = true;
                this.timer2.Interval = serialDataReceivedIntervel;
            }));
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.timer2.Enabled = false;
            this.dataRx = this.inputBuffer.ToArray(); 
            this.inputBuffer.Clear(); //clear Serial Port Rx Buffer

            //check start bit for DNP
            if ((dataRx[0] != 0x05) || (dataRx[1] != 0x64))
            {
                return;
            }

            //check CRC
            if (CRC.dnpCheckCRC(dataRx) != true) // drop package if dnp check if fail
            {
                return;
            }
            //check length again (useless checking)
            if(dataRx.Length < 10)
            {
                return;
            }

            if (dataRx.Length == 10)
            {
                //check acknowleage here
                DataLink_Header DLHeader = new DataLink_Header(dataRx);
                int masterAddress = Convert.ToInt32(this.textBox1.Text.ToString());
                int rtuAddress = Convert.ToInt32(this.textBox2.Text.ToString());

                //Acknowleage
                if ((DLHeader.Source == rtuAddress) && (DLHeader.Destination == masterAddress) && (DLHeader.Control.f_code == 0))
                {
                    this.timer1.Stop();
                    System.Threading.Thread.Sleep(200);
                    uploadState = (byte)State.masterAddress;
                    toolStripStatusLabel1.Text = "Connection : Success";

                    MessageBox.Show("Test Connection : Success" + Environment.NewLine
                        + "  Master Address = " + masterAddress + Environment.NewLine
                        + "  RTU Address = " + rtuAddress 
                        , "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    toolStripStatusLabel2.Text = "";
                    //this.timer1.Stop();
                    this.serialPort1.Close();
                    this.button1.Enabled = true;
                    this.button2.Enabled = true;
                    this.toolStripComboBox1.Enabled = true;
                    this.textBox1.Enabled = true;
                    this.textBox2.Enabled = true;
                    this.textBox3.Enabled = true;
                    this.textBox4.Enabled = true;
                }

                return;
            }

            DNP_Frame responseDNP = new DNP_Frame(dataRx);


            if (responseDNP.applicationHeader.FunctionCode == 129) //check response function
            {
                //progress bar
                try
                {
                    this.toolStripProgressBar1.Value += 12;
                }
                catch
                {
                    this.toolStripProgressBar1.Value = 100;
                }


                //next state
                if (uploadState == (byte)State.masterAddress)
                {
                    this.timer1.Stop();
                    uint rtuAddress = Convert.ToUInt32(this.textBox2.Text.ToString());
                    uploadState = (byte)State.rtuAddress;
                    //sendBCD
                    System.Threading.Thread.Sleep(200);
                    try
                    {
                        this.sendBCD(2, rtuAddress);
                    }
                    catch { }
                    //start timer
                    this.timer1.Interval = waitResponseInterval;
                    this.timer1.Start();
                    //progress bar
                    toolStripStatusLabel2.Text = "Writing RTU address";
                    this.toolStripProgressBar1.Value += 13;
                }
                else if (uploadState == (byte)State.rtuAddress)
                {
                    this.timer1.Stop();
                    uint preDelay = Convert.ToUInt32(this.textBox3.Text.ToString());
                    uploadState = (byte)State.preDelay;
                    //sendBCD
                    System.Threading.Thread.Sleep(200);
                    try
                    {
                        this.sendBCD(3, preDelay);
                    }
                    catch { }
                    //start timer
                    this.timer1.Interval = waitResponseInterval;
                    this.timer1.Start();
                    //progress bar
                    toolStripStatusLabel2.Text = "Writing pre-delay time";
                    this.toolStripProgressBar1.Value += 13;
                }
                else if (uploadState == (byte)State.preDelay)
                {
                    this.timer1.Stop();
                    uint postDelay = Convert.ToUInt32(this.textBox4.Text.ToString());
                    uploadState = (byte)State.postDelay;
                    //sendBCD
                    System.Threading.Thread.Sleep(200);
                    try
                    {
                        this.sendBCD(4, postDelay);
                    }
                    catch { }
                    //start timer
                    this.timer1.Interval = waitResponseInterval;
                    this.timer1.Start();
                    //progress bar
                    toolStripStatusLabel2.Text = "Writing post-delay time";
                    this.toolStripProgressBar1.Value += 13;
                }
                else if (uploadState == (byte)State.postDelay)
                {
                    this.timer1.Stop();
                    uploadState = (byte)State.masterAddress;
                    toolStripStatusLabel1.Text = "Complete";
                    toolStripStatusLabel2.Text = "";
                    //this.timer1.Stop();
                    this.serialPort1.Close();
                    this.button1.Enabled = true;
                    this.button2.Enabled = true;
                    this.toolStripComboBox1.Enabled = true;
                    this.textBox1.Enabled = true;
                    this.textBox2.Enabled = true;
                    this.textBox3.Enabled = true;
                    this.textBox4.Enabled = true;
                }
                return;
            }
        }

        private void sendBCD(uint address, uint value)
        {
            byte[] dataTx;
            ////////////// data link layer /////////////////////////////
            DataLink_Header DL = new DataLink_Header();
            DL.Control = new ControlByte(0xC4); // user data (unconfirm);
            DL.Destination = 65535;
            DL.Source = 1;

            ////////////// transport /////////////////////////////
            Transport_Header TH = new Transport_Header(0xc0); // transport alway 0xc4 for master

            ///////////// Application Header /////////////////////////////
            Application_Header AP = new Application_Header();
            // Application Control
            ApplicationControl AC = new ApplicationControl();
            AC.FIN = true;
            AC.FIR = true;
            AC.SEQ = 0;
            AP.ApplicationControl = AC;
            AP.FunctionCode = 2; // Write Function = 2

            //////////////////////////// Object ////////////////////////////////
            //----------------------- Object Field---------------------------//
            Object_Field[] OF = new Object_Field[1]; //have 1 object type

            // Object Header //
            Object_Header OH;
            Qualifier Q;
            ObjectData OD;

            // Object Header 1 O:60 V:2
            OH = new Object_Header();
            OH.ObjectGroup = 101; //Object 
            OH.ObjectVariation = 2; // Var
            Q = new Qualifier(0x17); // Qualifier Code = 0x17 fixed
            OH.Qualifier = Q;
            OH.Count = 1;           // have 1 data object
            OF[0] = new Object_Field();
            OF[0].ObjectHeader = OH;  // Object Header
            OD = new ObjectData(); //Data

            //Object Data
            OD.ObjData = new O101V2(value);
            OF[0].Obj = new ObjectData[1];
            OD.address = address;
            OF[0].Obj[0] = OD;
            // move to application header class
            AP.Object = OF;

            ////////////////////////// DNP /////////////////////////////
            DNP_Frame dnpFrame = new DNP_Frame(DL, TH, AP);

            dataTx = dnpFrame.ToRawData();
            this.serialPort1.Write(dataTx, 0, dataTx.Length);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int RTUAddress = Convert.ToInt32(this.textBox2.Text.ToString());
            int MasterAddress = Convert.ToInt32(this.textBox1.Text.ToString());
            
            // build data link layer
            byte[] rawdata = new byte[8];
            rawdata[0] = 0x05;
            rawdata[1] = 0x64;
            rawdata[2] = 0x05;
            rawdata[3] = 0xC0;
            byte[] Dest = typeConvert.DataToRaw(RTUAddress, 2);
            rawdata[4] = Dest[0];
            rawdata[5] = Dest[1];
            byte[] Src = typeConvert.DataToRaw(MasterAddress, 2);
            rawdata[6] = Src[0];
            rawdata[7] = Src[1];
            byte[] crc = CRC.genCRCtoRaw(rawdata);
            byte[] output = new byte[10];
            for (int i = 0; i < 8; i++)
            {
                output[i] = rawdata[i];
            }
            output[8] = crc[0];
            output[9] = crc[1];

            try
            {
                this.serialPort1.Open();
                this.button1.Enabled = false;
                this.button2.Enabled = false;
                this.toolStripComboBox1.Enabled = false;
                this.textBox1.Enabled = false;
                this.textBox2.Enabled = false;
                this.textBox3.Enabled = false;
                this.textBox4.Enabled = false;
                this.uploadState = (byte)State.resetLink;
                this.toolStripStatusLabel1.Text = "Testing Connection.";
                this.toolStripProgressBar1.Visible = false;
                this.serialPort1.Write(output, 0, output.Length);

                //start timer
                this.timer1.Interval = waitResponseInterval;
                this.timer1.Start();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutFrm = new AboutBox1();
            aboutFrm.ShowDialog();
        }

    }
}
