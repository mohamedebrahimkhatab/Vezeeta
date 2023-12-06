using Vezeeta.Core;
using Vezeeta.Data;
using Vezeeta.Core.Services;
using Vezeeta.Services.Local;

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
        services.AddTransient<IAppointmentService, AppointmentService>();
        services.AddTransient<ISpecializationService, SpecializationService>();
    }
}
