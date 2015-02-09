using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SOS.Services.Interfaces.Models.CellStation {
    public class SwapModemPost {
        public string NewSerialNumber { get; set; }
        public string SwapReason { get; set; }
        public string SpecialRequest { get; set; }
        public bool RestoreBackedUpSettingsAfterSwap { get; set; }
    }
}
