using ivrdating.ClassFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;


namespace ivrdating.Models
{
    [DataContract(Namespace = "")]
    public class MemberDetailVM
    {

        //public string Acc_Number { get; set; }

        //public string PassCode { get; set; }

        //public string CallerId { get; set; }

        //public string Email_Address { get; set; }

        //public string First_Name { get; set; }

        //public string Last_Name { get; set; }

        //public string address { get; set; }

        //public string City { get; set; }

        //public string State_Name { get; set; }

        //public string zip_code { get; set; }

        //public string Country { get; set; }

        //public string AccountType { get; set; }

        //public string ExpiryDate { get; set; }
        [DataMember]
        public string Acc_Number { get; set; }
        [DataMember]
        public string PassCode { get; set; }
        [DataMember]
        public string CallerId { get; set; }
        [DataMember]
        public string Email_Address { get; set; }
        [DataMember]
        public string First_Name { get; set; }
        [DataMember]
        public string Last_Name { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State_Name { get; set; }
        [DataMember]
        public string zip_code { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string AccountType { get; set; }
        [DataMember]
        public string ExpiryDate { get; set; }

    }
}