using FluentValidation;

namespace Core.Validation.FluentValidation
{
    public static class FluentValidationTool
    {
        public static void Validate<T>(IValidator<T> validator, T entity)
        {

            var result = validator.Validate(entity);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}