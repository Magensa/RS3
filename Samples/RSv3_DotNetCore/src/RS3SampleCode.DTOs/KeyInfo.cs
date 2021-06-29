using System;
using System.Collections.Generic;
using System.Text;

namespace RS3SampleCode.DTOs
{
    public class KeyInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string KeySlotNamePrefix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string KSI { get; set; }

        /// <summary>
        /// Protocol of the device this key is used for.
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HSM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DerivedKeyType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string KeyTypeRestrictionBitmask { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DUKPTDataTypeRestrictionBitmask { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime DateCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime DateModified { get; set; }
    }
}
