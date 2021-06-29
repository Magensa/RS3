using System;
using System.Collections.Generic;
using System.Text;

namespace RS3SampleCode.DTOs
{
    public class KeyUpdateTokenRequest
    {
        public string Protocol { get; set; }
        public string ProductName { get; set; }
        public string CurrentKSN { get; set; }
        public string DeviceChallenge { get; set; }
        public string DeviceSN { get; set; }
        public string KeyDerivationData { get; set; }
        public string KeyRestriction { get; set; }
        public string KeySlotID { get; set; }
        public string TargetKSI { get; set; }
        public string TransportKeyID { get; set; }
    }
}
