using BusinessObject;
using Repositories;
using Repositories.MEMBER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MEMBER
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository iMemberRepository;
        public MemberService()
        {
            iMemberRepository = new MemberRepository();
        }
        public Member GetMemberByEmail(string email)
        {
            return iMemberRepository.GetMemberByEmail(email);
        }

        public Member GetMemberById(int id)
        {
            return iMemberRepository.GetMemberById(id);
        }

        public List<Member> GetMembers()
        {
            return iMemberRepository.GetMembers();
        }

        public void RemoveMember(int id)
        {
            iMemberRepository.RemoveMember(id);
        }

        public void SaveMember(Member member)
        {
            iMemberRepository.SaveMember(member);
        }

        public void UpdateMember(Member member)
        {
            iMemberRepository.UpdateMember(member);
        }

        public void AddMember(Member member)
        {
            try
            {
                iMemberRepository.SaveMember(member);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding member: {ex.Message}");
            }
        }

        public void RestoreMember(int id)
        {
            iMemberRepository.RestoreMember(id);
        }
    }
}
