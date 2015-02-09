using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using SOS.FunctionalServices.Contracts.Models;
using SOS.Lib.Core.ErrorHandling;

namespace SOS.FunctionalServices.Models {
    [Serializable]
    [DataContract(Name = "FnsResult")]
    [DebuggerDisplay("Code = {Code}, PurchaseMessageDescription = {PurchaseMessageDescription}")]
    public class FnsResult : IFnsResult {
        #region .ctor

        public FnsResult() {
            Code = 0;
        }

        public FnsResult(IErrorMessage oMessage) {
            /** Check for PurchaseMessageDescription Type. */
            Code = (oMessage.Type == ErrorMessageType.Success)
                    ? 0
                    : 1;
            Message = oMessage.Message;
        }

        #endregion .ctor

        #region Properties

        [DataMember]
        public object Value { get; set; }

        #endregion Properties

        #region Implementation of IFnsResult

        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Message { get; set; }

        public object GetValue() {
            return Value;
        }

        #endregion Implementation of IFnsResult
    }

    public class FnsResult<TValueType> : IFnsResult<TValueType> {
        #region Implementation of IFnsResult

        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember(Name = "Value", Order = int.MaxValue)]
        public TValueType Value;

        public object GetValue() {
            return Value;
        }
        public TValueType GetTValue() {
            return Value;
        }

        #endregion Implementation of IFnsResult
    }
}