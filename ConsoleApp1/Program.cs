using System.Net;

long ip1 = IpToLong("140.90.206.8");       // Convert IP address to long
long ip2 = IpToLong("140.90.206.1");
long subnetMask = IpToLong("255.255.0.0"); // Convert subnet mask to long

bool result = AreInSameSubnet(ip1, ip2, subnetMask);
Console.WriteLine($"Are in the same subnet: {result}");

// Helper function to convert IP address string to long
static long IpToLong(string ipAddress)
{
    IPAddress ip = IPAddress.Parse(ipAddress);
    byte[] bytes = ip.GetAddressBytes();
    if (BitConverter.IsLittleEndian)
    {
        Array.Reverse(bytes);
    }
    return BitConverter.ToUInt32(bytes, 0);
}

static bool AreInSameSubnet(long ipAddress1, long ipAddress2, long subnetMask)
{
    // Calculate the network portions of the IP addresses
    long network1 = ipAddress1 & subnetMask;
    long network2 = ipAddress2 & subnetMask;

    // Compare the network portions
    return network1 == network2;
}