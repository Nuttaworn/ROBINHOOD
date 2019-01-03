using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DNP3Lib
{
    public class DNP_Frame
    {
        private DataLink_Header _dataLinkHeader;
        public DataLink_Header dataLinkHeader
        {
            get { return _dataLinkHeader; }
            set { _dataLinkHeader = value; }
        }
        
        private Transport_Header _transportHeader;
        public Transport_Header transportHeader
        {
            get { return _transportHeader; }
            set { _transportHeader = value; }
        }
        
        private Application_Header _applicationHeader;
        public Application_Header applicationHeader
        {
            get { return _applicationHeader; }
            set { _applicationHeader = value; }
        }
        byte[] rawdata;
        public DateTime dt = new DateTime();

        public DNP_Frame(DataLink_Header DL)
        {
            this.dataLinkHeader = DL;
        }

        public DNP_Frame(DataLink_Header DL, Transport_Header TH, Application_Header AP)
        {
            this.dataLinkHeader = DL;
            this.transportHeader = TH;
            this.applicationHeader = AP;
        }

        public DNP_Frame(byte[] rawdata)
        {
            dt = DateTime.Now;
            this.rawdata = rawdata;
            byte[] DLheader = new byte[10];
            for (int i = 0; i < 10;  i++) // rawdata has at least 10 bytes
            {
                DLheader[i] = rawdata[i];              
            }
            dataLinkHeader = new DataLink_Header(DLheader);
            int rawdataindex = 10;
            if (rawdata.Length > 10) // rawdata more than 10 bytes (DLH + TH + AH)
            {
                transportHeader = new Transport_Header(rawdata[10]);
                rawdataindex = 11;
                // Application_Layer Constructor Here
                byte[] APheader = new byte[rawdata.Length - 11];
                for (int i = 0; i < APheader.Length; i++)
                {
                    APheader[i] = rawdata[rawdataindex];
                    rawdataindex++;
                }
                applicationHeader = new Application_Header(this.dataLinkHeader, this.transportHeader, APheader);
            }
        }

        public DNP_Frame(byte [] rawdata,DataLink_Header DLH)
        {
            dt = DateTime.Now;
            this.rawdata = rawdata;
            this.dataLinkHeader = DLH;
            int rawdataindex = 10;

            if (rawdata.Length > 10) // rawdata more than 10 bytes (DLH + TH + AH)
            {
                transportHeader = new Transport_Header(rawdata[10]);
                rawdataindex = 11;
                // Application_Layer Constructor Here
                byte[] APheader = new byte[rawdata.Length - 11];
                for (int i = 0; i < APheader.Length; i++)
                {
                    APheader[i] = rawdata[rawdataindex];
                    rawdataindex++;
                }
                applicationHeader = new Application_Header(this.dataLinkHeader, this.transportHeader, APheader);
            }
            
        }

        public byte[] ToRawData()
        {
            byte[] DL = this.dataLinkHeader.ToRawData();
            if ((this.transportHeader != null) && (this.applicationHeader != null))
            {
                byte TH = this.transportHeader.ToRawData();
                byte [] AP = this.applicationHeader.ToRawData();
                // data link 
                DL[2] = (byte)(AP.Length + 6); // lenght of msg;
                byte[] tempcrc = CRC.genCRCtoRaw(DL);
                byte[] DLcrc = new byte[10];
                for (int i = 0; i < 8; i++)
                {
                    DLcrc[i] = DL[i];
                }
                DLcrc[8] = tempcrc[0];
                DLcrc[9] = tempcrc[1];
                // application + transport
                byte [] userdata = new byte[AP.Length+1];
                userdata[0] = TH;
                for (int i = 0 ; i < AP.Length ; i++)
                {
                    userdata[i+1] = AP[i];
                }
                byte [] data = CRC.putCRC(userdata);

                byte [] output = new byte[data.Length + 10];

                int outindex = 0;
                for (int i = 0; i < DLcrc.Length; i++)
                {
                    output[outindex] = DLcrc[i];
                    outindex++;
                }
                for (int i = 0; i < data.Length; i++)
                {
                    output[outindex] = data[i];
                    outindex++;
                }
                return output;
            }
            else
            {
                DL[2] = 5;
                byte[] tempcrc = CRC.genCRCtoRaw(DL);
                byte[] DLcrc = new byte[10];
                for (int i = 0; i < 8; i++)
                {
                    DLcrc[i] = DL[i];
                }
                DLcrc[8] = tempcrc[0];
                DLcrc[9] = tempcrc[1];
                return DLcrc;
            }
        }

        public String rawDataShow()
        {
            String str = null;
            str += "------------------------- raw data ------------------------- " + Environment.NewLine;
            str += this.dataLinkHeader.rawDataShow() + Environment.NewLine; // datalink raw data
            if (this.dataLinkHeader.Length > 5) // has Transport and Application Data
            {
                str += this.transportHeader.rawDataShow() + Environment.NewLine  // transport raw data
                    + this.applicationHeader.rawDataShow() + Environment.NewLine; //Applicaiton raw data
            }
            str += "-------------------------------------------------------------------";
            return str;
        }

        public override String ToString()
        {
            String str = null;
            str += this.dataLinkHeader.ToString() + Environment.NewLine; // datalink detail
            if (this.dataLinkHeader.Length > 5) // has Transport and Application Data
            {
                str += this.transportHeader.ToString() + Environment.NewLine //transport detail
                    + this.applicationHeader.ToString() + Environment.NewLine; //Application detail
            }
            return str;
        }

        public String rawDataMonitor()
        {
            String str = null;
            str += "** [" + this.dt.ToLongTimeString() + "." + this.dt.Millisecond + "] ********************************************************" + Environment.NewLine;
            str += this.dataLinkHeader.rawDataMonitor();
            if (this.dataLinkHeader.Length > 5) // has Transport and Application Data
            {   
                str += Environment.NewLine
                + "\t        " + this.transportHeader.rawDataMonitor() + Environment.NewLine
                + "\t        " + this.applicationHeader.rawDataMonitor();
            }
            return str;
        }

        public String decodeDetailMonitor()
        {
            String str = null;
            //str += this.rawDataMonitor() + Environment.NewLine;
            str += "\t" + this.dataLinkHeader.decodeDetailMonitor();
            if (this.dataLinkHeader.Length > 5) // has Transport and Application Data
            {   
                str += Environment.NewLine
                + "\t" + this.transportHeader.decodeDetailMonitor() + Environment.NewLine
                + "\t" + this.applicationHeader.decodeDetailMonitor();
            }
            return str;
        }

        public String eventLog()
        {
            String str = null;
            if (this.applicationHeader != null)
            {
                str += this.applicationHeader.EventLog() + " ";
            }
            str += this.dataLinkHeader.EventLog();
            return str;
        }
    }
}
