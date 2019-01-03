using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNP3Lib
{
    #region Object Library
    static class Object_Library
    {
        private const String noVar = "Unknown Var of this Object";

        public static String getName(byte Object,byte Var)
        {
            String Str = null;
            switch (Object)
            {
                case 1: //single bit input
                    if (Var == 1)
                    {
                        Str = "SINGLE-BIT BINARY INPUT";
                    }
                    else if (Var == 2)
                    {
                        Str = "BINARY INPUT WITH STATUS";
                    }
                    else if (Var == 0)
                    {
                        Str = "BINARY INPUT ANY VARIATION";
                    }
                    else
                    {
                        Str = noVar;
                    }
                    break;

                case 2: // digital input
                    if (Var == 1) 
                    {
                        Str = "BINARY INPUT CHANGE WITHOUT TIME";
                    }
                    else if (Var == 2) 
                    {
                        Str = "BINARY INPUT CHANGE WITH TIME";
                    }
                    else if (Var == 3)
                    {
                        Str = "BINARY INPUT CHANGE WITH RELATIVE TIME";
                    }
                    else // unknown Var
                    {
                        Str = noVar;
                    }
                    break;
                case 12: // output
                    if (Var == 1) 
                    {
                        Str = "CONTROL RELAY OUTPUT BLOCK";
                    }
                    else if (Var == 2)
                    {
                        Str = "PATTERN CONTROL BLOCK";
                    }
                    else if (Var == 3)
                    {
                        Str = "PATTERN MASK";
                    }
                    else
                    {
                        Str = noVar;
                    }
                    break;
                case 30:  // analog input
                    if (Var == 1)
                    {
                        Str = "32-BIT ANALOG INPUT";
                    }
                    else if (Var == 2)
                    {
                        Str = "16-BIT ANALOG INPUT";
                    }
                    else if (Var == 3)
                    {
                        Str = "32-BIT ANALOG INPUT WITHOUT FLAG";
                    }
                    else if (Var == 4)
                    {
                        Str = "16-BIT ANALOG INPUT WITHOUT FLAG";
                    }
                    else
                    {
                        Str = noVar;
                    }
                    break;
                case 32:  // analog change
                    if (Var == 1)
                    {
                        Str = "32-BIT ANALOG CHANGE EVENT WITHOUT TIME";
                    }
                    else if (Var == 2) 
                    {
                        Str = "16-BIT ANALOG CHANGE EVENT WITHOUT TIME";
                    }
                    else if (Var == 3) 
                    {
                        Str = "32-BIT ANALOG CHANGE EVENT WITH TIME";
                    }
                    else if (Var == 4)
                    {
                        Str = "16-BIT ANALOG CHANGE EVENT WITH TIME";
                    }
                    else
                    {
                        Str = noVar;
                    }
                    break;
                case 50: // Time and Date
                    if (Var == 1)
                    {
                        Str = "TIME AND DATE";
                    }
                    else if (Var == 2)
                    {
                        Str = "TIME AND DATE WITH INTERVAL";
                    }
                    else
                    {
                        Str = noVar;
                    }
                    break;
                case 51: //TIME AND DATE CTO
                    if (Var == 1)
                    {
                        Str = "TIME AND DATE CTO";
                    }
                    else if (Var == 2)
                    {
                        Str = "UN-SYNCHRONIZED TIME AND DATE CTO";
                    }
                    else
                    {
                        Str = noVar;
                    }
                    break;
                case 52: // Time Delay
                    if (Var == 1)
                    {
                        Str = "TIME DELAY COARSE";
                    }
                    else if (Var == 2)
                    {
                        Str = "TIME DELAY FINE";
                    }
                    else
                    {
                        Str = noVar;
                    }
                    break;
                case 60: // Class Data
                    if (Var == 1) // Class 0
                    {
                        Str = "CLASS 0 DATA";
                    }
                    else if (Var == 2) // Class 1
                    {
                        Str = "CLASS 1 DATA";
                    }
                    else if (Var == 3) // Class 2
                    {
                        Str = "CLASS 2 DATA";
                    }
                    else if (Var == 4) // Class 3
                    {
                        Str = "CLASS 3 DATA";
                    }
                    else
                    {
                        Str = noVar;
                    }
                    break;
                case 80: // INTERNAL INDICATIONS
                    if (Var == 1) // Internal Indications
                    {
                        Str = "INTERNAL INDICATIONS";
                    }
                    else
                    {
                        Str = noVar;
                    }
                    break;
                default: Str = "Unknown Object"; // not find in this lib
                    break;
            }
            return Str;
        }

        public static int getSize(byte Object, byte Var)
        {
            int size = 0;
            switch (Object)
            {
                case 1: //single bit input
                    if (Var == 1)
                    {
                        size = 0x81; // 1 - bit
                    }
                    else if (Var == 2)
                    {
                        size = 1;
                    }
                    else if (Var == 0)
                    {
                        size = 0;
                    }
                    else
                    {
                        size = 255;  //size = 255 is error code;
                    }
                    break;

                case 2: // digital input
                    if (Var == 1) 
                    {
                        size = 1;
                    }
                    else if (Var == 2) 
                    {
                        size = 7;
                    }
                    else if (Var == 3)
                    {
                        size = 3;
                    }
                    else // unknown Var
                    {
                        size = 255;  //size = 255 is error code;
                    }
                    break;
                case 12: // output
                    if (Var == 1)
                    {
                        size = 11;
                    }
                    else if (Var == 2)
                    {
                        size = 11;
                    }
                    else if (Var == 3)
                    {
                        size = 0x81; // 1 - bit 
                    }
                    else
                    {
                        size = 255;  //size = 255 is error code;
                    }
                    break;
                case 30:  // analog input
                    if (Var == 1)
                    {
                        size = 5;
                    }
                    else if (Var == 2)
                    {
                        size = 3;
                    }
                    else if (Var == 3)
                    {
                        size = 4;
                    }
                    else if (Var == 4)
                    {
                        size = 2;
                    }
                    else
                    {
                        size = 255;  //size = 255 is error code;
                    }
                    break;
                case 32:  // analog change
                    if (Var == 1)
                    {
                        size = 5;
                    }
                    else if (Var == 2) 
                    {
                        size = 3;
                    }
                    else if (Var == 3) 
                    {
                        size = 11;
                    }
                    else if (Var == 4)
                    {
                        size = 9;
                    }
                    else
                    {
                        size = 255;  //size = 255 is error code;
                    }
                    break;
                case 50: // Time and Date
                    if (Var == 1)
                    {
                        size = 6;
                    }
                    else if (Var == 2)
                    {
                        size = 10;
                    }
                    else
                    {
                        size = 255; //size = 255 is error code;
                    }
                    break;
                case 51: //TIME AND DATE CTO
                    if (Var == 1)
                    {
                        size = 6;
                    }
                    else if (Var == 2)
                    {
                        size = 6;
                    }
                    else
                    {
                        size = 255; //size = 255 is error code;
                    }
                    break;
                case 52: // Time Delay
                    if (Var == 1)
                    {
                        size = 2;
                    }
                    else if (Var == 2)
                    {
                        size = 2;
                    }
                    else
                    {
                        size = 255; //size = 255 is error code;
                    }
                    break;
                case 60: // Class Data
                    if (Var == 1) // Class 0
                    {
                        size = 0; // object has no data
                    }
                    else if (Var == 2) // Class 1
                    {
                        size = 0; // object has no data
                    }
                    else if (Var == 3) // Class 2
                    {
                        size = 0; // object has no data
                    }
                    else if (Var == 4) // Class 3
                    {
                        size = 0; // object has no data
                    }
                    else
                    {
                        size = 255;  //size = 255 is error code;
                    }
                    break;
                case 80: // INTERNAL INDICATIONS
                    if (Var == 1) // Internal Indications
                    {
                        size = 0x81; // 1-bit
                    }
                    else
                    {
                        size = 255; //size = 255 is error code;
                    }
                    break;
                case 101: // INTERNAL INDICATIONS
                    if (Var == 2) // Internal Indications
                    {
                        size = 4; // 1-bit
                    }
                    else
                    {
                        size = 255; //size = 255 is error code;
                    }
                    break;
                default: size = 255;   // not find in this lib
                    break;
            }
            return size;
        }

        public static ObjectDataLibrary getObjectType(byte Object, byte Var, byte[] data)
        {
            ObjectDataLibrary ObjData;
            switch (Object)
            {
                case 1: //single bit input
                    if (Var == 1)
                    {
                        ObjData = new O1V1(data);
                    }
                    else if (Var == 2)
                    {
                        ObjData = new O1V2(data);
                    }
                    else if (Var == 0)
                    {
                        ObjData = null;
                    }
                    else
                    {
                        ObjData = null;
                    }
                    break;

                case 2: // digital input
                    if (Var == 1)
                    {
                        ObjData = new O2V1(data);
                    }
                    else if (Var == 2)
                    {
                        ObjData = new O2V2(data);
                    }
                    else if (Var == 3)
                    {
                        ObjData = new O2V3(data);
                    }
                    else // unknown Var
                    {
                        ObjData = null;
                    }
                    break;
                case 12: // output
                    if (Var == 1)
                    {
                        ObjData = new O12V1(data);
                    }
                    else if (Var == 2)
                    {
                        ObjData = new O12V2(data);
                    }
                    else if (Var == 3)
                    {
                        ObjData = new O12V3(data);
                    }
                    else
                    {
                        ObjData = null;
                    }
                    break;
                case 30:  // analog input
                    if (Var == 1)
                    {
                        ObjData = new O30V1(data);
                    }
                    else if (Var == 2)
                    {
                        ObjData = new O30V2(data);
                    }
                    else if (Var == 3)
                    {
                        ObjData = new O30V3(data);
                    }
                    else if (Var == 4)
                    {
                        ObjData = new O30V4(data);
                    }
                    else
                    {
                        ObjData = null;
                    }
                    break;
                case 32:  // analog change
                    if (Var == 1)
                    {
                        ObjData = new O32V1(data);
                    }
                    else if (Var == 2)
                    {
                        ObjData = new O32V2(data);
                    }
                    else if (Var == 3)
                    {
                        ObjData = new O32V3(data);
                    }
                    else if (Var == 4)
                    {
                        ObjData = new O32V4(data);
                    }
                    else
                    {
                        ObjData = null;
                    }
                    break;
                case 50: // Time and Date
                    if (Var == 1)
                    {
                        ObjData = new O50V1(data);
                    }
                    else if (Var == 2)
                    {
                        ObjData = new O50V2(data);
                    }
                    else
                    {
                        ObjData = null;
                    }
                    break;
                case 51: // Time and Date CTO
                    if (Var == 1)
                    {
                        ObjData = new O51V1(data);
                    }
                    else if (Var == 2)
                    {
                        ObjData = new O51V2(data);
                    }
                    else
                    {
                        ObjData = null;
                    }
                    break;
                case 52: // Time Delay
                    if (Var == 1)
                    {
                        ObjData = new O52V1(data);
                    }
                    else if (Var == 2)
                    {
                        ObjData = new O52V2(data);
                    }
                    else
                    {
                        ObjData = null;
                    }
                    break;
                case 60: // Class Data
                    if (Var == 1) // Class 0
                    {
                        ObjData = null;
                    }
                    else if (Var == 2) // Class 1
                    {
                        ObjData = null; // object has no data
                    }
                    else if (Var == 3) // Class 2
                    {
                        ObjData = null; // object has no data
                    }
                    else if (Var == 4) // Class 3
                    {
                        ObjData = null; // object has no data
                    }
                    else
                    {
                        ObjData = null;  //size = 255 is error code;
                    }
                    break;
                case 80: // INTERNAL INDICATIONS
                    if (Var == 1) // Internal Indications
                    {
                        ObjData = new O80V1(data); // 1-bit
                    }
                    else
                    {
                        ObjData = null;
                    }
                    break;
                case 101:
                    if (Var == 2)
                    {
                        ObjData = new O101V2(data);
                    }
                    else
                    {
                        ObjData = null;
                    }
                    break;
                default: ObjData = null;   // not find in this lib
                    break;
            }
            return ObjData;
        }
    }
    #endregion

    #region Object Library Class
    public interface ObjectDataLibrary
    {
        String toString();
        String decodeDetailMonitor(ObjectData OD);
        byte[] ToRawData();
    }

    public class O1V1 : ObjectDataLibrary //digit input w/o flag
    {
        public bool State;
        public O1V1(bool state)
        {
            this.State = state;
        }
        public O1V1(byte data,byte digit)
        {
            String temp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(data)), 8);
            if (temp[digit] == '1')
            {
                this.State = true;
            }
            else this.State = false;
        }
        public O1V1(byte[] data)
        {
            if (data[0] == 1)
            {
                this.State = true;
            }
            else
            {
                this.State = false;
            }
        }
        public String toString()
        {
            string Str = null;
            Str += "State = " + Convert.ToByte(this.State).ToString();
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            uint tempaddress;
            tempaddress = OD.address;
            str += "DI " + tempaddress + " = " + Convert.ToByte(this.State); 
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    public class O1V2 : ObjectDataLibrary //digital input with flag
    {
        public byte Data;
        public bool Online;
        public bool Restart;
        public bool Comunication_lost;
        public bool Remote_Forced_Data;
        public bool Local_Forced_Data;
        public bool Chatter_Filter;
        public bool State;
        int flagCount = 0; // for check if flag is not 0
        public O1V2(byte data)
        {
            this.Data = data;
            String temp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(this.Data)), 8);
            if (temp[0] == '1')
            {
                this.State = true;
            }
            if (temp[2] == '1')
            {
                this.Chatter_Filter = true;
                this.flagCount++;
            }
            if (temp[3] == '1')
            {
                this.Local_Forced_Data = true;
                this.flagCount++;
            }
            if (temp[4] == '1')
            {
                this.Remote_Forced_Data = true;
                this.flagCount++;
            }
            if (temp[5] == '1')
            {
                this.Comunication_lost = true;
                this.flagCount++;
            }
            if (temp[6] == '1')
            {
                this.Restart = true;
                this.flagCount++;
            }
            if (temp[7] == '1')
            {
                this.Online = true;
                this.flagCount++;
            }
        }

        public O1V2(byte [] data)
        {
            this.Data = data[0];
            String temp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(this.Data)), 8);
            if (temp[0] == '1')
            {
                this.State = true;
            }
            if (temp[2] == '1')
            {
                this.Chatter_Filter = true;
                this.flagCount++;
            }
            if (temp[3] == '1')
            {
                this.Local_Forced_Data = true;
                this.flagCount++;
            }
            if (temp[4] == '1')
            {
                this.Remote_Forced_Data = true;
                this.flagCount++;
            }
            if (temp[5] == '1')
            {
                this.Comunication_lost = true;
                this.flagCount++;
            }
            if (temp[6] == '1')
            {
                this.Restart = true;
                this.flagCount++;
            }
            if (temp[7] == '1')
            {
                this.Online = true;
                this.flagCount++;
            }
        }
        public String toString()
        {
            string Str = null;
            if (this.Data < 16)
            {
                Str += "Object Data : 0x0" + typeConvert.HextoString(this.Data).ToUpper()
                    + Environment.NewLine;
            }
            else
            {
                Str += "Object Data : 0x" + typeConvert.HextoString(this.Data).ToUpper()
                    + Environment.NewLine;
            }
            Str += "State = " + Convert.ToByte(this.State).ToString();
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            uint tempaddress;
            tempaddress = OD.address;
            str += "DI " + tempaddress + " = " + Convert.ToByte(this.State);
            if (flagCount > 0) // has flag 
            {
                str += " [";
                if (this.Online)
                {
                    str += "Online";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Restart)
                {
                    str += "Restart";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Comunication_lost)
                {
                    str += "Communication lost";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Remote_Forced_Data)
                {
                    str += "Remote forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Local_Forced_Data)
                {
                    str += "Local forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Chatter_Filter)
                {
                    str += "Chatter Filter";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                str += "]";
            }
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    //Object 2  digit change 
    public class O2V1 : ObjectDataLibrary //w/o time
    {
        byte Data;
        bool Online;
        bool Restart;
        bool Comunication_lost;
        bool Remote_Forced_Data;
        bool Local_Forced_Data;
        bool Chatter_Filter;
        int flagCount = 0;
        public bool State;

        public O2V1(byte data)
        {
            this.Data = data;
            String temp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(this.Data)), 8);
            if (temp[0] == '1')
            {
                this.State = true;
            }
            if (temp[2] == '1')
            {
                this.Chatter_Filter = true;
                flagCount++;
            }
            if (temp[3] == '1')
            {
                this.Local_Forced_Data = true;
                flagCount++;
            }
            if (temp[4] == '1')
            {
                this.Remote_Forced_Data = true;
                flagCount++;
            }
            if (temp[5] == '1')
            {
                this.Comunication_lost = true;
                flagCount++;
            }
            if (temp[6] == '1')
            {
                this.Restart = true;
                flagCount++;
            }
            if (temp[7] == '1')
            {
                this.Online = true;
                flagCount++;
            }
        }
        public O2V1(byte [] data)
        {
            this.Data = data[0];
            String temp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(this.Data)), 8);
            if (temp[0] == '1')
            {
                this.State = true;
            }
            if (temp[2] == '1')
            {
                this.Chatter_Filter = true;
                flagCount++;
            }
            if (temp[3] == '1')
            {
                this.Local_Forced_Data = true;
                flagCount++;
            }
            if (temp[4] == '1')
            {
                this.Remote_Forced_Data = true;
                flagCount++;
            }
            if (temp[5] == '1')
            {
                this.Comunication_lost = true;
                flagCount++;
            }
            if (temp[6] == '1')
            {
                this.Restart = true;
                flagCount++;
            }
            if (temp[7] == '1')
            {
                this.Online = true;
                flagCount++;
            }
        }

        public String toString()
        {
            string Str = null;
            if (this.Data < 16)
            {
                Str += "Object Data : 0x0" + typeConvert.HextoString(this.Data).ToUpper()
                    + Environment.NewLine;
            }
            else
            {
                Str += "Object Data : 0x" + typeConvert.HextoString(this.Data).ToUpper()
                    + Environment.NewLine;
            }
            Str += "State = " + Convert.ToByte(this.State).ToString();
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            uint tempaddress;
            tempaddress = OD.address;
            str += "DI " + tempaddress + " = " + Convert.ToByte(this.State);
            if (flagCount > 0) // has flag 
            {
                str += " [";
                if (this.Online)
                {
                    str += "Online";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Restart)
                {
                    str += "Restart";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Comunication_lost)
                {
                    str += "Communication lost";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Remote_Forced_Data)
                {
                    str += "Remote forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Local_Forced_Data)
                {
                    str += "Local forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Chatter_Filter)
                {
                    str += "Chatter Filter";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                str += "]";
            }
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }

    }

    public class O2V2 : ObjectDataLibrary //digital input with time
    {
        byte [] Data;
        bool Online;
        bool Restart;
        bool Comunication_lost;
        bool Remote_Forced_Data;
        bool Local_Forced_Data;
        bool Chatter_Filter;
        public bool State;
        int flagCount;
        public UInt64 Time;
        public TimeCalculator dateTime;
        public DateTime realTime;

        public O2V2(byte [] data)
        {
            this.Data = data;

            //Status
            String temp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(this.Data[0])), 8);
            if (temp[0] == '1')
            {
                this.State = true;
            }
            if (temp[2] == '1')
            {
                this.Chatter_Filter = true;
                flagCount++;
            }
            if (temp[3] == '1')
            {
                this.Local_Forced_Data = true;
                flagCount++;
            }
            if (temp[4] == '1')
            {
                this.Remote_Forced_Data = true;
                flagCount++;
            }
            if (temp[5] == '1')
            {
                this.Comunication_lost = true;
                flagCount++;
            }
            if (temp[6] == '1')
            {
                this.Restart = true;
                flagCount++;
            }
            if (temp[7] == '1')
            {
                this.Online = true;
                flagCount++;
            }

            //Time
            for (int j = 0; j < 6; j++) // 6 loops for combine time data
            {
                this.Time += (UInt64)this.Data[j + 1] * (UInt64) Math.Pow(0x100,j);
            }
            dateTime = new TimeCalculator(this.Time);
            //create date time 2008-03-09 16:05:07.123
            this.realTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Min, dateTime.Sec, dateTime.mSec);
        }

        public String toString()
        {
            string Str = null;
            if (this.Data[0] < 16)
            {
                Str += "Object Data : 0x0" + typeConvert.HextoString(this.Data[0]).ToUpper()
                    + Environment.NewLine;
            }
            else
            {
                Str += "Object Data : 0x" + typeConvert.HextoString(this.Data[0]).ToUpper()
                    + Environment.NewLine;
            }
            Str += "State = " + Convert.ToByte(this.State).ToString() + Environment.NewLine;
            Str += "Time = " + this.Time;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            uint tempaddress;
            tempaddress = OD.address;
            str += "DI " + tempaddress + " = " + Convert.ToByte(this.State);
            if (flagCount > 0) // has flag 
            {
                str += " [";
                if (this.Online)
                {
                    str += "Online";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Restart)
                {
                    str += "Restart";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Comunication_lost)
                {
                    str += "Communication lost";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Remote_Forced_Data)
                {
                    str += "Remote forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Local_Forced_Data)
                {
                    str += "Local forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Chatter_Filter)
                {
                    str += "Chatter Filter";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                str += "]";
            }

            str += " "  + this.dateTime.ToString();
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    public class O2V3 : ObjectDataLibrary //digital input with relative time
    {
        byte[] Data;
        bool Online;
        bool Restart;
        bool Comunication_lost;
        bool Remote_Forced_Data;
        bool Local_Forced_Data;
        bool Chatter_Filter;
        public bool State;
        int flagCount;
        UInt64 Time;
        public O2V3(byte[] data)
        {
            this.Data = data;

            //Status
            String temp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(this.Data[0])), 8);
            if (temp[0] == '1')
            {
                this.State = true;
            }
            if (temp[2] == '1')
            {
                this.Chatter_Filter = true;
                flagCount++;
            }
            if (temp[3] == '1')
            {
                this.Local_Forced_Data = true;
                flagCount++;
            }
            if (temp[4] == '1')
            {
                this.Remote_Forced_Data = true;
                flagCount++;
            }
            if (temp[5] == '1')
            {
                this.Comunication_lost = true;
                flagCount++;
            }
            if (temp[6] == '1')
            {
                this.Restart = true;
                flagCount++;
            }
            if (temp[7] == '1')
            {
                this.Online = true;
                flagCount++;
            }

            //Time
            for (int j = 0; j < 2; j++) // 2 loops for combine time data
            {
                this.Time += (UInt64)this.Data[j + 1] * (UInt64)Math.Pow(0x100, j);
            }
        }

        public String toString()
        {
            string Str = null;
            if (this.Data[0] < 16)
            {
                Str += "Object Data : 0x0" + typeConvert.HextoString(this.Data[0]).ToUpper()
                    + Environment.NewLine;
            }
            else
            {
                Str += "Object Data : 0x" + typeConvert.HextoString(this.Data[0]).ToUpper()
                    + Environment.NewLine;
            }
            Str += "State = " + Convert.ToByte(this.State).ToString() + Environment.NewLine;
            Str += "Time = " + this.Time;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            uint tempaddress;
            tempaddress = OD.address;
            str += "DI " + tempaddress + " = " + Convert.ToByte(this.State);
            if (flagCount > 0) // has flag 
            {
                str += " [";
                if (this.Online)
                {
                    str += "Online";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Restart)
                {
                    str += "Restart";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Comunication_lost)
                {
                    str += "Communication lost";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Remote_Forced_Data)
                {
                    str += "Remote forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Local_Forced_Data)
                {
                    str += "Local forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Chatter_Filter)
                {
                    str += "Chatter Filter";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                str += "]";
            }
            str += "[relative time:" + this.Time + "]";
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    // Object 12 CONTROL RELAY OUTPUT BLOCK
    public class O12V1 : ObjectDataLibrary // CONTROL RELAY OUTPUT BLOCK
    {
        byte[] Data;
        public byte ControlCode;
        public byte Count;
        public uint OnTime;
        public uint OffTime;
        public byte Status;
        //Control Code
        public byte Code;
        public bool Queue;
        public bool Clear;
        public bool Close;    // 01 Close,10 Trip,00 NUL
        public bool Trip;

        public O12V1()
        {
        }

        public O12V1(byte[] data)
        {
            this.Data = data;

            //Status
            String temp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(this.Data[0])), 8);
            if (temp[0] == '1')
            {
                this.Trip = true;
            }
            if (temp[1] == '1')
            {
                this.Close = true;
            }
            if (temp[2] == '1')
            {
                this.Clear = true;
            }
            if (temp[3] == '1')
            {
                this.Queue = true;
            }
            this.Code = (byte)(this.Data[0] & (0x0F)); // Function code 
            // Count
            this.Count = this.Data[1];
            byte index = 2;
            //On Time
            for (int j = 0; j < 4; j++) // 32 bit
            {
                this.OnTime += ((uint)this.Data[index] * (uint)Math.Pow(0x100, j));
                index++;
            }
            //Off Time
            for (int j = 0; j < 4; j++) // 32 bit
            {
                this.OffTime += ((uint)this.Data[index] * (uint)Math.Pow(0x100, j));
                index++;
            }
            this.Status = this.Data[index];
        }

        public String toString()
        {
            string Str = null;
            if (this.ControlCode < 16)
            {
                Str += "Control Code : 0x0" + typeConvert.HextoString(this.ControlCode).ToUpper()
                    + Environment.NewLine;
            }
            else
            {
                Str += "Control Code : 0x" + typeConvert.HextoString(this.ControlCode).ToUpper()
                    + Environment.NewLine;
            }
            Str += "Close = " + Convert.ToByte(this.Close).ToString() + Environment.NewLine;
            Str += "Trip = " + Convert.ToByte(this.Trip).ToString() + Environment.NewLine;
            Str += "On-Time = " + this.OnTime + Environment.NewLine;
            Str += "Off-Time = " + this.OffTime + Environment.NewLine;
            if (this.Status < 16)
            {
                Str += "Cobtrol Code : 0x0" + typeConvert.HextoString(this.Status).ToUpper();
            }
            else
            {
                Str += "Control Code : 0x" + typeConvert.HextoString(this.Status).ToUpper();
            }
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            uint tempaddress;
            tempaddress = OD.address;
            str += "DO " + tempaddress + " ";
            // close trip
            if (this.Trip && !this.Close)
            {
                str += ",Trip";
            }
            else if (!this.Trip && this.Close)
            {
                str += ",Close";
            }
            else
            {
                str += ",NULL";
            }
            // code
            switch (this.Code)
            {
                case 0: str += ",NUL Operation";
                    break;
                case 1: str += ",Pulse On";
                    break;
                case 2: str += ",Pulse Off";
                    break;
                case 3: str += ",Latch On";
                    break;
                case 4: str += ",Latch Off";
                    break;
                default: str += ",Undefined";
                    break;
            }

            str += ",Count " + this.Count;
            str += ",On/Off Time " + this.OnTime + "-" + this.OffTime + " msecs";
            switch (this.Status)
            {
                case 0: str += ",[Accepted]";
                    break;
                case 1: str += ",[Timeout]";
                    break;
                case 2: str += ",[No Select]";
                    break;
                case 3: str += ",[Format Error]";
                    break;
                case 4: str += ",[Not Supported]";
                    break;
                case 5: str += ",[Point Active]";
                    break;
                case 6: str += ",[Hardware Error]";
                    break;
                case 7: str += ",[Local]";
                    break;
                default : str += ",[Undefined]";
                    break;
            }
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            byte Control = 0;
            if (this.Trip) Control |= 0x80;
            if (this.Close) Control |= 0x40;
            if (this.Clear) Control |= 0x20;
            if (this.Queue) Control |= 0x10;
            Control += this.Code;
            data.Add(Control);
            data.Add(this.Count);
            byte[] ontime = typeConvert.DataToRaw(this.OnTime, 4);
            for (int i = 0; i < ontime.Length; i++)
            {
                data.Add(ontime[i]);
            }
            byte[] offtime = typeConvert.DataToRaw(this.OffTime, 4);
            for (int i = 0; i < offtime.Length; i++)
            {
                data.Add(offtime[i]);
            }
            data.Add(this.Status);
            return data.ToArray();
        }
    }

    public class O12V2 : ObjectDataLibrary // PATTERN CONTROL BLOCK
    {
        byte[] Data;
        byte ControlCode;
        byte Count;
        uint OnTime;
        uint OffTime;
        byte Status;
        //Control Code
        byte Code;
        bool Queue;
        bool Clear;
        bool Close;    // 01 Close,10 Trip,00 NUL
        bool Trip;


        public O12V2(byte[] data)
        {
            this.Data = data;

            //Status
            String temp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(this.Data[0])), 8);
            if (temp[0] == '1')
            {
                this.Trip = true;
            }
            if (temp[1] == '1')
            {
                this.Close = true;
            }
            if (temp[2] == '1')
            {
                this.Clear = true;
            }
            if (temp[3] == '1')
            {
                this.Queue = true;
            }
            this.Code = (byte)(this.Data[0] & (0x0F)); // Function code 
            // Count
            this.Count = this.Data[1];
            byte index = 2;
            //On Time
            for (int j = 0; j < 4; j++) // 6 loops for combine time data
            {
                this.OnTime += (uint)this.Data[index] * (uint)Math.Pow(0x100, j);
                index++;
            }
            //Off Time
            for (int j = 0; j < 4; j++) // 
            {
                this.OffTime += (uint)this.Data[index] * (uint)Math.Pow(0x100, j);
                index++;
            }
            this.Status = this.Data[index];
        }

        public String toString()
        {
            string Str = null;
            if (this.ControlCode < 16)
            {
                Str += "Control Code : 0x0" + typeConvert.HextoString(this.ControlCode).ToUpper()
                    + Environment.NewLine;
            }
            else
            {
                Str += "Control Code : 0x" + typeConvert.HextoString(this.ControlCode).ToUpper()
                    + Environment.NewLine;
            }
            Str += "Close = " + Convert.ToByte(this.Close).ToString() + Environment.NewLine;
            Str += "Trip = " + Convert.ToByte(this.Trip).ToString() + Environment.NewLine;
            Str += "On-Time = " + this.OnTime + Environment.NewLine;
            Str += "Off-Time = " + this.OffTime + Environment.NewLine;
            if (this.Status < 16)
            {
                Str += "Control Code : 0x0" + typeConvert.HextoString(this.Status).ToUpper();
            }
            else
            {
                Str += "Control Code : 0x" + typeConvert.HextoString(this.Status).ToUpper();
            }
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            uint tempaddress;
            tempaddress = OD.address;
            str += "DO " + tempaddress + " ";
            // close trip
            if (this.Trip && !this.Close)
            {
                str += ",Trip";
            }
            else if (!this.Trip && this.Close)
            {
                str += ",Close";
            }
            else
            {
                str += ",NULL";
            }
            // code
            switch (this.Code)
            {
                case 0: str += ",NUL Operation";
                    break;
                case 1: str += ",Pulse On";
                    break;
                case 2: str += ",Pulse Off";
                    break;
                case 3: str += ",Latch On";
                    break;
                case 4: str += ",Latch Off";
                    break;
                default: str += ",Undefined";
                    break;
            }

            str += ",Count " + this.Count;
            str += ",On/Off Time " + this.OnTime + "-" + this.OffTime + " msecs";
            switch (this.Status)
            {
                case 0: str += ",[Accepted]";
                    break;
                case 1: str += ",[Timeout]";
                    break;
                case 2: str += ",[No Select]";
                    break;
                case 3: str += ",[Format Error]";
                    break;
                case 4: str += ",[Not Supported]";
                    break;
                case 5: str += ",[Point Active]";
                    break;
                case 6: str += ",[Hardware Error]";
                    break;
                case 7: str += ",[Local]";
                    break;
                default: str += ",[Undefined]";
                    break;
            }
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            byte Control = 0;
            if (this.Trip) Control |= 0x80;
            if (this.Close) Control |= 0x40;
            if (this.Clear) Control |= 0x20;
            if (this.Queue) Control |= 0x10;
            Control += this.Code;
            data.Add(Control);
            data.Add(this.Count);
            byte[] ontime = typeConvert.DataToRaw(this.OnTime, 4);
            for(int i=0;i<ontime.Length;i++)
            {
                data.Add(ontime[i]);
            }
            byte[] offtime = typeConvert.DataToRaw(this.OffTime, 4);
            for (int i = 0; i < offtime.Length; i++)
            {
                data.Add(offtime[i]);
            }
            data.Add(this.Status);
            return data.ToArray();
        }
    }

    public class O12V3 : ObjectDataLibrary //digit input w/o flag
    {
        bool Mask;
        public O12V3(bool state)
        {
            this.Mask = state;
        }
        public O12V3(byte data, byte digit)
        {
            String temp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(data)), 8);
            if (temp[digit] == '1')
            {
                this.Mask = true;
            }
            else this.Mask = false;
        }
        public O12V3(byte[] data)
        {
            if (data[0] == 1)
            {
                this.Mask = true;
            }
            else
            {
                this.Mask = false;
            }
        }
        public String toString()
        {
            string Str = null;
            Str += "Mask = " + Convert.ToByte(this.Mask).ToString();
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            str += "Mask " + OD.address + " = " + this.Mask;
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }
    
    // Object 30 analog input
    public class O30V1 : ObjectDataLibrary //32 bit with flag
    {
        byte[] Data;
        byte FLAG;
        int flagCount;
        // flag
        bool Online;
        bool Restart;
        bool Comunication_lost;
        bool Remote_Forced_Data;
        bool Local_Forced_Data;
        bool Over_Range;
        bool Reference_Check;

        public uint Value;

        public O30V1(byte [] data)
        {
            this.Data = data;
            this.FLAG = this.Data[0];

            String temp = typeConvert.fillZero(
                typeConvert.HextoBinary(
                    typeConvert.HextoString(this.FLAG)), 8);
            if (temp[1] == '1')
            {
                this.Reference_Check = true;
                flagCount++;
            }
            if (temp[2] == '1')
            {
                this.Over_Range = true;
                flagCount++;
            }
            if (temp[3] == '1')
            {
                this.Local_Forced_Data = true;
                flagCount++;
            }
            if (temp[4] == '1')
            {
                this.Remote_Forced_Data = true;
                flagCount++;
            }
            if (temp[5] == '1')
            {
                this.Comunication_lost = true;
                flagCount++;
            }
            if (temp[6] == '1')
            {
                this.Restart = true;
                flagCount++;
            }
            if (temp[7] == '1')
            {
                this.Online = true;
                flagCount++;
            }

            //value
            for (int j = 0; j < 4; j++) //32 bit value
            {
                this.Value += (uint)this.Data[j + 1] * (uint)Math.Pow(0x100, j);
            }
        }


        public String toString()
        {
            string Str = null;
            if (this.FLAG < 16)
            {
                Str += "Flag : 0x0" + typeConvert.HextoString(this.FLAG).ToUpper()
                    + Environment.NewLine;
            }
            else
            {
                Str += "Flag : 0x" + typeConvert.HextoString(this.FLAG).ToUpper()
                    + Environment.NewLine;
            }
            Str += "Value = " + this.Value;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            uint tempaddress;
            tempaddress = OD.address;
            str += "AI " + tempaddress + " = " + this.Value;
            if (flagCount > 0) // has flag 
            {
                str += " [";
                if (this.Online)
                {
                    str += "Online";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Restart)
                {
                    str += "Restart";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Comunication_lost)
                {
                    str += "Communication lost";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Remote_Forced_Data)
                {
                    str += "Remote forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Local_Forced_Data)
                {
                    str += "Local forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Over_Range)
                {
                    str += "Over Range";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Reference_Check)
                {
                    str += "Reference Check";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                str += "]";
            }
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    public class O30V2 : ObjectDataLibrary //16 bit with flag
    {
        byte[] Data;
        byte FLAG;
        int flagCount;

        // flag
        bool Online;
        bool Restart;
        bool Comunication_lost;
        bool Remote_Forced_Data;
        bool Local_Forced_Data;
        bool Over_Range;
        bool Reference_Check;

        public uint Value;
        public O30V2(byte[] data)
        {
            this.Data = data;
            this.FLAG = this.Data[0];
            String temp = typeConvert.fillZero(
                typeConvert.HextoBinary(
                    typeConvert.HextoString(this.FLAG)), 8);
            if (temp[1] == '1')
            {
                this.Reference_Check = true;
            }
            if (temp[2] == '1')
            {
                this.Over_Range = true;
            }
            if (temp[3] == '1')
            {
                this.Local_Forced_Data = true;
            }
            if (temp[4] == '1')
            {
                this.Remote_Forced_Data = true;
            }
            if (temp[5] == '1')
            {
                this.Comunication_lost = true;
            }
            if (temp[6] == '1')
            {
                this.Restart = true;
            }
            if (temp[7] == '1')
            {
                this.Online = true;
            }

            //value
            for (int j = 0; j < 2; j++) //16 bit value
            {
                this.Value += (uint)this.Data[j + 1] * (uint)Math.Pow(0x100, j);
            }
        }
        public String toString()
        {
            string Str = null;
            if (this.FLAG < 16)
            {
                Str += "Flag : 0x0" + typeConvert.HextoString(this.FLAG).ToUpper()
                    + Environment.NewLine;
            }
            else
            {
                Str += "Flag : 0x" + typeConvert.HextoString(this.FLAG).ToUpper()
                    + Environment.NewLine;
            }
            Str += "Value = " + this.Value;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            uint tempaddress;
            tempaddress = OD.address;
            str += "AI " + tempaddress + " = " + this.Value;
            if (flagCount > 0) // has flag 
            {
                str += " [";
                if (this.Online)
                {
                    str += "Online";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Restart)
                {
                    str += "Restart";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Comunication_lost)
                {
                    str += "Communication lost";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Remote_Forced_Data)
                {
                    str += "Remote forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Local_Forced_Data)
                {
                    str += "Local forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Over_Range)
                {
                    str += "Over Range";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Reference_Check)
                {
                    str += "Reference Check";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                str += "]";
            }
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    public class O30V3 : ObjectDataLibrary //32 bit w/o flag
    {
        byte[] Data;
        public uint Value;
        public O30V3(byte[] data)
        {
            this.Data = data;
            for (int j = 0; j < 4; j++) //16 bit value
            {
                this.Value += (uint)this.Data[j] * (uint)Math.Pow(0x100, j);
            }
        }
        public String toString()
        {
            string Str = null;
            Str += "Value = " + this.Value;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            uint tempaddress;
            tempaddress = OD.address;
            str += "AI " + tempaddress + " = " + this.Value;
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    public class O30V4 : ObjectDataLibrary //16 bit w/o flag
    {
        byte[] Data;
        public uint Value;
        public O30V4(byte[] data)
        {
            this.Data = data;
            for (int j = 0; j < 2; j++) //16 bit value
            {
                this.Value += (uint)this.Data[j] * (uint)Math.Pow(0x100, j);
            }
        }
        public String toString()
        {
            string Str = null;
            Str += "Value = " + this.Value;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            uint tempaddress;
            tempaddress = OD.address;
            str += "AI " + tempaddress + " = " + this.Value;
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    // Object 32 analog input change
    public class O32V1 : ObjectDataLibrary //32 bit with flag
    {
        byte[] Data;
        byte FLAG;
        int flagCount;
        // flag
        bool Online;
        bool Restart;
        bool Comunication_lost;
        bool Remote_Forced_Data;
        bool Local_Forced_Data;
        bool Over_Range;
        bool Reference_Check;

        public uint Value;
        public O32V1(byte[] data)
        {
            this.Data = data;
            this.FLAG = this.Data[0];
            String temp = typeConvert.fillZero(
                    typeConvert.HextoBinary(
                        typeConvert.HextoString(this.FLAG)), 8);
            if (temp[1] == '1')
            {
                this.Reference_Check = true;
                flagCount++;
            }
            if (temp[2] == '1')
            {
                this.Over_Range = true;
                flagCount++;
            }
            if (temp[3] == '1')
            {
                this.Local_Forced_Data = true;
                flagCount++;
            }
            if (temp[4] == '1')
            {
                this.Remote_Forced_Data = true;
                flagCount++;
            }
            if (temp[5] == '1')
            {
                this.Comunication_lost = true;
                flagCount++;
            }
            if (temp[6] == '1')
            {
                this.Restart = true;
                flagCount++;
            }
            if (temp[7] == '1')
            {
                this.Online = true;
                flagCount++;
            }
            //value
            for (int j = 0; j < 4; j++) //32 bit value 
            {
                this.Value += (uint)this.Data[j + 1] * (uint)Math.Pow(0x100, j);
            }
        }

        public String toString()
        {
            string Str = null;
            if (this.FLAG < 16)
            {
                Str += "Flag : 0x0" + typeConvert.HextoString(this.FLAG).ToUpper()
                    + Environment.NewLine;
            }
            else
            {
                Str += "Flag : 0x" + typeConvert.HextoString(this.FLAG).ToUpper()
                    + Environment.NewLine;
            }
            Str += "Value = " + this.Value;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            uint tempaddress;
            tempaddress = OD.address;
            str += "AI " + tempaddress + " = " + this.Value;
            if (flagCount > 0) // has flag 
            {
                str += " [";
                if (this.Online)
                {
                    str += "Online";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Restart)
                {
                    str += "Restart";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Comunication_lost)
                {
                    str += "Communication lost";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Remote_Forced_Data)
                {
                    str += "Remote forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Local_Forced_Data)
                {
                    str += "Local forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Over_Range)
                {
                    str += "Over Range";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Reference_Check)
                {
                    str += "Reference Check";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                str += "]";
            }
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    public class O32V2 : ObjectDataLibrary //16 bit with flag
    {
        byte[] Data;
        byte FLAG;
        int flagCount;
        // flag
        bool Online;
        bool Restart;
        bool Comunication_lost;
        bool Remote_Forced_Data;
        bool Local_Forced_Data;
        bool Over_Range;
        bool Reference_Check;

        public uint Value;
        public O32V2(byte[] data)
        {
            this.Data = data;
            this.FLAG = this.Data[0];
            String temp = typeConvert.fillZero(
                    typeConvert.HextoBinary(
                        typeConvert.HextoString(this.FLAG)), 8);
            if (temp[1] == '1')
            {
                this.Reference_Check = true;
                flagCount++;
            }
            if (temp[2] == '1')
            {
                this.Over_Range = true;
                flagCount++;
            }
            if (temp[3] == '1')
            {
                this.Local_Forced_Data = true;
                flagCount++;
            }
            if (temp[4] == '1')
            {
                this.Remote_Forced_Data = true;
                flagCount++;
            }
            if (temp[5] == '1')
            {
                this.Comunication_lost = true;
                flagCount++;
            }
            if (temp[6] == '1')
            {
                this.Restart = true;
                flagCount++;
            }
            if (temp[7] == '1')
            {
                this.Online = true;
                flagCount++;
            }
            //value
            for (int j = 0; j < 2; j++) //16 bit value
            {
                this.Value += (uint)this.Data[j + 1] * (uint)Math.Pow(0x100, j);
            }
        }
        public String toString()
        {
            string Str = null;
            if (this.FLAG < 16)
            {
                Str += "Flag : 0x0" + typeConvert.HextoString(this.FLAG).ToUpper()
                    + Environment.NewLine;
            }
            else
            {
                Str += "Flag : 0x" + typeConvert.HextoString(this.FLAG).ToUpper()
                    + Environment.NewLine;
            }
            Str += "Value = " + this.Value;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            uint tempaddress;
            tempaddress = OD.address;
            str += "AI " + tempaddress + " = " + this.Value;
            if (flagCount > 0) // has flag 
            {
                str += " [";
                if (this.Online)
                {
                    str += "Online";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Restart)
                {
                    str += "Restart";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Comunication_lost)
                {
                    str += "Communication lost";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Remote_Forced_Data)
                {
                    str += "Remote forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Local_Forced_Data)
                {
                    str += "Local forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Over_Range)
                {
                    str += "Over Range";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Reference_Check)
                {
                    str += "Reference Check";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                str += "]";
            }
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    public class O32V3 : ObjectDataLibrary //32 bit with time
    {
        byte[] Data;
        byte FLAG;
        int flagCount;
        // flag
        bool Online;
        bool Restart;
        bool Comunication_lost;
        bool Remote_Forced_Data;
        bool Local_Forced_Data;
        bool Over_Range;
        bool Reference_Check;

        public uint Value;
        UInt64 Time;
        TimeCalculator dateTime;
        public DateTime realTime;

        public O32V3(byte[] data)
        {
            this.Data = data;
            this.FLAG = this.Data[0];
            String temp = typeConvert.fillZero(
                    typeConvert.HextoBinary(
                        typeConvert.HextoString(this.FLAG)), 8);
            if (temp[1] == '1')
            {
                this.Reference_Check = true;
                flagCount++;
            }
            if (temp[2] == '1')
            {
                this.Over_Range = true;
                flagCount++;
            }
            if (temp[3] == '1')
            {
                this.Local_Forced_Data = true;
                flagCount++;
            }
            if (temp[4] == '1')
            {
                this.Remote_Forced_Data = true;
                flagCount++;
            }
            if (temp[5] == '1')
            {
                this.Comunication_lost = true;
                flagCount++;
            }
            if (temp[6] == '1')
            {
                this.Restart = true;
                flagCount++;
            }
            if (temp[7] == '1')
            {
                this.Online = true;
                flagCount++;
            }
            byte index = 1;
            //value
            for (int j = 0; j < 4; j++) //32 bit value 
            {
                this.Value += (uint)this.Data[index] * (uint)Math.Pow(0x100, j);
                index++;
            }
            //time
            for (int j = 0; j < 6; j++) //48 bit time
            {
                this.Time += (UInt64)this.Data[index] * (UInt64)Math.Pow(0x100, j);
                index++;
            }
            dateTime = new TimeCalculator(this.Time);
            this.realTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Min, dateTime.Sec, dateTime.mSec);
        }
        public String toString()
        {
            string Str = null;
            if (this.FLAG < 16)
            {
                Str += "Flag : 0x0" + typeConvert.HextoString(this.FLAG).ToUpper()
                    + Environment.NewLine;
            }
            else
            {
                Str += "Flag : 0x" + typeConvert.HextoString(this.FLAG).ToUpper()
                    + Environment.NewLine;
            }
            Str += "Value = " + this.Value + Environment.NewLine;
            Str += "Time = " + this.Time;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            uint tempaddress;
            tempaddress = OD.address;
            str += "AI " + tempaddress + " = " + this.Value;
            if (flagCount > 0) // has flag 
            {
                str += " [";
                if (this.Online)
                {
                    str += "Online";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Restart)
                {
                    str += "Restart";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Comunication_lost)
                {
                    str += "Communication lost";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Remote_Forced_Data)
                {
                    str += "Remote forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Local_Forced_Data)
                {
                    str += "Local forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Over_Range)
                {
                    str += "Over Range";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Reference_Check)
                {
                    str += "Reference Check";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                str += "]";
            }
            str += " " + this.dateTime.ToString();
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    public class O32V4 : ObjectDataLibrary //16 bit with time
    {
        byte[] Data;
        byte FLAG;
        int flagCount;
        // flag
        bool Online;
        bool Restart;
        bool Comunication_lost;
        bool Remote_Forced_Data;
        bool Local_Forced_Data;
        bool Over_Range;
        bool Reference_Check;

        public uint Value;
        UInt64 Time;
        TimeCalculator dateTime;
        public DateTime realTime;

        public O32V4(byte[] data)
        {
            this.Data = data;
            this.FLAG = this.Data[0];
            String temp = typeConvert.fillZero(
                    typeConvert.HextoBinary(
                        typeConvert.HextoString(this.FLAG)), 8);
            if (temp[1] == '1')
            {
                this.Reference_Check = true;
            }
            if (temp[2] == '1')
            {
                this.Over_Range = true;
            }
            if (temp[3] == '1')
            {
                this.Local_Forced_Data = true;
            }
            if (temp[4] == '1')
            {
                this.Remote_Forced_Data = true;
            }
            if (temp[5] == '1')
            {
                this.Comunication_lost = true;
            }
            if (temp[6] == '1')
            {
                this.Restart = true;
            }
            if (temp[7] == '1')
            {
                this.Online = true;
            }
            byte index = 1;
            //value
            for (int j = 0; j < 2; j++) //16 bit value 
            {
                this.Value += (uint)this.Data[index] * (uint)Math.Pow(0x100, j);
                index++;
            }
            //time
            for (int j = 0; j < 6; j++) //48 bit time
            {
                this.Time += (UInt64)this.Data[index] * (UInt64)Math.Pow(0x100, j);
                index++;
            }
            dateTime = new TimeCalculator(this.Time);
            this.realTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Min, dateTime.Sec, dateTime.mSec);
        }
        public String toString()
        {
            string Str = null;
            if (this.FLAG < 16)
            {
                Str += "Flag : 0x0" + typeConvert.HextoString(this.FLAG).ToUpper()
                    + Environment.NewLine;
            }
            else
            {
                Str += "Flag : 0x" + typeConvert.HextoString(this.FLAG).ToUpper()
                    + Environment.NewLine;
            }
            Str += "Value = " + this.Value + Environment.NewLine;
            Str += "Time = " + this.Time;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            uint tempaddress;
            tempaddress = OD.address;
            str += "AI " + tempaddress + " = " + this.Value;
            if (flagCount > 0) // has flag 
            {
                str += " [";
                if (this.Online)
                {
                    str += "Online";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Restart)
                {
                    str += "Restart";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Comunication_lost)
                {
                    str += "Communication lost";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Remote_Forced_Data)
                {
                    str += "Remote forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Local_Forced_Data)
                {
                    str += "Local forced data";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Over_Range)
                {
                    str += "Over Range";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                if (this.Reference_Check)
                {
                    str += "Reference Check";
                    flagCount--;
                    if (flagCount > 0) str += ",";
                }
                str += "]";
            }
            str += " " + this.dateTime.ToString();
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }
    // Object 50 Time
    public class O50V1 : ObjectDataLibrary
    {
        byte[] Data;

        UInt64 Time;
        TimeCalculator dateTime;
        DateTime realTime;
        public O50V1(byte[] data)
        {
            this.Data = data;
            //time
            for (int j = 0; j < 6; j++) //48 bit time value
            {
                this.Time += (UInt64)this.Data[j] * (UInt64)Math.Pow(0x100, j);
            }
            dateTime = new TimeCalculator(this.Time);
            this.realTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Min, dateTime.Sec, dateTime.mSec);
        }

        public O50V1(DateTime dt)
        {
            dateTime = new TimeCalculator(dt);
            this.Time = dateTime.Time;
        }

        public String toString()
        {
            string Str = null;
            Str += "Time = " + this.Time;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            str += "Time = " + this.dateTime.ToString();
            return str;
        }

        public byte[] ToRawData()
        {
            return typeConvert.DataToRaw((Int64)this.Time, 6);
        }
    }

    public class O50V2 : ObjectDataLibrary //with interval
    {
        byte[] Data;

        UInt64 Time;
        uint Interval;
        TimeCalculator dateTime;
        DateTime realTime;

        public O50V2(byte[] data)
        {
            this.Data = data;
            //time
            byte index = 0;
            for (int j = 0; j < 6; j++) //48 bit time value
            {
                this.Time += (uint)this.Data[index] * (uint)Math.Pow(0x100, j);
                index++;
            }
            dateTime = new TimeCalculator(this.Time);
            this.realTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Min, dateTime.Sec, dateTime.mSec);
            //interval
            for (int j = 0; j < 4; j++) //32 bit interval
            {
                this.Time += (UInt64)this.Data[index] * (UInt64)Math.Pow(0x100, j);
                index++;
            }
        }

        public String toString()
        {
            string Str = null;
            Str += "Time = " + this.Time + Environment.NewLine;
            Str += "Interval = " + this.Interval;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            str += "Time = " + this.dateTime.ToString();
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }
    // 
    public class O51V1 : ObjectDataLibrary
    {
        byte[] Data;

        UInt64 Time;
        TimeCalculator dateTime;
        DateTime realTime;
        public O51V1(byte[] data)
        {
            this.Data = data;
            //time
            for (int j = 0; j < 6; j++) //48 bit time value
            {
                this.Time += (UInt64)this.Data[j] * (UInt64)Math.Pow(0x100, j);
            }
            dateTime = new TimeCalculator(this.Time);
            this.realTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Min, dateTime.Sec, dateTime.mSec);
        }
        public String toString()
        {
            string Str = null;
            Str += "Time = " + this.Time;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            str += "Time = " + this.dateTime.ToString();
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    public class O51V2 : ObjectDataLibrary
    {
        byte[] Data;

        UInt64 Time;
        TimeCalculator dateTime;
        DateTime realTime;
        public O51V2(byte[] data)
        {
            this.Data = data;
            //time
            for (int j = 0; j < 6; j++) //48 bit time value
            {
                this.Time += (UInt64)this.Data[j] * (UInt64)Math.Pow(0x100, j);
            }
            dateTime = new TimeCalculator(this.Time);
            this.realTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Min, dateTime.Sec, dateTime.mSec);
        }
        public String toString()
        {
            string Str = null;
            Str += "Time = " + this.Time;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            str += "Time = " + this.dateTime.ToString();
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    // Object 52 Time Delay
    public class O52V1 : ObjectDataLibrary //COARSE
    {
        byte[] Data;

        public uint Second;
        public O52V1(byte[] data)
        {
            this.Data = data;
            //time
            for (int j = 0; j < 2; j++) //16 bit time value
            {
                this.Second += (uint)this.Data[j] * (uint)Math.Pow(0x100, j);
            }
        }
        public String toString()
        {
            string Str = null;
            Str += "Second = " + this.Second;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            str += "Delay time = " + this.Second + " Sec";
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    public class O52V2 : ObjectDataLibrary //COARSE
    {
        byte[] Data;

        public uint mSecond;
        public O52V2(byte[] data)
        {
            this.Data = data;
            //time
            for (int j = 0; j < 2; j++) //16 bit time value
            {
                this.mSecond += (uint)this.Data[j] * (uint)Math.Pow(0x100, j);
            }
        }
        public String toString()
        {
            string Str = null;
            Str += "mSecond = " + this.mSecond;
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            str += "Delay time = " + this.mSecond + " mSec";
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    // Object 80 IIN
    public class O80V1 : ObjectDataLibrary
    {
        byte[] Data;

        bool State;

        public O80V1(bool state)
        {
            this.State = state;
        }
        public O80V1(byte data,byte digit)
        {
            String temp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(data)), 8);
            if (temp[digit] == '1')
            {
                this.State = true;
            }
            else this.State = false;
        }
        public O80V1(byte[] data)
        {
            if (data[0] == 1)
            {
                this.State = true;
            }
            else
            {
                this.State = false;
            }
        }
        public String toString()
        {
            string Str = null;
            Str += "State = " + Convert.ToByte(this.State).ToString();
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            str += "IIN " + OD.address + " = " + Convert.ToByte(this.State).ToString();
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            return data.ToArray();
        }
    }

    // Object 101 Binary Coded Decimal
    public class O101V2 : ObjectDataLibrary
    {
        public byte[] Data = new byte[8];
        public byte[] rawData = new byte[4];

        UInt32 data;

        public O101V2(UInt32 data)
        {
            //this.State = state;
            // check if input value not more than 8 digits
            if (data <= 99999999)
            {
                //-------------------------------- Integer to BCD---------------------------------------------//
                //digit 1
                    if (data > 0)   //Digit 1
                    {
                        this.Data[0] = (byte)(data % 10);
                        this.rawData[0] = this.Data[0];
                    }
                //digit 2
                    if (data > 9)   //Digit 2 
                    {
                        this.Data[1] = (byte)((data % 100)/10);
                        this.rawData[0] |= (byte)(this.Data[1] << 4);
                    }
                //digit 3
                    if (data > 99)   //Digit 3
                    {
                        this.Data[2] = (byte)((data % 1000) / 100);
                        this.rawData[1] = this.Data[2];
                    }
                //digit 4
                    if (data > 999)  //Digit 4
                    {
                        this.Data[3] = (byte)((data % 10000) / 1000);
                        this.rawData[1] |= (byte)(this.Data[3] << 4);
                    }

                //digit 5
                    if (data > 9999)
                    {
                        this.Data[4] = (byte)((data % 100000) / 10000);
                        this.rawData[2] = this.Data[4];
                    }

                //digit 6
                    if (data > 99999)
                    {
                        this.Data[5] = (byte)((data % 1000000) / 100000);
                        this.rawData[2] = (byte)(this.Data[5] << 4);
                    }

                //digit 7
                    if (data > 999999)
                    {
                        this.Data[6] = (byte)((data % 10000000) / 1000000);
                        this.rawData[3] = this.Data[6];
                    }

                //digit 8
                    if (data > 9999999)
                    {
                        this.Data[7] = (byte)(data / 10000000);
                        this.rawData[3] = (byte)(this.Data[7] << 4);
                    }
            }
        }

        public O101V2(byte[] data)
        {
            for (int i = 0; i < 4; i++)
            {
                this.rawData[i] = data[i];
            }
        }

        public String toString()
        {
            string Str = null;
            //Str += "State = " + Convert.ToByte(this.State).ToString();
            return Str;
        }

        public String decodeDetailMonitor(ObjectData OD)
        {
            string str = null;
            //str += "IIN " + OD.address + " = " + Convert.ToByte(this.State).ToString();
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> data = new List<byte>();
            data.Add(rawData[0]);
            data.Add(rawData[1]);
            data.Add(rawData[2]);
            data.Add(rawData[3]);
            return data.ToArray();
        }
    }

    #endregion
}
