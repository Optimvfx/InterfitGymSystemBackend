using System.Net;
using Common.Exceptions.General;
using Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace CCL.Base;

public abstract class BaseController : Controller
{
    protected BadRequestObjectResult BadRequest(ModelStateErrorsCollection modelStateError)
    {
        if (modelStateError.HaveAnyError == false)
            throw new ArgumentException();
        
        var errors = modelStateError.Errors;
        
        string errorMessage = JsonConvert.SerializeObject(errors);


        return BadRequest(errorMessage);
    }
}