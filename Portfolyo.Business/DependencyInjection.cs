using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Portfolyo.Business.Behaviors;

namespace Portfolyo.Business
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddBusiness(
            this IServiceCollection services)
        {

            services.AddMediatR(x =>
            {
                x.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                x.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
