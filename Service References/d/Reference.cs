﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CbuPortal.FollowService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FollowService.IFollowEvents")]
    public interface IFollowEvents {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFollowEvents/getFollowers", ReplyAction="http://tempuri.org/IFollowEvents/getFollowersResponse")]
        CbuPortal.Entity.Models.tblFollowers[] getFollowers(MongoDB.Bson.ObjectId userID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFollowEvents/getFollowers", ReplyAction="http://tempuri.org/IFollowEvents/getFollowersResponse")]
        System.Threading.Tasks.Task<CbuPortal.Entity.Models.tblFollowers[]> getFollowersAsync(MongoDB.Bson.ObjectId userID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFollowEvents/getFollowing", ReplyAction="http://tempuri.org/IFollowEvents/getFollowingResponse")]
        CbuPortal.Entity.Models.tblFollowers[] getFollowing(MongoDB.Bson.ObjectId userID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFollowEvents/getFollowing", ReplyAction="http://tempuri.org/IFollowEvents/getFollowingResponse")]
        System.Threading.Tasks.Task<CbuPortal.Entity.Models.tblFollowers[]> getFollowingAsync(MongoDB.Bson.ObjectId userID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFollowEvents/setFollow", ReplyAction="http://tempuri.org/IFollowEvents/setFollowResponse")]
        bool setFollow(CbuPortal.Entity.Models.tblFollowing follow, bool followStatus);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFollowEvents/setFollow", ReplyAction="http://tempuri.org/IFollowEvents/setFollowResponse")]
        System.Threading.Tasks.Task<bool> setFollowAsync(CbuPortal.Entity.Models.tblFollowing follow, bool followStatus);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFollowEventsChannel : CbuPortal.FollowService.IFollowEvents, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FollowEventsClient : System.ServiceModel.ClientBase<CbuPortal.FollowService.IFollowEvents>, CbuPortal.FollowService.IFollowEvents {
        
        public FollowEventsClient() {
        }
        
        public FollowEventsClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FollowEventsClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FollowEventsClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FollowEventsClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public CbuPortal.Entity.Models.tblFollowers[] getFollowers(MongoDB.Bson.ObjectId userID) {
            return base.Channel.getFollowers(userID);
        }
        
        public System.Threading.Tasks.Task<CbuPortal.Entity.Models.tblFollowers[]> getFollowersAsync(MongoDB.Bson.ObjectId userID) {
            return base.Channel.getFollowersAsync(userID);
        }
        
        public CbuPortal.Entity.Models.tblFollowers[] getFollowing(MongoDB.Bson.ObjectId userID) {
            return base.Channel.getFollowing(userID);
        }
        
        public System.Threading.Tasks.Task<CbuPortal.Entity.Models.tblFollowers[]> getFollowingAsync(MongoDB.Bson.ObjectId userID) {
            return base.Channel.getFollowingAsync(userID);
        }
        
        public bool setFollow(CbuPortal.Entity.Models.tblFollowing follow, bool followStatus) {
            return base.Channel.setFollow(follow, followStatus);
        }
        
        public System.Threading.Tasks.Task<bool> setFollowAsync(CbuPortal.Entity.Models.tblFollowing follow, bool followStatus) {
            return base.Channel.setFollowAsync(follow, followStatus);
        }
    }
}