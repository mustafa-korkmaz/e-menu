using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Middlewares.Validations
{
    public class ObjectIdValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;

            return ObjectId.TryParse(value.ToString(), out _);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} field does not have an applicable ID";
        }
    }
}
