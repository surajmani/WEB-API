﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ivrdating.Models
{
    [DataContract(Namespace = "")]
    public class ActivateAndDeactivatAccountMV
    {
        [DataMember]
        public string Acc_Number { get; set; }
    }
}