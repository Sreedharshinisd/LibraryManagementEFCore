using LibraryManagementEFCore.Models;

namespace LibraryManagementEFCore.Interfaces;

public interface IMemberRepository
{
    Task AddMemberAsync(Member member);
    Task<List<Member>> GetAllMembersAsync();
    Task<Member?> GetMemberByIdAsync(int id);
}
