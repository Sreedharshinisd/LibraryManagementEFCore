using LibraryManagementEFCore.Models;

namespace LibraryManagementEFCore.Interfaces;

public interface IMemberService
{
    Task AddMemberAsync(Member member);
    Task<List<Member>> GetAllMembersAsync();
    Task<Member?> GetMemberByIdAsync(int id);
}
