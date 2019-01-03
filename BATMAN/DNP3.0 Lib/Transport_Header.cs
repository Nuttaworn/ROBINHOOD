using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace DNP3Lib
{
    public class Transport_Header
    {
        private bool FIN;
        private bool FIR;
        private byte Sequence;
        private byte rawdata;

        public Transport_Header(byte TH)
        {
            this.rawdata = TH;
            String TH_temp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(TH)),8);
            // Transport Frame Detail
            // 1 byte |-FIN-||-FIR-||-------- Sequence -------|
            //        |1 bit||1 bit||          8 bit          |
            if (TH_temp[0] == '1') // FIN = 1
            {
                this.FIN = true;
            }
            if (TH_temp[1] == '1') // FIR = 1
            {
                this.FIR = true;
            }
            this.Sequence = (byte)(TH & 0x3F); // move Sequence
        }
        
        public String rawDataShow()
        {
            String str = null;
            str += typeConvert.fillZero(typeConvert.HextoString(rawdata),2).ToUpper();
            return str;
        }

        public String rawDataMonitor()
        {
            String str = null;
            str += typeConvert.fillZero(typeConvert.HextoString(rawdata), 2).ToUpper();
            return str;
        }

        public String decodeDetailMonitor()
        {
            String str = null;
            //TrnHdr: FIR=1 FIN=1 SEQ=0
            str += "TrnHdr: ";
            str += "FIR=" + Convert.ToByte(this.FIR)
                + " FIN=" + Convert.ToByte(this.FIN)
                + " SEQ=" + this.Sequence;
            return str;
        }

        public override String ToString()
        {
            string str = null;
            str += "//////////////////////////////// [ Transportation Header Frame ] ///////////////////////////" + Environment.NewLine;
            str += "FIN : " + Convert.ToByte(this.FIN).ToString() + Environment.NewLine
                + "FIR : " + Convert.ToByte(this.FIR).ToString() + Environment.NewLine
                + "Sequence : " + this.Sequence + Environment.NewLine;
            str += "//////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine;
            return str;
        }

        public byte ToRawData()
        {
            return this.rawdata;
        }
    }
}
