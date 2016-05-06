using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ivrdating.Models
{
    public class ip2location_db15_ipv4m
    {
        public int Id { get; set; }
        public int ip_byte1 { get; set; }
        public int ip_start { get; set; }
        public int ip_end { get; set; }
        public string country { get; set; }
        public string country_name { get; set; }
        public string stateprov { get; set; }
        public string city { get; set; }
        public string AreaCode { get; set; }
    }
}