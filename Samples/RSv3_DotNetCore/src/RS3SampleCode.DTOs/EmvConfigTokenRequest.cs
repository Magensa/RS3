using System;
using System.Collections.Generic;
using System.Text;

namespace RS3SampleCode.DTOs
{
    public class EmvConfigTokenRequest
    {
        /// <summary>
        /// The protocol/class of the device. Currently support Apollo only.
        /// </summary>        
        public string Protocol { get; set; }

        /// <summary>
        /// The KSI of the base key to be updated.
        /// </summary>
        public string KSI { get; set; }

        /// <summary>
        /// Key derivation data. 32 hex.
        /// </summary>
        public string KeyDerivationData { get; set; }

        /// <summary>
        /// AES_SP800_108
        /// </summary>
        public string KeyDerivationAlgorithm { get; set; }

        /// <summary>
        /// Device Serial Number.
        /// </summary>
        public string DeviceSN { get; set; }

        /// <summary>
        /// Configuration in HEX.
        /// </summary>
        public string EMVConfigData { get; set; }
    }
}
