﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SOS.FOS.MonitoringStationServices.AGSignalService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Result", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class Result : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int ErrorNumField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorMessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int ErrorNum {
            get {
                return this.ErrorNumField;
            }
            set {
                if ((this.ErrorNumField.Equals(value) != true)) {
                    this.ErrorNumField = value;
                    this.RaisePropertyChanged("ErrorNum");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ErrorMessage {
            get {
                return this.ErrorMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMessageField, value) != true)) {
                    this.ErrorMessageField = value;
                    this.RaisePropertyChanged("ErrorMessage");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AGSignalService.ReceiverSoap")]
    public interface ReceiverSoap {
        
        // CODEGEN: Generating message contract since element name UserName from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Signal", ReplyAction="*")]
        SOS.FOS.MonitoringStationServices.AGSignalService.SignalResponse Signal(SOS.FOS.MonitoringStationServices.AGSignalService.SignalRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SignalRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Signal", Namespace="http://tempuri.org/", Order=0)]
        public SOS.FOS.MonitoringStationServices.AGSignalService.SignalRequestBody Body;
        
        public SignalRequest() {
        }
        
        public SignalRequest(SOS.FOS.MonitoringStationServices.AGSignalService.SignalRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SignalRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public System.Nullable<bool> PollMessageFlag;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string UserName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string UserPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string Receiver;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string Line;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string Account;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string SignalFormat;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string SignalCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string Point;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string Area;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=10)]
        public string UserID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=11)]
        public string Text;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=12)]
        public System.Nullable<System.DateTime> Date;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=13)]
        public string ANIPhone;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=14)]
        public System.Nullable<decimal> Longitude;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=15)]
        public System.Nullable<decimal> Latitude;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=16)]
        public string FileName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=17)]
        public string URL;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=18)]
        public string VideoType;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=19)]
        public System.Nullable<bool> TestSignalFlag;
        
        public SignalRequestBody() {
        }
        
        public SignalRequestBody(
                    System.Nullable<bool> PollMessageFlag, 
                    string UserName, 
                    string UserPassword, 
                    string Receiver, 
                    string Line, 
                    string Account, 
                    string SignalFormat, 
                    string SignalCode, 
                    string Point, 
                    string Area, 
                    string UserID, 
                    string Text, 
                    System.Nullable<System.DateTime> Date, 
                    string ANIPhone, 
                    System.Nullable<decimal> Longitude, 
                    System.Nullable<decimal> Latitude, 
                    string FileName, 
                    string URL, 
                    string VideoType, 
                    System.Nullable<bool> TestSignalFlag) {
            this.PollMessageFlag = PollMessageFlag;
            this.UserName = UserName;
            this.UserPassword = UserPassword;
            this.Receiver = Receiver;
            this.Line = Line;
            this.Account = Account;
            this.SignalFormat = SignalFormat;
            this.SignalCode = SignalCode;
            this.Point = Point;
            this.Area = Area;
            this.UserID = UserID;
            this.Text = Text;
            this.Date = Date;
            this.ANIPhone = ANIPhone;
            this.Longitude = Longitude;
            this.Latitude = Latitude;
            this.FileName = FileName;
            this.URL = URL;
            this.VideoType = VideoType;
            this.TestSignalFlag = TestSignalFlag;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SignalResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SignalResponse", Namespace="http://tempuri.org/", Order=0)]
        public SOS.FOS.MonitoringStationServices.AGSignalService.SignalResponseBody Body;
        
        public SignalResponse() {
        }
        
        public SignalResponse(SOS.FOS.MonitoringStationServices.AGSignalService.SignalResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SignalResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public SOS.FOS.MonitoringStationServices.AGSignalService.Result SignalResult;
        
        public SignalResponseBody() {
        }
        
        public SignalResponseBody(SOS.FOS.MonitoringStationServices.AGSignalService.Result SignalResult) {
            this.SignalResult = SignalResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ReceiverSoapChannel : SOS.FOS.MonitoringStationServices.AGSignalService.ReceiverSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ReceiverSoapClient : System.ServiceModel.ClientBase<SOS.FOS.MonitoringStationServices.AGSignalService.ReceiverSoap>, SOS.FOS.MonitoringStationServices.AGSignalService.ReceiverSoap {
        
        public ReceiverSoapClient() {
        }
        
        public ReceiverSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ReceiverSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReceiverSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReceiverSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SOS.FOS.MonitoringStationServices.AGSignalService.SignalResponse SOS.FOS.MonitoringStationServices.AGSignalService.ReceiverSoap.Signal(SOS.FOS.MonitoringStationServices.AGSignalService.SignalRequest request) {
            return base.Channel.Signal(request);
        }
        
        public SOS.FOS.MonitoringStationServices.AGSignalService.Result Signal(
                    System.Nullable<bool> PollMessageFlag, 
                    string UserName, 
                    string UserPassword, 
                    string Receiver, 
                    string Line, 
                    string Account, 
                    string SignalFormat, 
                    string SignalCode, 
                    string Point, 
                    string Area, 
                    string UserID, 
                    string Text, 
                    System.Nullable<System.DateTime> Date, 
                    string ANIPhone, 
                    System.Nullable<decimal> Longitude, 
                    System.Nullable<decimal> Latitude, 
                    string FileName, 
                    string URL, 
                    string VideoType, 
                    System.Nullable<bool> TestSignalFlag) {
            SOS.FOS.MonitoringStationServices.AGSignalService.SignalRequest inValue = new SOS.FOS.MonitoringStationServices.AGSignalService.SignalRequest();
            inValue.Body = new SOS.FOS.MonitoringStationServices.AGSignalService.SignalRequestBody();
            inValue.Body.PollMessageFlag = PollMessageFlag;
            inValue.Body.UserName = UserName;
            inValue.Body.UserPassword = UserPassword;
            inValue.Body.Receiver = Receiver;
            inValue.Body.Line = Line;
            inValue.Body.Account = Account;
            inValue.Body.SignalFormat = SignalFormat;
            inValue.Body.SignalCode = SignalCode;
            inValue.Body.Point = Point;
            inValue.Body.Area = Area;
            inValue.Body.UserID = UserID;
            inValue.Body.Text = Text;
            inValue.Body.Date = Date;
            inValue.Body.ANIPhone = ANIPhone;
            inValue.Body.Longitude = Longitude;
            inValue.Body.Latitude = Latitude;
            inValue.Body.FileName = FileName;
            inValue.Body.URL = URL;
            inValue.Body.VideoType = VideoType;
            inValue.Body.TestSignalFlag = TestSignalFlag;
            SOS.FOS.MonitoringStationServices.AGSignalService.SignalResponse retVal = ((SOS.FOS.MonitoringStationServices.AGSignalService.ReceiverSoap)(this)).Signal(inValue);
            return retVal.Body.SignalResult;
        }
    }
}
