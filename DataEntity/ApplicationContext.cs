using Microsoft.EntityFrameworkCore;
using Data.Entities;
using DataEntity.Mapping;

namespace DataEntity
{
    public class ApplicationContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<TutorSpecialization> TutorSpecializations { get; set; }
        public DbSet<TutorSubject> TutorSubjects { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CityConfig());
            modelBuilder.ApplyConfiguration(new ContactConfig());
            modelBuilder.ApplyConfiguration(new ContactTypeConfig());
            modelBuilder.ApplyConfiguration(new PhoneConfig());
            modelBuilder.ApplyConfiguration(new SpecializationConfig());
            modelBuilder.ApplyConfiguration(new SubjectConfig());
            modelBuilder.ApplyConfiguration(new TutorConfig());
            modelBuilder.ApplyConfiguration(new TutorSpecializationConfig());
            modelBuilder.ApplyConfiguration(new TutorSubjectConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
