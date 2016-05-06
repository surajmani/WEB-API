using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ivrdating.ClassFile
{
    public enum Errors
    {

        [Description("Invalid Authorization Key")]
        Invalid_Authorization_Key,
        [Description("WS_UserName credentials not matching")]
        WS_UserName_credentials_not_matching,
        [Description(" Unknown Group Prefix")]
        Unknown_Group_Prefix
    }
}