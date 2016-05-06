using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ivrdating.Models
{
    public class ws_agent
    {
        public int Id { get; set; }
        public string WS_Username { get; set; }
        public string WS_Password { get; set; }
        public string AuthKey { get; set; }
        public string IP_Address { get; set; }
        public DateTime First_Access_On { get; set; }
        public DateTime Last_Access_On { get; set; }
    }
}