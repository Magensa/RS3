using System;
using System.Collections.Generic;
using System.Text;

namespace RS3SampleCode.DTOs
{
    public class KeyUpdateTokenResponse
    {
        public string MagTranID { get; set; }
        public string CustomerTransactionID { get; set; }
        public string UpdateToken { get; set; }
        public bool IsRawCommand { get; set; }
    }
}
