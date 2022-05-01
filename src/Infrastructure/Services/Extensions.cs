
namespace Infrastructure.Services
{
    public static class Extensions
    {
        public static string GetNormalized(this string value)
        {
            return value.ToLowerInvariant();
        }

        public static string ResolveEnum(this Enum c)
        {
            return c.ToString("G").ToLowerInvariant();
        }

        public static T ToEnum<T>(this string s)
        {
            return (T)Enum.Parse(typeof(T), s, true);
        }
    }
}
