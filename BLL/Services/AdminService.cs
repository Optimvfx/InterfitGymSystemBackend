using DAL;

namespace BLL.Services;

public class AdminService
{
    private readonly ApplicationDbContext _db;

    public AdminService(ApplicationDbContext db)
    {
        _db = db;
    }
}