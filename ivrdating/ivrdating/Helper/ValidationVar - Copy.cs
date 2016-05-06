using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ivrdating.Helper
{
    public class ValidationVar
    {
        #region  class variable
        string WS_UserName = "";
        string WS_Password = "";
        string RetString = "";
        string ClientIP = "";
        string DestPage = "";
        string Charged_Amount = "";
        int RetCode = 0;
        string ResultPage = "";
        string ReqString = "";
        string Function = "";
        string AuthKey = "";
        int ErrCode = 0; int ErrCode2 = 0; int ErrCode3 = 0;
        int SysError = 1000000;
        int RetVal1 = 0;
        string ReturnString = "";
        string LastTimeStamp = "";
        string CC_UserName = "";
        string CC_IPAddress = "";
        string DateIn = "";
        string TimeIn = "";
        string DateOut = "";
        string TimeOut = "";
        string Group_Prefix = "";
        string Acc_Number = "";
        string RegisteredDate = "";
        string PassCode = "";
        int CallerId = 0;
        string PlanExpiresOn = "";
        string AccountType = "";
        string Active0In1 = "";
        string CustomerFirstName = "";
        string CustomerLastName = "";
        string WebUserName = "";
        string WebPassword = "";
        string CustomerAddress = "";
        string CustomerCity = "";
        string CustomerState = "";
        string CustomerZip_Code = "";
        string CustomerCountry = "";
        string CustomerEmail_Address = "";
        string Minutes_In_Package = "";
        string Old_Expiry = "";
        string New_Expiry = "";
        string Plan_Id = "0";
        int Plan_Amount = 0;
        int Plan_Validity = 0;
        string Session = "";
        string Package_Description = "";
        string FULL_CC_NUMBER = "";
        string CC_EXPDATE = "";
        string CVC = "";
        string Response_Code = "";
        string Response_Reason_Code = "";
        string Response_Reason_Text = "";
        string Approval_Code = "";
        string AVS_Result_Code = "";
        string Transaction_Id = "";
        string Payment_Type_Text = "";
        string Service_Source = "";
        string Area_Code = "";
        string SubscriberNo = "";
        string SMS_Id = "";
        string TicketId = "";
        string CarrierId = "";
        string ChargeAmount = "";
        string App1Del2 = "";
        string SettingName = "";
        string SettingValue = "";
        string ActiveServerIP = "";
        string Client_IP_Location = "";

        #endregion class variable

        #region replicate php fuction
        private int strlen(string str)
        {
            return str.Length;
        }
        private string strtolower(string str)
        {
            return str.ToLower();
        }
        private bool is_Numeric(object str)
        {
            int rs;
            return int.TryParse(str.ToString(), out rs);
        }
        private string str_replace(string str1, string str2, string str3)
        {
            return str3.Replace(str1, str2);
        }
        #endregion  replicate php fuction
        public string validate(Dictionary<string, object> fieldVal)
        {
            DbCurdOperation _DbCurdOperation = new DbCurdOperation();


            #region validation param
            foreach (var field in this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (fieldVal.ContainsKey(field.Name))
                {

                    object value;
                    if (fieldVal.TryGetValue(field.Name, out value))
                    {
                        if (field.FieldType == typeof(int))
                        {
                            int i;
                            if (int.TryParse(value == null ? "" : value.ToString(), out i))
                            {
                                field.SetValue(this, i);
                            }
                            else
                                field.SetValue(this, 0);
                        }
                        else
                        {
                            field.SetValue(this, value == null ? "" : value.ToString());
                        }
                    }
                }
            }
            //////////////////////////////////////////////////////
            if ((string.IsNullOrEmpty(AuthKey)))
            {
                AuthKey = "0";
                ErrCode = 1;
            }
            if ((string.IsNullOrEmpty(Function)))
            {
                Function = "0";
                ErrCode += 2;
            }
            else
            {
                Function = strtolower(Function);
            }


            if ((Function == "insert_login_log") || (Function == "update_login_log") || (Function == "getchargeamount"))
            {
                if ((string.IsNullOrEmpty(Session)))
                {
                    Session = "";
                }
                if ((string.IsNullOrEmpty(LastTimeStamp)))
                {
                    LastTimeStamp = "";
                }
                if (Function == "insert_login_log")
                {
                    if ((string.IsNullOrEmpty(CC_UserName)))
                        CC_UserName = "";
                    if ((string.IsNullOrEmpty(CC_IPAddress)))
                        CC_IPAddress = "";
                    if ((string.IsNullOrEmpty(DateIn)))
                        DateIn = "";
                    if ((string.IsNullOrEmpty(TimeIn)))
                        TimeIn = "";
                }
                if (Function == "update_login_log")
                {
                    if ((string.IsNullOrEmpty(DateOut)))
                        DateOut = "";
                    if ((string.IsNullOrEmpty(TimeOut)))
                        TimeOut = "";
                }
            }
            else
            {
                if (!((Function == "read_misc") || (Function == "set_misc") || (Function == "set_primary_apiserver") || (Function == "check_geo_location") || (Function == "get_node3_accesspoint_ip")) && (string.IsNullOrEmpty(Group_Prefix)))
                {
                    Group_Prefix = "-";
                    ErrCode += 524288;
                }

                if ((Function == "set_misc") || (Function == "read_misc") || (Function == "get_new_acc_number") || (Function == "get_n_activate_new_acc_number") || (Function == "set_primary_apiserver") || (Function == "check_geo_location") || (Function == "get_node3_accesspoint_ip"))
                {
                    Acc_Number = "0";
                }
                else
                {
                    if ((Function != "get_member_details") && (Function != "member_forgot_passcode"))
                    {
                        if ((string.IsNullOrEmpty(Acc_Number)))
                        {
                            Acc_Number = "0";
                            ErrCode += 4;
                        }
                        if ((string.IsNullOrEmpty(RegisteredDate)))
                        {
                            RegisteredDate = DateTime.Now.ToString();

                        }
                    }
                }
            }

            //Activate_Acc_Number, Deactivate_Acc_Number - Need not check anything else
            if (Function == "add_new_account")
            {
                //Check for all valid parameters
                //PassCode=Pcode&	CallerId=CId&	RegisteredDate=RegDate&	PlanExpiresOn=PlanExp&	AccountType=AccType&	Active0In1=Act1In0
                if ((string.IsNullOrEmpty(PassCode)))
                {
                    PassCode = "0";
                    ErrCode += 8;
                }
                if ((string.IsNullOrEmpty(CallerId.ToString())))
                {
                    CallerId = 0;
                }
                if ((string.IsNullOrEmpty(PlanExpiresOn)))
                {
                    PlanExpiresOn = DateTime.Now.ToString();
                    ErrCode += 32;
                }

                if ((string.IsNullOrEmpty(AccountType)))
                {
                    AccountType = "0";
                    ErrCode += 64;
                }
                if ((string.IsNullOrEmpty(Active0In1)))
                {
                    Active0In1 = "0";
                }
            }
            else if ((Function == "add_to_customer_master"))
            {
                //Check for all valid parameters
                //Acc_Number=Acc_Number&	CustomerFirstName=CustFirst_Name&	CustomerLastName=CustLast_Name&	WebUserName=wUsrName&	WebPassword=wPwd&	
                //CustomerAddress=address&	CustomerCity=City&	CustomerState=State&	CustomerZip_Code=zip_code&	CustomerCountry=Country&	
                //CustomerEmail_Address=Email_address&	RegisteredDate=RegDate
                if ((string.IsNullOrEmpty(CustomerFirstName)))
                {
                    CustomerFirstName = "-1";
                }
                if ((string.IsNullOrEmpty(CustomerLastName)))
                {
                    CustomerLastName = "-1";
                }
                if ((string.IsNullOrEmpty(WebUserName)))
                {
                    WebUserName = "-1";
                }
                if ((string.IsNullOrEmpty(WebPassword)))
                {
                    WebPassword = "-1";
                }
                if ((string.IsNullOrEmpty(CustomerAddress)))
                {
                    CustomerAddress = "-1";
                }
                if ((string.IsNullOrEmpty(CustomerCity)))
                {
                    CustomerCity = "-1";
                }
                if ((string.IsNullOrEmpty(CustomerState)))
                {
                    CustomerState = "-1";
                }
                if ((string.IsNullOrEmpty(CustomerZip_Code)))
                {
                    CustomerZip_Code = "-1";
                }
                if ((string.IsNullOrEmpty(CustomerCountry)))
                {
                    CustomerCountry = "-1";
                }
                if ((string.IsNullOrEmpty(CustomerEmail_Address)))
                {
                    CustomerEmail_Address = "-1";
                }
            }
            else if ((Function == "add_to_user_minute"))
            {
                if ((string.IsNullOrEmpty(Minutes_In_Package)))
                {
                    //Minutes_In_Package Not defined (Function Add_To_User_Minute)
                    Minutes_In_Package = "0";
                    ErrCode += 256;
                }
            }
            else if ((Function == "add_to_payment_details"))
            {
                if ((string.IsNullOrEmpty(CallerId.ToString())))
                {
                    CallerId = 0;
                }

                if ((string.IsNullOrEmpty(Old_Expiry)))
                {
                    //Old_Expiry Not defined (Function Add_To_Payment_Details)
                    Old_Expiry = "0";
                    ErrCode += 512;
                }
                else if (strlen(Old_Expiry) != 10)
                {
                    //Invalid Old_Expiry (Function Add_To_Payment_Details)
                    Old_Expiry = "0";
                    ErrCode += 512;
                }

                if ((string.IsNullOrEmpty(New_Expiry)))
                {
                    //New_Expiry Not defined (Function Add_To_Payment_Details)
                    New_Expiry = "0";
                    ErrCode += 1024;
                }
                else if (strlen(New_Expiry) != 10)
                {
                    //Invalid New_Expiry (Function Add_To_Payment_Details)
                    New_Expiry = "0";
                    ErrCode += 1024;
                }

                if ((string.IsNullOrEmpty(Plan_Id.ToString())))
                {
                    //Payment Plan_Id Not defined (Function Add_To_Payment_Details)
                    Plan_Id = "0";
                    ErrCode += 2048;
                }
                else if (!(is_Numeric(Plan_Id)))
                {
                    //Invalid Payment Plan_Id (Function Add_To_Payment_Details)
                    Plan_Id = "0";
                    ErrCode += 2048;
                }
                if ((string.IsNullOrEmpty(Plan_Amount.ToString())))
                {
                    //Plan_Amount Not defined (Function Add_To_Payment_Details)
                    Plan_Amount = 0;
                    ErrCode += 4096;
                }
                else if (!(is_Numeric(Plan_Amount)))
                {
                    //Invalid Payment Plan_Amount (Function Add_To_Payment_Details)
                    Plan_Amount = 0;
                    ErrCode += 4096;
                }
                if ((string.IsNullOrEmpty(Plan_Validity.ToString())))
                {
                    //Plan_Validity Not defined (Function Add_To_Payment_Details)
                    Plan_Validity = 0;
                    ErrCode += 8192;
                }
                else if (!(is_Numeric(Plan_Validity)))
                {
                    //Invalid Payment Plan_Validity (Function Add_To_Payment_Details)
                    Plan_Validity = 0;
                    ErrCode += 8192;
                }
                if ((string.IsNullOrEmpty(Minutes_In_Package)))
                {
                    //Minutes_In_Package Not defined (Function Add_To_Payment_Details)
                    Minutes_In_Package = "0";
                    ErrCode += 16384;
                }
                else if (!(is_Numeric(Minutes_In_Package)))
                {
                    //Invalid Payment Minutes_In_Package (Function Add_To_Payment_Details)
                    Minutes_In_Package = "0";
                    ErrCode += 16384;
                }
                if ((string.IsNullOrEmpty(Package_Description)))
                {
                    //Package_Description Not defined (Function Add_To_Payment_Details)
                    Package_Description = "0";
                    ErrCode += 32768;
                }
                if ((string.IsNullOrEmpty(FULL_CC_NUMBER)))
                {
                    //Credit Card Number Not defined (Function Add_To_Payment_Details)
                    FULL_CC_NUMBER = "";
                }
                if ((string.IsNullOrEmpty(CC_EXPDATE)))
                {
                    //Credit Card Expiry Date Not defined (Function Add_To_Payment_Details)
                    CC_EXPDATE = "";
                }
                if ((string.IsNullOrEmpty(CVC)))
                {
                    //Credit Card CVC Not defined (Function Add_To_Payment_Details)
                    CVC = "";
                }
                if ((string.IsNullOrEmpty(Response_Code)))
                {
                    //Response_Code Not defined (Function Add_To_Payment_Details)
                    Response_Code = "";
                }
                else if (!(is_Numeric(Response_Code)))
                {
                    //Invalid Response_Code (Function Add_To_Payment_Details)
                    Response_Code = "0";
                }
                if ((string.IsNullOrEmpty(Response_Reason_Code)))
                {
                    //Response_Reason_Code Not defined (Function Add_To_Payment_Details)
                    Response_Reason_Code = "";
                }
                if ((string.IsNullOrEmpty(Response_Reason_Text)))
                {
                    //Response_Reason_Text Not defined (Function Add_To_Payment_Details)
                    Response_Reason_Text = "";
                }
                if ((string.IsNullOrEmpty(Approval_Code)))
                {
                    //Approval_Code Not defined (Function Add_To_Payment_Details)
                    Approval_Code = "";
                }
                if ((string.IsNullOrEmpty(AVS_Result_Code)))
                {
                    //AVS_Result_Code Not defined (Function Add_To_Payment_Details)
                    AVS_Result_Code = "";
                }
                if ((string.IsNullOrEmpty(Transaction_Id)))
                {
                    //Transaction_Id Not defined (Function Add_To_Payment_Details)
                    Transaction_Id = "";
                }
                if ((string.IsNullOrEmpty(Payment_Type_Text)))
                {
                    //Payment_Type_Text Not defined (Function Add_To_Payment_Details)
                    Payment_Type_Text = "";
                }
            }
            else if ((Function == "add_to_service_source"))
            {
                if ((string.IsNullOrEmpty(Service_Source)))
                {
                    //Service_Source Not defined (Function Add_To_Service_Source)
                    Service_Source = "0";
                    ErrCode += 65536;
                }
                if ((string.IsNullOrEmpty(Area_Code)))
                {
                    //Area_Code Not defined (Function Add_To_Service_Source)
                    Area_Code = "0";
                    ErrCode += 131072;
                }
            }
            else if ((Function == "get_member_details") || (Function == "member_forgot_passcode"))
            {
                if ((string.IsNullOrEmpty(Acc_Number)) || (string.IsNullOrEmpty(CustomerEmail_Address)) || ((string.IsNullOrEmpty(PassCode)) && (string.IsNullOrEmpty(CallerId.ToString()))))
                {
                    //All Good - Proceed
                    if ((string.IsNullOrEmpty(Acc_Number)))
                    {
                        Acc_Number = "-1";
                    }
                    if ((string.IsNullOrEmpty(CustomerEmail_Address)))
                    {
                        CustomerEmail_Address = "-1";
                    }
                    if ((string.IsNullOrEmpty(PassCode)))
                    {
                        PassCode = "-1";
                    }
                    if ((string.IsNullOrEmpty(CallerId.ToString())))
                    {
                        CallerId = -1;
                    }
                }
                else
                {
                    // CAN NOT SEARCH CUSTOMER DETAILS, INCOMPLETE SEARCH CRITERIA
                    ErrCode += 262144;
                }
            }
            else if (Function == "update_account")
            {
                if ((string.IsNullOrEmpty(CallerId.ToString())))
                {
                    CallerId = 0;
                }
                if ((string.IsNullOrEmpty(New_Expiry)))
                {
                    //New_Expiry Not defined (Function UPDATE_Account)
                    New_Expiry = "0";
                }
                else if (strlen(New_Expiry) != 10)
                {
                    //New_Expiry Not defined (Function UPDATE_Account)
                    New_Expiry = "0";
                }

                if ((string.IsNullOrEmpty(AccountType)))
                {
                    AccountType = "0";
                }
                if ((string.IsNullOrEmpty(Active0In1)))
                {
                    Active0In1 = "0";
                }
            }
            else if (Function == "update_user_minute")
            {
                if ((string.IsNullOrEmpty(Minutes_In_Package)))
                {
                    //Minutes_In_Package Not defined (Function UPDATE_User_Minute)
                    Minutes_In_Package = "0";
                }
            }
            else if (Function == "update_customer_master")
            {
                if ((string.IsNullOrEmpty(CustomerFirstName)))
                {
                    CustomerFirstName = "-1";
                }
                if ((string.IsNullOrEmpty(CustomerLastName)))
                {
                    CustomerLastName = "-1";
                }
                if ((string.IsNullOrEmpty(CustomerAddress)))
                {
                    CustomerAddress = "-1";
                }
                if ((string.IsNullOrEmpty(CustomerCity)))
                {
                    CustomerCity = "-1";
                }
                if ((string.IsNullOrEmpty(CustomerState)))
                {
                    CustomerState = "-1";
                }
                if ((string.IsNullOrEmpty(CustomerZip_Code)))
                {
                    CustomerZip_Code = "-1";
                }
                if ((string.IsNullOrEmpty(CustomerCountry)))
                {
                    CustomerCountry = "-1";
                }
                if ((string.IsNullOrEmpty(CustomerEmail_Address)))
                {
                    CustomerEmail_Address = "-1";
                }
            }
            else if (Function == "add_complete_paid_account")
            {
                //Check for all valid parameters
                //Data for Account
                if ((string.IsNullOrEmpty(PassCode)))
                {
                    PassCode = "0";
                    ErrCode += 128;
                }
                if ((string.IsNullOrEmpty(CallerId.ToString())))
                {
                    CallerId = 0;
                }
                if ((string.IsNullOrEmpty(PlanExpiresOn)))
                {
                    PlanExpiresOn = DateTime.Now.ToString();
                    ErrCode2 += 2;
                }
                else if (strlen(PlanExpiresOn) != 10)
                {
                    //Invalid PlanExpiresOn (Function Add_Complete_Paid_Account)
                    PlanExpiresOn = "0";
                    ErrCode2 += 2;
                }

                if ((string.IsNullOrEmpty(AccountType)))
                {
                    AccountType = "0";
                    ErrCode2 += 4;
                }
                if ((string.IsNullOrEmpty(Active0In1)))
                {
                    Active0In1 = "0";
                }
                //Data for Customer Master
                if ((string.IsNullOrEmpty(CustomerFirstName)))
                {
                    CustomerFirstName = "";
                }
                if ((string.IsNullOrEmpty(CustomerLastName)))
                {
                    CustomerLastName = "";
                }
                if ((string.IsNullOrEmpty(WebUserName)))
                {
                    WebUserName = "";
                }
                WebPassword = PassCode;
                if ((string.IsNullOrEmpty(WebPassword)))
                {
                    WebPassword = "";
                }
                if ((string.IsNullOrEmpty(CustomerAddress)))
                {
                    CustomerAddress = "";
                }
                if ((string.IsNullOrEmpty(CustomerCity)))
                {
                    CustomerCity = "";
                }
                if ((string.IsNullOrEmpty(CustomerState)))
                {
                    CustomerState = "";
                }
                if ((string.IsNullOrEmpty(CustomerZip_Code)))
                {
                    CustomerZip_Code = "";
                }
                if ((string.IsNullOrEmpty(CustomerCountry)))
                {
                    CustomerCountry = "";
                }
                if ((string.IsNullOrEmpty(CustomerEmail_Address)))
                {
                    CustomerEmail_Address = "";
                }
                if ((string.IsNullOrEmpty(Minutes_In_Package)))
                {
                    //Minutes_In_Package Not defined (Function Add_Complete_Paid_Account)
                    Minutes_In_Package = "0";
                    ErrCode2 += 8;
                }
                //Payment_Details
                if ((string.IsNullOrEmpty(Old_Expiry)))
                {
                    //Old_Expiry Not defined (Function Add_Complete_Paid_Account)
                    Old_Expiry = "0";
                    ErrCode2 += 16;
                }
                else if (strlen(Old_Expiry) != 10)
                {
                    //Invalid Old_Expiry (Function Add_Complete_Paid_Account)
                    Old_Expiry = "0";
                    ErrCode2 += 16;
                }
                if ((string.IsNullOrEmpty(New_Expiry)))
                {
                    //New_Expiry Not defined (Function Add_Complete_Paid_Account)
                    New_Expiry = "0";
                    ErrCode2 += 32;
                }
                else if (strlen(New_Expiry) != 10)
                {
                    //Invalid New_Expiry (Function Add_Complete_Paid_Account)
                    New_Expiry = "0";
                    ErrCode2 += 32;
                }

                if ((string.IsNullOrEmpty(Plan_Id.ToString())))
                {
                    //Payment Plan_Id Not defined (Function Add_Complete_Paid_Account)
                    Plan_Id = "0";
                    ErrCode2 += 64;
                }
                else if (!(is_Numeric(Plan_Id)))
                {
                    //Invalid Payment Plan_Id (Function Add_Complete_Paid_Account)
                    Plan_Id = "0";
                    ErrCode2 += 64;
                }
                if ((string.IsNullOrEmpty(Plan_Amount.ToString())))
                {
                    //Plan_Amount Not defined (Function Add_Complete_Paid_Account)
                    Plan_Amount = 0;
                    ErrCode2 += 128;
                }
                else if (!(is_Numeric(Plan_Amount)))
                {
                    //Invalid Payment Plan_Amount (Function Add_Complete_Paid_Account)
                    Plan_Amount = 0;
                    ErrCode2 += 128;
                }
                if ((string.IsNullOrEmpty(Plan_Validity.ToString())))
                {
                    //Plan_Validity Not defined (Function Add_Complete_Paid_Account)
                    Plan_Validity = 0;
                    ErrCode2 += 256;
                }
                else if (!(is_Numeric(Plan_Validity)))
                {
                    //Invalid Payment Plan_Validity (Function Add_Complete_Paid_Account)
                    Plan_Validity = 0;
                    ErrCode2 += 256;
                }
                if ((string.IsNullOrEmpty(Package_Description)))
                {
                    //Package_Description Not defined (Function Add_Complete_Paid_Account)
                    Package_Description = "0";
                    ErrCode2 += 512;
                }
                if ((string.IsNullOrEmpty(FULL_CC_NUMBER)))
                {
                    //Credit Card Number Not defined (Function Add_Complete_Paid_Account)
                    FULL_CC_NUMBER = "";
                }
                if ((string.IsNullOrEmpty(CC_EXPDATE)))
                {
                    //Credit Card Expiry Date Not defined (Function Add_Complete_Paid_Account)
                    CC_EXPDATE = "";
                }
                if ((string.IsNullOrEmpty(CVC)))
                {
                    //Credit Card CVC Not defined (Function Add_Complete_Paid_Account)
                    CVC = "";
                }
                if ((string.IsNullOrEmpty(Response_Code)))
                {
                    //Response_Code Not defined (Function Add_Complete_Paid_Account)
                    Response_Code = "";
                }
                else if (!(is_Numeric(Response_Code)))
                {
                    //Invalid Response_Code (Function Add_Complete_Paid_Account)
                    Response_Code = "0";
                }
                if ((string.IsNullOrEmpty(Response_Reason_Code)))
                {
                    //Response_Reason_Code Not defined (Function Add_Complete_Paid_Account)
                    Response_Reason_Code = "";
                }
                if ((string.IsNullOrEmpty(Response_Reason_Text)))
                {
                    //Response_Reason_Text Not defined (Function Add_Complete_Paid_Account)
                    Response_Reason_Text = "";
                }
                if ((string.IsNullOrEmpty(Approval_Code)))
                {
                    //Approval_Code Not defined (Function Add_Complete_Paid_Account)
                    Approval_Code = "";
                }
                if ((string.IsNullOrEmpty(AVS_Result_Code)))
                {
                    //AVS_Result_Code Not defined (Function Add_Complete_Paid_Account)
                    AVS_Result_Code = "";
                }
                if ((string.IsNullOrEmpty(Transaction_Id)))
                {
                    //Transaction_Id Not defined (Function Add_Complete_Paid_Account)
                    Transaction_Id = "";
                }
                if ((string.IsNullOrEmpty(Payment_Type_Text)))
                {
                    //Payment_Type_Text Not defined (Function Add_Complete_Paid_Account)
                    Payment_Type_Text = "";
                }
                //Data for Service Source
                if ((string.IsNullOrEmpty(Service_Source)))
                {
                    //Service_Source Not defined (Function Add_Complete_Paid_Account)
                    Service_Source = "0";
                    ErrCode2 += 1024;
                }
                if ((string.IsNullOrEmpty(Area_Code)))
                {
                    //Area_Code Not defined (Function Add_Complete_Paid_Account)
                    Area_Code = "0";
                    ErrCode2 += 2048;
                }
            }
            else if (Function == "validate")
            {
                //Data for Account
                if ((string.IsNullOrEmpty(PassCode)))
                {

                    PassCode = "0";
                    ErrCode2 += 4096;
                }

            }
            else if (Function == "modify_customer_info")
            {
                if ((string.IsNullOrEmpty(PassCode)) || (string.IsNullOrEmpty(CallerId.ToString())) || (string.IsNullOrEmpty(WebPassword)))
                {
                    //All Good - Proceed
                    if ((string.IsNullOrEmpty(PassCode)))
                    {
                        PassCode = "-1";
                    }
                    if ((string.IsNullOrEmpty(CallerId.ToString())))
                    {
                        CallerId = -1;
                    }
                    if ((string.IsNullOrEmpty(WebPassword)))
                    {
                        WebPassword = "-1";
                    }
                }
                else
                {
                    // CAN NOT SEARCH CUSTOMER DETAILS, INCOMPLETE SEARCH CRITERIA
                    ErrCode2 += 8192;
                }
            }
            else if (Function == "process_mobile_charge")
            {
                // http://localhost/ivrdating/WS/webservices.php?AuthKey=1f3870be274f6c49b3e31a0c6728957f&WS_UserName=dev&WS_Password=abcd1234&Function=Process_Mobile_Charge&SubscriberNo=SSSSSSSSSS&Acc_Number=AAAAAA&PassCode=PPPP&SMS_Id=SS&TicketId=TTTTT&CarrierId=CC&ChargeAmount=CAMT
                // http://localhost/ivrdating/WS/webservices.php?AuthKey=1f3870be274f6c49b3e31a0c6728957f&WS_UserName=dev&WS_Password=abcd1234&Function=Process_Mobile_Charge&SubscriberNo=SSSSSSSSSS&Acc_Number=205493&PassCode=8888&SMS_Id=10&TicketId=TTTTT&CarrierId=77&ChargeAmount=9.99
                //Check for all valid parameters
                if ((string.IsNullOrEmpty(SubscriberNo)))
                {
                    SubscriberNo = "0";
                    ErrCode2 += 65536;
                }
                if ((string.IsNullOrEmpty(Acc_Number)))
                {
                    Acc_Number = "0";
                    ErrCode2 += 131072;
                }
                if ((string.IsNullOrEmpty(PassCode)))
                {
                    PassCode = "0";
                    ErrCode2 += 262144;
                }
                if ((string.IsNullOrEmpty(SMS_Id)))
                {
                    SMS_Id = "0";
                    ErrCode2 += 524288;
                }
                if ((string.IsNullOrEmpty(TicketId)))
                {
                    TicketId = "0";
                    ErrCode2 += 1048576;
                }
                if ((string.IsNullOrEmpty(CarrierId)))
                {
                    CarrierId = "0";
                    ErrCode2 += 2097152;
                }
                if ((string.IsNullOrEmpty(ChargeAmount)))
                {
                    ChargeAmount = "0";
                    ErrCode2 += 4194304;
                }
                if (!(is_Numeric(Acc_Number)))
                {
                    ErrCode2 += 8388608;
                }
                if (!(is_Numeric(PassCode)))
                {
                    ErrCode2 += 16777216;
                }
                if (!(is_Numeric(SMS_Id)))
                {
                    ErrCode2 += 33554432;
                }
                if (!(is_Numeric(CarrierId)))
                {
                    ErrCode2 += 67108864;
                }
                if (!(is_Numeric(ChargeAmount)))
                {
                    ErrCode2 += 134217728;
                }
            }
            else if (Function == "admin_web_screening")
            {
                if (!(is_Numeric(App1Del2)))
                {
                    App1Del2 = "";
                    ErrCode2 += 134217728;
                }
            }
            else if (Function == "getchargeamount")
            {
                if ((string.IsNullOrEmpty(Area_Code)))
                {
                    Area_Code = "";
                    ErrCode3 += 1;
                }
                if ((string.IsNullOrEmpty(Plan_Id)))
                {
                    Plan_Id = "";
                    ErrCode3 += 2;
                }
            }
            else if (Function == "set_misc")
            {
                if ((string.IsNullOrEmpty(SettingName)))
                {
                    SettingName = "";
                    ErrCode3 += 4;
                }
                if ((string.IsNullOrEmpty(SettingValue)))
                {
                    SettingValue = "";
                    ErrCode3 += 8;
                }
            }
            else if (Function == "set_primary_apiserver")
            {
                if ((string.IsNullOrEmpty(ActiveServerIP)))
                {
                    ActiveServerIP = "";
                    ErrCode3 += 16;
                }
            }
            else if (Function == "check_geo_location")
            {
                if ((string.IsNullOrEmpty(Client_IP_Location)))
                {
                    Client_IP_Location = "";
                    ErrCode3 += 32;
                }
            }
            // &Function=getchargeamount&Area_Code=718&Plan_Id=1

            if ((string.IsNullOrEmpty(ResultPage)))
            {
                ResultPage = "webservicesresult.php";
            }
            #endregion validation param

            ClientIP = GetIp();

            DataTable dt = new DataTable();

            #region dbQueryStart
            if ((ErrCode + ErrCode2 + ErrCode3) <= 0)
            {
                #region toDod
                //All parameters passed, check for veracity begins
                //if Valid AuthKey

                //    connectionstring = Connect_Production_Database_Node1();

                #region Validate Request Auth
                //    SQLQry1 = "SELECT Id, AuthKey, IP_Address FROM ws_agent ";
                //    SQLQry1 += "WHERE WS_UserName = "WS_UserName' AND WS_Password = "WS_Password' AND ";
                //    SQLQry1 += "IP_Address = ""+trim(ClientIP)+"' LIMIT 1";
                string SQLQry1 = "SELECT Id, AuthKey, IP_Address FROM ws_agent ";
                SQLQry1 += "WHERE WS_UserName = @WS_UserName AND WS_Password = @WS_Password AND ";
                SQLQry1 += "IP_Address = '" + ClientIP + "' LIMIT 1";

                dt = _DbCurdOperation.RetrieveDataFromDB(SQLQry1, new MySqlParameter[] { new MySqlParameter("@WS_UserName", WS_UserName), new MySqlParameter("@WS_Password", WS_Password) });

                //    QueryExe1 = CnExecute1(connectionstring, SQLQry1);

                //    if (odbc_fetch_row(QueryExe1))
                //    {
                //        DBWS_AuthKey = odbc_result(QueryExe1, "AuthKey");
                //        DBWS_IP_Address = odbc_result(QueryExe1, "IP_Address");
                //        DBWS_Id = odbc_result(QueryExe1, "Id");
                //        if (AuthKey != DBWS_AuthKey)
                //        {
                //            //Invalid Auth Key, Log & exit
                //            ErrCode += SysError + 1;
                //        }
                //        if (ClientIP != DBWS_IP_Address)
                //        {
                //            //Invalid Remote IP Address, Log & exit
                //            ErrCode += SysError + 2;
                //        }


                //    }
                //    else
                //    {
                //        DBWS_AuthKey = odbc_result(QueryExe1, "AuthKey");
                //        //WS_Agent credentials does not match; Contact Administrator
                //        DBWS_AuthKey = "0";
                //        DBWS_IP_Address = "0";
                //        DBWS_Id = "0";
                //        ErrCode += SysError + 4;
                //    }
                #endregion  Validate Request Auth

                #region validate Group_Prefix
                //    if ((Function == "set_misc") || (Function == "read_misc") || (Function == "insert_login_log") || (Function == "update_login_log") || (Function == "getchargeamount") || (Function == "set_primary_apiserver") || (Function == "check_geo_location") || (Function == "get_node3_accesspoint_ip"))
                //    {
                //        Group_Id = "0";
                //    }
                //    else
                //    {
                //        //Validate incoming Group_Prefix
                //        SQLQry830 = "SELECT Grp_Id FROM group_association ";
                //        SQLQry830 += "WHERE Grp_Prefix = "Group_Prefix' LIMIT 1";
                //        QueryExe830 = CnExecute1(connectionstring, SQLQry830);
                //        if (!(odbc_fetch_row(QueryExe830)))
                //        {
                //            Group_Id = "0";
                //            ErrCode2 += 268435456;
                //        }
                //        else
                //        {
                //            Group_Id = odbc_result(QueryExe830, "Grp_Id");
                //        }
                //    }
                #endregion  validate Group_Prefix

                #region Db curd operations
                if ((ErrCode + ErrCode2 + ErrCode3) <= 0)
                {
                    //        //UPDATE ws_agent
                    //        StrSQL_F0_1 = "UPDATE ws_agent SET Last_Access_On = Now() WHERE Id = "DBWS_Id' LIMIT 1";
                    //        CnExecute2(StrSQL_F0_1);

                    //        //Function=Fn&SerialNo=SS2&Val1=Amt1&ResultPage=result.php
                    //        if ((Function == "get_new_acc_number") || (Function == "get_n_activate_new_acc_number"))
                    //        {
                    //            CallerId_Match_Acc_Found = 0;
                    //            if (Function == "get_new_acc_number")
                    //            {
                    //                if (string.IsNullOrEmpty(CallerId.ToString()))
                    //                {
                    //                    //Check for CallerId linked Account No. - Get Male or Unknown Gender only NOT FEMALE
                    //                    StrSQL_F1_0 = "SELECT Acc_Number, Passcode FROM account ";
                    //                    StrSQL_F1_0 += "WHERE CallerId = "CallerId' AND Grp_Id = "Group_Id' ";
                    //                    StrSQL_F1_0 += "AND ( Gender = "1' OR Gender = "2' ) ORDER BY Gender ASC LIMIT 1";
                    //                    QueryExe_F1_0 = CnExecute1(connectionstring, StrSQL_F1_0);
                    //                    if (odbc_fetch_row(QueryExe_F1_0))
                    //                    {
                    //                        Ret_Fn1_Parm1 = odbc_result(QueryExe_F1_0, "Acc_Number");
                    //                        Ret_Fn1_Parm2 = odbc_result(QueryExe_F1_0, "PassCode");
                    //                        Ret_Fn1_Parm3 = Ret_Fn1_Parm1;
                    //                        ReturnString = "Ok|Ret_Fn1_Parm1|Ret_Fn1_Parm2|Ret_Fn1_Parm1";
                    //                        CallerId_Match_Acc_Found = 1;
                    //                    }
                    //                }
                    //            }

                    //            if (CallerId_Match_Acc_Found == 0)
                    //            {
                    //                //Check For Max and Min entries in accountids table
                    //                //Manual override to sppedup the process
                    //                if ("1" == "1")
                    //                {
                    //                    MinId = "1";
                    //                    MaxId = "950000";
                    //                    OrderIdValue = r&&(MinId, MaxId);
                    //                }
                    //                else
                    //                {
                    //                    StrSQL_F1_1 = "SELECT Min(Id) as MinId, Max(Id) as MaxId FROM accountids ";
                    //                    StrSQL_F1_1 += "WHERE Grp_Id"+Group_Id+" = "0'";
                    //                    QueryExe_F1_1 = CnExecute1(connectionstring, StrSQL_F1_1);
                    //                    if (odbc_fetch_row(QueryExe_F1_1))
                    //                    {
                    //                        MinId = odbc_result(QueryExe_F1_1, "MinId");
                    //                        MaxId = odbc_result(QueryExe_F1_1, "MaxId");
                    //                        OrderIdValue = r&&(MinId, MaxId);
                    //                    }
                    //                }
                    //                SQLQry_F1_2 = "SELECT Acc_Number, PassCode FROM accountids ";
                    //                SQLQry_F1_2 += "WHERE ( ( Grp_Id"+Group_Id+" = "0' ) AND ( Id > OrderIdValue ) ) LIMIT 1";
                    //                QueryExe_F1_2 = CnExecute1(connectionstring, SQLQry_F1_2);
                    //                if (odbc_fetch_row(QueryExe_F1_2))
                    //                {
                    //                    Ret_Fn1_Parm1 = odbc_result(QueryExe_F1_2, "Acc_Number");
                    //                    Ret_Fn1_Parm2 = mt_r&&(1111, 9999);
                    //                    Ret_Fn1_Parm3 = odbc_result(QueryExe_F1_2, "Acc_Number");
                    //                    if (Function == "get_n_activate_new_acc_number")
                    //                    {
                    //                        SQLQry_F1_3 = "UPDATE accountids SET Grp_Id"+Group_Id+" = "1' WHERE Acc_Number = "Ret_Fn1_Parm1' LIMIT 1";
                    //                        CnExecute2(SQLQry_F1_3);
                    //                    }
                    //                    ReturnString = "Ok|Ret_Fn1_Parm1|Ret_Fn1_Parm2|Ret_Fn1_Parm1";

                    //                    //Add to the acc_number_web TABLE
                    //                    SQLQry_F1_4 = "INSERT IGNORE INTO acc_number_web (Acc_Number, Grp_Id, AllocatedOn) ";
                    //                    SQLQry_F1_4 += "VALUES ('"+odbc_result(QueryExe_F1_2, "Acc_Number")+"', 'Group_Id', Now())";
                    //                    CnExecute2(SQLQry_F1_4);
                    //                }
                    //                else
                    //                {
                    //                    //No free records in accountids Table
                    //                    Ret_Fn1_Parm1 = "0";
                    //                    Ret_Fn1_Parm2 = "0";
                    //                    Ret_Fn1_Parm3 = "0";
                    //                    ErrCode += SysError + 8;
                    //                }
                    //            }
                    //        }
                    //        else if ((Function == "activate_acc_number") || (Function == "deactivate_acc_number"))
                    //        {
                    //            if (Function == "activate_acc_number")
                    //            {
                    //                SQLQry_F3_1 = "UPDATE accountids SET Grp_Id"+Group_Id+" = "1' WHERE Acc_Number = "Acc_Number' LIMIT 1";
                    //                CnExecute2(SQLQry_F3_1);

                    //                //Add to the acc_number_web TABLE
                    //                SQLQry_F3_2 = "INSERT IGNORE INTO acc_number_web (Acc_Number, Grp_Id, AllocatedOn) ";
                    //                SQLQry_F3_2 += "VALUES ('Acc_Number', 'Group_Id', Now())";
                    //                CnExecute2(SQLQry_F3_2);
                    //            }
                    //            else
                    //            {
                    //                //Check whether link to an active account, do not deactivate if valid account exists
                    //                SQLQry_F3_0 = "SELECT Acc_Number FROM account ";
                    //                SQLQry_F3_0 += "WHERE ( Grp_Id = ""+Group_Id+"' AND Acc_Number = ""+Acc_Number+"") LIMIT 1";
                    //                QueryExe_F3_0 = CnExecute1(connectionstring, SQLQry_F3_0);
                    //                if (!(odbc_fetch_row(QueryExe_F3_0)))
                    //                {
                    //                    SQLQry_F3_1 = "UPDATE accountids SET Grp_Id"+Group_Id+" = "0' WHERE Acc_Number = "Acc_Number' LIMIT 1";
                    //                    CnExecute2(SQLQry_F3_1);
                    //                }

                    //                //Remove from acc_number_web TABLE
                    //                SQLQry_F3_2 = "DELETE FROM acc_number_web WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //                CnExecute2(SQLQry_F3_2);
                    //            }
                    //            QueryExe_F3_1 = odbc_do(connectionstring, SQLQry_F3_1);
                    //            ReturnString = "Ok|Acc_Number";
                    //        }
                    //        else if (Function == "add_new_account")
                    //        {
                    //            SQLQry_F5_1 = "INSERT INTO account (Acc_Number, PassCode, CallerID, Gender, ";
                    //            SQLQry_F5_1 += "RegisteredOn, ExpiryDate, Grp_Id, AccRegisteredOn, AccountType, Active0In1) ";
                    //            SQLQry_F5_1 += "VALUES ( 'Acc_Number', 'PassCode', 'CallerId', '1', ";
                    //            SQLQry_F5_1 += "'RegisteredDate', 'PlanExpiresOn', 'Group_Id', 'RegisteredDate', 'AccountType', 'Active0In1' )";
                    //            CnExecute2(SQLQry_F5_1);

                    //            SQLLog("Creating Carrier Account for Carrier testing", WS_UserName);
                    //            SQLQry_F5_2 = "INSERT INTO mobile_carrier_account (Acc_Number, PassCode, ANI, Grp_Id, ";
                    //            SQLQry_F5_2 += "RegisteredOn, LastCalledOn, ExpiryDate) ";
                    //            SQLQry_F5_2 += "VALUES ('Acc_Number', 'PassCode', 'CallerId', 'Group_Id', ";
                    //            SQLQry_F5_2 += "'RegisteredDate', '0000-00-00 00:00:00', 'PlanExpiresOn")";
                    //            CnExecute2(SQLQry_F5_2);

                    //            ReturnString = "Ok|Acc_Number";
                    //        }
                    //        else if (Function == "add_to_customer_master")
                    //        {
                    //            //GET Account Id FROM account
                    //            SQLQry_F6_1 = "SELECT Id, Passcode FROM account WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //            QueryExe_F6_1 = CnExecute1(connectionstring, SQLQry_F6_1);
                    //            if (odbc_fetch_row(QueryExe_F6_1))
                    //            {
                    //                // Account exists
                    //                l_AId = odbc_result(QueryExe_F6_1, "Id");
                    //                l_PassCode = odbc_result(QueryExe_F6_1, "Passcode");

                    //                //CHECK Customer_Master Table
                    //                SQLQry_F6_10 = "SELECT AId FROM customer_master WHERE AId = "l_AId' LIMIT 1";
                    //                QueryExe_F6_10 = CnExecute1(connectionstring, SQLQry_F6_10);
                    //                if (odbc_fetch_row(QueryExe_F6_10))
                    //                {
                    //                    //Record found, update it
                    //                    UpdQry_F6_1 = add_string_if_not_blank(UpdQry_F6_1, 'First_Name', CustomerFirstName);
                    //                    UpdQry_F6_1 = add_string_if_not_blank(UpdQry_F6_1, 'Last_Name', CustomerLastName);
                    //                    UpdQry_F6_1 = add_string_if_not_blank(UpdQry_F6_1, 'Address', CustomerAddress);
                    //                    UpdQry_F6_1 = add_string_if_not_blank(UpdQry_F6_1, 'City', CustomerCity);
                    //                    UpdQry_F6_1 = add_string_if_not_blank(UpdQry_F6_1, 'State_Name', CustomerState);
                    //                    UpdQry_F6_1 = add_string_if_not_blank(UpdQry_F6_1, 'Zip_Code', CustomerZip_Code);
                    //                    UpdQry_F6_1 = add_string_if_not_blank(UpdQry_F6_1, 'Country', CustomerCountry);
                    //                    UpdQry_F6_1 = add_string_if_not_blank(UpdQry_F6_1, 'Email_Address', CustomerEmail_Address);
                    //                    if (strlen(UpdQry_F6_1) != 0)
                    //                    {
                    //                        SQLQry_F6_3 = "UPDATE customer_master SET UpdQry_F6_1, ModifiedOn = "RegisteredDate' ";
                    //                        SQLQry_F6_3 += "WHERE customer_master.AId = "l_AId' LIMIT 1";
                    //                        CnExecute2(SQLQry_F6_3);
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    //Record not found, add it
                    //                    SQLQry_F6_2 = "INSERT INTO customer_master (Aid";
                    //                    SQLQry_F6_3 = "VALUES ('l_AId'";
                    //                    if (CustomerFirstName != "-1")
                    //                    {
                    //                        SQLQry_F6_2 += ", First_Name";
                    //                        SQLQry_F6_3 += ", 'CustomerFirstName'";
                    //                    }
                    //                    if (CustomerLastName != "-1")
                    //                    {
                    //                        SQLQry_F6_2 += ", Last_Name";
                    //                        SQLQry_F6_3 += ", 'CustomerLastName'";
                    //                    }
                    //                    if (CustomerAddress != "-1")
                    //                    {
                    //                        SQLQry_F6_2 += ", Address";
                    //                        SQLQry_F6_3 += ", 'CustomerAddress'";
                    //                    }
                    //                    if (WebUserName != "-1")
                    //                    {
                    //                        SQLQry_F6_2 += ", WebUserName";
                    //                        SQLQry_F6_3 += ", 'WebUserName'";
                    //                    }
                    //                    if (WebPassword != "-1")
                    //                    {
                    //                        //Same passcode as in account
                    //                        SQLQry_F6_2 += ", WebPassword";
                    //                        SQLQry_F6_3 += ", 'l_PassCode'";
                    //                    }
                    //                    if (CustomerCity != "-1")
                    //                    {
                    //                        SQLQry_F6_2 += ", City";
                    //                        SQLQry_F6_3 += ", 'CustomerCity'";
                    //                    }
                    //                    if (CustomerState != "-1")
                    //                    {
                    //                        SQLQry_F6_2 += ", State_Name";
                    //                        SQLQry_F6_3 += ", 'CustomerState'";
                    //                    }
                    //                    if (CustomerZip_Code != "-1")
                    //                    {
                    //                        SQLQry_F6_2 += ", Zip_Code";
                    //                        SQLQry_F6_3 += ", 'CustomerZip_Code'";
                    //                    }
                    //                    if (CustomerCountry != "-1")
                    //                    {
                    //                        SQLQry_F6_2 += ", Country";
                    //                        SQLQry_F6_3 += ", '"+SUBSTR(CustomerCountry, 0, 20)+"'";
                    //                    }
                    //                    if (CustomerEmail_Address != "-1")
                    //                    {
                    //                        SQLQry_F6_2 += ", Email_Address";
                    //                        SQLQry_F6_3 += ", 'CustomerEmail_Address'";
                    //                    }
                    //                    SQLQry_F6_2 += ", RegisteredOn, ModifiedOn ) "+SQLQry_F6_3+", 'RegisteredDate', 'RegisteredDate")";
                    //                    CnExecute2(SQLQry_F6_2);
                    //                }
                    //                ReturnString = "Ok|Acc_Number";
                    //            }
                    //            else
                    //            {
                    //                ErrCode2 += ErrCode2 + 16384;
                    //                ReturnString = "0";
                    //            }
                    //        }
                    //        else if (Function == "add_to_user_minute")
                    //        {
                    //            Seconds_In_Package = Minutes_In_Package * 60;

                    //            SQLQry_F7_1 = "INSERT INTO user_minute (Acc_Number, Grp_Id, G_Seconds, G_UsedSeconds, ";
                    //            SQLQry_F7_1 += "R_Seconds, R_UsedSeconds, CreateDateTimeStamp, LastDateTimeStamp) ";
                    //            SQLQry_F7_1 += " VALUES ('Acc_Number', 'Group_Id', '0', '0', 'Seconds_In_Package', '0' , 'RegisteredDate', 'RegisteredDate")";
                    //            CnExecute2(SQLQry_F7_1);
                    //            ReturnString = "Ok|Acc_Number";
                    //        }
                    //        else if (Function == "add_to_payment_details")
                    //        {
                    //            if (strlen(FULL_CC_NUMBER) >= 1)
                    //            {
                    //                FIRST_ONE_CC = substr(FULL_CC_NUMBER, 0, 1);
                    //            }
                    //            else
                    //            {
                    //                FIRST_ONE_CC = "";
                    //            }
                    //            if (strlen(FULL_CC_NUMBER) >= 4)
                    //            {
                    //                LAST_FOUR_CC = substr(FULL_CC_NUMBER, -4);
                    //            }
                    //            else
                    //            {
                    //                LAST_FOUR_CC = "";
                    //            }

                    //            //if Charged_Amount is defined - calculate the Tax Component
                    //            if (!string.IsNullOrEmpty(Charged_Amount))
                    //                Charged_Amount = "-1";
                    //            if (!is_Numeric(Charged_Amount))
                    //                Charged_Amount = "-2";

                    //            Tax_Perc_Amount = "0.00";
                    //            AppFee_Static_Amount = "0.00";
                    //            AppFee_Perc_Amount = "0.00";

                    //            if (Charged_Amount > 0)
                    //            {
                    //                //Get Tax_Perc FROM Market
                    //                SQLQry_F14_5 = "SELECT ROUND( Tax_Perc * Plan_Amount, 2) AS Tax_Perc_Amount FROM market ";
                    //                SQLQry_F14_5 += "WHERE AreaCode = ""+substr(CallerId, 0, 3)+"' LIMIT 1";
                    //                QueryExe_F14_5 = CnExecute1(connectionstring, SQLQry_F14_5);
                    //                if (odbc_fetch_row(QueryExe_F14_5))
                    //                {
                    //                    Tax_Perc_Amount = odbc_result(QueryExe_F14_5, "Tax_Perc_Amount");
                    //                }

                    //                //Get Other Records FROM Payment_Plan_List
                    //                SQLQry_F14_6 = "SELECT ROUND( ApplicableFee_Perc * Plan_Amount, 2) AS AppFee_Perc_Amount, ";
                    //                SQLQry_F14_6 += "ApplicableFee_Static FROM payment_plan_list ";
                    //                SQLQry_F14_6 += "WHERE Id = "Plan_Id' LIMIT 1";
                    //                QueryExe_F14_6 = CnExecute1(connectionstring, SQLQry_F14_6);
                    //                if (odbc_fetch_row(QueryExe_F14_6))
                    //                {
                    //                    AppFee_Perc_Amount = odbc_result(QueryExe_F14_6, "AppFee_Perc_Amount");
                    //                    AppFee_Static_Amount = odbc_result(QueryExe_F14_6, "ApplicableFee_Static");
                    //                }
                    //            }

                    //            SQLQry_F8_1 = "INSERT INTO paymentdetails (Acc_Number, Grp_Id, RegistrationOn, LastExpiry, NewExpiry, ";
                    //            SQLQry_F8_1 += "PlanTakenID, FIRST_ONE_CC, LAST_FOUR_CC, EXP_DATE, FULL_CC_NUMBER, CVC, ";
                    //            SQLQry_F8_1 += "Amount, Tax_Perc_Amount, AppFee_Static_Amount, AppFee_Perc_Amount, ";
                    //            SQLQry_F8_1 += "packagevalidity, MinInPackage, Description,  ";
                    //            SQLQry_F8_1 += "ResponseCode, ResponseReasonCode, ResponseText, ApprovalCode, AVSResultCode, ";
                    //            SQLQry_F8_1 += "TransactionID, RegisteredBy, Source_Description) ";
                    //            SQLQry_F8_1 += "VALUES ('Acc_Number', 'Group_Id', 'RegisteredDate', 'Old_Expiry', 'New_Expiry', ";
                    //            SQLQry_F8_1 += "'Plan_Id', 'FIRST_ONE_CC', 'LAST_FOUR_CC', 'CC_EXPDATE', ";
                    //            SQLQry_F8_1 += "AES_Encrypt('FULL_CC_NUMBER', MD5('CC_Encryption_SALT")), ";
                    //            SQLQry_F8_1 += "AES_Encrypt('CVC', MD5('CVC_Encryption_SALT")), ";
                    //            SQLQry_F8_1 += "'Plan_Amount', 'Tax_Perc_Amount', 'AppFee_Static_Amount', 'AppFee_Perc_Amount', ";
                    //            SQLQry_F8_1 += "'Plan_Validity', 'Minutes_In_Package', 'Package_Description', ";
                    //            SQLQry_F8_1 += "'Response_Code', 'Response_Reason_Code', 'Response_Reason_Text', 'Approval_Code', 'AVS_Result_Code', ";
                    //            SQLQry_F8_1 += "'Transaction_Id', 'Payment_Type_Text', 'Web Transaction")";
                    //            CnExecute2(SQLQry_F8_1);
                    //            ReturnString = "Ok|Acc_Number";
                    //        }
                    //        else if (Function == "add_to_service_source")
                    //        {
                    //            SQLQry_F9_1 = "INSERT INTO servicesource (Acc_Number, Grp_Id, Source, AreaCode, OnDate) ";
                    //            SQLQry_F9_1 += "VALUES ('Acc_Number', 'Group_Id', 'Service_Source', 'Area_Code' , 'RegisteredDate")";
                    //            a = CnExecute2(SQLQry_F9_1);
                    //            ReturnString = "Ok|Acc_Number";
                    //        }
                    //        else if ((Function == "get_member_details") || (Function == "member_forgot_passcode"))
                    //        {
                    //            // Get Member Details (for forgot password)
                    //            SQLQry_F10_1 = "SELECT a.Acc_Number, a.PassCode, a.CallerId, ifNULL(c.Email_Address, '") as Email_Address ";
                    //            if (Function == "get_member_details")
                    //            {
                    //                SQLQry_F10_1 += ", ifNULL( c.First_Name, '' ) as First_Name, ifNULL( c.Last_Name, '' ) as Last_Name, ";
                    //                SQLQry_F10_1 += "ifNULL( c.Address, '' ) as address, ifNULL( c.City, '' ) as City, ifNULL( c.State_Name, '' ) as State_Name, ";
                    //                SQLQry_F10_1 += "ifNULL( c.Zip_Code, '' ) as zip_code, ifNULL( c.Country, '' ) as Country, ";
                    //                SQLQry_F10_1 += "a.AccountType, a.ExpiryDate ";
                    //            }
                    //            SQLQry_F10_1 += "FROM (";

                    //            if (CustomerEmail_Address != -1)
                    //            {
                    //                SQLQry_F10_1 += "SELECT AId, Email_Address, First_Name, Last_Name, Address, City, ";
                    //                SQLQry_F10_1 += "State_Name, Zip_Code, Country FROM customer_master ";
                    //                SQLQry_F10_1 += "WHERE Email_Address = "CustomerEmail_Address' ";
                    //                SQLQry_F10_1 += ") AS c INNER JOIN account as a ON a.ID = c.Aid ";
                    //                SQLQry_F10_1 += "WHERE a.Grp_Id = "Group_Id' LIMIT 1";
                    //            }
                    //            else
                    //            {
                    //                if (Acc_Number != -1)
                    //                {
                    //                    WhereClause = " Acc_Number = "Acc_Number'";
                    //                }
                    //                else
                    //                {
                    //                    WhereClause = " PassCode = "PassCode' AND CallerId = "CallerId'";
                    //                }

                    //                SQLQry_F10_1 += "SELECT ID, Acc_Number, PassCode, CallerId, AccountType, ";
                    //                SQLQry_F10_1 += "ExpiryDate FROM account ";
                    //                SQLQry_F10_1 += "WHERE "+WhereClause+" AND Grp_Id = "Group_Id' ";
                    //                SQLQry_F10_1 += ") AS a LEFT JOIN customer_master as c ON a.ID = c.Aid ";
                    //                SQLQry_F10_1 += "LIMIT 1";
                    //            }

                    //            QueryExe_F10_1 = CnExecute1(connectionstring, SQLQry_F10_1);

                    //            fp = fopen('lidn.txt', 'w");
                    //            fwrite(fp, SQLQry_F10_1);
                    //            fclose(fp);

                    //            if (odbc_fetch_row(QueryExe_F10_1))
                    //            {
                    //                Ret_Fn10_Parm1 = odbc_result(QueryExe_F10_1, "Acc_Number");
                    //                Ret_Fn10_Parm2 = odbc_result(QueryExe_F10_1, "PassCode");
                    //                Ret_Fn10_Parm3 = odbc_result(QueryExe_F10_1, "CallerId");
                    //                Ret_Fn10_Parm13 = odbc_result(QueryExe_F10_1, "Email_address");

                    //                if (Function == "get_member_details")
                    //                {
                    //                    Ret_Fn10_Parm4 = odbc_result(QueryExe_F10_1, "AccountType");
                    //                    Ret_Fn10_Parm5 = odbc_result(QueryExe_F10_1, "ExpiryDate");
                    //                    Ret_Fn10_Parm6 = odbc_result(QueryExe_F10_1, "First_Name");
                    //                    Ret_Fn10_Parm7 = odbc_result(QueryExe_F10_1, "Last_Name");
                    //                    Ret_Fn10_Parm8 = odbc_result(QueryExe_F10_1, "address");
                    //                    Ret_Fn10_Parm9 = odbc_result(QueryExe_F10_1, "City");
                    //                    Ret_Fn10_Parm10 = odbc_result(QueryExe_F10_1, "State_Name");
                    //                    Ret_Fn10_Parm11 = odbc_result(QueryExe_F10_1, "zip_code");
                    //                    Ret_Fn10_Parm12 = odbc_result(QueryExe_F10_1, "Country");

                    //                    //				Ret_Fn10_Parm10	= "CA';
                    //                    ReturnString = "Ok|Ret_Fn10_Parm1|Ret_Fn10_Parm2|Ret_Fn10_Parm3|Ret_Fn10_Parm4|Ret_Fn10_Parm5|";
                    //                    ReturnString += "Ret_Fn10_Parm6|Ret_Fn10_Parm7|Ret_Fn10_Parm8|Ret_Fn10_Parm9|";
                    //                    ReturnString += "Ret_Fn10_Parm10|Ret_Fn10_Parm11|Ret_Fn10_Parm12|";
                    //                    ReturnString += "Ret_Fn10_Parm13";
                    //                }
                    //                else
                    //                {
                    //                    ReturnString = "Ok|Ret_Fn10_Parm1|Ret_Fn10_Parm2|Ret_Fn10_Parm3|Ret_Fn10_Parm13";
                    //                }
                    //            }
                    //            else
                    //            {
                    //                //No matching account found
                    //                ErrCode += SysError + 16;
                    //            }
                    //        }
                    //        else if (Function == "update_account")
                    //        {
                    //            //GET Id FROM account
                    //            SQLQry_F11_1 = "SELECT Id FROM account WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //            QueryExe_F11_1 = CnExecute1(connectionstring, SQLQry_F11_1);
                    //            l_AId = odbc_result(QueryExe_F11_1, "Id");

                    //            SQLQry_F11_2 = "UPDATE account SET CallerID = "CallerId', ExpiryDate = "New_Expiry', ";
                    //            SQLQry_F11_2 += "AccountType = "AccountType', Active0In1 = "Active0In1' ";
                    //            SQLQry_F11_2 += "WHERE account.ID = "l_AId' LIMIT 1";
                    //            CnExecute2(SQLQry_F11_2);
                    //            ReturnString = "Ok|Acc_Number";
                    //        }
                    //        else if (Function == "update_user_minute")
                    //        {
                    //            //CHECK User_Minute Table
                    //            SQLQry_F12_1 = "SELECT Acc_Number FROM user_minute WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //            QueryExe_F12_1 = CnExecute1(connectionstring, SQLQry_F12_1);
                    //            if (odbc_fetch_row(QueryExe_F12_1))
                    //            {
                    //                Seconds_In_Package = Minutes_In_Package * 60;

                    //                //Record found, update it - UPDATE User Minute
                    //                SQLQry_F12_2 = "UPDATE user_minute SET R_Seconds = R_Seconds + Seconds_In_Package, ";
                    //                SQLQry_F12_2 += "LastDateTimeStamp = "RegisteredDate' WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //                CnExecute2(SQLQry_F12_2);
                    //            }
                    //            else
                    //            {
                    //                Seconds_In_Package = Minutes_In_Package * 60;

                    //                SQLQry_F12_3 = "INSERT INTO user_minute (Acc_Number, Grp_Id, G_Seconds, G_UsedSeconds, ";
                    //                SQLQry_F12_3 += "R_Seconds, R_UsedSeconds, CreateDateTimeStamp, LastDateTimeStamp) ";
                    //                SQLQry_F12_3 += " VALUES ('Acc_Number', 'Group_Id', '0', '0', 'Seconds_In_Package', '0', Now(), Now())";
                    //                CnExecute2(SQLQry_F12_3);
                    //            }
                    //            ReturnString = "Ok|Acc_Number";
                    //        }
                    //        else if (Function == "update_customer_master")
                    //        {
                    //            //GET Id FROM account
                    //            SQLQry_F13_1 = "SELECT Id, Acc_Number, Passcode FROM account WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //            QueryExe_F13_1 = CnExecute1(connectionstring, SQLQry_F13_1);
                    //            if (odbc_fetch_row(QueryExe_F13_1))
                    //            {
                    //                // Account exists
                    //                l_AId = odbc_result(QueryExe_F13_1, "Id");
                    //                WebUserName = odbc_result(QueryExe_F13_1, "Acc_Number");
                    //                WebPassword = odbc_result(QueryExe_F13_1, "Passcode");

                    //                //CHECK Customer_Master Table
                    //                SQLQry_F13_2 = "SELECT AId FROM customer_master WHERE AId = "l_AId' LIMIT 1";
                    //                QueryExe_F13_2 = CnExecute1(connectionstring, SQLQry_F13_2);
                    //                if (odbc_fetch_row(QueryExe_F13_2))
                    //                {
                    //                    //Record found, update it
                    //                    UpdQry_F13_1 = add_string_if_not_blank(UpdQry_F13_1, 'First_Name', CustomerFirstName);
                    //                    UpdQry_F13_1 = add_string_if_not_blank(UpdQry_F13_1, 'Last_Name', CustomerLastName);
                    //                    UpdQry_F13_1 = add_string_if_not_blank(UpdQry_F13_1, 'Email_Address', CustomerEmail_Address);
                    //                    UpdQry_F13_1 = add_string_if_not_blank(UpdQry_F13_1, 'Address', CustomerAddress);
                    //                    UpdQry_F13_1 = add_string_if_not_blank(UpdQry_F13_1, 'City', CustomerCity);
                    //                    UpdQry_F13_1 = add_string_if_not_blank(UpdQry_F13_1, 'State_Name', CustomerState);
                    //                    UpdQry_F13_1 = add_string_if_not_blank(UpdQry_F13_1, 'Zip_Code', CustomerZip_Code);
                    //                    UpdQry_F13_1 = add_string_if_not_blank(UpdQry_F13_1, 'Country', CustomerCountry);

                    //                    if (strlen(UpdQry_F13_1) != 0)
                    //                    {
                    //                        SQLQry_F13_3 = "UPDATE customer_master SET UpdQry_F13_1, ModifiedOn = "RegisteredDate' ";
                    //                        SQLQry_F13_3 += "WHERE customer_master.AId = "l_AId' LIMIT 1";
                    //                        CnExecute2(SQLQry_F13_3);
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    //Record not found, add it
                    //                    SQLQry_F13_4 = "INSERT INTO customer_master (Aid";
                    //                    SQLQry_F13_5 = "VALUES ('l_AId'";
                    //                    if (CustomerFirstName != "-1")
                    //                    {
                    //                        SQLQry_F13_4 += ", First_Name";
                    //                        SQLQry_F13_5 += ", 'CustomerFirstName'";
                    //                    }

                    //                    if (CustomerLastName != "-1")
                    //                    {
                    //                        SQLQry_F13_4 += ", Last_Name";
                    //                        SQLQry_F13_5 += ", 'CustomerLastName'";
                    //                    }
                    //                    if (CustomerEmail_Address != "-1")
                    //                    {
                    //                        SQLQry_F13_4 += ", Email_Address";
                    //                        SQLQry_F13_5 += ", 'CustomerEmail_Address'";
                    //                    }
                    //                    if (WebUserName != "-1")
                    //                    {
                    //                        SQLQry_F13_4 += ", WebUserName";
                    //                        SQLQry_F13_5 += ", 'WebUserName'";
                    //                    }
                    //                    if (WebPassword != "-1")
                    //                    {
                    //                        SQLQry_F13_4 += ", WebPassword";
                    //                        SQLQry_F13_5 += ", 'WebPassword'";
                    //                    }
                    //                    if (CustomerAddress != "-1")
                    //                    {
                    //                        SQLQry_F13_4 += ", Address";
                    //                        SQLQry_F13_5 += ", 'CustomerAddress'";
                    //                    }
                    //                    if (CustomerCity != "-1")
                    //                    {
                    //                        SQLQry_F13_4 += ", City";
                    //                        SQLQry_F13_5 += ", 'CustomerCity'";
                    //                    }
                    //                    if (CustomerState != "-1")
                    //                    {
                    //                        SQLQry_F13_4 += ", State_Name";
                    //                        SQLQry_F13_5 += ", 'CustomerState'";
                    //                    }
                    //                    if (CustomerZip_Code != "-1")
                    //                    {
                    //                        SQLQry_F13_4 += ", Zip_Code";
                    //                        SQLQry_F13_5 += ", 'CustomerZip_Code'";
                    //                    }
                    //                    if (CustomerCountry != "-1")
                    //                    {
                    //                        SQLQry_F13_4 += ", Country";
                    //                        SQLQry_F13_5 += ", '"+SUBSTR(CustomerCountry, 0, 20)+"'";
                    //                    }

                    //                    SQLQry_F13_4 += ", RegisteredOn, ModifiedOn ) "+SQLQry_F13_5+", 'RegisteredDate', 'RegisteredDate")";
                    //                    CnExecute2(SQLQry_F13_4);
                    //                }
                    //                ReturnString = "Ok|Acc_Number";
                    //            }
                    //            else
                    //            {
                    //                ErrCode2 += ErrCode2 + 16384;
                    //                ReturnString = "0";
                    //            }
                    //        }
                    //        else if (Function == "add_complete_paid_account")
                    //        {
                    //            if (strlen(FULL_CC_NUMBER) >= 1)
                    //            {
                    //                FIRST_ONE_CC = substr(FULL_CC_NUMBER, 0, 1);
                    //            }
                    //            else
                    //            {
                    //                FIRST_ONE_CC = "";
                    //            }
                    //            if (strlen(FULL_CC_NUMBER) >= 4)
                    //            {
                    //                LAST_FOUR_CC = substr(FULL_CC_NUMBER, -4);
                    //            }
                    //            else
                    //            {
                    //                LAST_FOUR_CC = "";
                    //            }

                    //            //CHECK if ACCOUNT ALREADY EXISTS ( BASED ON CALLERID AND GROUP ACCOUNT MAY HAVE EXISTED )
                    //            l_AId = 0;

                    //            StrSQL_F14_00 = "SELECT Id FROM account ";
                    //            StrSQL_F14_00 += "WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //            QueryExe_F14_00 = CnExecute1(connectionstring, StrSQL_F14_00);
                    //            if (odbc_fetch_row(QueryExe_F14_00))
                    //            {
                    //                l_AId = odbc_result(QueryExe_F14_00, "Id");
                    //            }

                    //            if (l_AId == 0)
                    //            {
                    //                //Update accountids
                    //                SQLQry_F14_0 = "UPDATE accountids SET Grp_Id"+Group_Id+" = "1' WHERE Acc_Number = "Acc_Number' LIMIT 1";
                    //                CnExecute2(SQLQry_F14_0);

                    //                //Add to Account
                    //                SQLQry_F14_1 = "INSERT INTO account (Acc_Number, PassCode, CallerID, Gender, ";
                    //                SQLQry_F14_1 += "RegisteredOn, ExpiryDate, Grp_Id, AccRegisteredOn, AccountType, Active0In1) ";
                    //                SQLQry_F14_1 += "VALUES ('Acc_Number', 'PassCode', 'CallerId', '1', ";
                    //                SQLQry_F14_1 += "'RegisteredDate', 'PlanExpiresOn', 'Group_Id', 'RegisteredDate', 'AccountType', 'Active0In1")";
                    //                CnExecute2(SQLQry_F14_1);

                    //                //Add to Customer Master
                    //                SQLQry_F14_2 = "SELECT Id FROM account WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //                QueryExe_F14_2 = CnExecute1(connectionstring, SQLQry_F14_2);
                    //                l_AId = odbc_result(QueryExe_F14_2, "Id");

                    //                SQLQry_F14_3 = "INSERT INTO customer_master (Aid, First_Name, Last_Name, WebUserName, WebPassword, ";
                    //                SQLQry_F14_3 += "Address, City, State_Name, Zip_Code, Country, ";
                    //                SQLQry_F14_3 += "Email_Address, RegisteredOn, ModifiedOn ) ";
                    //                SQLQry_F14_3 += "VALUES ('l_AId', 'CustomerFirstName', 'CustomerLastName', 'WebUserName', 'PassCode', ";
                    //                SQLQry_F14_3 += "'CustomerAddress', 'CustomerCity', 'CustomerState', 'CustomerZip_Code', 'CustomerCountry', ";
                    //                SQLQry_F14_3 += "'CustomerEmail_Address', 'RegisteredDate', 'RegisteredDate")";
                    //                CnExecute2(SQLQry_F14_3);

                    //                Seconds_In_Package = Minutes_In_Package * 60;

                    //                //Add to User Minute
                    //                SQLQry_F14_4 = "INSERT INTO user_minute (Acc_Number, Grp_Id, G_Seconds, G_UsedSeconds, ";
                    //                SQLQry_F14_4 += "R_Seconds, R_UsedSeconds, CreateDateTimeStamp, LastDateTimeStamp) ";
                    //                SQLQry_F14_4 += " VALUES ('Acc_Number', 'Group_Id', '0', '0', 'Seconds_In_Package', '0' , 'RegisteredDate', 'RegisteredDate")";
                    //                CnExecute2(SQLQry_F14_4);

                    //                //Add to Service Source
                    //                SQLQry_F14_6 = "INSERT INTO servicesource (Acc_Number, Grp_Id, Source, AreaCode, OnDate) ";
                    //                SQLQry_F14_6 += "VALUES ('Acc_Number', 'Group_Id', 'Service_Source', 'Area_Code' , 'RegisteredDate")";
                    //                CnExecute2(SQLQry_F14_6);

                    //                //Remove from acc_number_web TABLE
                    //                SQLQry_F14_7 = "DELETE FROM acc_number_web WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //                CnExecute2(SQLQry_F14_7);
                    //            }
                    //            else
                    //            {
                    //                //UPDATE Account
                    //                SQLQry_F14_41 = "UPDATE account SET AccountType = "AccountType', ExpiryDate = "PlanExpiresOn' ";
                    //                SQLQry_F14_41 += "WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //                CnExecute2(SQLQry_F14_41);

                    //                //Add to User Minute
                    //                Seconds_In_Package = Minutes_In_Package * 60;

                    //                SQLQry_F14_42 = "UPDATE user_minute SET R_Seconds = R_Seconds + 'Seconds_In_Package' ";
                    //                SQLQry_F14_42 += "WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //                CnExecute2(SQLQry_F14_42);
                    //            }


                    //            //if Charged_Amount is defined - calculate the Tax Component
                    //            if (!string.IsNullOrEmpty(Charged_Amount))
                    //                Charged_Amount = "-1";
                    //            if (!is_Numeric(Charged_Amount))
                    //                Charged_Amount = "-2";

                    //            Tax_Perc_Amount = "0.00";
                    //            AppFee_Static_Amount = "0.00";
                    //            AppFee_Perc_Amount = "0.00";
                    //            if (Charged_Amount > 0)
                    //            {
                    //                //Get Tax_Perc FROM Market
                    //                SQLQry_F14_5 = "SELECT ROUND( Tax_Perc * Plan_Amount, 2) AS Tax_Perc_Amount FROM market ";
                    //                SQLQry_F14_5 += "WHERE AreaCode = ""+substr(CallerId, 0, 3)+"' LIMIT 1";
                    //                QueryExe_F14_5 = CnExecute1(connectionstring, SQLQry_F14_5);
                    //                if (odbc_fetch_row(QueryExe_F14_5))
                    //                {
                    //                    Tax_Perc_Amount = odbc_result(QueryExe_F14_5, "Tax_Perc_Amount");
                    //                }

                    //                //Get Other Records FROM Payment_Plan_List
                    //                SQLQry_F14_6 = "SELECT ROUND( ApplicableFee_Perc * Plan_Amount, 2) AS AppFee_Perc_Amount, ";
                    //                SQLQry_F14_6 += "ApplicableFee_Static FROM payment_plan_list ";
                    //                SQLQry_F14_6 += "WHERE Id = "Plan_Id' LIMIT 1";
                    //                QueryExe_F14_6 = CnExecute1(connectionstring, SQLQry_F14_6);
                    //                if (odbc_fetch_row(QueryExe_F14_6))
                    //                {
                    //                    AppFee_Perc_Amount = odbc_result(QueryExe_F14_6, "AppFee_Perc_Amount");
                    //                    AppFee_Static_Amount = odbc_result(QueryExe_F14_6, "ApplicableFee_Static");
                    //                }
                    //            }

                    //            //Add to Payment Details
                    //            SQLQry_F14_7 = "INSERT INTO paymentdetails (Acc_Number, Grp_Id, RegistrationOn, LastExpiry, NewExpiry, ";
                    //            SQLQry_F14_7 += "PlanTakenID, FIRST_ONE_CC, LAST_FOUR_CC, EXP_DATE, FULL_CC_NUMBER, CVC, ";
                    //            SQLQry_F14_7 += "Amount, Tax_Perc_Amount, AppFee_Static_Amount, AppFee_Perc_Amount, ";
                    //            SQLQry_F14_7 += "packagevalidity, MinInPackage, Description,  ";
                    //            SQLQry_F14_7 += "ResponseCode, ResponseReasonCode, ResponseText, ApprovalCode, AVSResultCode, ";
                    //            SQLQry_F14_7 += "TransactionID, RegisteredBy, Source_Description) ";
                    //            SQLQry_F14_7 += "VALUES ('Acc_Number', 'Group_Id', 'RegisteredDate', 'Old_Expiry', 'New_Expiry', ";
                    //            SQLQry_F14_7 += "'Plan_Id', 'FIRST_ONE_CC', 'LAST_FOUR_CC', 'CC_EXPDATE', ";
                    //            SQLQry_F14_7 += "AES_Encrypt('FULL_CC_NUMBER', MD5('CC_Encryption_SALT")), ";
                    //            SQLQry_F14_7 += "AES_Encrypt('CVC', MD5('CVC_Encryption_SALT")), ";
                    //            SQLQry_F14_7 += "'Plan_Amount', 'Tax_Perc_Amount', 'AppFee_Static_Amount', 'AppFee_Perc_Amount', ";
                    //            SQLQry_F14_7 += "'Plan_Validity', 'Minutes_In_Package', 'Package_Description', ";
                    //            SQLQry_F14_7 += "'Response_Code', 'Response_Reason_Code', 'Response_Reason_Text', 'Approval_Code', 'AVS_Result_Code', ";
                    //            SQLQry_F14_7 += "'Transaction_Id', 'Payment_Type_Text', 'Web Transaction")";
                    //            CnExecute2(SQLQry_F14_7);

                    //            ReturnString = "Ok|Acc_Number";
                    //        }
                    //        else if (Function == "validate")
                    //        {


                    //            //GET Id FROM account
                    //            SQLQry_F15_1 = "SELECT Id FROM account WHERE Acc_Number = "Acc_Number' AND Passcode = "PassCode' AND Grp_Id = "Group_Id' LIMIT 1";



                    //            QueryExe_F15_1 = CnExecute1(connectionstring, SQLQry_F15_1);
                    //            if (odbc_fetch_row(QueryExe_F15_1))
                    //            {
                    //                ReturnString = "Ok|Acc_Number";
                    //            }
                    //            else
                    //            {
                    //                ReturnString = "No|Account does not exist";
                    //            }
                    //        }
                    //        else if (Function == "modify_customer_info")
                    //        {
                    //            if ((string.IsNullOrEmpty(ModifiedOn)))
                    //            {
                    //                ModifiedOn = DateTime.Now.ToString();
                    //            }

                    //            if ((PassCode == -1) && (CallerId == -1))
                    //            {
                    //                //GET Id FROM account
                    //                SQLQry_F16_1 = "SELECT Id, Passcode FROM account WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //                QueryExe_F16_1 = odbc_do(connectionstring, SQLQry_F16_1);
                    //                l_AId = odbc_result(QueryExe_F16_1, "Id");
                    //                l_Passcode = odbc_result(QueryExe_F16_1, "Passcode");

                    //                SQLQry_F16_2 = "UPDATE customer_master SET WebPassword  = "l_Passcode',  ModifiedOn = "ModifiedOn' ";
                    //                SQLQry_F16_2 += "WHERE Aid = "l_AId' LIMIT 1";
                    //                CnExecute2(SQLQry_F16_2);
                    //            }
                    //            else if ((PassCode == -1) && (WebPassword == -1))
                    //            {
                    //                SQLQry_F16_3 = "UPDATE account SET CallerID = "CallerId' ";
                    //                SQLQry_F16_3 += "WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //                CnExecute2(SQLQry_F16_3);
                    //            }
                    //            else if ((CallerId == -1) && (WebPassword == -1))
                    //            {
                    //                //GET Id FROM account
                    //                SQLQry_F16_4 = "SELECT Id FROM account WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //                QueryExe_F16_4 = odbc_do(connectionstring, SQLQry_F16_4);
                    //                l_AId = odbc_result(QueryExe_F16_4, "Id");

                    //                SQLQry_F16_5 = "UPDATE account SET PassCode = "PassCode' ";
                    //                SQLQry_F16_5 += "WHERE Id = "l_AId' LIMIT 1";
                    //                CnExecute2(SQLQry_F16_5);

                    //                SQLQry_F16_6 = "UPDATE customer_master SET WebPassword  = "Passcode',  ModifiedOn = "ModifiedOn' ";
                    //                SQLQry_F16_6 += "WHERE Aid = "l_AId' LIMIT 1";
                    //                CnExecute2(SQLQry_F16_6);
                    //            }
                    //            ReturnString = "Ok|Acc_Number";
                    //        }
                    //        else if (Function == "get_member_minutes")
                    //        {
                    //            //GET Account related details FROM account
                    //            SQLQry_F17_1 = "SELECT Id, RegisteredOn, ExpiryDate, AccountType, Gender FROM account WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //            QueryExe_F17_1 = odbc_do(connectionstring, SQLQry_F17_1);
                    //            if (odbc_fetch_row(QueryExe_F17_1))
                    //            {
                    //                // Account exists
                    //                l_AId = odbc_result(QueryExe_F17_1, "Id");
                    //                Acc_RegisteredOn = odbc_result(QueryExe_F17_1, "RegisteredOn");
                    //                Acc_ExpiryDate = odbc_result(QueryExe_F17_1, "ExpiryDate");
                    //                Acc_AccountType = odbc_result(QueryExe_F17_1, "AccountType");
                    //                Gender = odbc_result(QueryExe_F17_1, "Gender");

                    //                //GET Available Minutes details FROM user_minute
                    //                SQLQry_F17_2 = "SELECT FLO||( ifNULL( R_Seconds - R_UsedSeconds, 0)/60) AS RegisteredMinutesLeft, ";
                    //                SQLQry_F17_2 += "FLO||( ifNULL( G_Seconds - G_UsedSeconds, 0)/60) AS GuestMinutesLeft FROM user_minute ";
                    //                SQLQry_F17_2 += "WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //                QueryExe_F17_2 = odbc_do(connectionstring, SQLQry_F17_2);
                    //                if (odbc_fetch_row(QueryExe_F17_2))
                    //                {
                    //                    // Account exists
                    //                    Acc_RegisMinutes = odbc_result(QueryExe_F17_2, "RegisteredMinutesLeft");
                    //                    Acc_GuestMinutes = odbc_result(QueryExe_F17_2, "GuestMinutesLeft");
                    //                    if (Acc_RegisMinutes >= "0")
                    //                    {
                    //                        Acc_GuestMinutes = "0";
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    // No entries under User_Minute
                    //                    Acc_RegisMinutes = "0";
                    //                    Acc_GuestMinutes = "0";
                    //                }
                    //                ReturnString = "Ok|Acc_RegisteredOn|Acc_ExpiryDate|Acc_AccountType|Acc_RegisMinutes|Acc_GuestMinutes|Gender";
                    //            }
                    //            else
                    //            {
                    //                ErrCode2 += ErrCode2 + 16384;
                    //                ReturnString = "0";
                    //            }
                    //        }
                    //        else if (Function == "process_mobile_charge")
                    //        {
                    //            //710 - "1001 - 1500"; 2900 - "2001 - 2500"; VM01 - "3001 - 3500"
                    //            l_Port = "1";
                    //            //Check for Active Server
                    //            SQLQry_F20_1 = "SELECT s.PortStop + 1 AS PortLog FROM serverdefns AS s INNER JOIN ";
                    //            SQLQry_F20_1 += "(SELECT Port FROM liveusers ORDER BY LoginOn DESC LIMIT 1) AS l ON ";
                    //            SQLQry_F20_1 += "l.Port BETWEEN s.PortStart AND s.PortStop";
                    //            QueryExe_F20_1 = CnExecute1(connectionstring, SQLQry_F20_1);
                    //            if (odbc_fetch_row(QueryExe_F20_1))
                    //            {
                    //                //Read the #
                    //                l_Port = odbc_result(QueryExe_F20_1, "PortLog");
                    //            }

                    //            SQLQry_F20_2 = "INSERT INTO sms_queue_active (SubscriberNo, Acc_Number, Grp_Id, PassCode, Port, SMS_Id, Ticket_Id, ";
                    //            SQLQry_F20_2 += "CarrierId, ChargeType, ChargeAmount, Job_Time, Queue_Time, NextRetryAt, MessageSendAt, 'OM0_M1', ";
                    //            SQLQry_F20_2 += "ivr1sms2, Q0A1S2F3) VALUES (";
                    //            SQLQry_F20_2 += "'SubscriberNo', 'Acc_Number', 'Group_Id', 'PassCode', 'l_Port', 'SMS_Id', 'TicketId', ";
                    //            SQLQry_F20_2 += "'CarrierId', '1', 'ChargeAmount', Now(), Now(), Now(), Now(), '0', '2', '0")";
                    //            CnExecute2(SQLQry_F20_2);
                    //            ReturnString = "Ok|Acc_Number";
                    //        }
                    //        else if (Function == "insert_login_log")
                    //        {
                    //            SQLQry_F21_1 = "INSERT INTO login_log (SessionNo, Username, IPAddress, DateIn, TimeIn, LastTimeStamp) ";
                    //            SQLQry_F21_1 += "VALUES ('Session', 'CC_UserName', 'CC_IPAddress', 'DateIn', 'TimeIn', 'LastTimeStamp")";
                    //            CnExecute2(SQLQry_F21_1);
                    //            RetCode = "0";
                    //            ReturnString = "Ok";
                    //        }
                    //        else if (Function == "update_login_log")
                    //        {
                    //            SQLQry_F22_1 = "UPDATE login_log SET LastTimeStamp = "LastTimeStamp', DateOut = "DateOut', TimeOut = "TimeOut' WHERE SessionNo = "Session' ";
                    //            CnExecute2(SQLQry_F22_1);
                    //            RetCode = "0";
                    //            ReturnString = "Ok";
                    //        }
                    //        else if (Function == "admin_web_screening")
                    //        {
                    //            //Check for entry under additional_profile
                    //            SQLQry_F23_1 = "SELECT Id FROM account WHERE Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //            QueryExe_F23_1 = CnExecute1(connectionstring, SQLQry_F23_1);
                    //            if (odbc_fetch_row(QueryExe_F23_1))
                    //            {
                    //                // MAKE THE dr Request
                    //                if (App1Del2 == "1")
                    //                {
                    //                    drdata = "26,"+Acc_Number+",111,"+Group_Prefix;
                    //                }
                    //                else if (App1Del2 == "2")
                    //                {
                    //                    drdata = "26,"+Acc_Number+",222,"+Group_Prefix;
                    //                }
                    //                drfilename = doc_root+"\\Log\\Admin_Web_Screening_"+date("YmjHis")+"+dr";
                    //                writefile = fopen(drfilename, "w");
                    //                fwrite(writefile, drdata);
                    //                fclose(writefile);
                    //                ReturnString = "Ok";
                    //            }
                    //            else
                    //            {
                    //                ReturnString = "0, Account Not Find";
                    //            }
                    //        }
                    //        else if (Function == "getchargeamount")
                    //        {
                    //            //Read the Tax_Perc from market
                    //            SQLQry_F24_1 = "SELECT m.Tax_Perc FROM market AS m WHERE m.AreaCode = "Area_Code' LIMIT 1";
                    //            QueryExe_F24_1 = CnExecute1(connectionstring, SQLQry_F24_1);
                    //            if (odbc_fetch_row(QueryExe_F24_1))
                    //            {
                    //                //Read the Tax Perc
                    //                Tax_Perc = odbc_result(QueryExe_F24_1, "Tax_Perc");
                    //            }
                    //            else
                    //            {
                    //                //Undefined Area Code
                    //                Tax_Perc = "0";
                    //            }

                    //            //Read the Tax Component from payment_Plan_List Table if Tax > 0
                    //            SQLQry_F24_2 = "SELECT p.Amount, ";
                    //            SQLQry_F24_2 += "ROUND(p.Amount * ( 1 + Tax_Perc + p.ApplicableFee_Perc ) + p.ApplicableFee_Static, 2) ";
                    //            SQLQry_F24_2 += "AS ChargeAmount FROM payment_plan_list AS p WHERE p.Id = "Plan_Id' LIMIT 1";
                    //            QueryExe_F24_2 = CnExecute1(connectionstring, SQLQry_F24_2);
                    //            if (odbc_fetch_row(QueryExe_F24_2))
                    //            {
                    //                //Get the Amount
                    //                if (Tax_Perc > 0)
                    //                {
                    //                    ReturnString = "Ok|"+odbc_result(QueryExe_F24_2, "ChargeAmount");
                    //                }
                    //                else
                    //                {
                    //                    ReturnString = "Ok|"+odbc_result(QueryExe_F24_2, "Amount");
                    //                }
                    //            }
                    //            else
                    //            {
                    //                ReturnString = "0, Payment Plan Not Found";
                    //            }
                    //        }
                    //        else if (Function == "delete_completeaccount")
                    //        {
                    //            //Read if request already queued
                    //            SQLQry_F25_1 = "SELECT COUNT(*) AS Ct FROM action_queue WHERE FuncTable_Name = "DELETE_CompleteAccount' ";
                    //            SQLQry_F25_1 += "AND Acc_Number = "Acc_Number' AND Grp_Id = "Group_Id' LIMIT 1";
                    //            QueryExe_F25_1 = CnExecute1(connectionstring, SQLQry_F25_1);
                    //            if (odbc_fetch_row(QueryExe_F25_1))
                    //            {
                    //                //Read the #
                    //                ct = odbc_result(QueryExe_F25_1, "Ct");
                    //            }
                    //            else
                    //            {
                    //                //Will never happen
                    //                ct = "0";
                    //            }

                    //            if (ct <= 0)
                    //            {
                    //                //710 - "1001 - 1500"; 2900 - "2001 - 2500"; VM01 - "3001 - 3500"
                    //                Port = "1";
                    //                //Check for Active Server
                    //                SQLQry_F25_2 = "SELECT s.PortStart FROM serverdefns AS s INNER JOIN ";
                    //                SQLQry_F25_2 += "(SELECT Port FROM liveusers ORDER BY LoginOn DESC LIMIT 1) AS l ON ";
                    //                SQLQry_F25_2 += "l.Port BETWEEN s.PortStart AND s.PortStop";
                    //                QueryExe_F25_2 = CnExecute1(connectionstring, SQLQry_F25_2);
                    //                if (odbc_fetch_row(QueryExe_F25_2))
                    //                {
                    //                    //Read the #
                    //                    Port = odbc_result(QueryExe_F25_2, "PortStart");
                    //                }

                    //                //INSERT INTO action_queue for maintenance to take care of it
                    //                SQLQry_F25_3 = "INSERT IGNORE INTO action_queue (QDateTimeStamp, Function1Table2, FuncTable_Name, ";
                    //                SQLQry_F25_3 += "Field_Name, Acc_Number, Grp_Id, Port, WhereClause, Action) ";
                    //                SQLQry_F25_3 += "VALUES( Now(), '1', 'DELETE_CompleteAccount', '', 'Acc_Number', 'Group_Id', ";
                    //                SQLQry_F25_3 += "'Port', '', 'EXECUTE")";
                    //                CnExecute2(SQLQry_F25_3);

                    //                ReturnString = "0k|Delete account request queued.";
                    //            }
                    //            else
                    //            {
                    //                ReturnString = "Request canceled|Duplicate delete account request - canceled.";
                    //            }
                    //        }
                    //        else if (Function == "read_misc")
                    //        {
                    //            arr_retval = array();
                    //            //Get Setting Details
                    //            SQLQry_F26_1 = "SELECT SettingName, SettingValue FROM misc WHERE ForWeb = 1 order by SettingName";
                    //            //SQLQry_F26_1 = "SELECT * FROM misc WHERE ForWeb = 1 AND ID NOT IN (4, 5, 79, 72, 80, 856, 81, 48, 27, 26, 29, 30, 33, 34, 47, 84, 85, 86, 87, 88, 89, 69, 70) order by SettingName";

                    //            QueryExe_F26_1 = CnExecute1(connectionstring, SQLQry_F26_1);
                    //            while (odbc_fetch_row(QueryExe_F26_1))
                    //            {
                    //                //Read the values
                    //                arr_retval[odbc_result(QueryExe_F26_1, "SettingName")] = odbc_result(QueryExe_F26_1, "SettingValue");
                    //                param_ct++;
                    //            }

                    //            arr_retval_s = json_encode(arr_retval);
                    //            ReturnString = "0k|param_ct parameters read.";
                    //        }
                    //        else if (Function == "set_misc")
                    //        {
                    //            //Update Setting Details
                    //            SQLQry_F27_1 = "UPDATE misc SET SettingValue = "SettingValue' WHERE SettingName = "SettingName' LIMIT 1";

                    //            CnExecute2(SQLQry_F27_1);
                    //            ReturnString = "0k|Value updated.";
                    //        }
                    //        else if (Function == "set_primary_apiserver")
                    //        {
                    //            //Read count(*) of api_servers - will define max priority
                    //            SQLQry_F28_1 = "SELECT COUNT(*) AS Ct FROM api_servers";
                    //            QueryExe_F28_1 = CnExecute1(connectionstring, SQLQry_F28_1);
                    //            if (odbc_fetch_row(QueryExe_F28_1))
                    //            {
                    //                max_priority = odbc_result(QueryExe_F28_1, "Ct");
                    //                //Read current priority of Active Server IP
                    //                SQLQry_F28_2 = "SELECT ip_priority FROM api_servers WHERE ip_address = "ActiveServerIP' LIMIT 1";
                    //                QueryExe_F28_2 = CnExecute1(connectionstring, SQLQry_F28_2);
                    //                if (odbc_fetch_row(QueryExe_F28_2))
                    //                {
                    //                    curr_ip_priority = odbc_result(QueryExe_F28_2, "ip_priority");
                    //                    if (curr_ip_priority == "1")
                    //                    {
                    //                        //Return - IP is already set to be primary
                    //                        ReturnString = "0k|API Server already set as Primary.";
                    //                    }
                    //                    else
                    //                    {
                    //                        //New_Priority = Old_Priority - ( Active_Server_Priority - 1 )
                    //                        SQLQry_F28_3 = "UPDATE api_servers SET ip_priority = ip_priority - (curr_ip_priority - 1)";
                    //                        CnExecute2(SQLQry_F28_3);
                    //                        //if New_Priority <= 0 THEN New_Priority = New_Priority + MAX_PRIORITY
                    //                        SQLQry_F28_4 = "UPDATE api_servers SET ip_priority = ip_priority + max_priority WHERE ip_priority <= 0";
                    //                        CnExecute2(SQLQry_F28_4);
                    //                        ReturnString = "0k|API Server set as Primary.";
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    ReturnString = "0k|Requested Server IP not found.";
                    //                }
                    //            }
                    //            else
                    //            {
                    //                ReturnString = "0k|No entries found.";
                    //            }
                    //        }
                    //        else if (Function == "check_geo_location")
                    //        {
                    //            //Connect to Node3/ VMNode3 Database
                    //            conn_string_Node3 = Connect_ReadOnly_Database_Node3();

                    //            //Check whether IPv4 OR IPv6
                    //            if (strpos(Client_IP_Location, ":") === false)
                    //            {
                    //                //IP v4 Address
                    //                SQLQry_F29_1 = "SELECT Country, StateProv, City, AreaCode FROM ip2location_db15_ipv4m ";
                    //                SQLQry_F29_1 += "WHERE ip_byte1 = 1 * SUBSTRING_INDEX( 'Client_IP_Location', '.', 1 ) AND ";
                    //                SQLQry_F29_1 += "INET_ATON( 'Client_IP_Location' ) BETWEEN ip_start AND ip_end LIMIT 1";
                    //            }
                    //            else
                    //            {
                    //                //user_ip = inet_pton( Client_IP_Location );

                    //                //IP v6 Address
                    //                SQLQry_F29_1 = "SELECT Country, StateProv, City, AreaCode FROM ip2location_db15_ipv6m ";
                    //                SQLQry_F29_1 += "WHERE INET6_ATON('Client_IP_Location") BETWEEN ip_start AND ip_end LIMIT 1";
                    //            }
                    //            QueryExe_F29_1 = CnExecute1(conn_string_Node3, SQLQry_F29_1);

                    //            //print( "[Client_IP_Location] - [SQLQry_F29_1]" );
                    //            if (odbc_fetch_row(QueryExe_F29_1))
                    //            {
                    //                //READ geo location data
                    //                geo_country = odbc_result(QueryExe_F29_1, "Country");
                    //                geo_stateprov = odbc_result(QueryExe_F29_1, "StateProv");
                    //                geo_city = odbc_result(QueryExe_F29_1, "City");
                    //                geo_areacode = odbc_result(QueryExe_F29_1, "AreaCode");

                    //                ReturnString = "0k|geo_country|geo_stateprov|geo_city|geo_areacode";
                    //            }
                    //            else
                    //            {
                    //                //No matching records found
                    //                ReturnString = "0|0|0|0|0";
                    //            }

                    //            //Disconnect from Node3/ VMNode3 Database
                    //            Close_ReadOnly_Database_Node3(conn_string_Node3);
                    //        }
                    //        else if (Function == "get_node3_accesspoint_ip")
                    //        {
                    //            ip_address = "10.0.0.144";
                    //            //Get Setting Details
                    //            SQLQry_F30_1 = "SELECT SettingValue FROM misc WHERE SettingName = "Node3_AccessPoint_IP' LIMIT 1";

                    //            QueryExe_F30_1 = CnExecute1(connectionstring, SQLQry_F30_1);
                    //            if (odbc_fetch_row(QueryExe_F30_1))
                    //            {
                    //                //Read IP Address
                    //                ip_address = odbc_result(QueryExe_F30_1, "SettingValue");
                    //            }

                    //            ReturnString = "0k|ip_address";
                    //        }
                    //        else if (Function == "new_function")
                    //        {
                    //        }
                }
                #endregion  Db curd operations
                //    //disconnect from database 
                //    Close_Production_Database_Node1(connectionstring);
                #endregion
            }
            #endregion
            RetCode = ErrCode;
            GenerateErrorMessageIfAny(ref RetCode, ref ErrCode, ref ErrCode2, ref ErrCode3, SysError, ref ReturnString);

            #region Request String
            if ((string.IsNullOrEmpty(Function)))
            {
                ReqString = "AuthKey=" + AuthKey + "&WS_UserName=" + WS_UserName + "&WS_Password=" + WS_Password + "&Function=NOT_SET&ResultPage=" + ResultPage;
            }
            else
            {
                ReqString = "AuthKey=" + AuthKey + "&WS_UserName=" + WS_UserName + "&WS_Password=" + WS_Password + "&Function=" + Function;
                if (Group_Prefix != "")
                {
                    ReqString += "&Group_Prefix=" + Group_Prefix;
                }
                if ((Function == "get_new_acc_number") || (Function == "get_n_activate_new_acc_number"))
                {
                    ReqString += "&ResultPage=" + ResultPage;
                }
                else if ((Function == "get_member_details") || (Function == "member_forgot_passcode"))
                {
                    ReqString += "&Acc_Number=" + Acc_Number + "&CustomerEmail_Address=" + CustomerEmail_Address + "&PassCode=" + PassCode;
                    ReqString += "&CallerId=" + CallerId + "&ResultPage=" + ResultPage;
                }
                else if (Function == "add_new_account")
                {
                    ReqString += "&PassCode=" + PassCode + "&CallerId=" + CallerId + "&PlanExpiresOn=" + PlanExpiresOn;
                    ReqString += "&AccountType=" + AccountType + "&Active0In1=" + Active0In1 + "&ResultPage=" + ResultPage;
                }
                else if ((Function == "add_to_customer_master"))
                {
                    ReqString += "&CustomerFirstName=" + CustomerFirstName;
                    ReqString += "&CustomerLastName=" + CustomerLastName + "&WebUserName=" + WebUserName;
                    ReqString += "&WebPassword=" + WebPassword + "&CustomerAddress=" + CustomerAddress + "&CustomerCity=" + CustomerCity;
                    ReqString += "&CustomerState=" + CustomerState + "&CustomerZip_Code=" + CustomerZip_Code + "&CustomerCountry=" + CustomerCountry;
                    ReqString += "&CustomerEmail_Address=" + CustomerEmail_Address + "&ResultPage=" + ResultPage;
                }
                else if ((Function == "add_to_user_minute"))
                {
                    ReqString += "&Minutes_In_Package=" + Minutes_In_Package + "&ResultPage=" + ResultPage;
                }
                else if ((Function == "add_to_payment_details"))
                {
                    ReqString += "&CallerId=" + CallerId + "&Old_Expiry=" + Old_Expiry + "&New_Expiry=" + New_Expiry + "&Plan_Id=" + Plan_Id + "&Plan_Amount=" + Plan_Amount + "&Charged_Amount=" + Charged_Amount;
                    ReqString += "&Plan_Validity=" + Plan_Validity + "&Minutes_In_Package=" + Minutes_In_Package;
                    ReqString += "&Package_Description=" + Package_Description + "&FULL_CC_NUMBER=" + FULL_CC_NUMBER + "&CC_EXPDATE=" + CC_EXPDATE;
                    ReqString += "&CVC=" + CVC + "&Response_Code=" + Response_Code + "&Response_Reason_Code=" + Response_Reason_Code;
                    ReqString += "&Response_Reason_Text=" + Response_Reason_Text + "&Approval_Code=" + Approval_Code;
                    ReqString += "&AVS_Result_Code=" + AVS_Result_Code + "&Transaction_Id=" + Transaction_Id;
                    ReqString += "&Payment_Type_Text=" + Payment_Type_Text + "&ResultPage=" + ResultPage;
                }
                else if ((Function == "add_to_service_source"))
                {
                    ReqString += "&Service_Source=" + Service_Source + "&Area_Code=" + Area_Code + "&ResultPage=" + ResultPage;
                }
                else if (Function == "update_account")
                {
                    ReqString += "&CallerId=" + CallerId + "&New_Expiry=" + New_Expiry + "&AccountType=" + AccountType + "&Active0In1=" + Active0In1;
                    ReqString += "&ResultPage=" + ResultPage;
                }
                else if (Function == "update_user_minute")
                {
                    ReqString += "&Minutes_In_Package=" + Minutes_In_Package + "&ResultPage=" + ResultPage;
                }
                else if (Function == "update_customer_master")
                {
                    ReqString += "&CustomerFirstName=" + CustomerFirstName;
                    ReqString += "&CustomerLastName=" + CustomerLastName;
                    ReqString += "&CustomerAddress=" + CustomerAddress + "&CustomerCity=" + CustomerCity + "&CustomerState=" + CustomerState;
                    ReqString += "&CustomerZip_Code=" + CustomerZip_Code + "&CustomerCountry=" + CustomerCountry + "&CustomerEmail_Address=" + CustomerEmail_Address;
                    ReqString += "&ResultPage=" + ResultPage;
                }
                else if (Function == "add_complete_paid_account")
                {
                    ReqString += "&PassCode=" + PassCode + "&CallerId=" + CallerId + "&PlanExpiresOn=" + PlanExpiresOn;
                    ReqString += "&AccountType=" + AccountType + "&Active0In1=" + Active0In1 + "&CustomerFirstName=" + CustomerFirstName;
                    ReqString += "&CustomerLastName=" + CustomerLastName + "&WebUserName=" + WebUserName + "&WebPassword=" + WebPassword;
                    ReqString += "&CustomerAddress=" + CustomerAddress + "&CustomerCity=" + CustomerCity + "&CustomerState=" + CustomerState;
                    ReqString += "&CustomerZip_Code=" + CustomerZip_Code + "&CustomerCountry=" + CustomerCountry + "&CustomerEmail_Address=" + CustomerEmail_Address;
                    ReqString += "&Minutes_In_Package=" + Minutes_In_Package + "&Old_Expiry=" + Old_Expiry + "&New_Expiry=" + New_Expiry;
                    ReqString += "&Plan_Id=" + Plan_Id + "&Plan_Amount=" + Plan_Amount + "&Plan_Validity=" + Plan_Validity;
                    ReqString += "&Package_Description=" + Package_Description + "&FULL_CC_NUMBER=" + FULL_CC_NUMBER + "&CC_EXPDATE=" + CC_EXPDATE;
                    ReqString += "&CVC=" + CVC + "&Response_Code=" + Response_Code + "&Response_Reason_Code=" + Response_Reason_Code;
                    ReqString += "&Response_Reason_Text=" + Response_Reason_Text + "&Approval_Code=" + Approval_Code;
                    ReqString += "&AVS_Result_Code=" + AVS_Result_Code + "&Transaction_Id=" + Transaction_Id;
                    ReqString += "&Payment_Type_Text=" + Payment_Type_Text + "&Service_Source=" + Service_Source + "&Area_Code=" + Area_Code;
                    ReqString += "&ResultPage=" + ResultPage;
                }
                else if (Function == "validate")
                {
                    ReqString += "&Acc_Number=" + Acc_Number + "&PassCode=" + PassCode + "&ResultPage=" + ResultPage;
                }
                else if (Function == "modify_customer_info")
                {
                    ReqString += "&PassCode=" + PassCode + "&CallerId=" + CallerId + "&WebPassword=" + WebPassword + "&ResultPage=" + ResultPage;
                }
                else if (Function == "get_member_minutes")
                {
                    ReqString += "&Acc_Number=" + Acc_Number + "&ResultPage=" + ResultPage;
                }
                else if (Function == "process_mobile_charge")
                {
                    ReqString += "&SubscriberNo=" + SubscriberNo + "&Acc_Number=" + Acc_Number + "&PassCode=" + PassCode + "&SMS_Id=" + SMS_Id + "&TicketId=" + TicketId + "&CarrierId=" + CarrierId + "&ChargeAmount=" + ChargeAmount + "&ResultPage=" + ResultPage;
                }
                else if (Function == "insert_login_log")
                {
                    ReqString += "&Session=" + Session + "&CC_UserName=" + CC_UserName + "&CC_IPAddress=" + CC_IPAddress + "&DateIn=" + DateIn + "&TimeIn=" + TimeIn + "&LastTimeStamp=" + LastTimeStamp;
                }
                else if (Function == "update_login_log")
                {
                    ReqString += "&Session=" + Session + "&DateOut=" + DateOut + "&TimeOut=" + TimeOut + "&LastTimeStamp=" + LastTimeStamp;
                }
                else if (Function == "admin_web_screening")
                {
                    ReqString += "&Acc_Number=" + Acc_Number + "&App1Del2=" + App1Del2 + "&ResultPage=" + ResultPage;
                }
                else if (Function == "getchargeamount")
                {
                    ReqString += "&Area_Code=" + Area_Code + "&PlanId=" + Plan_Id + "&ResultPage=" + ResultPage;
                }
                else if (Function == "delete_completeaccount")
                {
                    ReqString += "&Acc_Number=" + Acc_Number + "&ResultPage=" + ResultPage;
                }
                else if (Function == "read_misc")
                {
                }
                else if (Function == "set_misc")
                {
                    ReqString += "&SettingName=" + SettingName + "&SettingValue=" + SettingValue + "&ResultPage=" + ResultPage;
                }
                else if (Function == "set_primary_apiserver")
                {
                    ReqString += "&ActiveServerIP=" + ActiveServerIP + "&ResultPage=" + ResultPage;
                }
                else if (Function == "check_geo_location")
                {
                    ReqString += "&Client_IP_Location=" + Client_IP_Location + "&ResultPage=" + ResultPage;
                }
                else if (Function == "get_node3_accesspoint_ip")
                {
                    ReqString += "&ResultPage=" + ResultPage;
                }
            }
            //RetString = "StatusCode=RetCode&ResultString=ReturnString";
            //TimeLog = sprintf("%s", date("Ymd His"));
            //log = STimeLog + "," + TimeLog + "," + ClientIP + "(IP)," + _SERVER['PHP_SELF'] + "(Page)," + "(Request for [ReqString]) -> (Return String [RetString])\r\n";

            //Send Request Back
            if ((1 == 2) && (ClientIP == "127.0.0.1"))
            {
                // LOG THE SYSTEM'S ACTIVITY
                //MakeLog(ll.log);
                ReturnString = str_replace(" ", "%20", ReturnString);
                //To test with local machine
                if (ReturnString == "Set_PragmaFile")
                {
                    DestPage = "ResultPage?History=txtfile&HFileSize=HFileSize";
                }
                else
                {
                    DestPage = "ResultPage?StatusCode=RetCode&ResultString=ReturnString";
                }

                //header("Location: " + DestPage);
                //exit;
            }
            else
            {
                if (Function == "read_misc")
                {
                    //print_r(arr_retval_s);
                }
                else
                {
                    //echo(ReturnString);
                    //// LOG THE SYSTEM'S ACTIVITY
                    //filename = doc_root + "\\Log\\WebServices_Log_" + date("Ym") + "+txt";
                    //writefile = fopen(filename, "a");
                    //fwrite(writefile, log);
                    //fclose(writefile);

                    ////Remote server
                    ////if ( ReturnString == "Set_PragmaFile" )
                    ////{
                    ////	DestPage = "http://" . ClientIP +"/" . ResultPage ."?History=txtfile&HFileSize=HFileSize";
                    ////}
                    ////else
                    ////{
                    ////	DestPage = "http://" . ResultPage ."?StatusCode=RetCode&ResultString=ReturnString";
                    ////}
                    ////header("Location: " . DestPage);
                    ////exit;
                }
            }
            #endregion
            return ReturnString;
        }

        private void GenerateErrorMessageIfAny(ref int RetCode, ref int ErrCode, ref int ErrCode2, ref int ErrCode3, int SysError, ref string ReturnString)
        {
            if (RetCode <= 0)
            {
                RetCode = -ErrCode2;
            }
            if (ErrCode > 0)
            {
                //ERROR in incoming data - Add Error Message
                if (ErrCode >= SysError + 16)
                {
                    ErrCode -= (SysError + 16);
                    ReturnString = "No matching account found (Function Get_Member_Details/ Member_Forgot_Passcode)";
                }
                if (ErrCode >= SysError + 8)
                {
                    ErrCode -= (SysError + 8);
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString = "No free records in accountids Table (Function Get_New_Acc_Number)";
                }
                if (ErrCode >= SysError + 4)
                {
                    ErrCode -= (SysError + 4);
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "WS_UserName credentials not matching";
                }
                if (ErrCode >= SysError + 2)
                {
                    ErrCode -= (SysError + 2);
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Invalid remote IP Address";
                }
                if (ErrCode >= SysError + 1)
                {
                    ErrCode -= (SysError + 1);
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Invalid Authorization Key";
                }
                if (ErrCode >= 524288)
                {
                    ErrCode -= 524288;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Group Id Not defined";
                }
                if (ErrCode >= 262144)
                {
                    ErrCode -= 262144;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "No incoming Search Parameter (Function Get_Member_Details/ Member_Forgot_Passcode)";
                }
                if (ErrCode >= 131072)
                {
                    ErrCode -= 131072;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Area_Code Not defined (Function Add_To_Service_Source)";
                }
                if (ErrCode >= 65536)
                {
                    ErrCode -= 65536;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Service_Source Not defined (Function Add_To_Service_Source)";
                }
                if (ErrCode >= 32768)
                {
                    ErrCode -= 32768;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Package_Description Not defined (Function Add_To_Payment_Details)";
                }
                if (ErrCode >= 16384)
                {
                    ErrCode -= 16384;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Minutes_In_Package Not defined (Function Add_To_Payment_Details)";
                }
                if (ErrCode >= 8192)
                {
                    ErrCode -= 8192;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Plan_Validity Not defined (Function Add_To_Payment_Details)";
                }
                if (ErrCode >= 4096)
                {
                    ErrCode -= 4096;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Plan_Amount Not defined (Function Add_To_Payment_Details)";
                }
                if (ErrCode >= 2048)
                {
                    ErrCode -= 2048;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Payment Plan_Id Not defined (Function Add_To_Payment_Details)";
                }
                if (ErrCode >= 1024)
                {
                    ErrCode -= 1024;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "New_Expiry Not defined YYYY-MM-DD (Function Add_To_Payment_Details)";
                }
                if (ErrCode >= 512)
                {
                    ErrCode -= 512;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Old_Expiry Not defined YYYY-MM-DD (Function Add_To_Payment_Details)";
                }
                if (ErrCode >= 256)
                {
                    ErrCode -= 256;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Minutes_In_Package Not defined (Function Add_To_User_Minute)";
                }
                if (ErrCode >= 128)
                {
                    ErrCode -= 128;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Passcode Not defined (Function Add_Complete_Paid_Account)";
                }
                if (ErrCode >= 64)
                {
                    ErrCode -= 64;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Account Type Not defined (Function Add_New_Account)";
                }
                if (ErrCode >= 32)
                {
                    ErrCode -= 32;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Plan Expires On Not defined (Function Add_New_Account)";
                }
                if (ErrCode >= 16)
                {
                    ErrCode -= 16;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Mail Box Not defined (Function Add_New_Account)";
                }
                if (ErrCode >= 8)
                {
                    ErrCode -= 8;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Passcode Not defined (Function Add_New_Account)";
                }
                if (ErrCode >= 4)
                {
                    ErrCode -= 4;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "MISSING: Account Number (Acc_Number)";
                }
                if (ErrCode >= 2)
                {
                    ErrCode -= 2;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "MISSING: Function to execute";
                }
                if (ErrCode >= 1)
                {
                    ErrCode -= 1;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "MISSING: Specify Authentication Key [AuthKey]";
                }
            }
            else if (ErrCode2 > 0)
            {
                if (ErrCode2 >= 536870912)
                {
                    ErrCode2 -= 536870912;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Incomplete Request (Function Admin_Web_Screening: App1Del2)";
                }
                if (ErrCode2 >= 268435456)
                {
                    ErrCode2 -= 268435456;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Unknown Group Prefix";
                }
                if (ErrCode2 >= 134217728)
                {
                    ErrCode2 -= 134217728;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Not a valid Charge Amount - not numeric (Function Process_Mobile_Charge)";
                }
                if (ErrCode2 >= 67108864)
                {
                    ErrCode2 -= 67108864;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Not a valid Carrier Id - not numeric (Function Process_Mobile_Charge)";
                }
                if (ErrCode2 >= 33554432)
                {
                    ErrCode2 -= 33554432;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Not a valid SMS Id - not numeric (Function Process_Mobile_Charge)";
                }
                if (ErrCode2 >= 16777216)
                {
                    ErrCode2 -= 16777216;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Not a valid Subscriber Account PassCode - not numeric (Function Process_Mobile_Charge)";
                }
                if (ErrCode2 >= 8388608)
                {
                    ErrCode2 -= 8388608;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Not a valid Subscriber Account Number - not numeric (Function Process_Mobile_Charge)";
                }
                if (ErrCode2 >= 4194304)
                {
                    ErrCode2 -= 4194304;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Charge Amount not specified (Function Process_Mobile_Charge)";
                }
                if (ErrCode2 >= 2097152)
                {
                    ErrCode2 -= 2097152;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Subscriber's Carrier Id not specified (Function Process_Mobile_Charge)";
                }
                if (ErrCode2 >= 1048576)
                {
                    ErrCode2 -= 1048576;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Mobile Billing Ticket Id not specified (Function Process_Mobile_Charge)";
                }
                if (ErrCode2 >= 524288)
                {
                    ErrCode2 -= 524288;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Mobile Billing SMS Id not specified (Function Process_Mobile_Charge)";
                }
                if (ErrCode2 >= 262144)
                {
                    ErrCode2 -= 262144;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Mobile Billing - Subscriber's Passcode not specified (Function Process_Mobile_Charge)";
                }
                if (ErrCode2 >= 131072)
                {
                    ErrCode2 -= 131072;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Subscriber's Account Number not specified (Function Process_Mobile_Charge)";
                }
                if (ErrCode2 >= 65536)
                {
                    ErrCode2 -= 65536;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Mobile Billing - Subscriber's Mobile Number not specified (Function Process_Mobile_Charge)";
                }
                if (ErrCode2 >= 32768)
                {
                    ErrCode2 -= 32768;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Modifiable parameter not defined (Function)";
                }
                if (ErrCode2 >= 16384)
                {
                    ErrCode2 -= 16384;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Account Number does not exist in account table (Function)";
                }
                if (ErrCode2 >= 8192)
                {
                    ErrCode2 -= 8192;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Search criteria not defined (Function Update_Member_Info)";
                }
                if (ErrCode2 >= 4096)
                {
                    ErrCode2 -= 4096;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Passcode Not defined (Function Validate)";
                }
                if (ErrCode2 >= 2048)
                {
                    ErrCode2 -= 2048;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Area_Code Not defined (Function Add_Complete_Paid_Account)";
                }
                if (ErrCode2 >= 1024)
                {
                    ErrCode2 -= 1024;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Service_Source Not defined (Function Add_Complete_Paid_Account)";
                }
                if (ErrCode2 >= 512)
                {
                    ErrCode2 -= 512;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Package_Description Not defined (Function Add_Complete_Paid_Account)";
                }
                if (ErrCode2 >= 256)
                {
                    ErrCode2 -= 256;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Plan_Validity Not defined (Function Add_Complete_Paid_Account)";
                }
                if (ErrCode2 >= 128)
                {
                    ErrCode2 -= 128;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Plan_Amount Not defined (Function Add_Complete_Paid_Account)";
                }
                if (ErrCode2 >= 64)
                {
                    ErrCode2 -= 64;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Payment Plan_Id Not defined (Function Add_Complete_Paid_Account)";
                }
                if (ErrCode2 >= 32)
                {
                    ErrCode2 -= 32;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "New_Expiry Not defined (Function Add_Complete_Paid_Account)";
                }
                if (ErrCode2 >= 16)
                {
                    ErrCode2 -= 16;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Old_Expiry Not defined (Function Add_Complete_Paid_Account)";
                }
                if (ErrCode2 >= 8)
                {
                    ErrCode2 -= 8;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Minutes_In_Package Not defined (Function Add_Complete_Paid_Account)";
                }
                if (ErrCode2 >= 4)
                {
                    ErrCode2 -= 4;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Account Type Not defined (Function Add_Complete_Paid_Account)";
                }
                if (ErrCode2 >= 2)
                {
                    ErrCode2 -= 2;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Plan Expires On Not defined (Function Add_Complete_Paid_Account)";
                }
                if (ErrCode2 >= 1)
                {
                    ErrCode2 -= 1;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Mail Box Not defined (Function Add_Complete_Paid_Account)";
                }
            }
            if (ErrCode3 > 0)
            {
                if (ErrCode3 >= 32)
                {
                    ErrCode3 -= 32;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Client_IP_Location Not Defined ( Function Check_Geo_Location )";
                }
                if (ErrCode3 >= 16)
                {
                    ErrCode3 -= 16;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "ActiveServerIP Not Defined ( Function Set_Primary_APIServer )";
                }
                if (ErrCode3 >= 8)
                {
                    ErrCode3 -= 8;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Setting Value Not Defined ( Function Set_MISC )";
                }
                if (ErrCode3 >= 4)
                {
                    ErrCode3 -= 4;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Setting Name Not Defined ( Function Set_MISC )";
                }
                if (ErrCode3 >= 2)
                {
                    ErrCode3 -= 2;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Plan Id Not Defined (Function GetChargeAmount)";
                }
                if (ErrCode3 >= 1)
                {
                    ErrCode3 -= 1;
                    if (strlen(ReturnString) > 0)
                    {
                        ReturnString += "<br>";
                    }
                    ReturnString += "Area Code Not Defined (Function GetChargeAmount)";
                }
            }
        }

        private string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return "127.0.0.1";
        }
    }
}