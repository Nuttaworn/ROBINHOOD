using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using DNP3Lib.Encoder;

namespace DNP3Lib.Encoder
{
    //public class masterSimulate
    //{
    //    // properties
    //    public int MasterAddress;
    //    public int RTUAddress;
    //    public int runningSEQ;
    //    public int needSEQ;
    //    public int retryCount;
    //    public bool EnableUns1;
    //    public bool EnableUns2;
    //    public int lastRespSEQ;
    //    public int PollClass1SEQ;
    //    public int PollClass1Retry = 1;
    //    public int PollClass2SEQ;
    //    public int PollClass2Retry = 1;
    //    public int PollDigitalSEQ;
    //    public int PollDigitalRetry = 1;
    //    public int PollAnalogSEQ;
    //    public int PollAnalogRetry = 1;
    //    //data
    //    public uint mSecDelay;
    //    public uint SecDelay;
    //    // flag
    //    public bool getDelay_mSec;
    //    public bool getDelay_Sec;
    //    public bool getUpdateIO;
    //    public bool needConfirm;
    //    public bool SteadyState;
    //    public bool PollClass1Flag;
    //    public bool PollClass1IO;
    //    public bool PollClass2Flag;
    //    public bool PollClass2IO;
    //    public bool PollDigitalFlag;
    //    public bool PollDigitalIO;
    //    public bool PollAnalogFlag;
    //    public bool PollAnalogIO;
    //    // Master properties
    //    public bool UnsolicatedClass1; // enable unsolicate for class 1
    //    public bool UnsolicatedClass2; // enable unsolicate for class 2 
    //    public int Class1Scan;        // Scan time for class 1 DI event change
    //    public int Class2Scan;        // Scan time for class 2 AI event change
    //    public int AnalogBurst;       // Scan time for all AI
    //    public int DigitalBurst;      // Scan time for all DI
    //    public int Receive_Timeout;   // time out for waiting message 
    //    public int Confirm_Timeout;   // time out for reset link
    //    public int Number_of_retries; // maximun retries sending before go to error state
    //    public int Pre_Send_Time; // time before send next message
    //    public masterState masterState;

    //    //public BindingList<DOmonitor> DOstatus = new BindingList<DOmonitor>();
        
    //    //public masterSimulate()
    //    //{
    //    //    this.init();
    //    //    this.resetMaster();
    //    //    this.masterState = new ResetLinkState();
    //    //}

    //    //public masterSimulate(int masteraddress, int rtuaddress, BindingList<DOmonitor> DOstatus)
    //    //{
    //    //    this.init();
    //    //    this.resetMaster();
    //    //    this.MasterAddress = masteraddress;
    //    //    this.RTUAddress = rtuaddress;
    //    //    this.masterState = new ResetLinkState();
    //    //}

    //    public void SetState(masterState masterState)
    //    {
    //        this.masterState = masterState;
    //    }

    //    public void resetMaster()
    //    {
    //        this.runningSEQ = 0;
    //        this.needSEQ = 0;
    //        this.retryCount = 1;
    //        this.getDelay_mSec = false;
    //        this.getDelay_Sec = false;
    //        this.getUpdateIO = false;
    //        this.SteadyState = false;
    //        this.SetState(new ResetLinkState());
    //    }

    //    public void init()
    //    {
    //        UnsolicatedClass1 = true; // enable unsolicate for class 1
    //        UnsolicatedClass2 = false; // enable unsolicate for class 2 
    //        Class1Scan = 2000;        // Scan time for class 1 DI event change
    //        Class2Scan = 10000;        // Scan time for class 2 AI event change
    //        AnalogBurst = 120000;       // Scan time for all AI
    //        DigitalBurst = 120000;      // Scan time for all DI
    //        Receive_Timeout = 2000;   // time out for waiting message 
    //        Confirm_Timeout = 250;   // time out for reset link
    //        Number_of_retries = 6;  // maximun retries sending before go to error state
    //        Pre_Send_Time = 1000;  // time before send next message
    //    }

    //    public byte[] sendMessage()
    //    {
    //        return this.masterState.sendMessage(this);
    //    }

    //    //public bool receiveMessage(FilteredDNP fdnpFrame)
    //    //{
    //    //    return this.masterState.receiveMessage(this,fdnpFrame);
    //    //}

    //    public byte[] confirmMessage()
    //    {
    //        ////////////// data link layer /////////////////////////////
    //        DataLink_Header DL = new DataLink_Header();
    //        DL.Control = new ControlByte(0xC4); // user data (unconfirm);
    //        DL.Destination = (uint)this.RTUAddress;
    //        DL.Source = (uint)this.MasterAddress;

    //        ////////////// transport /////////////////////////////
    //        Transport_Header TH = new Transport_Header(0xc0); // transport alway 0xc4 for master

    //        ///////////// Application Header /////////////////////////////
    //        Application_Header AP = new Application_Header();
    //        // Application Control
    //        ApplicationControl AC = new ApplicationControl();
    //        AC.FIN = true;
    //        AC.FIR = true;
    //        AC.SEQ = (byte)this.lastRespSEQ;
    //        AP.ApplicationControl = AC;
    //        AP.FunctionCode = 0; // Confirm
    //        ////////////// DNP /////////////////////////////
    //        DNP_Frame dnpFrame = new DNP_Frame(DL, TH, AP);

    //        // seg management
    //        return dnpFrame.ToRawData();
    //    }

    //    public byte[] confirmMessage(byte SEQ)
    //    {
    //        ////////////// data link layer /////////////////////////////
    //        DataLink_Header DL = new DataLink_Header();
    //        DL.Control = new ControlByte(0xC4); // user data (unconfirm);
    //        DL.Destination = (uint)this.RTUAddress;
    //        DL.Source = (uint)this.MasterAddress;

    //        ////////////// transport /////////////////////////////
    //        Transport_Header TH = new Transport_Header(0xc0); // transport alway 0xc4 for master

    //        ///////////// Application Header /////////////////////////////
    //        Application_Header AP = new Application_Header();
    //        // Application Control
    //        ApplicationControl AC = new ApplicationControl();
    //        AC.FIN = true;
    //        AC.FIR = true;
    //        AC.SEQ = SEQ;
    //        AP.ApplicationControl = AC;
    //        AP.FunctionCode = 0; // Confirm
    //        ////////////// DNP /////////////////////////////
    //        DNP_Frame dnpFrame = new DNP_Frame(DL, TH, AP);

    //        // seg management
    //        return dnpFrame.ToRawData();
    //    }

    //    public void changeState()
    //    {
    //        this.masterState.changeState(this);
    //    }
    //}

    //// state pattern
    //public interface masterState
    //{
    //    byte[] sendMessage(masterSimulate master);
    //    //bool receiveMessage(masterSimulate master, FilteredDNP fdnpFrame);
    //    void changeState(masterSimulate master);
    //}

    //#region ResetLink
    //public class ResetLinkState : masterState
    //{
    //    public byte[] sendMessage(masterSimulate master) // send reset Link
    //    {
    //        // data link layer
    //        byte[] rawdata = new byte[8];
    //        rawdata[0] = 0x05;
    //        rawdata[1] = 0x64;
    //        rawdata[2] = 0x05;
    //        rawdata[3] = 0xC0;
    //        byte[] Dest = typeConvert.DataToRaw(master.RTUAddress, 2);
    //        rawdata[4] = Dest[0];
    //        rawdata[5] = Dest[1];
    //        byte[] Src = typeConvert.DataToRaw(master.MasterAddress, 2);
    //        rawdata[6] = Src[0];
    //        rawdata[7] = Src[1];
    //        byte[] crc = CRC.genCRCtoRaw(rawdata);
    //        byte[] output = new byte[10];
    //        for (int i = 0; i < 8; i++)
    //        {
    //            output[i] = rawdata[i];
    //        }
    //        output[8] = crc[0];
    //        output[9] = crc[1];
    //        return output;
    //    }

    //    public bool receiveMessage(masterSimulate master,FilteredDNP fdnpFrame)
    //    {
    //        bool respOK = false;
    //        // wait for ack 
    //        if ((fdnpFrame.datalink_functioncode == 0)
    //            && (fdnpFrame.dnpFrame.dataLinkHeader.Control.PRM == false))
    //        {
    //            respOK = true;
    //            master.changeState();
    //        }
    //        return respOK;
    //    }

    //    public void changeState(masterSimulate master)
    //    {
    //        master.masterState = new DisableUnsolicitedState();
    //    }
    //} 
    //#endregion

    //#region DisableUnsolicited
    //public class DisableUnsolicitedState : masterState
    //{
    //    public byte[] sendMessage(masterSimulate master) 
    //    {
    //        ////////////// data link layer /////////////////////////////
    //        DataLink_Header DL = new DataLink_Header();
    //        DL.Control = new ControlByte(0xC4); // user data (unconfirm);
    //        DL.Destination = (uint)master.RTUAddress;
    //        DL.Source = (uint)master.MasterAddress;
            
    //        ////////////// transport /////////////////////////////
    //        Transport_Header TH = new Transport_Header(0xc0); // transport alway 0xc4 for master
            
    //        ///////////// Application Header /////////////////////////////
    //        Application_Header AP = new Application_Header();
    //        // Application Control
    //        ApplicationControl AC = new ApplicationControl();
    //        AC.FIN = true;
    //        AC.FIR = true;
    //        AC.SEQ = (byte)master.runningSEQ;
    //        AP.ApplicationControl = AC;
    //        AP.FunctionCode = 21; // dis UNS msg function
    //        // Object Field
    //        Object_Field [] Obj= new Object_Field[3];
    //        Object_Header OH;
    //        Qualifier Q;
    //        // Object Header 1 O:60 V:2
    //        OH = new Object_Header();
    //        OH.ObjectGroup = 60; //Object 
    //        OH.ObjectVariation = 2; // Var
    //        Q = new Qualifier(0x06); // all point range
    //        OH.Qualifier = Q;
    //        Obj[0] = new Object_Field();
    //        Obj[0].ObjectHeader = OH;  // Object Header
    //        // Object Header 2 O:60 V:3
    //        OH = new Object_Header();
    //        OH.ObjectGroup = 60; //Object 
    //        OH.ObjectVariation = 3; //Var
    //        Q = new Qualifier(0x06); // all point range
    //        OH.Qualifier = Q;
    //        Obj[1] = new Object_Field();
    //        Obj[1].ObjectHeader = OH; // Object Header
    //        // Object Header 3 O:60 V:4
    //        OH = new Object_Header();
    //        OH.ObjectGroup = 60; //Object 
    //        OH.ObjectVariation = 4; //Var
    //        Q = new Qualifier(0x06); // all point range
    //        OH.Qualifier = Q;
    //        Obj[2] = new Object_Field();
    //        Obj[2].ObjectHeader = OH; // Object Header
    //        // move to application header class
    //        AP.Object = Obj;

    //        ////////////// DNP /////////////////////////////
    //        DNP_Frame dnpFrame = new DNP_Frame(DL, TH, AP);
            
    //        // seg management
    //        master.needSEQ = master.runningSEQ;
    //        return dnpFrame.ToRawData();
    //    }

    //    public bool receiveMessage(masterSimulate master, FilteredDNP fdnpFrame)
    //    {
    //        bool respOK = false;
    //        if (fdnpFrame.hasUserdata == false)
    //        {
    //            return false;
    //        }
    //        if ((fdnpFrame.Application_functioncode == 129)
    //            && (fdnpFrame.SEQ == master.needSEQ))
    //        {
    //            respOK = true;
    //            master.EnableUns1 = false;
    //            master.EnableUns2 = false;
    //            master.changeState();
    //            if (master.runningSEQ == 15)
    //            {
    //                master.runningSEQ = 0;
    //            }
    //            else
    //            {
    //                master.runningSEQ++;
    //            }
    //        }
    //        return respOK;
    //    }

    //    public void changeState(masterSimulate master)
    //    {
    //        master.masterState = new ClearRestartState();
    //    }
    //}
    //#endregion

    //#region ClearRestart
    //public class ClearRestartState : masterState
    //{
    //    public byte[] sendMessage(masterSimulate master)
    //    {
    //        ////////////// data link layer /////////////////////////////
    //        DataLink_Header DL = new DataLink_Header();
    //        DL.Control = new ControlByte(0xC4); // user data (unconfirm);
    //        DL.Destination = (uint)master.RTUAddress;
    //        DL.Source = (uint)master.MasterAddress;

    //        ////////////// transport /////////////////////////////
    //        Transport_Header TH = new Transport_Header(0xc0); // transport alway 0xc4 for master

    //        ///////////// Application Header /////////////////////////////
    //        Application_Header AP = new Application_Header();
    //        // Application Control
    //        ApplicationControl AC = new ApplicationControl();
    //        AC.FIN = true;
    //        AC.FIR = true;
    //        AC.SEQ = (byte)master.runningSEQ;
    //        AP.ApplicationControl = AC;
    //        AP.FunctionCode = 2; // write
    //        // Object Field
    //        Object_Field[] OF = new Object_Field[1]; // 1 object
    //        Object_Header OH;
    //        Qualifier Q;
    //        // Object Header 1 O:60 V:2
    //        OH = new Object_Header();
    //        OH.ObjectGroup = 80; //Object 
    //        OH.ObjectVariation = 1; // Var
    //        Q = new Qualifier(0x00); // all point range
    //        OH.Qualifier = Q;
    //        OH.Start = 7;
    //        OH.Stop = 7;
    //        OF[0] = new Object_Field();
    //        OF[0].ObjectHeader = OH;  // Object Header
    //        ObjectData OD = new ObjectData(); //Data
    //        OD.Data = new byte[1];
    //        OD.Data[0] = 0;
    //        OF[0].Obj = new ObjectData[1];
    //        OF[0].Obj[0] = OD;
    //        // move to application header class
    //        AP.Object = OF;

    //        ////////////// DNP /////////////////////////////
    //        DNP_Frame dnpFrame = new DNP_Frame(DL, TH, AP);

    //        // seg management
    //        master.needSEQ = master.runningSEQ;
    //        return dnpFrame.ToRawData();
    //    }

    //    public bool receiveMessage(masterSimulate master, FilteredDNP fdnpFrame)
    //    {
    //        bool respOK = false;
    //        if (fdnpFrame.hasUserdata == false)
    //        {
    //            return false;
    //        }
    //        if ((fdnpFrame.Application_functioncode == 129)  // response
    //            && (fdnpFrame.SEQ == master.needSEQ))        
    //        {
    //            respOK = true;
    //            master.changeState();
    //            if (master.runningSEQ == 15)
    //            {
    //                master.runningSEQ = 0;
    //            }
    //            else
    //            {
    //                master.runningSEQ++;
    //            }
    //        }
    //        return respOK;
    //    }

    //    public void changeState(masterSimulate master)
    //    {
    //        master.masterState = new DelayMeasurementState();
    //    }
    //}
    //#endregion

    //#region DelayMeasurement
    //public class DelayMeasurementState : masterState
    //{
    //    public byte[] sendMessage(masterSimulate master)
    //    {
    //        ////////////// data link layer /////////////////////////////
    //        DataLink_Header DL = new DataLink_Header();
    //        DL.Control = new ControlByte(0xC4); // user data (unconfirm);
    //        DL.Destination = (uint)master.RTUAddress;
    //        DL.Source = (uint)master.MasterAddress;

    //        ////////////// transport /////////////////////////////
    //        Transport_Header TH = new Transport_Header(0xc0); // transport alway 0xc4 for master

    //        ///////////// Application Header /////////////////////////////
    //        Application_Header AP = new Application_Header();
    //        // Application Control
    //        ApplicationControl AC = new ApplicationControl();
    //        AC.FIN = true;
    //        AC.FIR = true;
    //        AC.SEQ = (byte)master.runningSEQ;
    //        AP.ApplicationControl = AC;
    //        AP.FunctionCode = 23; // delay measurement
    //        ////////////// DNP /////////////////////////////
    //        DNP_Frame dnpFrame = new DNP_Frame(DL, TH, AP);

    //        // seg management
    //        master.needSEQ = master.runningSEQ;
    //        return dnpFrame.ToRawData();
    //    }

    //    public bool receiveMessage(masterSimulate master, FilteredDNP fdnpFrame)
    //    {
    //        bool respOK = false;
    //        if (fdnpFrame.hasUserdata == false)
    //        {
    //            return false;
    //        }
    //        if ((fdnpFrame.Application_functioncode == 129)  // response
    //            && (fdnpFrame.SEQ == master.needSEQ))
    //        {
    //            if (fdnpFrame.dataFromObject.Length == 1)
    //            {
    //                if (fdnpFrame.dataFromObject[0].ObjectGroup == 52)
    //                {
    //                    if (fdnpFrame.dataFromObject[0].ObjectVariation == 1)
    //                    {
    //                        if (fdnpFrame.dataFromObject[0].Data.Length > 0)
    //                        {
    //                            O52V1 delay = (O52V1)fdnpFrame.dataFromObject[0].Data[0].ObjData;
    //                            master.SecDelay = delay.Second;
    //                            master.getDelay_Sec = true;
    //                        }
    //                        else
    //                        {
    //                            return false;
    //                        }
    //                    }
    //                    else if (fdnpFrame.dataFromObject[0].ObjectVariation == 2)
    //                    {
    //                        if (fdnpFrame.dataFromObject[0].Data.Length > 0)
    //                        {
    //                            O52V2 delay = (O52V2)fdnpFrame.dataFromObject[0].Data[0].ObjData;
    //                            master.mSecDelay = delay.mSecond;
    //                            master.getDelay_mSec = true;
    //                        }
    //                        else
    //                        {
    //                            return false;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        return false;
    //                    }
    //                }
    //                else
    //                {
    //                    return false;
    //                }
    //            }
    //            else
    //            {
    //                return false;
    //            }
    //            respOK = true;
    //            master.changeState();
    //            if (master.runningSEQ == 15)
    //            {
    //                master.runningSEQ = 0;
    //            }
    //            else
    //            {
    //                master.runningSEQ++;
    //            }
    //        }
    //        return respOK;
    //    }

    //    public void changeState(masterSimulate master)
    //    {
    //        master.masterState = new WriteTimetState();
    //    }
    //}
    //#endregion

    //#region WriteTime
    //public class WriteTimetState : masterState
    //{
    //    public byte[] sendMessage(masterSimulate master)
    //    {
    //        DateTime dt = new DateTime();
    //        // add delay conpensation;
    //        dt = DateTime.Now;
    //        if (master.getDelay_Sec)
    //        {
    //            dt.AddSeconds(master.SecDelay);
    //        }
    //        if (master.getDelay_mSec)
    //        {
    //            dt.AddMilliseconds(master.mSecDelay);
    //        }
    //        // clear time delay flag
    //        master.getDelay_Sec = false;
    //        master.getDelay_mSec = false;

    //        ////////////// data link layer /////////////////////////////
    //        DataLink_Header DL = new DataLink_Header();
    //        DL.Control = new ControlByte(0xC4); // user data (unconfirm);
    //        DL.Destination = (uint)master.RTUAddress;
    //        DL.Source = (uint)master.MasterAddress;

    //        ////////////// transport /////////////////////////////
    //        Transport_Header TH = new Transport_Header(0xc0); // transport alway 0xc4 for master

    //        ///////////// Application Header /////////////////////////////
    //        Application_Header AP = new Application_Header();
    //        // Application Control
    //        ApplicationControl AC = new ApplicationControl();
    //        AC.FIN = true;
    //        AC.FIR = true;
    //        AC.SEQ = (byte)master.runningSEQ;
    //        AP.ApplicationControl = AC;
    //        AP.FunctionCode = 2; // Write
    //        // Object Field
    //        Object_Field[] OF = new Object_Field[1]; // 1 object
    //        Object_Header OH;
    //        Qualifier Q;
    //        ObjectData OD;
    //        // Object Header 1 O:60 V:2
    //        OH = new Object_Header();
    //        OH.ObjectGroup = 50; //Object 
    //        OH.ObjectVariation = 1; // Var
    //        Q = new Qualifier(0x07); // all point range
    //        OH.Qualifier = Q;
    //        OH.Count = 1;
    //        OF[0] = new Object_Field();
    //        OF[0].ObjectHeader = OH;  // Object Header
    //        OD = new ObjectData(); //Data
    //        OD.ObjData = new O50V1(dt);
    //        OF[0].Obj = new ObjectData[1];
    //        OF[0].Obj[0] = OD;
    //        // move to application header class
    //        AP.Object = OF;

    //        ////////////// DNP /////////////////////////////
    //        DNP_Frame dnpFrame = new DNP_Frame(DL, TH, AP);

    //        // seg management
    //        master.needSEQ = master.runningSEQ;
    //        return dnpFrame.ToRawData();
    //    }

    //    public bool receiveMessage(masterSimulate master, FilteredDNP fdnpFrame)
    //    {
    //        bool respOK = false;
    //        if (fdnpFrame.hasUserdata == false)
    //        {
    //            return false;
    //        }
    //        if ((fdnpFrame.Application_functioncode == 129)  // response
    //            && (fdnpFrame.SEQ == master.needSEQ))
    //        {
    //            respOK = true;
    //            master.changeState();
    //            if (master.runningSEQ == 15)
    //            {
    //                master.runningSEQ = 0;
    //            }
    //            else
    //            {
    //                master.runningSEQ++;
    //            }
    //        }
    //        return respOK;
    //    }

    //    public void changeState(masterSimulate master)
    //    {
    //        master.masterState = new PollClass1State();
    //    }
    //}
    //#endregion

    //#region PollClass1
    //public class PollClass1State : masterState
    //{
    //    public byte[] sendMessage(masterSimulate master)
    //    {
    //        ////////////// data link layer /////////////////////////////
    //        DataLink_Header DL = new DataLink_Header();
    //        DL.Control = new ControlByte(0xC4); // user data (unconfirm);
    //        DL.Destination = (uint)master.RTUAddress;
    //        DL.Source = (uint)master.MasterAddress;

    //        ////////////// transport /////////////////////////////
    //        Transport_Header TH = new Transport_Header(0xc0); // transport alway 0xc4 for master

    //        ///////////// Application Header /////////////////////////////
    //        Application_Header AP = new Application_Header();
    //        // Application Control
    //        ApplicationControl AC = new ApplicationControl();
    //        AC.FIN = true;
    //        AC.FIR = true;
    //        if (master.SteadyState)
    //        {
    //            AC.SEQ = (byte)master.PollClass1SEQ;
    //        }
    //        else
    //        {
    //            AC.SEQ = (byte)master.runningSEQ;
    //        }
    //        AP.ApplicationControl = AC;
    //        AP.FunctionCode = 01; // read
    //        // Object Field
    //        Object_Field[] Obj = new Object_Field[1];
    //        Object_Header OH;
    //        Qualifier Q;
    //        // Object Header 1 O:60 V:2
    //        OH = new Object_Header();
    //        OH.ObjectGroup = 60; //Object 
    //        OH.ObjectVariation = 2; // Var
    //        Q = new Qualifier(0x06); // all point range
    //        OH.Qualifier = Q;
    //        Obj[0] = new Object_Field();
    //        Obj[0].ObjectHeader = OH;  // Object Header
    //        // move to application header class
    //        AP.Object = Obj;

    //        ////////////// DNP /////////////////////////////
    //        DNP_Frame dnpFrame = new DNP_Frame(DL, TH, AP);

    //        // seg management
    //        if (master.SteadyState == false)
    //        {
    //            master.needSEQ = master.runningSEQ;
    //        }
    //        else
    //        {
    //            master.needSEQ = master.PollClass1SEQ;
    //        }
          
    //        return dnpFrame.ToRawData();
    //    }

    //    public bool receiveMessage(masterSimulate master, FilteredDNP fdnpFrame)
    //    {
    //        bool respOK = false;
    //        if (fdnpFrame.hasUserdata == false)
    //        {
    //            return false;
    //        }
    //        if ((fdnpFrame.Application_functioncode == 129)
    //            && ((fdnpFrame.SEQ == master.needSEQ)||
    //            ((master.SteadyState) && (fdnpFrame.SEQ == master.PollClass1SEQ))))
    //        {
    //            /* for test *//*
    //            master.getUpdateIO = true; // send flag to master
    //            master.needConfirm = true; // send flag to need confirm
    //            respOK = true;*/
    //            for (int i = 0; i < fdnpFrame.dataFromObject.Length; i++) // scan data each Object Header 
    //            {
    //                int Group = fdnpFrame.dataFromObject[i].ObjectGroup;
    //                int Var = fdnpFrame.dataFromObject[i].ObjectVariation;
    //                if (Group == 2) // digital event change
    //                {
    //                    if (master.SteadyState)
    //                    {
    //                        master.PollClass1IO = true;
    //                    }
    //                    else
    //                    {
    //                        master.getUpdateIO = true; // send flag to master
    //                    }
    //                    master.needConfirm = true; // send flag to need confirm
    //                    respOK = true;
    //                }
    //            }
    //            if (master.getUpdateIO == false) // change state if no IO
    //            {
    //                respOK = true;
    //                if (master.SteadyState == false)
    //                {
    //                    master.changeState();
    //                }
    //            }
    //            if (!master.SteadyState)
    //            {
    //                if (master.runningSEQ == 15)
    //                {
    //                    master.runningSEQ = 0;
    //                }
    //                else
    //                {
    //                    master.runningSEQ++;
    //                }
    //            }
    //        }
    //        return respOK;
    //    }

    //    public void changeState(masterSimulate master)
    //    {
    //        master.masterState = new PollClass2State();
    //    }
    //}
    //#endregion

    //#region PollClass2
    //public class PollClass2State : masterState
    //{
    //    public byte[] sendMessage(masterSimulate master)
    //    {
    //        ////////////// data link layer /////////////////////////////
    //        DataLink_Header DL = new DataLink_Header();
    //        DL.Control = new ControlByte(0xC4); // user data (unconfirm);
    //        DL.Destination = (uint)master.RTUAddress;
    //        DL.Source = (uint)master.MasterAddress;

    //        ////////////// transport /////////////////////////////
    //        Transport_Header TH = new Transport_Header(0xc0); // transport alway 0xc4 for master

    //        ///////////// Application Header /////////////////////////////
    //        Application_Header AP = new Application_Header();
    //        // Application Control
    //        ApplicationControl AC = new ApplicationControl();
    //        AC.FIN = true;
    //        AC.FIR = true;
    //        if (master.SteadyState)
    //        {
    //            AC.SEQ = (byte)master.PollClass2SEQ;
    //        }
    //        else
    //        {
    //            AC.SEQ = (byte)master.runningSEQ;
    //        }
    //        AP.ApplicationControl = AC;
    //        AP.FunctionCode = 01; // read
    //        // Object Field
    //        Object_Field[] Obj = new Object_Field[1];
    //        Object_Header OH;
    //        Qualifier Q;
    //        // Object Header 1 O:60 V:2
    //        OH = new Object_Header();
    //        OH.ObjectGroup = 60; //Object 
    //        OH.ObjectVariation = 3; // Var
    //        Q = new Qualifier(0x06); // all point range
    //        OH.Qualifier = Q;
    //        Obj[0] = new Object_Field();
    //        Obj[0].ObjectHeader = OH;  // Object Header
    //        // move to application header class
    //        AP.Object = Obj;

    //        ////////////// DNP /////////////////////////////
    //        DNP_Frame dnpFrame = new DNP_Frame(DL, TH, AP);

    //        // seg management
    //        if (master.SteadyState == false)
    //        {
    //            master.needSEQ = master.runningSEQ;
    //        }
    //        else
    //        {
    //            master.needSEQ = master.PollClass2SEQ;
    //        }
    //        return dnpFrame.ToRawData();
    //    }

    //    public bool receiveMessage(masterSimulate master, FilteredDNP fdnpFrame)
    //    {
    //        bool respOK = false;
    //        if (fdnpFrame.hasUserdata == false)
    //        {
    //            return false;
    //        }
    //        if ((fdnpFrame.Application_functioncode == 129)
    //            && ((fdnpFrame.SEQ == master.needSEQ)|| 
    //            ((master.SteadyState)&&(fdnpFrame.SEQ == master.PollClass2SEQ))))
    //        {
    //            for (int i = 0; i < fdnpFrame.dataFromObject.Length; i++) // scan data each Object Header 
    //            {
    //                int Group = fdnpFrame.dataFromObject[i].ObjectGroup;
    //                int Var = fdnpFrame.dataFromObject[i].ObjectVariation;
    //                if (Group == 32) // analog event change
    //                {
    //                    if (master.SteadyState)
    //                    {
    //                        master.PollClass2IO = true;
    //                    }
    //                    else
    //                    {
    //                        master.getUpdateIO = true; // send flag to master
    //                    }
    //                    master.needConfirm = true; // send flag to need confirm
    //                    respOK = true;
    //                }
    //            }
    //            if (master.getUpdateIO == false) // change state if no IO
    //            {
    //                respOK = true;
    //                if (master.SteadyState == false)
    //                {
    //                    master.changeState();
    //                }
    //            }
    //            if (!master.SteadyState)
    //            {
    //                if (master.runningSEQ == 15)
    //                {
    //                    master.runningSEQ = 0;
    //                }
    //                else
    //                {
    //                    master.runningSEQ++;
    //                }
    //            }
    //        }
    //        return respOK;
    //    }

    //    public void changeState(masterSimulate master)
    //    {
    //        master.masterState = new PollDigitalState();
    //    }
    //}
    //#endregion

    //#region PollDigital
    //public class PollDigitalState : masterState
    //{
    //    public byte[] sendMessage(masterSimulate master)
    //    {
    //        ////////////// data link layer /////////////////////////////
    //        DataLink_Header DL = new DataLink_Header();
    //        DL.Control = new ControlByte(0xC4); // user data (unconfirm);
    //        DL.Destination = (uint)master.RTUAddress;
    //        DL.Source = (uint)master.MasterAddress;

    //        ////////////// transport /////////////////////////////
    //        Transport_Header TH = new Transport_Header(0xc0); // transport alway 0xc4 for master

    //        ///////////// Application Header /////////////////////////////
    //        Application_Header AP = new Application_Header();
    //        // Application Control
    //        ApplicationControl AC = new ApplicationControl();
    //        AC.FIN = true;
    //        AC.FIR = true;
    //        if (master.SteadyState)
    //        {
    //            AC.SEQ = (byte)master.PollDigitalSEQ;
    //        }
    //        else
    //        {
    //            AC.SEQ = (byte)master.runningSEQ;
    //        }
    //        AP.ApplicationControl = AC;
    //        AP.FunctionCode = 01; // read
    //        // Object Field
    //        Object_Field[] Obj = new Object_Field[1];
    //        Object_Header OH;
    //        Qualifier Q;
    //        // Object Header 1 O:1 V:0
    //        OH = new Object_Header();
    //        OH.ObjectGroup = 1; //Object 
    //        OH.ObjectVariation = 0; // Var
    //        Q = new Qualifier(0x06); // all point range
    //        OH.Qualifier = Q;
    //        Obj[0] = new Object_Field();
    //        Obj[0].ObjectHeader = OH;  // Object Header
    //        // move to application header class
    //        AP.Object = Obj;

    //        ////////////// DNP /////////////////////////////
    //        DNP_Frame dnpFrame = new DNP_Frame(DL, TH, AP);

    //        // seg management
    //        if (master.SteadyState == false)
    //        {
    //            master.needSEQ = master.runningSEQ;
    //        }
    //        else
    //        {
    //            master.needSEQ = master.PollDigitalSEQ;
    //        }
    //        return dnpFrame.ToRawData();
    //    }

    //    public bool receiveMessage(masterSimulate master, FilteredDNP fdnpFrame)
    //    {
    //        bool respOK = false;
    //        if (fdnpFrame.hasUserdata == false)
    //        {
    //            return false;
    //        }
    //        if ((fdnpFrame.Application_functioncode == 129)
    //            && ((fdnpFrame.SEQ == master.needSEQ)||
    //            ((master.SteadyState) && (fdnpFrame.SEQ == master.PollDigitalSEQ))))
    //        {
    //            if (master.SteadyState)
    //            {
    //                master.PollDigitalIO = true;
    //            }
    //            else
    //            {
    //                master.getUpdateIO = true; // send flag to master
    //            }
    //            respOK = true;
    //            if (master.SteadyState == false)
    //            {
    //                master.changeState();
    //            }
    //            if (!master.SteadyState)
    //            {
    //                if (master.runningSEQ == 15)
    //                {
    //                    master.runningSEQ = 0;
    //                }
    //                else
    //                {
    //                    master.runningSEQ++;
    //                }
    //            }
    //        }
    //        return respOK;
    //    }

    //    public void changeState(masterSimulate master)
    //    {
    //        master.masterState = new PollAnalogState();
    //    }
    //}
    //#endregion

    //#region PollAnalog
    //public class PollAnalogState : masterState
    //{
    //    public byte[] sendMessage(masterSimulate master)
    //    {
    //        ////////////// data link layer /////////////////////////////
    //        DataLink_Header DL = new DataLink_Header();
    //        DL.Control = new ControlByte(0xC4); // user data (unconfirm);
    //        DL.Destination = (uint)master.RTUAddress;
    //        DL.Source = (uint)master.MasterAddress;

    //        ////////////// transport /////////////////////////////
    //        Transport_Header TH = new Transport_Header(0xc0); // transport alway 0xc4 for master

    //        ///////////// Application Header /////////////////////////////
    //        Application_Header AP = new Application_Header();
    //        // Application Control
    //        ApplicationControl AC = new ApplicationControl();
    //        AC.FIN = true;
    //        AC.FIR = true;
    //        if (master.SteadyState)
    //        {
    //            AC.SEQ = (byte)master.PollAnalogSEQ;
    //        }
    //        else
    //        {
    //            AC.SEQ = (byte)master.runningSEQ;
    //        }
    //        AP.ApplicationControl = AC;
    //        AP.FunctionCode = 01; // read
    //        // Object Field
    //        Object_Field[] Obj = new Object_Field[1];
    //        Object_Header OH;
    //        Qualifier Q;
    //        // Object Header 1 O:30 V:2
    //        OH = new Object_Header();
    //        OH.ObjectGroup = 30; //Object 
    //        OH.ObjectVariation = 2; // Var
    //        Q = new Qualifier(0x06); // all point range
    //        OH.Qualifier = Q;
    //        Obj[0] = new Object_Field();
    //        Obj[0].ObjectHeader = OH;  // Object Header
    //        // move to application header class
    //        AP.Object = Obj;

    //        ////////////// DNP /////////////////////////////
    //        DNP_Frame dnpFrame = new DNP_Frame(DL, TH, AP);

    //        // seg management
    //        if (master.SteadyState == false)
    //        {
    //            master.needSEQ = master.runningSEQ;
    //        }
    //        else
    //        {
    //            master.needSEQ = master.PollAnalogSEQ;
    //        }
    //        return dnpFrame.ToRawData();
    //    }

    //    public bool receiveMessage(masterSimulate master, FilteredDNP fdnpFrame)
    //    {
    //        bool respOK = false;
    //        if (fdnpFrame.hasUserdata == false)
    //        {
    //            return false;
    //        }
    //        if ((fdnpFrame.Application_functioncode == 129)
    //            && ((fdnpFrame.SEQ == master.needSEQ)||
    //            ((master.SteadyState) && (fdnpFrame.SEQ == master.PollAnalogSEQ))))
    //        {
    //            if (master.SteadyState)
    //            {
    //                master.PollAnalogIO = true;
    //            }
    //            else
    //            {
    //                master.getUpdateIO = true; // send flag to master
    //            }
    //            respOK = true;
    //            if (master.SteadyState == false)
    //            {
    //                master.changeState();
    //            }
    //            if (!master.SteadyState)
    //            {
    //                if (master.runningSEQ == 15)
    //                {
    //                    master.runningSEQ = 0;
    //                }
    //                else
    //                {
    //                    master.runningSEQ++;
    //                }
    //            }
    //        }
    //        return respOK;
    //    }

    //    public void changeState(masterSimulate master)
    //    {
    //        master.masterState = new EnableUnsolicitedState();
    //    }
    //}
    //#endregion

    //#region EnableUnsolicited
    //public class EnableUnsolicitedState : masterState
    //{
    //    public byte[] sendMessage(masterSimulate master)
    //    {
    //        ////////////// data link layer /////////////////////////////
    //        DataLink_Header DL = new DataLink_Header();
    //        DL.Control = new ControlByte(0xC4); // user data (unconfirm);
    //        DL.Destination = (uint)master.RTUAddress;
    //        DL.Source = (uint)master.MasterAddress;

    //        ////////////// transport /////////////////////////////
    //        Transport_Header TH = new Transport_Header(0xc0); // transport alway 0xc4 for master

    //        ///////////// Application Header /////////////////////////////
    //        Application_Header AP = new Application_Header();
    //        // Application Control
    //        ApplicationControl AC = new ApplicationControl();
    //        AC.FIN = true;
    //        AC.FIR = true;
    //        AC.SEQ = (byte)master.runningSEQ;
    //        AP.ApplicationControl = AC;
    //        AP.FunctionCode = 20; // Enable UNS msg function

    //        // Object Field
    //        Object_Header OH;
    //        Qualifier Q;
    //        List<Object_Field> Obj = new List<Object_Field>();
    //        if (master.UnsolicatedClass1) // Class 1
    //        {
    //            OH = new Object_Header();
    //            OH.ObjectGroup = 60; //Object 
    //            OH.ObjectVariation = 2; // Var
    //            Q = new Qualifier(0x06); // all point range
    //            OH.Qualifier = Q;
    //            Object_Field Obj1 = new Object_Field();
    //            Obj1.ObjectHeader = OH;
    //            Obj.Add(Obj1);
    //        }
    //        if (master.UnsolicatedClass2) // Class 2
    //        {
    //            OH = new Object_Header();
    //            OH.ObjectGroup = 60; //Object 
    //            OH.ObjectVariation = 3; // Var
    //            Q = new Qualifier(0x06); // all point range
    //            OH.Qualifier = Q;
    //            Object_Field Obj2 = new Object_Field();
    //            Obj2.ObjectHeader = OH;
    //            Obj.Add(Obj2);
    //        }
    //        // move to application header class
    //        AP.Object = Obj.ToArray();

    //        ////////////// DNP /////////////////////////////
    //        DNP_Frame dnpFrame = new DNP_Frame(DL, TH, AP);

    //        // seg management
    //        master.needSEQ = master.runningSEQ;
    //        return dnpFrame.ToRawData();
    //    }

    //    public bool receiveMessage(masterSimulate master, FilteredDNP fdnpFrame)
    //    {
    //        bool respOK = false;
    //        if (fdnpFrame.hasUserdata == false)
    //        {
    //            return false;
    //        }
    //        if ((fdnpFrame.Application_functioncode == 129)
    //            && (fdnpFrame.SEQ == master.needSEQ))
    //        {
    //            respOK = true;
    //            if (master.UnsolicatedClass1) // Class 1
    //            {
    //                master.EnableUns1 = true;
    //            }
    //            if (master.UnsolicatedClass2) // Class 2
    //            {
    //                master.EnableUns1 = true;
    //            }
    //            master.changeState();
    //            if (master.runningSEQ == 15)
    //            {
    //                master.runningSEQ = 0;
    //            }
    //            else
    //            {
    //                master.runningSEQ++;
    //            }
    //        }
    //        return respOK;
    //    }

    //    public void changeState(masterSimulate master)
    //    {
    //        master.SteadyState = true;
    //    }
    //}
    //#endregion
}
