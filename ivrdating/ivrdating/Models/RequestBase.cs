using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ivrdating.Models
{
    public class RequestBase
    {
        public string AuthKey { get; set; }
        public string WS_UserName { get; set; }
        public string WS_Password { get; set; }
        public string Group_Prefix { get; set; }
    }

    public class GetMember : RequestBase
    {
        public string CustomerEmail_Address { get; set; }
        public string PassCode { get; set; }
        public string Acc_Number { get; set; }
        public string CallerId { get; set; }
    }

    public class GetNewAndActivateAccount : RequestBase
    {
        public string CallerId { get; set; }
    }

    public class ActivateAndDeactivateAccount : RequestBase
    {
        public string Acc_Number { get; set; }
    }
    public class AddNewAccount : RequestBase
    {
        public string Acc_Number { get; set; }
        public string AccountType { get; set; }
        public string Active0In1 { get; set; }
        public string CallerId { get; set; }
        public string PassCode { get; set; }
        public DateTime? PlanExpiresOn { get; set; }
        public DateTime? RegisteredDate { get; set; }
    }
}