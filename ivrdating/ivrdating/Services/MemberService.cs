using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ivrdating.ClassFile;
using ivrdating.Models;
using ivrdating.Repository;
using ivrdating.Helper;

namespace ivrdating.Services
{
    public class MemberService
    {
        MemberRepository _memberRepository;
        ValidationVar _validation = new ValidationVar();
        public MemberService()
        {
            _memberRepository = new MemberRepository();
        }


        internal ReturnData GetMemberData(GetMember _GetMember, string function)
        {

          
                return _memberRepository.GetMemberData(_GetMember, function);
        }

        

    }
}