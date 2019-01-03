using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNP3Lib
{
    static class typeConvert
    {
        public static String HextoString(uint input)
        {
            String str = null;
            str = Convert.ToString(input, 16);
            return str;
        }

        public static string HextoBinary(string hexvalue)
        {
            string binaryval = "";
            binaryval = Convert.ToString(Convert.ToInt32(hexvalue, 16), 2);
            return binaryval;
        }

        public static string fillZero(String input, byte digit)
        {
            for (int i = input.Length; i < digit; i++)
            {
                input = String.Format("0{0}", input);
            }
            return input;
        }

        private static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        public static byte[] DataToRaw(Int64 data, int count)
        {
            byte[] rawdata = new byte[count];
            for (int i = 0; i < count; i++)
            {
                rawdata[i] = (byte)(data & 0xFF);
                data = data >> 8;
            }
            return rawdata;
        }

        public static byte[] uint32toBCD(uint input,uint digit)
        {
            byte[] BCD = new byte[10];
            if(digit <= 10)
            {
                BCD = new byte[digit];
            }
            return BCD;
        }
    }

    public class TimeCalculator
    {
        public int Day;
        public int Month;
        public int Year;
        public int Hour;
        public int Min;
        public int Sec;
        public int mSec;
        public bool isLeapYear;
        // raw data
        public UInt64 Time;

        const int startYear = 1970;
        const int startMonth = 1;
        const int startDay = 1;
        const int m_Sec = 1000;
        const int m_Min = m_Sec * 60;
        const int m_Hour = m_Min * 60;
        const UInt64 m_Day = m_Hour * 24;
        const UInt64 m_Year = m_Day * 365;
        const UInt64 m_LeapYear = m_Day * 366;
        const UInt64 mSec4years = (m_Year * 3) + m_LeapYear;

        int [] Months = {0,31,59,90,120,151,181,212,243,273,304,334,365};
        int [] Months_Leap = {0,31,60,91,121,152,182,213,244,274,305,335,366};

        enum Months2Num : byte
        {
            Jan = 31, 
            Feb = 29, 
            Mar = 31, 
            Arp = 30,
            May = 31, 
            Jun = 30, 
            Jul = 31, 
            Aug = 31, 
            Sep = 30, 
            Oct = 31, 
            Nov = 30, 
            Dec = 31
        };

        public TimeCalculator(UInt64 time)
        {
            int tempyear = 0;
            int tempmonth = 0;
            UInt64 remaintime = time;
            // 4 year level
            while (remaintime > mSec4years) 
            {
                remaintime -= mSec4years;
                tempyear += 4;
            }
            // year level
            while (remaintime > m_Year) 
            {
                remaintime -= m_Year;
                tempyear++;
            }
            this.Year = startYear + tempyear;
            if((this.Year % 4) == 0) this.isLeapYear = true;
            // month
            int temp = (int)(remaintime / m_Day);
            int daypasstemp = 0;
            if (this.isLeapYear) // is leap year
            {
                for (int i = 1; this.Months_Leap[i] <= temp; i++)
                {
                    tempmonth++;
                    daypasstemp = this.Months_Leap[i];
                }
            }
            else // not Leap year
            {
                for (int i = 1; this.Months[i] <= temp; i++)
                {
                    tempmonth++;
                    daypasstemp = this.Months[i];
                }
            }
            this.Month = startMonth + tempmonth;
            remaintime -= ((UInt64)daypasstemp * m_Day);
            //day
            this.Day = startDay + (int)(remaintime / m_Day);
            remaintime %= m_Day;
            //hour
            this.Hour = (int)(remaintime / m_Hour);
            remaintime %= m_Hour;
            //Min
            this.Min = (int)(remaintime / m_Min);
            remaintime %= m_Min;
            //Sec
            this.Sec = (int)(remaintime / m_Sec);
            remaintime %= m_Sec;
            //mSec
            this.mSec = (int)remaintime;
        }

        public TimeCalculator(DateTime dt)
        {
            UInt64 time = 0;
            // Year
            if (dt.Year > 1972)
            {
                UInt64 count_4years = (UInt64)(dt.Year - 1972) / 4;
                UInt64 count_remainyears = (UInt64)(dt.Year - 1972) % 4;
                time += (count_4years * (mSec4years)) + (count_remainyears * m_Year);
            }
            time += 2 * m_Year; // + 2 first year;

            //day hour min sec msec
            UInt64 day;
            if ((dt.Year % 4) < 2)
            {
                day = (UInt64)(dt.DayOfYear - 1) * m_Day;
            }
            else
            {
                day = (UInt64)(dt.DayOfYear) * m_Day;
            }
            UInt64 hour = (UInt64)dt.Hour * m_Hour;
            UInt64 min = (UInt64)dt.Minute * m_Min;
            UInt64 sec = (UInt64)dt.Second * m_Sec;
            UInt64 msec = (UInt64)dt.Millisecond;
            time += day + hour + min + sec + msec;
            this.Time = time;
        }

        public override string ToString()
        {
            string str = null;
            str += this.Day + "/" + this.Month + "/" + this.Year + " "
                + this.Hour + ":" + this.Min + ":" + this.Sec + "." + this.mSec;
            return str;
        }
    }
}
