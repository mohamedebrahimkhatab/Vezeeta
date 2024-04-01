using Vezeeta.Data.Repositories.UnitOfWork;
using Vezeeta.Services.DomainServices.Interfaces;
using Vezeeta.Services.DomainServices.Services;

namespace Vezeeta.Api.Helpers;

public static class ServicesExtentions
{
    public static void AddLocalServices(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ICouponService, CouponService>();
        services.AddTransient<IDoctorService, DoctorService>();
        services.AddTransient<IBookingService, BookingService>();
        services.AddTransient<IPatientService, PatientService>();
        services.AddTransient<IDashboardService, DashboardService>();
        services.AddTransient<IAppointmentService, AppointmentService>();
        services.AddTransient<ISpecializationService, SpecializationService>();
    }
}
