using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ivrdating.Models
{

    [DataContract(Namespace = "")]
    public class BaseReturnData
    {
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
    }

    [DataContract(Namespace = "")]
    public class ReturnData : BaseReturnData
    {
        [DataMember]
        public MemberDetailVM WsResult { get; set; }
    }

    [DataContract(Namespace = "")]
    public class ReturnGetNewAndActivateAccount : BaseReturnData
    {
        [DataMember]
        public GetNewAndActivateAccountVM WsResult { get; set; }
    }
    [DataContract(Namespace = "")]
    public class ReturnGetActivateAndDeactivatAccount : BaseReturnData
    {
        [DataMember]
        public ActivateAndDeactivatAccountMV WsResult { get; set; }

    }

    //
    /// <summary>
    /// add_new_account return Data type
    /// </summary>
    [DataContract(Namespace = "")]
    public class ReturnAddNewAccount : BaseReturnData
    {
        [DataMember]
        public AddNewAccountVM WsResult { get; set; }

    }
}