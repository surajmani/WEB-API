using ivrdating.ClassFile;
using ivrdating.Models;
using ivrdating.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using ServiceStack.Text;
using ivrdating.Helper;

namespace ivrdating.Controllers
{
    public class webservicesController : ApiController
    {

        MemberService _memberService;

        ValidationVar _validation = new ValidationVar();

        public webservicesController()
        {
            _memberService = new MemberService();
        }

        [Route("api/webservices/get_member_details")]
        [HttpPost]
        public IHttpActionResult get_member_details(GetMember _RequestBase, string output = null)
        {

            ReturnData vm = _memberService.GetMemberData(_RequestBase, MethodBase.GetCurrentMethod().Name);

            return GetMemberDetails(output, vm);

        }

        [Route("api/webservices/get_member_details")]
        [HttpGet]
        public IHttpActionResult get_member_details(string AuthKey, string WS_UserName, string WS_Password, string Group_Prefix, string CustomerEmail_Address, string PassCode, string Acc_Number, string CallerId, string output = null)
        {
            ReturnData vm = _memberService.GetMemberData(new GetMember() { Acc_Number = Acc_Number, AuthKey = AuthKey, CallerId = CallerId, CustomerEmail_Address = CustomerEmail_Address, Group_Prefix = Group_Prefix, PassCode = PassCode, WS_Password = WS_Password, WS_UserName = WS_UserName }, MethodBase.GetCurrentMethod().Name);
            return GetMemberDetails(output, vm);
        }

        [NonAction]
        private IHttpActionResult GetMemberDetails(string output, ReturnData vm)
        {
            if (string.Equals(output, "text", StringComparison.OrdinalIgnoreCase))
            {
                //return Ok(Helper.ExtensionMethods.SerializeToPlainText(typeof(MemberDetailVM).GetProperties().ToList(), vm.WsResult));
                if (vm.ErrorMessage != null)
                    return Content(HttpStatusCode.OK, vm.ErrorMessage, Configuration.Formatters.JsonFormatter);
                return Content(HttpStatusCode.OK, Helper.ExtensionMethods.SerializeToPlainText(typeof(MemberDetailVM).GetProperties().ToList(), vm.WsResult), Configuration.Formatters.JsonFormatter);
            }
            else if (string.Equals(output, "csv", StringComparison.OrdinalIgnoreCase))
            {
                // return Ok(CsvSerializer.SerializeToCsv(new List<MemberDetailVM> { vm.WsResult }));
                if (vm.ErrorMessage != null)
                    return Content(HttpStatusCode.OK, vm.ErrorMessage, Configuration.Formatters.JsonFormatter);
                return Content(HttpStatusCode.OK, CsvSerializer.SerializeToCsv(new List<MemberDetailVM> { vm.WsResult }), Configuration.Formatters.JsonFormatter);
            }
            else if (string.Equals(output, "json", StringComparison.OrdinalIgnoreCase))
            {
                //return Json(vm);
                return Content(HttpStatusCode.OK, vm, Configuration.Formatters.JsonFormatter);
            }
            else if (string.Equals(output, "xml", StringComparison.OrdinalIgnoreCase))
            {
                //return Ok(XmlSerializer.SerializeToString(vm.WsResult));
                return Content(HttpStatusCode.OK, vm, Configuration.Formatters.XmlFormatter);
            }
            else
            {
                return Ok(vm);
            }
        }


        #region Get_New_And_Activate_Account
        [Route("api/webservices/get_new_acc_number")]
        [HttpPost]
        public IHttpActionResult get_new_acc_number(GetNewAndActivateAccount _GetNewAndActivateAccount, string output = null)
        {
            return FN_Get_New_And_Activate_Account(_GetNewAndActivateAccount, MethodBase.GetCurrentMethod().Name, output);
        }

        [Route("api/webservices/get_new_acc_number")]
        [HttpGet]
        public IHttpActionResult get_new_acc_number(string AuthKey, string WS_UserName, string WS_Password, string Group_Prefix, string CallerId, string output = null)
        {

            return FN_Get_New_And_Activate_Account(new GetNewAndActivateAccount() { AuthKey = AuthKey, WS_UserName = WS_UserName, WS_Password = WS_Password, Group_Prefix = Group_Prefix, CallerId = CallerId }, MethodBase.GetCurrentMethod().Name, output);
        }


        [Route("api/webservices/get_n_activate_new_acc_number")]
        [HttpPost]
        public IHttpActionResult get_n_activate_new_acc_number(GetNewAndActivateAccount _GetNewAndActivateAccount, string output = null)
        {
            return FN_Get_New_And_Activate_Account(_GetNewAndActivateAccount, MethodBase.GetCurrentMethod().Name, output);
        }

        [Route("api/webservices/get_n_activate_new_acc_number")]
        [HttpGet]
        public IHttpActionResult get_n_activate_new_acc_number(string AuthKey, string WS_UserName, string WS_Password, string Group_Prefix, string output = null)
        {
            return FN_Get_New_And_Activate_Account(new GetNewAndActivateAccount() { AuthKey = AuthKey, WS_UserName = WS_UserName, WS_Password = WS_Password, Group_Prefix = Group_Prefix }, MethodBase.GetCurrentMethod().Name, output);
        }


        [NonAction]
        private IHttpActionResult FN_Get_New_And_Activate_Account(GetNewAndActivateAccount _GetNewAndActivateAccount, string function, string output = null)
        {
            string msg;
            GetNewAndActivateAccountVM data = (GetNewAndActivateAccountVM)_validation.validate(ExtensionMethods.ReturnAllParamerterValues(typeof(GetNewAndActivateAccount).GetProperties().ToList(), _GetNewAndActivateAccount, function), out msg);

            ReturnGetNewAndActivateAccount vm = new ReturnGetNewAndActivateAccount();

            if (msg.StartsWith("Ok|"))
            {
                vm.ErrorMessage = null;
                vm.Count = 1;
                vm.WsResult = data;
            }
            else
            {
                vm.ErrorMessage = msg;
                vm.Count = 0;
            }

            if (string.Equals(output, "text", StringComparison.OrdinalIgnoreCase))
            {
                //return Ok(Helper.ExtensionMethods.SerializeToPlainText(typeof(MemberDetailVM).GetProperties().ToList(), vm.WsResult));
                if (vm.ErrorMessage != null)
                    return Content(HttpStatusCode.OK, vm.ErrorMessage, Configuration.Formatters.JsonFormatter);
                return Content(HttpStatusCode.OK, Helper.ExtensionMethods.SerializeToPlainText(typeof(GetNewAndActivateAccountVM).GetProperties().ToList(), vm.WsResult), Configuration.Formatters.JsonFormatter);
            }
            else if (string.Equals(output, "csv", StringComparison.OrdinalIgnoreCase))
            {
                // return Ok(CsvSerializer.SerializeToCsv(new List<MemberDetailVM> { vm.WsResult }));
                if (vm.ErrorMessage != null)
                    return Content(HttpStatusCode.OK, vm.ErrorMessage, Configuration.Formatters.JsonFormatter);
                return Content(HttpStatusCode.OK, CsvSerializer.SerializeToCsv(new List<GetNewAndActivateAccountVM> { vm.WsResult }), Configuration.Formatters.JsonFormatter);
            }
            else if (string.Equals(output, "json", StringComparison.OrdinalIgnoreCase))
            {
                //return Json(vm);
                return Content(HttpStatusCode.OK, vm, Configuration.Formatters.JsonFormatter);
            }
            else if (string.Equals(output, "xml", StringComparison.OrdinalIgnoreCase))
            {
                //return Ok(XmlSerializer.SerializeToString(vm.WsResult));
                return Content(HttpStatusCode.OK, vm, Configuration.Formatters.XmlFormatter);
            }
            else
            {
                return Ok(vm);
            }
        }

        #endregion Get_New_And_Activate_Account


        #region activate_acc_number and deactivate_acc_number
        [Route("api/webservices/activate_acc_number")]
        [HttpPost]
        public IHttpActionResult activate_acc_number(ActivateAndDeactivateAccount _RequestBase, string output = null)
        {

            return FN_Activate_And_DeactivateAccount(_RequestBase, MethodBase.GetCurrentMethod().Name, output);
        }

        [Route("api/webservices/activate_acc_number")]
        [HttpGet]
        public IHttpActionResult activate_acc_number(string AuthKey, string WS_UserName, string WS_Password, string Group_Prefix, string Acc_Number, string output = null)
        {
            return FN_Activate_And_DeactivateAccount(new ActivateAndDeactivateAccount() { Acc_Number = Acc_Number, AuthKey = AuthKey, Group_Prefix = Group_Prefix, WS_Password = WS_Password, WS_UserName = WS_UserName }, MethodBase.GetCurrentMethod().Name, output);
        }
        [Route("api/webservices/deactivate_acc_number")]
        [HttpPost]
        public IHttpActionResult deactivate_acc_number(ActivateAndDeactivateAccount _RequestBase, string output = null)
        {
            return FN_Activate_And_DeactivateAccount(_RequestBase, MethodBase.GetCurrentMethod().Name, output);
        }

        [Route("api/webservices/deactivate_acc_number")]
        [HttpGet]
        public IHttpActionResult deactivate_acc_number(string AuthKey, string WS_UserName, string WS_Password, string Group_Prefix, string Acc_Number, string output = null)
        {
            return FN_Activate_And_DeactivateAccount(new ActivateAndDeactivateAccount() { Acc_Number = Acc_Number, AuthKey = AuthKey, Group_Prefix = Group_Prefix, WS_Password = WS_Password, WS_UserName = WS_UserName }, MethodBase.GetCurrentMethod().Name, output);
        }

        [NonAction]
        private IHttpActionResult FN_Activate_And_DeactivateAccount(ActivateAndDeactivateAccount _ActivateAndDeactivateAccount, string function, string output = null)
        {
            string msg;
            ActivateAndDeactivatAccountMV data = (ActivateAndDeactivatAccountMV)_validation.validate(ExtensionMethods.ReturnAllParamerterValues(typeof(ActivateAndDeactivateAccount).GetProperties().ToList(), _ActivateAndDeactivateAccount, function), out msg);

            ReturnGetActivateAndDeactivatAccount vm = new ReturnGetActivateAndDeactivatAccount();

            if (msg.StartsWith("Ok|"))
            {
                vm.ErrorMessage = null;
                vm.Count = 1;
                vm.WsResult = data;
            }
            else
            {
                vm.ErrorMessage = msg;
                vm.Count = 0;
            }

            if (string.Equals(output, "text", StringComparison.OrdinalIgnoreCase))
            {
                //return Ok(Helper.ExtensionMethods.SerializeToPlainText(typeof(MemberDetailVM).GetProperties().ToList(), vm.WsResult));
                if (vm.ErrorMessage != null)
                    return Content(HttpStatusCode.OK, vm.ErrorMessage, Configuration.Formatters.JsonFormatter);
                return Content(HttpStatusCode.OK, Helper.ExtensionMethods.SerializeToPlainText(typeof(ActivateAndDeactivatAccountMV).GetProperties().ToList(), vm.WsResult), Configuration.Formatters.JsonFormatter);
            }
            else if (string.Equals(output, "csv", StringComparison.OrdinalIgnoreCase))
            {
                // return Ok(CsvSerializer.SerializeToCsv(new List<MemberDetailVM> { vm.WsResult }));
                if (vm.ErrorMessage != null)
                    return Content(HttpStatusCode.OK, vm.ErrorMessage, Configuration.Formatters.JsonFormatter);
                return Content(HttpStatusCode.OK, CsvSerializer.SerializeToCsv(new List<ActivateAndDeactivatAccountMV> { vm.WsResult }), Configuration.Formatters.JsonFormatter);
            }
            else if (string.Equals(output, "json", StringComparison.OrdinalIgnoreCase))
            {
                //return Json(vm);
                return Content(HttpStatusCode.OK, vm, Configuration.Formatters.JsonFormatter);
            }
            else if (string.Equals(output, "xml", StringComparison.OrdinalIgnoreCase))
            {
                //return Ok(XmlSerializer.SerializeToString(vm.WsResult));
                return Content(HttpStatusCode.OK, vm, Configuration.Formatters.XmlFormatter);
            }
            else
            {
                return Ok(vm);
            }
        }

        #endregion activate_acc_number and deactivate_acc_number

        #region add_new_account
        [Route("api/webservices/add_new_account")]
        [HttpPost]
        public IHttpActionResult add_new_account(AddNewAccount _RequestBase, string output = null)
        {
            return Fn_Add_New_Account(_RequestBase, MethodBase.GetCurrentMethod().Name,output);
        }

        [Route("api/webservices/add_new_account")]
        [HttpGet]
        public IHttpActionResult add_new_account(string AuthKey, string WS_UserName, string WS_Password, string Group_Prefix, string Acc_Number, string AccountType, string Active0In1, string CallerId, string PassCode, DateTime? PlanExpiresOn, DateTime? RegisteredDate, string output = null)
        {
            return Fn_Add_New_Account(new AddNewAccount() {AccountType=AccountType,Acc_Number=Acc_Number,Active0In1=Active0In1,AuthKey=AuthKey,CallerId=CallerId,Group_Prefix=Group_Prefix,PassCode=PassCode,PlanExpiresOn=PlanExpiresOn,RegisteredDate=RegisteredDate,WS_Password=WS_Password,WS_UserName=WS_UserName }, MethodBase.GetCurrentMethod().Name, output);
        }

        [NonAction]
        private IHttpActionResult Fn_Add_New_Account(AddNewAccount _AddNewAccount, string function, string output = null)
        {
            string msg;
            AddNewAccountVM data = (AddNewAccountVM)_validation.validate(ExtensionMethods.ReturnAllParamerterValues(typeof(AddNewAccount).GetProperties().ToList(), _AddNewAccount, function), out msg);

            ReturnAddNewAccount vm = new ReturnAddNewAccount();

            if (msg.StartsWith("Ok|"))
            {
                vm.ErrorMessage = null;
                vm.Count = 1;
                vm.WsResult = data;
            }
            else
            {
                vm.ErrorMessage = msg;
                vm.Count = 0;
            }

            if (string.Equals(output, "text", StringComparison.OrdinalIgnoreCase))
            {
                //return Ok(Helper.ExtensionMethods.SerializeToPlainText(typeof(MemberDetailVM).GetProperties().ToList(), vm.WsResult));
                if (vm.ErrorMessage != null)
                    return Content(HttpStatusCode.OK, vm.ErrorMessage, Configuration.Formatters.JsonFormatter);
                return Content(HttpStatusCode.OK, Helper.ExtensionMethods.SerializeToPlainText(typeof(AddNewAccountVM).GetProperties().ToList(), vm.WsResult), Configuration.Formatters.JsonFormatter);
            }
            else if (string.Equals(output, "csv", StringComparison.OrdinalIgnoreCase))
            {
                // return Ok(CsvSerializer.SerializeToCsv(new List<MemberDetailVM> { vm.WsResult }));
                if (vm.ErrorMessage != null)
                    return Content(HttpStatusCode.OK, vm.ErrorMessage, Configuration.Formatters.JsonFormatter);
                return Content(HttpStatusCode.OK, CsvSerializer.SerializeToCsv(new List<AddNewAccountVM> { vm.WsResult }), Configuration.Formatters.JsonFormatter);
            }
            else if (string.Equals(output, "json", StringComparison.OrdinalIgnoreCase))
            {
                //return Json(vm);
                return Content(HttpStatusCode.OK, vm, Configuration.Formatters.JsonFormatter);
            }
            else if (string.Equals(output, "xml", StringComparison.OrdinalIgnoreCase))
            {
                //return Ok(XmlSerializer.SerializeToString(vm.WsResult));
                return Content(HttpStatusCode.OK, vm, Configuration.Formatters.XmlFormatter);
            }
            else
            {
                return Ok(vm);
            }
        }

        #endregion add_new_account
    }
}
