using BLL.Services.Database;
using CLL.ControllersLogic.Interface;
using Common.Exceptions.General;
using Common.Exceptions.General.NotFoundException;
using DAL.Entities.Gym.Hardware;

namespace CLL.ControllersLogic;

public class HardwareLogic : IHardwareLogic
{
    private readonly ConsumableService _consumableService;
    private readonly TrainingDeviceService _trainingDeviceService;
    private readonly TechnicalHardwareService _technicalHardwareService;

    public HardwareLogic(ConsumableService consumableService, TrainingDeviceService trainingDeviceService, TechnicalHardwareService technicalHardwareService)
    {
        _consumableService = consumableService;
        _trainingDeviceService = trainingDeviceService;
        _technicalHardwareService = technicalHardwareService;
    }

    public async Task<bool> ExistsTrainingDevice(Guid gymId, Guid id)
    {
        if (await _trainingDeviceService.Any(id) == false)
            return false;

        var trainingDevice = await _trainingDeviceService.Get(id);

        return trainingDevice.GymId == gymId;
    }
    
    public async Task<bool> ExistsConsumable(Guid gymId, Guid id)
    {
        if (await _consumableService.Any(id) == false)
            return false;

        var consumable = await _consumableService.Get(id);

        return consumable.GymId == gymId;
    }

    public async Task<bool> ExistsTechnicalHardware(Guid gymId, Guid id)
    {
        if (await _technicalHardwareService.Any(id) == false)
            return false;

        TechnicalHardware technicalHardware = await _technicalHardwareService.Get(id);

        return technicalHardware.GymId == gymId;
    }
}

public class TechnicalHardwareService
{
    public async Task<TechnicalHardware> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Any(Guid id)
    {
        throw new NotImplementedException();
    }
}

public class TrainingDeviceService
{
    public async Task<bool> Any(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<TrainingDevice> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> GetTypeId(Guid requestTrainingDeviceId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ValidBreakdown(Guid trainingDeviceTypeId, Guid requestBreakdownTypeId)
    {
        throw new NotImplementedException();
    }
}

public class ConsumableService
{
    public async Task<bool> Any(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Consumable> Get(Guid id)
    {
        throw new NotImplementedException();
    }
}