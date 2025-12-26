namespace MDA.Web.Application.Shared
{
    public static class SharedLogic
    {
        public static decimal? R2(decimal? value)
        {
            return value.HasValue ? Math.Round(value.Value, 2, MidpointRounding.AwayFromZero) : null;
        }

        public static decimal R(decimal value)
        {
            return Math.Round(value, 0, MidpointRounding.AwayFromZero);
        }
    }
}
