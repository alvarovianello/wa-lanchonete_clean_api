namespace WA.Api.Extensions
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddResolveAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(WA.Application.Mapper.AutoMapper).Assembly);
            return services;
        }
    }
}