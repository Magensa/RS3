using System;
using System.Collections.Generic;
using System.Text;

namespace RS3SampleCode.DTOs
{
    public class GetKeyListResponse
    {
        public string MagTranID { get; set; }

        public string CustomerTransactionID { get; set; }

        public KeyInfo[] Keys { get; set; }
    }
}
