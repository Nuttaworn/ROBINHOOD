using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNP3Lib
{
    static class CRC
    {
        public static long [] tableCRC = new long[256];
        public static int[] crc_tabdnp = new int[256];

        public static byte[] genCRCtoRaw(byte[] data)
        {
            int crc = genCRC(data);
            byte[] CRC = new byte[2];
            CRC[1] = (byte)(crc & 0xFF);
            CRC[0] = (byte)(crc >> 8);
            return CRC;
        }

        public static int genCRC(byte[] data)
        {
            UInt16 crc = 0;
            for (int i = 0; i < data.Length; i++)
            {
                crc = update_crc_dnp(crc, data[i]);
            }
            UInt16 a = (UInt16)(0xFFFF - crc);
            int b = CRC.swapInt(a);
            return b;
        }
        
        public static UInt16 update_crc_dnp( UInt16 crc, byte c ) 
        {
            UInt16 tmp, UInt16_c;

            UInt16_c = (UInt16)(0x00ff & (UInt16)c);

            tmp = (UInt16)(crc ^ UInt16_c);
            crc = (UInt16)((crc >> 8) ^ crc_tabdnp[tmp & 0xff]);
            return crc;
        }

        public static int swapInt (UInt16 crc)
        {
            UInt16 tmp = (UInt16)(crc & 0x00ff);
            crc = (UInt16)(crc >> 8);
            tmp = (UInt16)(tmp << 8);
            crc = (UInt16)(tmp + crc);
            return (int)crc;
        }

       public static void init_crcdnp_tab() 
       {
            int i, j;
            int crc, c;
            for (i=0; i<256; i++) 
            {
                crc = 0;
                c   = i;
                for (j=0; j<8; j++) 
                {
                    if (((crc ^ c) & 0x0001) == 1) crc = (crc >> 1) ^ 0xA6BC;
                    else                      crc =   crc >> 1;
                    c = c >> 1;
                }
                crc_tabdnp[i] = crc;
            }
        }

       public static bool dnpCheckCRC(byte[] rawdata)
       {
           if (rawdata.Length < 10)
           {
               return false;
           }
           else
           {
               // data link data
               byte[] temp = new byte[8];
               for (int i = 0; i < 8; i++)
               {
                   temp[i] = rawdata[i];
               }
               int crctemp = (rawdata[8] * 0x100) + rawdata[9];
               int resulttemp = genCRC(temp);
               if (crctemp != resulttemp) // crc fail !!
               {
                   return false;
               }
               else
               {
                   if (rawdata.Length > 10) // has userdata
                   {
                       temp = new byte[rawdata.Length - 10];
                       for (int i = 0; i < rawdata.Length - 10; i++)
                       {
                           temp[i] = rawdata[i + 10];
                       }
                       int count;
                       if ((temp.Length % 18) == 0)
                       {
                           count = temp.Length / 18;
                       }
                       else count = (temp.Length / 18) + 1;
                       int rawdataindex = 0;
                       byte[] word = new byte[16];
                       for (int j = 0; j < count; j++)
                       {
                           if ((temp.Length - rawdataindex) >= 18)
                           {
                               for (int k = 0; k < 16; k++, rawdataindex++)
                               {
                                   word[k] = temp[rawdataindex];
                               }
                               crctemp = (temp[rawdataindex] * 0x100) + temp[rawdataindex+1];
                               rawdataindex += 2;
                               resulttemp = genCRC(word);
                               if (crctemp != resulttemp) // crc fail !!
                               {
                                   return false;
                               }
                           }
                           else
                           {
                               try
                               {
                                   word = new byte[(temp.Length - 2) - rawdataindex];
                               }
                               catch
                               {
                                   return false;
                               }
                               for (int k = 0; k < word.Length; k++, rawdataindex++)
                               {
                                   word[k] = temp[rawdataindex];
                               }
                               crctemp = (temp[rawdataindex] * 0x100) + temp[rawdataindex + 1];
                               resulttemp = genCRC(word);
                               if (crctemp != resulttemp) // crc fail !!
                               {
                                   return false;
                               }
                           }
                       }
                       return true;
                   }
                   else return true;
               }
           }
       }

       public static byte[] putCRC(byte[] userdata)
       {
           int index = 0;
           int outindex = 0;
           int count;
           if ((userdata.Length % 16) == 0)
           {
               count = userdata.Length / 16;
           }
           else count = (userdata.Length / 16) + 1;
           byte [] output = new byte[userdata.Length + (count*2)];

           while ((userdata.Length - index) >= 16)
           {
               byte[] tempdata = new byte[16];
               for (int i = 0; i < 16; i++)
               {
                   output[outindex] = userdata[index];
                   tempdata[outindex] = userdata[index];
                   index++;
                   outindex++;
               }
               byte[] crctemp = CRC.genCRCtoRaw(tempdata);
               output[outindex] = crctemp[0];
               output[outindex + 1] = crctemp[1];
               outindex += 2;
           }
           byte[] restByte = new byte[userdata.Length - index];
           byte [] temp = new byte[userdata.Length - index];
           for (int j = 0; j < restByte.Length; j++)
           {
               output[outindex] = userdata[index];
               temp[j] = userdata[index];
               index++;
               outindex++;
           }
           byte [] crc = CRC.genCRCtoRaw(temp);
           output[outindex] = crc[0];
           output[outindex + 1] = crc[1];

           return output;
       }
    }
}
