using SnmpSharpNet;
using System;
using System.Net;

namespace FortigateSNMP_SnmpSharpNet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // SNMP community name
            OctetString community = new OctetString("your snmp name here");
            // Define agent parameters class
            AgentParameters param = new AgentParameters(community);
            // Set SNMP version to 1 (or 2)
            param.Version = SnmpVersion.Ver2;
            // Construct the agent address object
            // IpAddress class is easy to use here because
            //  it will try to resolve constructor parameter if it doesn't
            //  parse to an IP address
            IpAddress agent = new IpAddress("your fortigate ip address here");
            // Construct target
            UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);
            // Pdu class used for all requests
            Pdu pdu = new Pdu(PduType.GetBulk);

            pdu.VbList.Add(".1.3.6.1.4.1.12356.101.4.1.1.0"); 
           

            // Make SNMP request
            SnmpV2Packet result = (SnmpV2Packet)target.Request(pdu, param);
            // If result is null then agent didn't reply or we couldn't parse the reply.
            if (result != null)
            {
                // ErrorStatus other then 0 is an error returned by
                // the Agent - see SnmpConstants for error definitions
                if (result.Pdu.ErrorStatus != 0)
                {
                    // agent reported an error with the request
                    Console.WriteLine("Error in SNMP reply. Error {0} index {1}",
                        result.Pdu.ErrorStatus,
                        result.Pdu.ErrorIndex);
                }
                else
                {

                    foreach (var item in result.Pdu.VbList)
                    {
                        Console.WriteLine("sysDescr({0}) ({1}): {2}",
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
