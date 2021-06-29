using System;
using System.Net.Http;

namespace RS3SampleCode.DTOs
{
    public class EmvConfigTokenResponse
    {
        /// <summary>
        /// Magensa Transaction ID.
        /// </summary>
        public string MagTranID { get; set; }

        /// <summary>
        /// Customer Transaction ID.
        /// </summary>
        public string CustomerTransactionID { get; set; }

        /// <summary>
        /// Generated EMV configuration token.
        /// </summary>
        public string Token { get; set; }       
    }
}
