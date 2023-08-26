using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using NSE.Core.DomainObjects;

namespace NSE.WebApp.MVC.Extensions;

public class CpfAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        return Cpf.IsValid(value.ToString()) ? ValidationResult.Success : new ValidationResult("Invalid CPF format");
    }
}

public class CpfAttributeAdapter : AttributeAdapterBase<CpfAttribute>
{
    public CpfAttributeAdapter(CpfAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute,
        stringLocalizer)
    {
        
    }

    public override void AddValidation(ClientModelValidationContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-cpf", GetErrorMessage(context));
    }

    public override string GetErrorMessage(ModelValidationContextBase validationContextBase)
    {
        return "Invalid CPF format";
    }
}

public class CpfValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
{
    private readonly IValidationAttributeAdapterProvider _attributeAdapterProvider =
        new ValidationAttributeAdapterProvider();

    public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
    {
        if (attribute is CpfAttribute cpfAttribute)
        {
            return new CpfAttributeAdapter(cpfAttribute, stringLocalizer);
        }

        return _attributeAdapterProvider.GetAttributeAdapter(attribute, stringLocalizer);
    }
}


