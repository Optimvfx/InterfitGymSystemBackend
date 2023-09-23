using System.ComponentModel.DataAnnotations;
using GymCardSystemBackend.Singleton;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GymCardSystemBackend.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
public class GuidNullableConvertible : ValidationAttribute, IModelBinder
{
    public override bool IsValid(object? value)
    {
        var strValue = value as string;

        if (strValue == null)
            return true;

        return GuidCoderSingleton.GetGuidCoder()
            .TryDecrypt(strValue)
            .IsSuccess();
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        var value = valueProviderResult.FirstValue;
        
        if(value == null)
            return Task.CompletedTask;
        
        var result = GuidCoderSingleton.GetGuidCoder()
            .TryDecrypt(value);
            
        if(result.IsSuccess())
        {
            bindingContext.Result = ModelBindingResult.Success(result.Value);
        }
        else
        {
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid GUID format.");
        }
        
        return Task.CompletedTask;
    }
}