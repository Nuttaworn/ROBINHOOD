using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DNP3Lib
{
    #region Qualifier
    public class Qualifier
    {
        public byte IndexSize;
        public byte Qcode;
        public byte rawdata;
        public bool isCountType;

        public Qualifier()
        {
        }

        public Qualifier(byte qualifier)
        {
            this.rawdata = qualifier;
            this.Qcode = (byte)(qualifier & 0x0F);
            this.IndexSize = (byte)(qualifier & 0x70);
            this.IndexSize >>= 4;
            if ((this.Qcode >= 0) && (this.Qcode <= 5)) //Start - Stop
            {
                this.isCountType = false;
            }
            else if ((this.Qcode >= 7) && (this.Qcode <= 9)) // Quantity
            {
                this.isCountType = true;
            }
        }

        public int getRangeSize()
        {
            int totalsize = 0;  
            switch (this.Qcode)
            {
                case 0: totalsize += 2;  // 1 byte start 1 byte stop
                    break;
                case 1: totalsize += 4; // 2 byte start 2 byte stop
                    break;
                case 2: totalsize += 8; // 4 byte start 4 byte stop
                    break;
                case 3: totalsize += 2; // 1 byte start 1 byte stop
                    break;
                case 4: totalsize += 4; // 2 byte start 2 byte stop
                    break;
                case 5: totalsize += 8; // 4 byte start 4 byte stop
                    break;
                case 6:
                    totalsize += 0; // nodata
                    return totalsize;
                case 7: totalsize += 1;
                    break;
                case 8: totalsize += 2;
                    break;
                case 9: totalsize += 4;
                    break;
            }
            return totalsize;
        }

        public int getIndexSize()
        {
            //check index size
            int IndexSize = 0;
            switch (this.IndexSize)
            {
                case 1:
                    IndexSize = 1;
                    //this.isSizeIndex = false;
                    break;
                case 2:
                    IndexSize = 2;
                    //this.isSizeIndex = false;
                    break;
                case 3:
                    IndexSize = 4;
                    //this.isSizeIndex = false;
                    break;
                case 4: // 2 byte presix object size
                    IndexSize = 1;
                    //this.isSizeIndex = true;
                    break;
                case 5:
                    IndexSize = 2;
                    //this.isSizeIndex = true;
                    break;
                case 6:
                    IndexSize = 4;
                    //this.isSizeIndex = true;
                    break;
                default: IndexSize = 0;
                    break;
            }
            return IndexSize;
        }

        public bool getIndexType()
        {
            bool isIndexSize = false;
            if (this.IndexSize >= 4 && this.IndexSize <= 6)
            {
                isIndexSize = true;
            }
            return isIndexSize;
        }

        public override String ToString()
        {
            string str = null;
            str += "IndexSize : " + this.IndexSize + Environment.NewLine
                + "Qualifier Code : " + this.Qcode;
            return str;
        }

        public byte ToRawData()
        {
            byte Q = (byte)(this.IndexSize << 4);
            Q += this.Qcode;
            return Q;
        }

        public String decodeDetailMonitor()
        {
            String str = null;
            if (this.rawdata < 16)
            {
                str += "Qualifier 0x0" + typeConvert.HextoString(rawdata).ToUpper();
            }
            else
            {
                str += "Qualifier 0x" + typeConvert.HextoString(rawdata).ToUpper();
            }
            return str;
        }
    }
    #endregion

    #region ObjectData
    public class ObjectData
    {
        private byte ObjectGroup;
        private byte ObjectVariation;
        private uint Index;
        public uint address;
        public byte[] Data;
        private bool hasIndex;
        public bool isBitData;
        public ObjectDataLibrary ObjData;

        public ObjectData()
        {
        }

        public ObjectData(byte og,byte ov,uint index,uint address,byte [] data,bool hasIndex)
        {
            this.ObjectGroup = og;
            this.ObjectVariation = ov;
            this.Index = index;
            this.address = address;
            this.Data = data;
            this.hasIndex = hasIndex;
            this.ObjData = Object_Library.getObjectType(this.ObjectGroup, this.ObjectVariation, data);
        }

        public ObjectData(byte og,byte ov,uint index, uint address, byte[] data, bool hasIndex,bool isBitData)
        {
            this.ObjectGroup = og;
            this.ObjectVariation = ov;
            this.Index = index;
            this.address = address;
            this.Data = data;
            this.hasIndex = hasIndex;
            this.isBitData = true;
            this.ObjData = Object_Library.getObjectType(this.ObjectGroup, this.ObjectVariation, data);
        }

        public override String ToString()
        {
            string str = null;
            if (hasIndex)
            {
                str += "Index : " + this.Index + Environment.NewLine;
            }
            for (int i = 0; i < this.Data.Length; i++)
            {
                if (i == this.Data.Length - 1) // last data
                {
                    str += "Databyte " + i + " : " + this.Data[i];
                }
                else
                {
                    str += "Databyte " + i + " : " + this.Data[i] + Environment.NewLine;
                }
            }
            str += Environment.NewLine;
            str += "******* Object **********" + Environment.NewLine;
            str += this.ObjData.toString() + Environment.NewLine;
            str += "************************";
            return str;
        }

        public String decodeDetailMonitor()
        {
            return this.ObjData.decodeDetailMonitor(this);
        }

        public byte[] ToRawData()
        {
            List<byte> objData = new List<byte>();
            byte[] data = ObjData.ToRawData();
            for (int i = 0; i < data.Length; i++)
            {
                objData.Add(data[i]);
            }
            return objData.ToArray();
        }
    }
    #endregion

    #region Object Header
    public class Object_Header
    {
        private byte _ObjectGroup;
        public byte ObjectGroup
        {
            get { return _ObjectGroup; }
            set { _ObjectGroup = value; }
        }

        private byte _ObjectVariation;
        public byte ObjectVariation
        {
            get { return _ObjectVariation; }
            set { _ObjectVariation = value; }
        }

        private Qualifier _Qualifier;
        public Qualifier Qualifier
        {
            get { return _Qualifier; }
            set { _Qualifier = value; }
        }

        private uint _Start;
        public uint Start
        {
            get { return _Start; }
            set { _Start = value; }
        }

        private uint _Stop;
        public uint Stop
        {
            get { return _Stop; }
            set { _Stop = value; }
        }

        private uint _Count;
        public uint Count
        {
            get { return _Count; }
            set { _Count = value; }
        }
        private bool isQuantity;
        public String ObjectName;
        private byte[] rawrangedata;

        public Object_Header()
        {
        }

        public Object_Header(byte Object,byte Var,Qualifier qualifier,byte [] Range)
        {
            this.ObjectGroup = Object;
            this.ObjectVariation = Var;
            this.ObjectName = Object_Library.getName(this.ObjectGroup,this.ObjectVariation);

            this.Qualifier = qualifier;
            this.rawrangedata = Range;
            // construct Range by array of byte due to Q-code
            switch (this.Qualifier.Qcode)
            {
                case 0: //Q-code 8 bit Start Stop bit
                    this.Start = Range[0];
                    this.Stop = Range[1];
                    isQuantity = false;
                    break;

                case 1: //Q-code 16 bit Start Stop bit
                    this.Start = (uint)(Range[1] << 8) + Range[0];
                    this.Stop = (uint)(Range[3] << 8) + Range[2];
                    isQuantity = false;
                    break;

                case 2: //Q-code 32 bit Start Stop bit
                    this.Start = (uint)(Range[3] << 24) + (uint)(Range[2] << 16)
                        + (uint)(Range[1] << 8) + Range[0];
                    this.Stop = (uint)(Range[7] << 24) + (uint)(Range[6] << 16)
                        + (uint)(Range[5] << 8) + Range[4];
                    isQuantity = false;
                    break;

                case 3: //Q-code 8 bit Start Stop bit absolute
                    this.Start = Range[0];
                    this.Stop = Range[1];
                    isQuantity = false;
                    break;

                case 4: //Q-code 16 bit Start Stop bit absolute
                    this.Start = (uint)(Range[1] << 8) + Range[0];
                    this.Stop = (uint)(Range[3] << 8) + Range[2];
                    isQuantity = false;
                    break;

                case 5: //Q-code 32 bit Start Stop bit absolute
                    this.Start = (uint)(Range[3] << 24) + (uint)(Range[2] << 16)
                        + (uint)(Range[1] << 8) + Range[0];
                    this.Stop = (uint)(Range[7] << 24) + (uint)(Range[6] << 16)
                        + (uint)(Range[5] << 8) + Range[4];
                    isQuantity = false;
                    break;

                case 6: // Q-code = 6 all poll all Object in the class
                    this.Start = 0;
                    this.Stop = 0;
                    this.Count = 0; // message end here
                    break;

                case 7: // range = 8 bit Quantity indicator
                    this.Count = Range[0];
                    isQuantity = true;
                    break;

                case 8: // range = 16 bit Quantity indicator
                    this.Count = (uint)(Range[1] << 8) + Range[0];
                    isQuantity = true;
                    break;

                case 9: // range = 16 bit Quantity indicator
                    this.Count = (uint)(Range[3] << 24) + (uint)(Range[2] << 16)
                        + (uint)(Range[1] << 8) + Range[0];
                    isQuantity = true;
                    break;

                case 11: // do not supported yet
                    break;
            }
        }
        public int getObjectSize()
        {
            int size = 0;
            size += Qualifier.getIndexSize();
            size += Object_Library.getSize(this.ObjectGroup, this.ObjectVariation);
            if (size == 0x81) 
            { 
                size = 1; 
            }
            return size;
        }
        public int getDataSize()
        {
            int size = 0;
            size += Object_Library.getSize(this.ObjectGroup, this.ObjectVariation);
            return size;
        }

        public uint getObjectQuantity() //
        {
            uint Ocount;
            if ((this.Qualifier.Qcode >= 0) && (this.Qualifier.Qcode <= 5)) //Start - Stop
            {
                Ocount = (this.Stop - this.Start) + 1;
            }
            else if ((this.Qualifier.Qcode >= 7) && (this.Qualifier.Qcode <= 9)) // Quantity
            {
                Ocount = this.Count;
            }
            else
            {
                Ocount = 0;
            }
            // ------------------- check if Object is bit -----------------------------//
            int Osize = Object_Library.getSize(this.ObjectGroup, this.ObjectVariation);
            if (Osize == 0x81)   // bit quantity
            {
                if ((Ocount % 8) == 0) // convert bit to byte
                {
                    Ocount = Ocount / 8;
                }
                else
                {
                    Ocount = (Ocount / 8) + 1;
                }
            }
            //--------------------------------------------------------------------------//
            return Ocount;
        }

        public int getObjectCount()
        {
            int count;
            if ((this.Qualifier.Qcode >= 0) && (this.Qualifier.Qcode <= 5)) //Start - Stop
            {
                count = (int)(this.Stop - this.Start) + 1;
            }
            else if ((this.Qualifier.Qcode >= 7) && (this.Qualifier.Qcode <= 9)) // Quantity
            {
                count = (int)this.Count;
            }
            else
            {
                count = 0;
            }
            return count;
        }

        public int getTotalSize()
        {
            int total = 0;
            total = (int)this.getObjectQuantity() * this.getObjectSize();
            return total;
        }

        public override String ToString()
        {
            string str = null;
            str += "Object Group : " + this.ObjectGroup + Environment.NewLine
                + "Object Variation : " + this.ObjectVariation + Environment.NewLine
                + "Object Name : " + this.ObjectName + Environment.NewLine
                + this.Qualifier.ToString() + Environment.NewLine;
            if (this.Qualifier.Qcode != 6)
            {
                if (!isQuantity) // is start - stop 
                {
                    str += "Start : " + this.Start + Environment.NewLine
                        + "Stop : " + this.Stop;
                }
                else // is count
                {
                    str += "Quantity : " + this.Count;
                }
            }
            return str;
        }

        public String decodeDetailMonitor()
        {
            string str = null;/*
            ObjHdr: Obj 32 Var 2 (16 bit analog change event without time)
                    16 bit quantity of objects prefixed with two byte index
                    Quantity=1 ObjectIdSize=0 Start=0 Stop=0 */
            str += "ObjHdr: Obj " + this.ObjectGroup + " Var " + this.ObjectVariation
                + " (" + this.ObjectName + ")" + Environment.NewLine + "\t        "
                + this.Qualifier.decodeDetailMonitor() + " ";
            if (this.Qualifier.Qcode != 6)
            {
                if (!isQuantity) // is start - stop 
                {
                    str += "Start " + this.Start
                        + " Stop " + this.Stop;
                }
                else // is count
                {
                    str += "Count " + this.Count;
                }
            }
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> objfieldData = new List<byte>();
            objfieldData.Add(this.ObjectGroup);
            objfieldData.Add(this.ObjectVariation);
            objfieldData.Add(this.Qualifier.ToRawData());
            int temprange = this.Qualifier.getRangeSize();
            if ((this.Qualifier.Qcode >= 0) && (this.Qualifier.Qcode <= 5)) //Start - Stop
            {
                byte[] start = typeConvert.DataToRaw(this.Start, temprange / 2);
                for (int i = 0; i < start.Length; i++)
                {
                    objfieldData.Add(start[i]);
                }
                byte[] stop = typeConvert.DataToRaw(this.Stop, temprange / 2);
                for (int i = 0; i < stop.Length; i++)
                {
                    objfieldData.Add(stop[i]);
                }
                //objfieldData.Add((byte)this.Start);
                //objfieldData.Add((byte)this.Stop);
            }
            else if ((this.Qualifier.Qcode >= 7) && (this.Qualifier.Qcode <= 9)) // Quantity
            {
                byte[] count = typeConvert.DataToRaw(this.Count, temprange);
                for (int i = 0; i < count.Length; i++)
                {
                    objfieldData.Add(count[i]);
                }
                //objfieldData.Add((byte)this.Count);
            }
            return objfieldData.ToArray();
        }
    }
    #endregion

    #region Object Field
    public class Object_Field
    {
        public Object_Header ObjectHeader;
        public int IndexSize;
        public bool isSizeIndex;  
        public int DataSize;
        public ObjectData [] Obj;

        public Object_Field()
        {
        }

        public Object_Field(Object_Header OH,byte [] userdata)
        {
            this.ObjectHeader = OH;

            int userdataindex = 0; // index for running userdata to be Class's attributes
            this.DataSize = this.ObjectHeader.getDataSize();
            
            //check index size
            this.IndexSize = this.ObjectHeader.Qualifier.getIndexSize();
            this.isSizeIndex = this.ObjectHeader.Qualifier.getIndexType();
            uint Ocount = this.ObjectHeader.getObjectQuantity(); // get object quantity of byte
            int Count = this.ObjectHeader.getObjectCount(); // get object count
                        
            //---------------------------- Data Object--------------------------------------//
            if (this.DataSize == 0x81) // Object is bit
            {
                this.Obj = new ObjectData[Count];
                byte Oindex = 0;  // Object Array Index
                uint tempaddress = this.ObjectHeader.Start;
                for (int i = 0; i < Ocount; i++) // loop = amount of total bytes
                {
                    uint indextemp = 0;
                    bool hasIndex = false;
                    //byte[] datatemp = new byte[Ocount];
                    for (int k = 0; (k < 8)&&(Oindex < Count); k++,Oindex++) // loop 8 times for every bits in 1 byte
                    {
                        byte[] datatemp = new byte[1];
                        datatemp[0] = (byte)(userdata[userdataindex] >> k); //shift k bit 
                        datatemp[0] &= 0x01; // Check LSB 0 or 1
                        Obj[Oindex] = new ObjectData(this.ObjectHeader.ObjectGroup,this.ObjectHeader.ObjectVariation,
                            indextemp, tempaddress, datatemp, hasIndex, true);
                        tempaddress++;
                    }
                    userdataindex++;
                }
            }
            else // Object is byte
            {
                Obj = new ObjectData[Ocount];

                if (this.ObjectHeader.Qualifier.Qcode != 6 && this.DataSize != 100) // if Q-code isn't 6 and has data
                {
                    //Ocount = this.getObjectQuantity();
                    if (IndexSize > 0)  //object has index
                    {
                        for (int i = 0; i < Obj.Length; i++)
                        {
                            uint indextemp = 0;
                            //uint tempaddress = 0;
                            bool hasIndex = true;
                            byte[] datatemp = new byte[this.DataSize];
                            for (int j = 0; j < IndexSize; j++) // loop for combine index data
                            {
                                indextemp += (uint)(userdata[userdataindex] << (8 * j));
                                userdataindex++;
                            }
                            for (int k = 0; k < this.DataSize; k++)
                            {
                                datatemp[k] = userdata[userdataindex];
                                userdataindex++;
                            }
                            Obj[i] = new ObjectData(this.ObjectHeader.ObjectGroup, this.ObjectHeader.ObjectVariation, 
                                indextemp, indextemp, datatemp, hasIndex);            
                        }
                    }
                    else //object has no index
                    {
                        uint tempaddress = this.ObjectHeader.Start; ;
                        uint indextemp = 0;
                        bool hasIndex = false;
                        for (int i = 0; i < Obj.Length; i++)
                        {
                            byte[] datatemp = new byte[this.DataSize];
                            for (int k = 0; k < this.DataSize; k++)
                            {
                                datatemp[k] = userdata[userdataindex];
                                userdataindex++;
                            }
                            Obj[i] = new ObjectData(this.ObjectHeader.ObjectGroup, this.ObjectHeader.ObjectVariation,
                                indextemp, tempaddress, datatemp, hasIndex);
                            tempaddress++;
                        }
                    }
                }
            }
            //--------------------------------------------------------------------------------//
        }

        public String decodeDetailMonitor()
        {
            String str = null;
            str += this.ObjectHeader.decodeDetailMonitor();
            str += "\t        ";
            String Ostr = null;
            for (int i = 0; i < Obj.Length; i++)
            {
                if (i == Obj.Length - 1)
                {
                    Ostr += Obj[i].decodeDetailMonitor();

                }
                else
                {
                    Ostr += Obj[i].decodeDetailMonitor() + Environment.NewLine + "\t        ";         
                }
            }
            if ((Ostr != "")&&(Ostr != null))
            {
                str += Environment.NewLine;
                str += "\t        " + Ostr;
            }
            return str;
        }

        public override String ToString()
        {
            string str = null;
            str += this.ObjectHeader.ToString() + Environment.NewLine;
            for (int i = 0; i < Obj.Length; i++)
            {
                if (i == Obj.Length - 1)
                {
                    str += "---[ Data : " + i + " ] --------------------------------" + Environment.NewLine
                        + Obj[i].ToString() + Environment.NewLine
                        + "---------------------------------------------------------";
                }
                else
                {
                    str += "---[ Data : " + i + " ] --------------------------------" + Environment.NewLine
                        + Obj[i].ToString() + Environment.NewLine
                        + "---------------------------------------------------------" + Environment.NewLine;
                }
            }
            return str;
        }
        public byte[] ToRawData()
        {
            List<byte> objfieldData = new List<byte>();
            byte[] OHdata = this.ObjectHeader.ToRawData();
            for (int i = 0; i < OHdata.Length; i++)
            {
                objfieldData.Add(OHdata[i]);
            }
            //uint ocount = this.ObjectHeader.getObjectQuantity();
            if (Obj != null)
            {
                int datasize = this.ObjectHeader.getDataSize();
                if (datasize == 0x81) // isbit
                {
                    uint ocount = this.ObjectHeader.getObjectQuantity();
                    byte[] userdata = new byte[ocount];
                    byte temp = 0x01;
                    int x = 0;
                    for (int b = 0; b < Obj.Length; b++)
                    {
                        if (this.Obj[b].Data[0] == 1)
                        {
                            userdata[x] |= temp;
                        }
                        if (temp == 0x80)
                        {
                            x++;
                            temp = 0x01;
                        }
                        else
                        {
                            temp = (byte)(temp << 1);
                        }
                    }
                    for (int c = 0; c < userdata.Length; c++)
                    {
                        objfieldData.Add(userdata[c]);
                    }
                }
                else // is byte
                {
                    for (int j = 0; j < Obj.Length; j++)
                    {
                        if (this.ObjectHeader.Qualifier.IndexSize > 0)
                        {
                            int tempIndexSize = this.ObjectHeader.Qualifier.getIndexSize();
                            byte[] index = typeConvert.DataToRaw(this.Obj[j].address,tempIndexSize);
                            for (int i = 0; i < index.Length; i++)
                            {
                                objfieldData.Add(index[i]);
                            }
                            //objfieldData.Add((byte)this.Obj[j].address);
                        }
                        byte[] userdata = Obj[j].ToRawData();
                        for (int d = 0; d < userdata.Length; d++)
                        {
                            objfieldData.Add(userdata[d]);
                        }
                    }
                }
            }
            return objfieldData.ToArray();
        }
    }
    #endregion

    #region ApplicationControl
    public class ApplicationControl
    {
        public bool FIR = false;
        public bool FIN = false;
        public bool CON = false;
        public bool UNS = false;
        public byte SEQ = 0;
        public byte AC = 0;

        public ApplicationControl()
        {
        }

        public ApplicationControl(byte AC)
        {
            String temp;
            this.AC = AC;
            temp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(this.AC)), 8);
            if (temp[0] == '1') // FIR = 1
            {
                this.FIR = true;
            }
            if (temp[1] == '1') // FIN = 1 
            {
                this.FIN = true;
            }
            if (temp[2] == '1') // CON = 1
            {
                this.CON = true;
            }
            if (temp[3] == '1') // Unsolicite 
            {
                this.UNS = true;
            }
            this.SEQ = (byte)(this.AC & 0x1F);
        }

        public override String ToString()
        {
            string str = null;
            if (AC < 16)
            {
                str += "Application Control : 0x0" + typeConvert.HextoString(AC).ToUpper()
                    + Environment.NewLine;
            }
            else
            {
                str += "Application Control : 0x" + typeConvert.HextoString(AC).ToUpper()
                    + Environment.NewLine;
            }
            str += "\tFIR : " + Convert.ToByte(this.FIR).ToString() + Environment.NewLine
                + "\tFIN : " + Convert.ToByte(this.FIN).ToString() + Environment.NewLine
                + "\tCON : " + Convert.ToByte(this.CON).ToString() + Environment.NewLine
                + "\tUNS : " + Convert.ToByte(this.UNS).ToString() + Environment.NewLine
                + "\tSequence : " + this.SEQ;
            return str;
        }

        public String decodeDetailMonitor()
        {
            String str = null;
            str += "FIR=" + Convert.ToByte(this.FIR)
                + " FIN=" + Convert.ToByte(this.FIN)
                + " CON=" + Convert.ToByte(this.CON)
                + " UNS=" + Convert.ToByte(this.UNS)
                + " SEQ=" + this.SEQ;
            return str;
        }

        public byte ToRawData()
        {
            byte AC = 0;
            if (this.FIR) AC |= 0x80;
            if (this.FIN) AC |= 0x40;
            if (this.CON) AC |= 0x20;
            AC += SEQ;
            return AC;
        }
    }
    #endregion

    #region IIN
    public class IIN
    {
        private bool [] MSB = new bool[8];
        private bool [] LSB = new bool[8];
        // First Octet 0 - 7 
        public bool All_Stations;
        public bool Class1;
        public bool Class2;
        public bool Class3;
        public bool Time_Synch;
        public bool Local;
        public bool Dev_Trouble;
        public bool Dev_Restart;
        // Secont Octet 0 - 7
        public bool Function_Bad;
        public bool Object_Bad;
        public bool Parameter_Bad;
        public bool Buffer_Overflow;
        public bool Operation_Busy;
        public bool Config_Corrupt;

        public byte mByte;
        public byte lByte;
        private int count = 0;

        public IIN(byte MSB, byte LSB)
        {
            String Mtemp;
            String Ltemp;
            this.mByte = MSB;
            this.lByte = LSB;
            Mtemp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(this.mByte)), 8);
            Ltemp = typeConvert.fillZero(typeConvert.HextoBinary(typeConvert.HextoString(this.lByte)), 8);
            for (int i = 0,j = 7; i < 8; i++,j--)  // 7 6 5 4 3 2 1 0
            {
                // MSB check
                if (Mtemp[i] == '1') // bit i = 0 1 2 3 4 5 6 7 , j = 7 6 5 4 3 2 1
                {
                    this.MSB[j] = true;
                    count++;
                }
                else
                {
                    this.MSB[j] = false;
                }
                // LSB check
                if (Ltemp[i] == '1')
                {
                    this.LSB[j] = true;
                    count++;
                }
                else
                {
                    this.LSB[j] = false;
                }
            }
            if (this.MSB[0] == true) this.All_Stations = true;
            if (this.MSB[1] == true) this.Class1 = true;
            if (this.MSB[2] == true) this.Class2 = true;
            if (this.MSB[3] == true) this.Class3 = true;
            if (this.MSB[4] == true) this.Time_Synch = true;
            if (this.MSB[5] == true) this.Local = true;
            if (this.MSB[6] == true) this.Dev_Trouble = true;
            if (this.MSB[7] == true) this.Dev_Restart = true;

            if (this.LSB[0] == true) this.Function_Bad = true;
            if (this.LSB[1] == true) this.Object_Bad = true;
            if (this.LSB[2] == true) this.Parameter_Bad = true;
            if (this.LSB[3] == true) this.Buffer_Overflow = true;
            if (this.LSB[4] == true) this.Operation_Busy = true;
            if (this.LSB[5] == true) this.Config_Corrupt = true;
        }

        public String decodeDetailMonitor()
        {
            string str = null;
            str += "IntInd: ";
            if (mByte < 16)
            {
                str += "0x0" + typeConvert.HextoString(mByte).ToUpper();
            }
            else
            {
                str += "0x" + typeConvert.HextoString(mByte).ToUpper();
            }
            if (lByte < 16)
            {
                str += "0" + typeConvert.HextoString(lByte).ToUpper();
            }
            else
            {
                str += typeConvert.HextoString(lByte).ToUpper();
            }

            // has IIN
            if (count > 0) // 
            {
                int tempCount = count;
                str += " [";
                if (this.All_Stations)
                {
                    str += "All Stations";
                    tempCount--;
                    if (tempCount > 0) str += ",";
                }
                if (this.Class1)
                {
                    str += "Class 1";
                    tempCount--;
                    if (tempCount > 0) str += ",";
                }
                if (this.Class2)
                {
                    str += "Class 2";
                    tempCount--;
                    if (tempCount > 0) str += ",";
                }
                if (this.Class3)
                {
                    str += "Class 3";
                    tempCount--;
                    if (tempCount > 0) str += ",";
                }
                if (this.Time_Synch)
                {
                    str += "Need Time";
                    tempCount--;
                    if (tempCount > 0) str += ",";
                }
                if (this.Local)
                {
                    str += "Local";
                    tempCount--;
                    if (tempCount > 0) str += ",";
                }
                if (this.Dev_Trouble)
                {
                    str += "Device Trouble";
                    tempCount--;
                    if (tempCount > 0) str += ",";
                }
                if (this.Dev_Restart)
                {
                    str += "Restart";
                    tempCount--;
                    if (tempCount > 0) str += ",";
                }
                // LSB
                if (this.Function_Bad)
                {
                    str += "Function Bad";
                    tempCount--;
                    if (tempCount > 0) str += ",";
                }
                if (this.Object_Bad)
                {
                    str += "Object Bad";
                    tempCount--;
                    if (tempCount > 0) str += ",";
                }
                if (this.Parameter_Bad)
                {
                    str += "Parameter Bad";
                    tempCount--;
                    if (tempCount > 0) str += ",";
                }
                if (this.Buffer_Overflow)
                {
                    str += "Buffer Overflow";
                    tempCount--;
                    if (tempCount > 0) str += ",";
                }
                if (this.Operation_Busy)
                {
                    str += "Operation_Busy";
                    tempCount--;
                    if (tempCount > 0) str += ",";
                }
                if (this.Config_Corrupt)
                {
                    str += "Config Corrupt";
                    tempCount--;
                    if (tempCount > 0) str += ",";
                }
                str += "]";
            }
            return str;
        }

        public override String ToString()
        {
            string str = null;

            if (mByte < 16)
            {
                str += "IIN : 0x0" + typeConvert.HextoString(mByte).ToUpper();
            }
            else
            {
                str += "IIN : 0x" + typeConvert.HextoString(mByte).ToUpper();
            }
            if (lByte < 16)
            {
                str += "0" + typeConvert.HextoString(lByte).ToUpper() + Environment.NewLine;
            }
            else
            {
                str += typeConvert.HextoString(lByte).ToUpper() + Environment.NewLine;
            }
            str += "\tMSB : ";
            for(int i = 7; i >=0 ;i--) //MSB
            {
                str += Convert.ToByte(this.MSB[i]).ToString() + " ";
            }
            str += Environment.NewLine;

            str += "\tLSB : ";
            for(int i = 7; i >=0 ;i--) //LSB
            {
                str += Convert.ToByte(this.LSB[i]).ToString() + " ";
            }
            return str;
        }
    }
    #endregion

    #region Application Header
    public class Application_Header
    {
        private DataLink_Header DataLink;//Data from DataLink Layer
        private Transport_Header Transport; //Data from Application Layer
        private byte[] rawdata; // data include crc
        private byte[] Userdata;  //data w/o crc
        public ApplicationControl ApplicationControl;
        public byte FunctionCode;
        private String FunctionName;
        public IIN IIN;
        private ArrayList ObjectList = new ArrayList();
        public Object_Field [] Object;
        private uint[] CRC;

        public Application_Header()
        {
        }

        public Application_Header(DataLink_Header DL,Transport_Header TH,byte [] rawdata)
        {
            this.DataLink = DL;
            this.Transport = TH;
            this.rawdata = rawdata;
            this.pullCRC();

            this.ApplicationControl = new ApplicationControl(this.Userdata[0]);
            this.FunctionCode = this.Userdata[1];
            this.FunctionName = getFunctionName(this.FunctionCode);

            int userdataindex = 2; // index for the next data that have to manage
            if (this.DataLink.Control.DIR == true) //REQ HEADER
            {
                userdataindex = 2;
            }
            else if(this.FunctionCode != 0) //RES HEADER
            {
                this.IIN = new IIN(Userdata[2], Userdata[3]); // IIN data |--MSB--||--LSB--|
                userdataindex = 4;
            }

            //still have data 
            while (userdataindex < this.Userdata.Length)
            {
                byte objtemp = Userdata[userdataindex];
                byte vartemp = Userdata[userdataindex + 1];
                Qualifier qtemp = new Qualifier(Userdata[userdataindex + 2]);
                userdataindex += 3;  //  for Object + Var + Qualifier total 3 bytes
                int rangesizetemp = qtemp.getRangeSize();
                byte[] rangetemp = new byte[rangesizetemp];
                for (int i = 0; i < rangetemp.Length ; i++)
                {
                    rangetemp[i] = Userdata[userdataindex];
                    userdataindex++;
                }
                
                // Construct Object Header
                Object_Header OH = new Object_Header(objtemp, vartemp, qtemp, rangetemp);
                int osizetemp = OH.getTotalSize();
                byte [] datatemp = new byte[osizetemp];
                for (int i = 0; i < datatemp.Length; i++)
                {
                    datatemp[i] = Userdata[userdataindex];
                    userdataindex++;
                }
                
                //Construct Object Field
                Object_Field otemp = new Object_Field(OH, datatemp);
                ObjectList.Add(otemp);
                //this.Object[objectindex] = new Object_Field(OH, datatemp);
            }
            this.Object = new Object_Field[ObjectList.Count];
            this.Object = ObjectList.ToArray(typeof(Object_Field)) as Object_Field[];

        }

        public void pullCRC()
        {
            int CRCcount = 0;
            int Applenght = this.DataLink.Length - 6; // alldata - DLdata - Thdata = appdatasize
            this.Userdata = new byte[Applenght];
            CRCcount = rawdata.Length - Applenght; // lenght with CRC - lenght w/o CRC = CRC amount
            try
            {
                this.CRC = new uint[CRCcount / 2];
            }
            catch
            {
                return;
            }
            int userdataindex = 0;
            int rawdataindex = 0;
            int CRCindex = 0;
            for (int i = 15; i < this.rawdata.Length-2; i = i + 18)
            {
                while(rawdataindex < i)
                {
                    this.Userdata[userdataindex] = this.rawdata[rawdataindex];
                    userdataindex++;
                    rawdataindex++;
                }
                this.CRC[CRCindex] = (uint)(this.rawdata[i+1] << 8) + this.rawdata[i];
                rawdataindex += 2;
                CRCindex++;
            }
            // last 16 byte and  last CRC
            while (rawdataindex < this.rawdata.Length - 2)
            {
                this.Userdata[userdataindex] = this.rawdata[rawdataindex];  // last data
                userdataindex++;
                rawdataindex++;
            }
            this.CRC[CRCindex] = (uint)(this.rawdata[this.rawdata.Length - 1] << 8) 
                + this.rawdata[this.rawdata.Length - 2];  // last CRC
        }

        public static String getFunctionName(byte fcode)
        {
            String fname = null;
            switch (fcode)
            {
                case 0: fname = "Confirm"; break;
                case 1: fname = "Read"; break;
                case 2: fname = "Write"; break;
                case 3: fname = "Select"; break;
                case 4: fname = "Operate"; break;
                case 5: fname = "Direct Operate"; break;
                case 6: fname = "Direct Operate - No Acknowledgement"; break;
                case 7: fname = "Immediate Freeze"; break;
                case 8: fname = "Immediate Freeze - No Acknowledgement"; break;
                case 9: fname = "Freeze and Clear"; break;
                case 10: fname = "Freeze and Clear - No Acknowledgement"; break;
                case 11: fname = "Freeze with Time"; break;
                case 12: fname = "Freeze with Time - No Acknowledgement"; break;
                case 13: fname = "Cold Restart"; break;
                case 14: fname = "Warm Restart"; break;
                case 15: fname = "Initialize Data to Defaults"; break;
                case 16: fname = "Initialize Application"; break;
                case 17: fname = "Start Application"; break;
                case 18: fname = "Stop Application"; break;
                case 19: fname = "Save Configuration"; break;
                case 20: fname = "Enable Unsolicited Messages"; break;
                case 21: fname = "Disable Unsolicited Messages"; break;
                case 22: fname = "Assign Class"; break;
                case 23: fname = "Delay Measurement"; break;
                case 129: fname = "Response"; break;
                case 130: fname = "Unsolicited Message"; break;

                default: fname = "Unknow function code";
                    break;
            }
            return fname;
        }

        public override String ToString()
        {
            string str = null;
            str += "////////////////////////////// [ Application Header Frame ] ///////////////////////////////" + Environment.NewLine;
            str += this.ApplicationControl.ToString() + Environment.NewLine
                + "Function Code : " + this.FunctionCode + Environment.NewLine;
            if (this.DataLink.Control.DIR == false) //RES HEADER
            {
                str += this.IIN.ToString() + Environment.NewLine;
            }

            for (int i = 0; i < Object.Length; i++)
            {
                str += "********************* [ Object : " + (i + 1) + " ] ****************************" + Environment.NewLine
                    + this.Object[i].ToString() + Environment.NewLine
                    + "********************** [ End Object : " + (i + 1) + " ] **********************" + Environment.NewLine;
            }
            for (int i = 0; i < this.CRC.Length; i++)
            {
                str += "CRC [" + (i+1) + "] : 0x" + typeConvert.fillZero(typeConvert.HextoString(this.CRC[i]), 4).ToUpper() + Environment.NewLine;
            }
            str += "/////////////////////////////////////////////////////////////////////////////////////////";
            return str;
        }

        public String decodeDetailMonitor()
        {
            String str = null;
            str += "AppHdr: ";
            str += this.ApplicationControl.decodeDetailMonitor() + Environment.NewLine + "\t        "
                + getFunctionName(this.FunctionCode);
            //IIN
            if (this.DataLink.Control.DIR == false && this.FunctionCode != 0) //RES HEADER
            {
                str += Environment.NewLine + "\t";
                str += this.IIN.decodeDetailMonitor();
            }
            if (this.Object.Length > 0)
            {
                str += Environment.NewLine + "\t";
            }
            // object
            for (int i = 0; i < Object.Length; i++)
            {
                if (i == Object.Length - 1)
                {
                    str += this.Object[i].decodeDetailMonitor();
                }
                else
                {
                    str += this.Object[i].decodeDetailMonitor() + Environment.NewLine + "\t";
                }
            }
            return str;
        }

        public String rawDataShow()
        {
            String str = null;
            for (int i = 0; i < rawdata.Length; i++)
            {
                str += typeConvert.fillZero(typeConvert.HextoString(rawdata[i]), 2).ToUpper() + " ";
            }
            return str;
        }

        public String rawDataMonitor()
        {
            String str = null;
            int index = 0;
           for (int i = 0; i < rawdata.Length; i++)
           {
               if (((i % 10) == 0)&& (i != 0))
               {
                   str += Environment.NewLine + "\t        ";
               }
               str += typeConvert.fillZero(typeConvert.HextoString(rawdata[index]), 2).ToUpper() + " ";
               index++;
           }
           return str;
        }

        public String EventLog()
        {
            String str = null;
            str += this.FunctionName + " ";
            for (int i = 0; i < this.Object.Length; i++)
            {
                str += this.Object[i].ObjectHeader.ObjectName;
                if (i != this.Object.Length - 1) // last object
                    str += ",";
            }
            return str;
        }

        public byte[] ToRawData()
        {
            List<byte> allData = new List<byte>();
            allData.Add(this.ApplicationControl.ToRawData());
            allData.Add(this.FunctionCode);
            if (this.Object != null)
            {
                for (int i = 0; i < this.Object.Length; i++)
                {
                    byte[] userdata = Object[i].ToRawData();
                    for (int j = 0; j < userdata.Length; j++)
                    {
                        allData.Add(userdata[j]);
                    }
                }
            }
            return allData.ToArray(); ;
        }
    }
    #endregion
}