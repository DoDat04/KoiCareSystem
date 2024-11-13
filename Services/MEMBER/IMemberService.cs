using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MEMBER
{
    public interface IMemberService
    {
        List<Member> GetMembers();
        Member GetMemberByEmail(string email);
        Member GetMemberById(int id);
        void AddMember(Member member);
        void UpdateMember(Member member);
        void RemoveMember(int id);
        void RestoreMember(int id);
    }
}
