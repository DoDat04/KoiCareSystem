using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public Member GetMemberByEmail(string email)
        {
            var _dbContext = new KoiCareContext();
            var member = _dbContext.Members.FirstOrDefault(m => m.Email == email);
            return member;
        }

        public Member GetMemberById(int id)
        {
            var _dbContext = new KoiCareContext();
            var member = _dbContext.Members.FirstOrDefault(m => m.MemberId == id);
            return member;
        }

        public List<Member> GetMembers()
        {
            var _dbContext = new KoiCareContext();
            return _dbContext.Members
                .Where(x => x.RoleId == 2)
                .ToList();
        }

        public void RemoveMember(int id)
        {
            try
            {
                var _dbContext = new KoiCareContext();
                var member = _dbContext.Members.Find(id);
                if (member != null)
                {
                    member.IsActive = false;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing member: {ex.Message}");
            }
        }

        public void RestoreMember(int id)
        {
            using var _dbContext = new KoiCareContext();
            var member = _dbContext.Members.Find(id);
            if (member != null)
            {
                member.IsActive = true;
                _dbContext.SaveChanges();
            }
        }

        public void SaveMember(Member member)
        {
            try
            {
                var _dbContext = new KoiCareContext();
                _dbContext.Members.Add(member);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving member: {ex.Message}");
            }
        }

        public void UpdateMember(Member member)
        {
            try
            {
                var _dbContext = new KoiCareContext();
                var existingMember = _dbContext.Members.FirstOrDefault(m => m.MemberId == member.MemberId);
                if (existingMember != null)
                {
                    existingMember.FirstName = member.FirstName;
                    existingMember.LastName = member.LastName;
                    existingMember.PhoneNumber = member.PhoneNumber;
                    existingMember.Email = member.Email;
                    existingMember.Password = member.Password;
                    existingMember.UpdateDate = DateTime.Now;
                    existingMember.UpdateBy = "Member_" + member.MemberId;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating member: {ex.Message}");
            }
        }
    }
}
