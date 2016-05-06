using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ivrdating.Models
{
    public class account
    {
        public int Id { get; set; }
        public int Acc_Number { get; set; }
        public string PassCode { get; set; }
        public string callerid { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime AccRegisteredOn { get; set; }
        public int ANI_StateId { get; set; }
        public int DNIS_StateId { get; set; }
        public int ANI_CityId { get; set; }
        public int DNIS_CityId { get; set; }
        public int Market_Id { get; set; }
        public int HUB_Definition_Id { get; set; }
        public string Callout_No { get; set; }
        public string Callout_Start { get; set; }
        public string Callout_End { get; set; }
        public DateTime LastLogon { get; set; }
        public DateTime LastGreetingRecordedOn { get; set; }
        public int NumberOfCalls { get; set; }
        public int TotalCallDuration { get; set; }
        
    }
}