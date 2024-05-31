using diary_back.DTO;
using diary_back.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserService
{
    Task<User> Authenticate(string email, string password);
    IEnumerable<User> GetAllUsers();
    Task<IEnumerable<Diaryentry>> GetDiaryEntriesByUserId(int userId);
    Task<Diaryentry> AddDiaryEntry(int userId, DiaryEntryDto diaryEntryDto);
    Task<User> GetUserWithRank(int userId);

}
