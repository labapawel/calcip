using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _4c_calcip
{

    public class IP
    {
        private int iIP = 0;
        private int iMask = 0;
        public int Network = 0;
        public int Broadcast = 0;
        public int MinHost = 0;
        public int MaxHost = 0;
        public int HostCount = 0;
        public int Mask { get { return iMask; } }
        public int Ip { get { return iIP; } }
        public int lZer = 0;

        public string regIP = @"^([0-9]{1,3})\.([0-9]{1,3})\.([0-9]{1,3})\.([0-9]{1,3})(\/([0-9]{1,2}))?$";
        public IP(string adres, string maska="255.255.255.0")
        {

            Regex rx = new Regex(this.regIP);
            MatchCollection adIP = rx.Matches(adres);
            if (adIP.Count==1 && adIP[0].Success)
            {
                if (adIP[0].Groups.Count == 7 && adIP[0].Groups[6].Value!="")
                {
                    int _maska_ = int.Parse(adIP[0].Groups[6].Value);
                    iMask = 0;
                    for(int i=0; i<32; i++)
                    {
                        iMask <<= 1;
                        iMask |= i<_maska_?1:0;
                    }

                }

               if (adIP[0].Groups.Count == 7 || adIP[0].Groups.Count == 5)
                {
                    for(int i=1; i<5; i++)
                    {
                        this.iIP <<= 8;
                        int num = int.Parse(adIP[0].Groups[i].Value);
                        this.iIP += num; 
                    }

                }
                Network = iIP & iMask;
                Broadcast = Network;
                lZer = 0;
                for (int i = 0; i < 32; i++)
                {
                    if((iMask & (1 << i)) == 0)
                    {
                        lZer++;
                        Broadcast |=  (1 << i);
                    }
                }


                //Multicast |= (iMask & (1 << i)) == 0 ? 1 : 0;

                MinHost = Network + 1;
                MaxHost = Broadcast - 1;
                HostCount = (int) Math.Pow(2, lZer) - 2;

/*                string s = Convert.ToString(Network, 2);
                string s1 = Convert.ToString(iMask, 2);
                string s2 = Convert.ToString(iIP, 2);
                string s3 = Convert.ToString(Multicast, 2);*/
            }

        }
        public string toString(int val, int typ=10)
        {
            string res = "";
            for(int i=3;i>=0; i--)
            {
                if (res != "") res += ".";
                UInt32 tmp =  (UInt32)((0xff << i*8) & val);
                tmp >>= i * 8;
                if (typ == 2)
                    res += Convert.ToString(tmp, 2).PadLeft(8,'0');
                else
                    res += tmp.ToString();
                
            }

            return res;
        }

        public byte octet(int num, int octet)
        {
            int dane = num >> (8 * octet);
            return (byte)(dane & 0xff);
        }
    }
}
