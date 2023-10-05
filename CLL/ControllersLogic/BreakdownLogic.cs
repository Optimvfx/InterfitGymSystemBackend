using BLL.Models.Hardware.Breakdowm;
using BLL.Models.Hardware.Repair;
using BLL.Services.PaginationViewFactory;
using CLL.ControllersLogic.Interface;
using Common.Exceptions.General.NotFoundException;
using Common.Models.PaginationView;
using DAL.Entities.Gym.Hardware;

namespace CLL.ControllersLogic;

public class BreakdownLogic : IBreakdownLogic
{
    private readonly BreakdownService _breakdownService;
    private readonly TrainingDeviceService _trainingDeviceService;
    private readonly ConsumableService _consumableService;
    private readonly TechnicalHardwareService _technicalHardwareService;

    private readonly IPaginationViewFactory _paginationViewFactory;

    public BreakdownLogic(BreakdownService breakdownService, TrainingDeviceService trainingDeviceService, ConsumableService consumableService, TechnicalHardwareService technicalHardwareService, IPaginationViewFactory paginationViewFactory)
    {
        _breakdownService = breakdownService;
        _trainingDeviceService = trainingDeviceService;
        _consumableService = consumableService;
        _technicalHardwareService = technicalHardwareService;
        _paginationViewFactory = paginationViewFactory;
    }

    public async Task<bool> RegisterTrainingDeviceBreakdown(TrainingDeviceBreakdownRegisterRequest request)
    {
        if (await _trainingDeviceService.Any(request.TrainingDeviceId) == false)
            throw new ValueNotFoundByIdException(typeof(TrainingDevice), request.TrainingDeviceId);

        Guid trainingDeviceTypeId = await _trainingDeviceService.GetTypeId(request.TrainingDeviceId);

        if (await _trainingDeviceService.ValidBreakdown(trainingDeviceTypeId, request.BreakdownTypeId) == false)
            throw new ArgumentException("Breakdown type is not valid for this training device.");

        await _breakdownService.AddTrainingDeviceBreakdown(request);

        return true;
    }

    public async Task<bool> RegisterConsumableBreakdown(ConsumableBreakdownRegisterRequest request)
    {
        if (await _consumableService.Any(request.ConsumableId) == false)
            throw new ValueNotFoundByIdException(typeof(Consumable), request.ConsumableId);

        await _breakdownService.AddConsumableBreakdown(request);
        
        return true;
    }

    public async Task<bool> RegisterTechnicalHardwareBreakdown(TechnicalHardwareBreakdownRegisterRequest request)
    {
        if (await _consumableService.Any(request.TechnicalHardwareId) == false)
            throw new ValueNotFoundByIdException(typeof(TechnicalHardware), request.TechnicalHardwareId);

        await _breakdownService.AddTechnicalHardwareBreakdown(request);

        return true;
    }

    public async Task<BasePaginationView<TrainingDeviceBreakdowmVM>> GetAllTrainingDevice()
    {
        IQueryable<TrainingDeviceBreakdown> all = _breakdownService.GetAllTrainingDevice();

        return _paginationViewFactory.CreatePaginationView<TrainingDeviceBreakdown, TrainingDeviceBreakdowmVM>(all);
    }

    public async Task<BasePaginationView<TechnicalHardwareBreakdowmVM>> GetAllTechnicalHardware()
    {
        IQueryable<TechnicalHardwareBreakdown> all = _breakdownService.GetAllTechnicalHardware();

        return _paginationViewFactory.CreatePaginationView<TechnicalHardwareBreakdown, TechnicalHardwareBreakdowmVM>(all);
    }

    public async Task<BasePaginationView<ConsumableBreakdowmVM>> GetAllConsumable()
    {
        IQueryable<ConsumableBreakdowm> all = _breakdownService.GetAllConsumable();

        return _paginationViewFactory.CreatePaginationView<ConsumableBreakdowm, ConsumableBreakdowmVM>(all);
    }

    public async Task<bool> RegisterTechnicalHardwareRepair(TechnicalHardwareRepairRegisterRequest request)
    {
        if (await _technicalHardwareService.Any(request.TechnicalHardwareId) == false)
            throw new ValueNotFoundByIdException(typeof(TechnicalHardware), request.TechnicalHardwareId);

        if (await _breakdownService.HasTechinalHardwereBreakdown(request.TechnicalHardwareId) == false)
            throw new ArgumentException("This techinal hardware dont have breakdown.");
        
        await _breakdownService.AddTechnicalHardwareRepair(request);

        return true;
    }

    public async Task<bool> RegisterTrainingDeviceRepair(TrainingDeviceRepairRegisterRequest request)
    {
        if (await _trainingDeviceService.Any(request.TrainingDeviceId) == false)
            throw new ValueNotFoundByIdException(typeof(TrainingDevice), request.TrainingDeviceId);

        if (await _breakdownService.HasTrainingDeviceBreakdown(request.TrainingDeviceId) == false)
            throw new ArgumentException("This training device dont have breakdown.");
        
        await _breakdownService.AddTrainingDeviceRepair(request);

        return true;
    }
}

public class ConsumableBreakdowm
{
}

public class TechnicalHardwareBreakdown
{
}

public class TrainingDeviceBreakdown
{
}

public class BreakdownService
{
    public IQueryable<TrainingDeviceBreakdown> GetAllTrainingDevice()
    {
        throw new NotImplementedException();
    }

    public IQueryable<ConsumableBreakdowm> GetAllConsumable()
    {
        throw new NotImplementedException();
    }

    public IQueryable<TechnicalHardwareBreakdown> GetAllTechnicalHardware()
    {
        throw new NotImplementedException();
    }

    public async Task AddTechnicalHardwareBreakdown(TechnicalHardwareBreakdownRegisterRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task AddTrainingDeviceBreakdown(TrainingDeviceBreakdownRegisterRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task AddConsumableBreakdown(ConsumableBreakdownRegisterRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> HasTechinalHardwereBreakdown(Guid requestTechnicalHardwareId)
    {
        throw new NotImplementedException();
    }

    public async Task AddTechnicalHardwareRepair(TechnicalHardwareRepairRegisterRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> HasTrainingDeviceBreakdown(Guid requestTrainingDeviceId)
    {
        throw new NotImplementedException();
    }

    public async Task AddTrainingDeviceRepair(TrainingDeviceRepairRegisterRequest request)
    {
        throw new NotImplementedException();
    }
}