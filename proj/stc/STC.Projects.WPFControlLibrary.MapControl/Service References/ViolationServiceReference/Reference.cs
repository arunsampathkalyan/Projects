﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace STC.Projects.WPFControlLibrary.MapControl.ViolationServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ViolationServiceReference.IViolationsLayer")]
    public interface IViolationsLayer {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IViolationsLayer/GetViolationCountsByAssetCode", ReplyAction="http://tempuri.org/IViolationsLayer/GetViolationCountsByAssetCodeResponse")]
        STC.Projects.ClassLibrary.DTO.AssetViolationCountDTO GetViolationCountsByAssetCode(string assetCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IViolationsLayer/GetViolationCountsByAssetCode", ReplyAction="http://tempuri.org/IViolationsLayer/GetViolationCountsByAssetCodeResponse")]
        System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.AssetViolationCountDTO> GetViolationCountsByAssetCodeAsync(string assetCode);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IViolationsLayerChannel : STC.Projects.WPFControlLibrary.MapControl.ViolationServiceReference.IViolationsLayer, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ViolationsLayerClient : System.ServiceModel.ClientBase<STC.Projects.WPFControlLibrary.MapControl.ViolationServiceReference.IViolationsLayer>, STC.Projects.WPFControlLibrary.MapControl.ViolationServiceReference.IViolationsLayer {
        
        public ViolationsLayerClient() {
        }
        
        public ViolationsLayerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ViolationsLayerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ViolationsLayerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ViolationsLayerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public STC.Projects.ClassLibrary.DTO.AssetViolationCountDTO GetViolationCountsByAssetCode(string assetCode) {
            return base.Channel.GetViolationCountsByAssetCode(assetCode);
        }
        
        public System.Threading.Tasks.Task<STC.Projects.ClassLibrary.DTO.AssetViolationCountDTO> GetViolationCountsByAssetCodeAsync(string assetCode) {
            return base.Channel.GetViolationCountsByAssetCodeAsync(assetCode);
        }
    }
}
