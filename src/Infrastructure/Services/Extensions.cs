
namespace Infrastructure.Services
{
    public static class Extensions
    {
        public static string GetNormalized(this string value)
        {
            return value.ToLowerInvariant();
        }
    }
}
