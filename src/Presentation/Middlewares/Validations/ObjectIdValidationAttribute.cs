using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Middlewares.Validations
{
    public class ObjectIdValidationAttribute : ValidationAttribute
    {
        public bool AllowNull { get; set; }

        public override bool IsValid(object? value)
        {
            if (AllowNull && value == null)
            {
                return true;
            }

            return ObjectId.TryParse(value?.ToString(), out _);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name}_FIELD_VALUE_IS_INCORRECT";
        }
    }
}
