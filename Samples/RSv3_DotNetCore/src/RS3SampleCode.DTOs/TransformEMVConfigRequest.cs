using System;
using System.Collections.Generic;
using System.Text;

namespace RS3SampleCode.DTOs
{
    public class TransformEMVConfigRequest
    {
        /// <summary>
        /// The protocol/class of the device. Currently support Apollo only.
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// Base64 encoded configuration from excel file.
        /// </summary>
        public string FileBase64 { get; set; }

        /// <summary>
        /// Flag to indicate whether the input is from legacy excel (.XLS) or latest version (.XLSX).
        /// </summary>
        public bool IsLegacyExcel { get; set; }
    }
}
