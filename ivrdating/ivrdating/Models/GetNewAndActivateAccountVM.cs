using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ivrdating.Models
{
    [DataContract(Namespace = "")]
    public class GetNewAndActivateAccountVM
    {
        [DataMember]
        public string Acc_Number { get; set; }
        [DataMember]
        public string PassCode { get; set; }
        [DataMember]
        public string Acc_Number2 { get; set; }
    }
}