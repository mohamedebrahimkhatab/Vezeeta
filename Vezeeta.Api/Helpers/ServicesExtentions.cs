using System.Runtime.CompilerServices;
using Vezeeta.Core.Services;
using Vezeeta.Core;
using Vezeeta.Data;
using Vezeeta.Services.Local;

namespace Vezeeta.Api.Helpers;

public static class ServicesExtentions
{
    public static void AddLocalServices(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IDoctorService, DoctorService>();
        services.AddTransient<ISpecializationService, SpecializationService>();
    }
}
