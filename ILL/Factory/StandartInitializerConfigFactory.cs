using BLL.Services.TimeService;
using Common.Extensions;
using Common.Models;
using DAL.Entities._Configuration;
using DAL.Entities.Access.AccessType;
using DAL.Entities.Gym;
using DAL.Entities.Gym.Hardware;
using DAL.Entities.Gym.Hardware.Breakdown;
using DAL.Entities.Gym.Person;
using DAL.Entities.Gym.SalesLogic;
using DAL.Entities.Primary;

namespace ILL.Factory;

public class StandartInitializerConfigFactory : IInitializerConfigFactory
{
    private readonly Config _config;

    private readonly ITimeService _timeService;

    private readonly Random _random;
    
    public StandartInitializerConfigFactory(Config config, ITimeService timeService)
    {
        _config = config;
        _timeService = timeService;

        _random = new Random();
    }

    public InitializerConfig Create()
    {
        var gyms = ListExtensions.Create(_config.GymConfigs.Count, i => CreateGymConfig(_config.GymConfigs[i]));
    }

    private Gym CreateGymConfig(GymConfig gymConfig)
    {
        var terminals = ListExtensions.Create(gymConfig.TerminalsCount, i => CreateTerminalConfig());
        var cards = ListExtensions.Create(gymConfig.CardsCount, i => CreateCardConfig());

        var trainingDevices = ListExtensions.Create(gymConfig.TrainingDevicesCount, i => CreateTrainingDeviceConfig())
        var consumable = new List<Consumable>();
        var techinicalHardware = new List<TechnicalHardware>();

        var clients = new List<Client>();
        var personal = new List<Employee>();
        var coaches = new List<Coach>();

        var orders = new List<Order>();
        var abbonnitureProfiles = new List<AbbonitureProfile>();
    }

    private Card CreateCardConfig()
    {
        DateTime exceptionDateOffset = _random.NextDate(_config.CardConfig.ExceptionOffsetRange);
        
        DateTime creationDate = _timeService.GetCurrentDateTime();
        DateTime exceptionDate = creationDate.AddTicks(exceptionDateOffset.Ticks);

        return new Card()
        {
            CreationDate = creationDate,
            ExpirationDate = exceptionDate
        };
    }

    private Terminal CreateTerminalConfig()
    {
        var title = _config.TerminalConfig.Titles.Random(_random);
        var description = _config.TerminalConfig.Descripitons.Random(_random);

        return new Terminal()
        {
            Title = title,
            Description = description
        };
    }
    
    private TrainingDevice CreateTrainingDeviceConfig()
    {
        var deviceConfig = _config.TrainingDeviceConfigs.Random();
        var price = deviceConfig.MinPrice + _random.NextUint(deviceConfig.MaxExtraPrice);
        
        return new TrainingDevice()
        {
            BuyPrice = price,
        };
    }
    
    public class Config
    {
        public readonly ApiKeyConfig ApiKeyConfig;
        public readonly IReadOnlyList<GymConfig> GymConfigs;

        public readonly TerminalConfig TerminalConfig;
        public readonly CardConfig CardConfig;
        public readonly List<TrainingDeviceConfig> TrainingDeviceConfigs;

        public IEnumerable<BreakdownTypeConfig> GetAllBreakdownTypes =>
            TrainingDeviceConfigs.SelectMany(t => t.BreakdownTypeConfigs);
    }

    public class ApiKeyConfig
    {
        public readonly uint Lenght;

        public ApiKeyConfig(uint lenght)
        {
            Lenght = lenght;
        }
    }
    
    public struct GymConfig
    {
        public uint TerminalsCount { get; set; }
        public uint CardsCount { get; set; }
        
        public uint TrainingDevicesCount { get; set; }
        public uint ConsumableCount { get; set; }
        public uint TechnicalHardwareCount { get; set; }
        
        public uint ClientsCount { get; set; }
        public uint PersonalTotalCount { get; set; }
        public uint CoachesTotalCount { get; set; }

        public uint OrdersCount { get; set; }
        public uint AbbonnitureProfilesCount { get; set; }
        public uint TradeTransactions { get; set; }
        public uint HistoryOfTrainingsCount { get; set; }
    }
    
    public class TerminalConfig
    {
        public readonly IReadOnlyList<string> Titles;
        public readonly IReadOnlyList<string> Descripitons;

        public TerminalConfig(IEnumerable<string> titles, IEnumerable<string> descripitons)
        {
            Titles = titles.ToList();
            Descripitons = descripitons.ToList();
        }
    }
    
    public class CardConfig
    {
        public readonly ValueRange<DateTime> ExceptionOffsetRange;

        public CardConfig(ValueRange<DateTime> exceptionOffsetRange)
        {
            if (exceptionOffsetRange.Min.Ticks < 0)
                throw new AggregateException();
            
            ExceptionOffsetRange = exceptionOffsetRange;
        }
    }
    
    public class TrainingDeviceConfig
    {
        public readonly uint MinPrice;
        public readonly uint MaxExtraPrice;

        public readonly string Title;
        public readonly string? Description;

        public readonly IReadOnlyList<BreakdownTypeConfig> BreakdownTypeConfigs;
    }
    
    public class BreakdownTypeConfig
    {
        public readonly string Title;
        public readonly string? Description;
    }
}

