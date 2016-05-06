using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ivrdating.Models
{
    public class action_queue
    {
        public int ID { get; set; }
        public DateTime QDateTimeStamp { get; set; }
        public DateTime ProcessedAt { get; set; }
        public string FuncTable_Name { get; set; }
        public string Field_Name { get; set; }
        public int Acc_Number { get; set; }
        public int Port { get; set; }
        public string WhereClause { get; set; }
        public string Action { get; set; }
    }
}