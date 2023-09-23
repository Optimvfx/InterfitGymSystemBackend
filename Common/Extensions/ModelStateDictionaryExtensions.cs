using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Common.Extensions;

public static class ModelStateDictionaryExtensions
{
    public static void AddModelErrors(this ModelStateDictionary modelState, ModelStateErrorsCollection errorsCollection)
    {
        errorsCollection.AddErrorsToModelState(modelState);
    }
}