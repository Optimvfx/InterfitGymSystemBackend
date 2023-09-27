using BLL.Models.Employee;
using BLL.Models.Fininaces;
using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Models;
using Common.Models.PaginationView;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using GymCardSystemBackend.ValidationAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCardSystemBackend.Controllers.PersonalManager;

[Authorize(Policy = PolicyConsts.AdminPolicy)]
[ApiController]
[Route("api/employee")]
public class EmployeePersonalManagerController : BaseAdminController
{
    private readonly IEmployeeLogic _employeeLogic;

    public EmployeePersonalManagerController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth, IEmployeeLogic employeeLogic) : base(guidCryptor, auth)
    {
        _employeeLogic = employeeLogic;   
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create(EmployeeCreationRequest employee)
    {
        var result = await _employeeLogic.Create(employee);

        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmployeeVM), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get([GuidConvertible] string id)
    {
        var guidId = DecryptGuid(id);

        var result = await _employeeLogic.Get(guidId);

        return Ok(result);
    }

    [HttpGet("all/{page}")]
    [ProducesResponseType(typeof(IEnumerable<EmployeeVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    public async Task<IActionResult> GetAll(uint? page = null)
    {
        BasePaginationView<EmployeeVM> paginationView = await _employeeLogic.GetAll();

        return PaginationView(paginationView, page);
    }
    
    [HttpGet("all/{gymId}/{page}")]
    [ProducesResponseType(typeof(IEnumerable<EmployeeVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAllByGym([GuidConvertible] string gymId, uint? page = null)
    {
        var gymGuidId = DecryptGuid(gymId);
        BasePaginationView<EmployeeVM> paginationView = await _employeeLogic.GetAllInGym(gymGuidId);

        return PaginationView(paginationView, page);
    }
    
    [HttpGet("top/salary/{page}")]
    [ProducesResponseType(typeof(IEnumerable<EmployeeVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetTopBySalary(uint? page = null, bool reversed = false, uint? max = null, uint? min = null)
    {
        max = max ?? uint.MaxValue;
        min = min ?? uint.MinValue;

        if (max < min)
            return BadRequest("Maximal is smaller thin minimal");

        var range = new ValueRange<uint>(min.Value, max.Value);
        
        BasePaginationView<EmployeeVM> paginationView = await _employeeLogic.GetTopBySalary(reversed, range);

        return PaginationView(paginationView, page);
    } 

    [HttpGet("top/work_time/{page}")]
    [ProducesResponseType(typeof(IEnumerable<WorkTimeInfoVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetTopByWorkTime(DateOnly max, DateOnly min, uint? page = null, bool reversed = false, bool includeRecyclingWorks = false)
    {
        if (min > max)
            return BadRequest("Negative date is not possible.");
        
        var paginationView = await _employeeLogic.GetTopByWorkHours(reversed, 
            includeRecyclingWorks, new ValueRange<DateOnly>(min, max));

        if (page == null)
            return Ok(paginationView.GetPagesRange());

        if (paginationView.PageOutOfRange(page.Value))
            return NotFound("Page out of range.");

        return Ok(paginationView.Get(page.Value));
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Edit([GuidConvertible] string id, EmployeeEditRequest reqest)
    {
        var guidId = DecryptGuid(id);

        if (await _employeeLogic.Exists(guidId) == false)
            return NotFound("No employee by id");

        await _employeeLogic.Edit(guidId, reqest);

        return Ok();
    }
    
    [HttpPut("/{id}/dismiss")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Dismiss([GuidConvertible] string id)
    {
        var guidId = DecryptGuid(id);

        if (await _employeeLogic.Exists(guidId) == false)
            return NotFound("No employee by id");

        await _employeeLogic.Dismiss(guidId);

        return Ok();
    }
    
    [HttpPut("/{id}/reinstatement")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Reinstatement([GuidConvertible] string id)
    {
        var guidId = DecryptGuid(id);

        if (await _employeeLogic.Exists(guidId) == false)
            return NotFound("No employee by id");

        await _employeeLogic.Reinstatement(guidId);

        return Ok();
    }
}