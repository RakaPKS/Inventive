using Inventive.AdminAPI.Controllers.V1;
using FluentValidation;
using FluentValidation.AspNetCore;
using Inventive.AdminAPI.Validators.Equipment;

namespace Inventive.AdminAPI.Util;

internal static class DependencyInjectionHelper
{
    public static void ConfigureServices(IServiceCollection services)
    {
        ConfigureInjection(services);
        ConfigureValidators(services);
    }

    private static void ConfigureInjection(IServiceCollection services) => services.AddTransient<EquipmentController>();

    private static void ConfigureValidators(IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluentValidationAutoValidation();
        serviceCollection.AddValidatorsFromAssemblyContaining<AddEquipmentRequestValidator>();
    }
}
