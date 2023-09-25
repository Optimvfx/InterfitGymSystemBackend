namespace CLL.ControllersLogic.Interface;

public interface IHardwareLogic
{
    Task<bool> ExistsTrainingDevice(Guid gymId, Guid trainingDeviceId);
    Task<bool> ValidTrainingDeviceBreakdown(Guid requestBreakdownId);
    Task<bool> ExistsConsumable(Guid gym, Guid requestConsumableId);
    Task<bool> ExistsTechnicalHardware(Guid gym, Guid requestTechnicalHardwareId);
}