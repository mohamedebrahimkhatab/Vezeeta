using Vezeeta.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vezeeta.Data.Configurations;

internal class SpecializationConfiguration : BaseEntityConfiguration<Specialization>
{
    public override void Configure(EntityTypeBuilder<Specialization> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.Name).IsRequired();
        builder.HasData(
            new Specialization { Id = 1, Name = "Allergy and Immunology" },
            new Specialization { Id = 2, Name = "Anesthesiology" },
            new Specialization { Id = 3, Name = "Audiology" },
            new Specialization { Id = 4, Name = "Cardiology" },
            new Specialization { Id = 5, Name = "Cardiothoracic Surgery" },
            new Specialization { Id = 6, Name = "Cardiology and Vascular Disease" },
            new Specialization { Id = 7, Name = "Chest and Respiratory" },
            new Specialization { Id = 8, Name = "Colon and Rectal Surgery" },
            new Specialization { Id = 9, Name = "Dentistry" },
            new Specialization { Id = 10, Name = "Dermatology" },
            new Specialization { Id = 11, Name = "Diabetes and Endocrinology" },
            new Specialization { Id = 12, Name = "Dietitian and Nutrition" },
            new Specialization { Id = 13, Name = "Emergency Medicine" },
            new Specialization { Id = 14, Name = "Ear, Nose and Throat" },
            new Specialization { Id = 15, Name = "Family Medicine" },
            new Specialization { Id = 16, Name = "Forensic Pathology" },
            new Specialization { Id = 17, Name = "Gastroenterology and Endoscopy" },
            new Specialization { Id = 18, Name = "Genetics and Genomics" },
            new Specialization { Id = 19, Name = "General Practice" },
            new Specialization { Id = 20, Name = "General Surgery" },
            new Specialization { Id = 21, Name = "Geriatrics" },
            new Specialization { Id = 22, Name = "Hematology" },
            new Specialization { Id = 23, Name = "Hepatology" },
            new Specialization { Id = 24, Name = "Hospital Medicine" },
            new Specialization { Id = 25, Name = "Hospice and Palliative Medicine" },
            new Specialization { Id = 26, Name = "IVF and Infertility" },
            new Specialization { Id = 27, Name = "Internal Medicine" },
            new Specialization { Id = 28, Name = "Interventional Radiology" },
            new Specialization { Id = 29, Name = "Laboratories" },
            new Specialization { Id = 30, Name = "Neurology" },
            new Specialization { Id = 31, Name = "Neurosurgery" },
            new Specialization { Id = 32, Name = "Obesity and Laparoscopic Surgery" },
            new Specialization { Id = 33, Name = "Oncologic Surgery" },
            new Specialization { Id = 34, Name = "Oncology" },
            new Specialization { Id = 35, Name = "Ophthalmic Surgery" },
            new Specialization { Id = 36, Name = "Ophthalmology" },
            new Specialization { Id = 37, Name = "Orthopedic Surgery" },
            new Specialization { Id = 38, Name = "Osteopathy" },
            new Specialization { Id = 39, Name = "Otolaryngology" },
            new Specialization { Id = 40, Name = "Pain Management" },
            new Specialization { Id = 41, Name = "Pathology" },
            new Specialization { Id = 42, Name = "Pediatric Surgery" },
            new Specialization { Id = 43, Name = "Pediatrics" },
            new Specialization { Id = 44, Name = "Phoniatrics" },
            new Specialization { Id = 45, Name = "Physical Medicine and Rehabilitation" },
            new Specialization { Id = 46, Name = "Physiotherapy and Sports Injuries" },
            new Specialization { Id = 47, Name = "Plastic Surgery" },
            new Specialization { Id = 48, Name = "Preventive Medicine" },
            new Specialization { Id = 49, Name = "Psychiatry" },
            new Specialization { Id = 50, Name = "Radiology" },
            new Specialization { Id = 51, Name = "Rheumatology" },
            new Specialization { Id = 52, Name = "Sleep Medicine" },
            new Specialization { Id = 53, Name = "Spinal Surgery" },
            new Specialization { Id = 54, Name = "Thoracic Surgery" },
            new Specialization { Id = 55, Name = "Urology" },
            new Specialization { Id = 56, Name = "Vascular Surgery" }
        );
    }
}
