using AutoMapper;
using BLL.Services.TimeService;
using Common.Helpers;
using DAL;
using DAL.Entities;
using DAL.Entities.Access;
using DAL.Entities.Access.AccessType;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class AuthService
{
    private readonly ApplicationDbContext _db;

    public AuthService(ApplicationDbContext db)
    {
        _db = db;
    }
}