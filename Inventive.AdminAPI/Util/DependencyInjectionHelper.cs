using Inventive.AdminAPI.Controllers.V1;
using FluentValidation;
using FluentValidation.AspNetCore;
using Inventive.AdminAPI.Validators.Equipment;
using Inventive.Core.Interfaces;
using Inventive.Core.Interfaces.Services.Equipment;
using Inventive.Data;
using Inventive.Services.Equipment;

namespace Inventive.AdminAPI.Util;

internal static class DependencyInjectionHelper
{
    public static void ConfigureServices(IServiceCollection services)
    {
        ConfigureInjection(services);
        ConfigureValidators(services);
    }

    private static void ConfigureInjection(IServiceCollection services)
    {
        // Controllers
        services.AddTransient<EquipmentController>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();


        services.AddScoped<IAdminEquipmentService, AdminEquipmentService>();
    }

    private static void ConfigureValidators(IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluentValidationAutoValidation();
        serviceCollection.AddValidatorsFromAssemblyContaining<AddEquipmentRequestValidator>();
    }
}
