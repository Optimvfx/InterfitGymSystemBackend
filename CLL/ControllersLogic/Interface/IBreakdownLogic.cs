using BLL.Models.Hardware.Breakdowm;
using BLL.Models.Hardware.Repair;
using Common.Models;
using Common.Models.PaginationView;

namespace CLL.ControllersLogic.Interface;

public interface IBreakdownLogic
{
    Task<bool> RegisterTrainingDeviceBreakdown(TrainingDeviceBreakdownRegisterRequest request);
    Task<bool> RegisterConsumableBreakdowns(ConsumableBreakdownRegisterRequest request);
    Task<bool> RegisterTechnicalHardwareBreakdown(TechnicalHardwareBreakdownRegisterRequest request);
    Task<BasePaginationView<TrainingDeviceBreakdowmVM>> GetAllTrainingDevice();
    Task<BasePaginationView<TechnicalHardwareBreakdowmVM>> GetAllTechnicalHardware();
    Task<BasePaginationView<ConsumableBreakdowmVM>> GetAllConsumable();
    Task<bool> RegisterTechnicalHardwareRepair(TechnicalHardwareRepairRegisterRequest request);
    Task<bool> AnyTechnicalHardware(Guid requestHardwareId);
    Task<bool> RegisterTrainingDeviceRepair(TrainingDeviceRepairRegisterRequest request);
    Task<bool> AnyTrainingDevice(Guid requestHardwareId);
}