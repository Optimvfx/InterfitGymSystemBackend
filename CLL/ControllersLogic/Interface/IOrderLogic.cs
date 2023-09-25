using BLL.Models.Order;

namespace CLL.ControllersLogic.Interface;

public interface IOrderLogic
{ 
    Task<bool>TryRegister(OrderRegistrationRequest request);
    Task<bool> TryRegisterHardware(TechnicalHardwareOrderRegistrationRequest request);
    Task<bool> TryRegisterConsumable(ConsumableOrderRegistrationRequest request);
    Task<bool> TryRegisterTrainingDevice(TrainingDeviceOrderRegistrationRequest request);
}