using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using diary_back.DTO;
using diary_back.Models;

public interface IUserService
{
    Task<User> Authenticate(string email, string password);
    Task<User> GetUserWithRank(int userId);
    Task<IEnumerable<Diaryentry>> GetDiaryEntriesByUserId(int userId);
    Task<Diaryentry> AddDiaryEntry(int userId, DiaryEntryDto diaryEntryDto);
    IEnumerable<User> GetAllUsers();
}
