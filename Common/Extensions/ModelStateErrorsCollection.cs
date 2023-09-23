using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Common.Extensions;

public class ModelStateErrorsCollection
{
    private readonly List<ModelStateError> _modelStateErrors = new();
    
    public IEnumerable<ModelStateError> Errors => _modelStateErrors;
    public bool HaveAnyError => _modelStateErrors.Any();
    
    public void Add(string key, string errorMessage)
    {
        Add(new ModelStateError(key, errorMessage));
    }
    
    public void Add(ModelStateError modelStateError)
    {
        _modelStateErrors.Add(modelStateError);
    }

    public void AddErrorsToModelState(ModelStateDictionary modelState)
    {
        foreach (var modelStateError in _modelStateErrors)
        {
            modelState.AddModelError(modelStateError.Key, modelStateError.ErrorMessage);
        }
    }
}