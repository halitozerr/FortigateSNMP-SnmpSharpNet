using SnmpSharpNet;
using System;
using System.Net;

namespace FortigateSNMP_SnmpSharpNet
{
    internal class Program
    {
        static void Main(string[] args)
        {
        
            OctetString community = new OctetString("your snmp name here");
      
            AgentParameters param = new AgentParameters(community);
   
            param.Version = SnmpVersion.Ver2;
   
            IpAddress agent = new IpAddress("your fortigate ip address here");
            UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);
            Pdu pdu = new Pdu(PduType.Get);
            //You can find more oids from google
            pdu.VbList.Add(".1.3.6.1.4.1.12356.101.4.1.4.0"); //Bellek Kullanımı
            pdu.VbList.Add(".1.3.6.1.4.1.12356.101.4.1.9.0"); //Bellek Kullanımı
            pdu.VbList.Add(".1.3.6.1.4.1.12356.101.4.1.3.0"); //CPU Kullanımı
            pdu.VbList.Add(".1.3.6.1.4.1.12356.101.4.1.7.0"); //Disk Kapasitesi
            pdu.VbList.Add(".1.3.6.1.4.1.12356.101.4.1.8.0"); //Aktif Session Sayısı
           

          
            SnmpV2Packet result = (SnmpV2Packet)target.Request(pdu, param);
         
            if (result != null)
            {
                if (result.Pdu.ErrorStatus != 0)
                {
                    
                    Console.WriteLine("Error in SNMP reply. Error {0} index {1}",
                        result.Pdu.ErrorStatus,
                        result.Pdu.ErrorIndex);
                }
                else
                {

                    foreach (var item in result.Pdu.VbList)
                    {
                        Console.WriteLine("Descr({0}) ({1}): {2}",
                                              item.Oid.ToString(),
                                               SnmpConstants.GetTypeName(item.Value.Type),
                                               item.Value.ToString()
                                               );
                    }


                }
            }
            else
            {
                Console.WriteLine("No response received from SNMP agent.");
            }
            target.Close();
        }
    }
}
