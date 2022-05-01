# FortigateSNMP-SnmpSharpNet
Retrieve data from fortigate devices using SNMP and C#.  

## Used Technologies
- .Net Core 5.0
- SnmpSharpNet 0.9.5
- Console Application
- Microsoft.NET.Sdk

OctetString community = new OctetString("your snmp name here"); // You should write your snmp name in this field.

IpAddress agent = new IpAddress("your fortigate ip address here"); //You must first activate the snmp service in the fortigate settings, then write your fortigate ip address in the ip address field.

param.Version = SnmpVersion.Ver2; // You can change snmp version from here. If you want to use ver3, you should use a user authentication settings.

 Pdu pdu = new Pdu(PduType.Get); // This field allow you change pdu type.
