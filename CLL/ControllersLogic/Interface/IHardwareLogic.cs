namespace CLL.ControllersLogic.Interface;

public interface IHardwareLogic
{
    Task<bool> ExistsTrainingDevice(Guid gymId, Guid id);
    Task<bool> ValidTrainingDeviceBreakdown(Guid trainingDeviceId, Guid breakdownId);
    Task<bool> ExistsConsumable(Guid gymId, Guid id);
    Task<bool> ExistsTechnicalHardware(Guid gymId, Guid id);
}