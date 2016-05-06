using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ivrdating.ClassFile;
using ivrdating.Models;
using ivrdating.Helper;
using System.Data;
using MySql.Data.MySqlClient;

namespace ivrdating.Repository
{
    public class MemberRepository
    {
        DbFuction _DbFuction = new DbFuction();

        DataTable dt = new DataTable();
        internal ReturnData GetMemberData(GetMember _GetMember, string Function)
        {
            string WhereClause = "";
            string str = _DbFuction.IsValidRequest(_GetMember.AuthKey, _GetMember.WS_UserName, _GetMember.WS_Password);
            if (str.Equals("OK"))
            {
                string Group_Id = _DbFuction.Validate_Incoming_Group_Prefix(Function, _GetMember.Group_Prefix);
                if (Group_Id == Errors.Unknown_Group_Prefix.ToString())
                {
                    return new ReturnData() { ErrorMessage = Group_Id, Count = 0 };
                }
                else
                {
                    string SQLQry_F10_1 = "SELECT a.Acc_Number, a.PassCode, a.CallerId, IFNULL(c.Email_Address, '') as Email_Address ";
                    if (Function == "get_member_details")
                    {
                        SQLQry_F10_1 += ", IFNULL( c.First_Name, '' ) as First_Name, IFNULL( c.Last_Name, '' ) as Last_Name, ";
                        SQLQry_F10_1 += "IFNULL( c.Address, '' ) as address, IFNULL( c.City, '' ) as City, IFNULL( c.State_Name, '' ) as State_Name, ";
                        SQLQry_F10_1 += "IFNULL( c.Zip_Code, '' ) as zip_code, IFNULL( c.Country, '' ) as Country, ";
                        SQLQry_F10_1 += "a.AccountType, a.ExpiryDate ";
                    }
                    SQLQry_F10_1 += "FROM (";

                    if (_GetMember.CustomerEmail_Address != null)
                    {
                        SQLQry_F10_1 += "SELECT AId, Email_Address, First_Name, Last_Name, Address, City, ";
                        SQLQry_F10_1 += "State_Name, Zip_Code, Country FROM customer_master ";
                        SQLQry_F10_1 += "WHERE Email_Address = @CustomerEmail_Address ";
                        SQLQry_F10_1 += ") AS c INNER JOIN account as a ON a.ID = c.Aid ";
                        SQLQry_F10_1 += "WHERE a.Grp_Id = @Group_Id LIMIT 1";
                    }
                    else
                    {
                        if (_GetMember.Acc_Number != null)
                        {
                            WhereClause = " Acc_Number = @Acc_Number";
                        }
                        else
                        {
                            WhereClause = " PassCode = @PassCode AND CallerId = @CallerId";
                        }

                        SQLQry_F10_1 += "SELECT ID, Acc_Number, PassCode, CallerId, AccountType, ";
                        SQLQry_F10_1 += "ExpiryDate FROM account ";
                        SQLQry_F10_1 += "WHERE " + WhereClause + " AND Grp_Id = @Group_Id ";
                        SQLQry_F10_1 += ") AS a LEFT JOIN customer_master as c ON a.ID = c.Aid ";
                        SQLQry_F10_1 += "LIMIT 1";
                    }
                    dt = _DbFuction.RetrieveDataFromDB(SQLQry_F10_1, new MySqlParameter[] {
                        new MySqlParameter("@CustomerEmail_Address",_GetMember.CustomerEmail_Address),
                        new MySqlParameter("@Group_Id",Group_Id),
                        new MySqlParameter("@Acc_Number",_GetMember.Acc_Number),
                        new MySqlParameter("@PassCode",_GetMember.PassCode),
                        new MySqlParameter("@CallerId",_GetMember.CallerId)

                    });
                    if (Function == "get_member_details")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            ReturnData rt = new ReturnData();
                            rt.WsResult = new MemberDetailVM();
                            var rs = ExtensionMethods.CreateItemFromRow(dt.Rows[0], typeof(MemberDetailVM).GetProperties().ToList(), rt.WsResult);
                            rt.Count = dt.Rows.Count;
                            return rt;
                        }
                    }
                }


            }
            else
            {
                return new ReturnData() { ErrorMessage = str, Count = 0 };
            }
            return new ReturnData() { ErrorMessage = "Success", Count = 0 };
        }
    }
}