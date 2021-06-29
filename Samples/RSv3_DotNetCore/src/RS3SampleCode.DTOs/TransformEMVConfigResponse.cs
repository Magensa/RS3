using System;
using System.Collections.Generic;
using System.Text;

namespace RS3SampleCode.DTOs
{
    public class TransformEMVConfigResponse
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
        /// Config Name
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EMVConfigBin[] Bins { get; set; }
    }
}
