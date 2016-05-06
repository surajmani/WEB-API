using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ivrdating.Models
{
    public class customer_master
    {
        public int Id { get; set; }
        public int AId { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string WebUserName { get; set; }
        public string WebPassword { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State_Name { get; set; }
        public string Zip_Code { get; set; }
        public string Country { get; set; }
        public string Email_Address { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public account account { get; set; }
    }
}