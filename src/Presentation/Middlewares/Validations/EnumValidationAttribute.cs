using System.ComponentModel.DataAnnotations;
using Infrastructure.Services;

namespace Presentation.Middlewares.Validations
{
    public class EnumValidationAttribute : ValidationAttribute
    {
        public Type Type { get; set; } = default!;
        public bool AllowNull { get; set; }

        public override bool IsValid(object? value)
        {
            if (AllowNull && value == null)
            {
                return true;
            }

            var str = value as string;

            return GetApplicableValues().Contains(str ?? "");
        }

        public override string FormatErrorMessage(string name)
        {
            var message = string.Join(", ", GetApplicableValues());
            return $"Applicable {name.ToLowerInvariant()} field values: {message}";
        }

        private IEnumerable<string> GetApplicableValues()
        {
            var values = Enum.GetValues(Type).Cast<Enum>();

            return values.Select(enumVal => enumVal.ResolveEnum());
        }
    }
}