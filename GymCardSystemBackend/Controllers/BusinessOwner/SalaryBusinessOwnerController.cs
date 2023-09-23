using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Models;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using GymCardSystemBackend.ValidationAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCardSystemBackend.Controllers.BusinessOwner;

[Authorize(Policy = PolicyConsts.AdminPolicy)]
[ApiController]
[Route("api/salary")]
public class SalaryBusinessOwnerController : BaseAdminController
{
    private readonly ISalaryLogic _salaryLogic;

    public SalaryBusinessOwnerController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth, ISalaryLogic salaryLogic) : base(guidCryptor, auth)
    {
        _salaryLogic = salaryLogic;
    }

    #region Global

    [HttpGet("/all")]
    [ProducesResponseType(typeof(IEnumerable<SalaryVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetSalarys(DateOnly from, DateOnly to, bool includeRecycling = false, uint? page = null)
    {  
        if (to < from)
            return BadRequest("Data range is invalid.");

        var dataRange = new ValueRange<DateOnly>(from, to);

        BasePaginationView<SalaryVM> paginationView = await _salaryLogic.GetAll(includeRecycling, dataRange);

        return PaginationView(paginationView, page);
    }

    #endregion

    #region Gym

    [HttpGet("{gymId}/all")]
    [ProducesResponseType(typeof(SalaryVM), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetGymSalarys([GuidConvertible] string gymId,DateOnly from, DateOnly to, bool includeRecycling = false, uint? page = null)
    {
        var gymGuidId = DecryptGuid(gymId);
        
        if (to < from)
            return BadRequest("Data range is invalid.");

        var dataRange = new ValueRange<DateOnly>(from, to);

        BasePaginationView<SalaryVM> paginationView = await _salaryLogic.GetAllByGym(gymGuidId, includeRecycling, dataRange);

        return PaginationView(paginationView, page);
    }

    #endregion
}