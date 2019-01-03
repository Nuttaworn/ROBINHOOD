using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace DNP3Lib
{
    public class ControlByte
    {
        private bool _DIR;
        public bool DIR
        {
            get { return _DIR; }
            set { _DIR = value; }
        }

        private bool _PRM;
        public bool PRM
        {
            get { return _PRM; }
            set { _PRM = value; }
        }
        private bool _FCB;
        public bool FCB
        {
            get { return _FCB; }
            set { _FCB = value; }
        }
        private bool _FCV;
        public bool FCV
        {
            get { return _FCV; }
            set { _FCV = value; }
        }
        private bool _RES;
        public bool RES
        {
            get { return _RES; }
            set { _RES = value; }
        }
        private bool _DFC;
        public bool DFC
        {
            get { return _DFC; }
            set { _DFC = value; }
        }

        private byte _f_code;
        internal byte f_code
        {
            get { return _f_code; }
            set { _f_code = value; }
        }
        internal byte control; // keep control byte data
        
        public ControlByte(byte control)
        {
            String con_temp = "";
            this.DIR = false;
            this.PRM = false;
            this.FCB = false;
            this.FCV = false;
            this.RES = false;
            this.DFC = false;
            this.f_code = 0;
            this.control = control;

            con_temp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(this.control)),8);
            if (con_temp[0] == '1') // DIR = 1
            {
                this.DIR = true;
            }
            if (con_temp[1] == '1') // PRM = 1
            {
                this.PRM = true;
                if (con_temp[2] == '1') // FCB = 1
                {
                    this.FCB = true;
                }
                if (con_temp[3] == '1') // FCV = 1
                {
                    this.FCV = true;
                }
            }
            else // PRM = 0
            {
                this.PRM = false;
                if (con_temp[2] == '1') // RES = 1
                {
                    this.RES = true;
                }
                if (con_temp[3] == '1') // DFC = 1
                {
                    this.DFC = true;
                }
            }
            this.f_code = (byte)(control & (0x0F)); // Function code 
        }

        public byte ToRawData()
        {
            byte control = 0;
            //if (this.DIR) control |= 0x80;
            control = this.control;
            return control;
        }
        
        public override String ToString()
        {
            string str = null;
            if (control < 16)
            {
                str += "Control : 0x0" + typeConvert.HextoString(control).ToUpper() 
                    + Environment.NewLine;
            }
            else
            {
                str += "Control : 0x" + typeConvert.HextoString(control).ToUpper() 
                    + Environment.NewLine;
            }
            str += "\tDIR : " + Convert.ToByte(this.DIR).ToString() + Environment.NewLine
                + "\tPRM : " + Convert.ToByte(this.PRM).ToString() + Environment.NewLine;
            if (PRM == true)
            {
                str += "\tFCB : " + Convert.ToByte(this.FCB).ToString() + Environment.NewLine
                + "\tFCV : " + Convert.ToByte(this.FCV).ToString() + Environment.NewLine;
            }
            else // PRM = 0
            {
                str += "\tRES : " + Convert.ToByte(this.RES).ToString() + Environment.NewLine
                + "\tDFC : " + Convert.ToByte(this.DFC).ToString() + Environment.NewLine;
            }
            str += "Function Code : " + this.f_code;
            return str;
        }
    }

    public class DataLink_Header
    {
        const byte byteSize = 8;
        //------ 10  byte DataLink Header
        public uint Start; // 2 byte always 0x0564
        public byte Length; // 1 byte
        
        // Control Detail
        // 1 byte |-DIR-||-PRM-||-FCM/RES-||-FCV/DFC-||---Function Code ---|
        //        |1 bit||1 bit||  1 bit  ||  1 bit  ||     4 bit          |
        private ControlByte _Control;
        public ControlByte Control
        {
            get { return _Control; }
            set { _Control = value; }
        }

        public uint Destination; // 2 byte Little Endian Type |-LSB-||-MSB-|
        public uint Source; // 2 byte Little Endian Type |-LSB-||-MSB-|
        public uint dlCRC; // 2 byte Little Endian Type |-LSB-||-MSB-|
        // raw data
        public byte[] rawdata;

        public DataLink_Header()
        {
        }

        public DataLink_Header(byte[] DLhead)
        {
            rawdata = DLhead; // copy raw data
            //---- start code is always 0x0564  -----/
            this.Start = 0x564;
            this.Length = DLhead[2];
            Control = new ControlByte(DLhead[3]);
            this.Destination = (uint)(DLhead[5] << 8) + DLhead[4];
            this.Source = (uint)(DLhead[7] << 8) + DLhead[6];
            this.dlCRC = (uint)(DLhead[9] << 8) + DLhead[8];    
        }

        public String rawDataShow()
        {
            String str = null;
            for (int i = 0; i < rawdata.Length; i++)
            {
                str += typeConvert.fillZero(typeConvert.HextoString(rawdata[i]),2).ToUpper() + " ";
            }
            return str;
        }
        
        public String rawDataMonitor()
        {
            String str = null;
            if (this.Control.DIR == true) str += "--> \t";
            else str += "<-- \t";
            str += "Packet: ";
            for (int i = 0; i < rawdata.Length; i++)
            {
                str += typeConvert.fillZero(typeConvert.HextoString(rawdata[i]), 2).ToUpper() + " ";
            }
            return str;
        }
        
        public String decodeDetailMonitor()
        {
            String str = null;
            //Src=1024($400) Dst=205($CD) Len=11 DIR=1 PRM=1 FCB=0 FCV=0
            //Unconfirmed user data (No reply expected)
            str += "DlkHdr: ";
            str += "Src="+ this.Source 
                +" Dst=" + this.Destination
                +" Len=" + this.Length
                +" DIR=" + Convert.ToByte(this.Control.DIR)
                +" PRM=" + Convert.ToByte(this.Control.PRM);
            if (this.Control.PRM == true)
            {
                str += " FCB=" + Convert.ToByte(this.Control.FCB)
                    + " FCV=" + Convert.ToByte(this.Control.FCV) + Environment.NewLine;
            }
            else
            {
                str += " RES=" + Convert.ToByte(this.Control.RES)
                    + " DFC=" + Convert.ToByte(this.Control.DFC) + Environment.NewLine;
            }
            str += "\t        " + this.getFunctionName();
            return str;
        }

        //@override
        public override String ToString()
        {
            String str = null;
            str += "//////////////////////////////// [ Datalink Header Frame ] ////////////////////////////////" + Environment.NewLine;
            str += "Start : 0x0" + typeConvert.HextoString(this.Start) + Environment.NewLine
                + "Length : " + this.Length + Environment.NewLine
                + this.Control.ToString() + Environment.NewLine
                + "Desitination :" + this.Destination + Environment.NewLine
                + "Source :" + this.Source + Environment.NewLine
                + "CRC : 0x" + typeConvert.fillZero(typeConvert.HextoString(this.dlCRC),4).ToUpper() + Environment.NewLine;
            str += "/////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine;
            return str;
        }

        public String EventLog()
        {
            String str = null;
            str += this.getFunctionName();
            return str;
        }

        public byte[] ToRawData()
        {
            // data link layer
            byte[] rawdata = new byte[8];
            rawdata[0] = 0x05;
            rawdata[1] = 0x64;
            rawdata[2] = this.Length;
            rawdata[3] = this.Control.ToRawData();
            byte[] Dest = typeConvert.DataToRaw(this.Destination, 2);
            rawdata[4] = Dest[0];
            rawdata[5] = Dest[1];
            byte[] Src = typeConvert.DataToRaw(this.Source, 2);
            rawdata[6] = Src[0];
            rawdata[7] = Src[1];
            return rawdata;
        }

        // Fucntion Code Name
        public String getFunctionName()
        {
            String Str = null;
            if (this.Control.PRM == true) // PRM = 1
            {
                switch (this.Control.f_code)
                {
                    case 0:
                        Str = "Reset of remote link"; //FCV = 0
                        break;
                    case 1:
                        Str = "Reset of user process"; //FCV = 0
                        break;
                    case 2:
                        Str = "Test function for link"; //FCV = 1
                        break;
                    case 3:
                        Str = "User Data"; //FCV = 1
                        break;
                    case 4:
                        Str = "Unconfirmed User Data"; //FCV = 0
                        break;
                    case 9:
                        Str = "REQUEST LINK STATUS"; //FCV = 0
                        break;
                    default :
                        Str = "Unknown function code";
                        break;
                }
            }
            else // PRM = 0
            {
                switch (this.Control.f_code)
                {
                    case 0:
                        Str = "ACK";
                        break;
                    case 1:
                        Str = "NACK";
                        break;
                    case 11:
                        Str = "Status of Link";
                        break;
                    default:
                        Str = "Unknown function code";
                        break;
                }
            }
            return Str;
        }
    }
}
