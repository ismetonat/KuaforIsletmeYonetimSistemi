using KuaforIsletmeYonetimSistemi.Models;
using Microsoft.EntityFrameworkCore;

public class KuaforYonetimDbContext : DbContext
{
    public KuaforYonetimDbContext(DbContextOptions<KuaforYonetimDbContext> options)
        : base(options)
    {
    }

    // Modellerimizi burada tanımlıyoruz
    public DbSet<Kullanicilar> Kullanicilar { get; set; }
    public DbSet<Salonlar> Salonlar { get; set; }
    public DbSet<Calisanlar> Calisanlar { get; set; }
    public DbSet<Islemler> Islemler { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Hizmetler> Hizmetler { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Salon> Salons { get; set; }
    public DbSet<Randevular> Randevular { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Randevular - Kullanıcı ilişkisi
        modelBuilder.Entity<Randevular>()
            .HasOne(r => r.Kullanici)
            .WithMany()
            .HasForeignKey(r => r.KullaniciId)
            .OnDelete(DeleteBehavior.Restrict); // ON DELETE CASCADE yerine Restrict

        // Randevular - Çalışan ilişkisi
        modelBuilder.Entity<Randevular>()
            .HasOne(r => r.Calisan)
            .WithMany()
            .HasForeignKey(r => r.CalisanId)
            .OnDelete(DeleteBehavior.Restrict); // ON DELETE CASCADE yerine Restrict

        // Randevular - İşlem ilişkisi
        modelBuilder.Entity<Randevular>()
            .HasOne(r => r.Islem)
            .WithMany()
            .HasForeignKey(r => r.IslemId)
            .OnDelete(DeleteBehavior.Restrict); // ON DELETE CASCADE yerine Restrict
    }
}
