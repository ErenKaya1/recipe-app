namespace RecipeApp.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ConvertToSlugFormat(this string value) => value.ToLower().Replace("ü", "u").Replace("ı", "i").Replace("ğ", "g").Replace("ş", "s").Replace("ö", "o").Replace(" ", "-").Replace(".", "-");
    }
}