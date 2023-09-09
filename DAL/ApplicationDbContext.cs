using DAL.Entities.Access;
using DAL.Entities.Access.AccessType;
using DAL.Entities.Gym;
using DAL.Entities.Gym.Guarantee;
using DAL.Entities.Gym.Hardware;
using DAL.Entities.Gym.Hardware.Breakdown;
using DAL.Entities.Gym.Person;
using DAL.Entities.Gym.Person.Clients;
using DAL.Entities.Gym.Person.Employeers;
using DAL.Entities.Gym.SalesLogic;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ApplicationDbContext : DbContext
{
    #region Accesses

    public DbSet<Access> Accesses => Set<Access>();
    public DbSet<ApiAdministrator> ApiAdministrators => Set<ApiAdministrator>();
    public DbSet<Terminal> Terminals => Set<Terminal>();

    #endregion

    #region Gym

    public DbSet<Gym> Gyms => Set<Gym>();

    #region SalesLogic

    public DbSet<Training> Trainings => Set<Training>();
    public DbSet<TradeTransaction> TradeTransactions => Set<TradeTransaction>();
    public DbSet<OrderType> OrderTypes => Set<OrderType>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<AbbonitureProfile> AbbonitureProfiles => Set<AbbonitureProfile>();

    #endregion

    #region Persons

    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Coach> Coaches => Set<Coach>();
    public DbSet<Client> Clients => Set<Client>();

    #region Employees

    public DbSet<Position> Positions => Set<Position>();
    public DbSet<TerminalAdministrator> TerminalAdministrators => Set<TerminalAdministrator>();
    public DbSet<Timetable> Timetables => Set<Timetable>();
    public DbSet<Vacation> Vacations => Set<Vacation>();

    #endregion

    public DbSet<Abbonniture> Abbonnitures => Set<Abbonniture>();

    #endregion

    #region Hardware

    public DbSet<Consumable> Consumables => Set<Consumable>();
    public DbSet<ConsumableInformation> ConsumableInformations => Set<ConsumableInformation>();
    public DbSet<TechnicalHardware> TechnicalHardwares => Set<TechnicalHardware>();
    public DbSet<TechnicalHardwareInformation> TechnicalHardwareInformations => Set<TechnicalHardwareInformation>();
    public DbSet<TrainingDevice> TrainingDevices => Set<TrainingDevice>();
    public DbSet<TrainingDeviceInformation> TrainingDeviceInformations => Set<TrainingDeviceInformation>();

    #region Breakdowns

    public DbSet<Breakdown> Breakdowns => Set<Breakdown>();
    public DbSet<BreakdownType> BreakdownTypes => Set<BreakdownType>();

#endregion

    #endregion

    public DbSet<Guarantee> Guarantees => Set<Guarantee>();
    public DbSet<StoragePlace> StoragePlaces => Set<StoragePlace>();
    #endregion
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public bool IsInitialized()
    {
        return Terminals.Any();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyAllConfigurations();
        

        base.OnModelCreating(modelBuilder);
    }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.UseCustomValueConverterSelector();
    }
}