using Vezeeta.Core.Models;
using Vezeeta.Data.Repositories.Implementation;
using Vezeeta.Data.Repositories.Interfaces;
using Vezeeta.Data.Repositories.UnitOfWork;
using Vezeeta.Services.DomainServices.Interfaces;
using Vezeeta.Services.DomainServices.Services;

namespace Vezeeta.Api.Helpers;

public static class ServicesExtentions
{
    public static void AddLocalServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICouponService, CouponService>();

        services.AddScoped<IDoctorService, DoctorService>();

        services.AddScoped<IBookingService, BookingService>();

        services.AddScoped<IPatientService, PatientService>();


        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IAppointmentService, AppointmentService>();

        services.AddScoped<ISpecializationService, SpecializationService>();

        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped(typeof(IPaginationRepository<>), typeof(PaginationRepository<>));
    }
}
