using System;
using System.Collections.Generic;
using System.Text;

namespace RS3SampleCode.DTOs
{
    public class RSError
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
        /// Error code.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Error message.
        /// </summary>
        public string Message { get; set; }

    }
}
