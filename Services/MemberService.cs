using LibraryManagementEFCore.Interfaces;
using LibraryManagementEFCore.Models;

namespace LibraryManagementEFCore.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _repository;

    public MemberService(IMemberRepository repository)
    {
        _repository = repository;
    }

    public async Task AddMemberAsync(Member member)
    {
        if (string.IsNullOrWhiteSpace(member.FullName))
            throw new ArgumentException("Member name is required.");
        if (string.IsNullOrWhiteSpace(member.Email))
            throw new ArgumentException("Email is required.");
        if (!member.Email.Contains("@") || !member.Email.Contains("."))
            throw new ArgumentException("Enter a valid email address.");

        await _repository.AddMemberAsync(member);
    }

    public Task<List<Member>> GetAllMembersAsync()
    {
        return _repository.GetAllMembersAsync();
    }

    public Task<Member?> GetMemberByIdAsync(int id)
    {
        return _repository.GetMemberByIdAsync(id);
    }
}
