using BLL.Models.Order;
using CLL.ControllersLogic.Interface;
using DAL.Entities.Gym.SalesLogic;

namespace CLL.ControllersLogic;

public class OrderLogic : IOrderLogic
{
    private readonly OrderService _orderService;

    public OrderLogic(OrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<bool> TryRegister(OrderRegistrationRequest request)
    {
        await _orderService.Register(request);

        return true;
    }

    public async Task<bool> TryRegisterHardware(TechnicalHardwareOrderRegistrationRequest request)
    {
        await _orderService.RegisterHardware(request);

        return true;
    }

    public async Task<bool> TryRegisterConsumable(ConsumableOrderRegistrationRequest request)
    {
        await _orderService.RegisterConsumable(request);

        return true;
    }

    public async Task<bool> TryRegisterTrainingDevice(TrainingDeviceOrderRegistrationRequest request)
    {
        await _orderService.RegisterTrainingDevice(request);

        return true;
    }
}

public class OrderService
{
    public async Task Register(OrderRegistrationRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task RegisterHardware(TechnicalHardwareOrderRegistrationRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task RegisterConsumable(ConsumableOrderRegistrationRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task RegisterTrainingDevice(TrainingDeviceOrderRegistrationRequest request)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Order> GetAll()
    {
        throw new NotImplementedException();
    }
}