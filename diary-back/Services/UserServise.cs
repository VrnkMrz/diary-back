using diary_back.Models;
using diary_back.Context;
using Microsoft.EntityFrameworkCore;
using diary_back.DTO;

public class UserService : IUserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }

    public async Task<User> Authenticate(string email, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email && x.Password == password);

        if (user == null)
            return null;

        return user;
    }

    public async Task<User> GetUserWithRank(int userId)
    {
        return await _context.Users
            .Include(u => u.RankNavigation)
            .SingleOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<IEnumerable<Diaryentry>> GetDiaryEntriesByUserId(int userId)
    {
        return await _context.Diaryentries
            .Where(d => d.User == userId)
            .Include(d => d.AiEmotion)
            .Include(d => d.UserEmotion)
            .ToListAsync();
    }

    public async Task<Diaryentry> AddDiaryEntry(int userId, DiaryEntryDto diaryEntryDto)
    {
        var newEntry = new Diaryentry
        {
            Text = diaryEntryDto.Text,
            UserEmotionId = diaryEntryDto.UserEmotionId,
            AiEmotionId = diaryEntryDto.AiEmotionId,
            User = userId
        };

        _context.Diaryentries.Add(newEntry);
        await _context.SaveChangesAsync();

        return newEntry;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }
}
