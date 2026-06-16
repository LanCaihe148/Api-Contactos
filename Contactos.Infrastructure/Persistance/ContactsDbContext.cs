using Contactos.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.Intrinsics.X86;
using System.Threading.Channels;


namespace Contactos.Infrastructure.Persistance
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options)
        {
        }

        // Constructor para migraciones
        public ContactsDbContext() : base()
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseNpgsql("");
        //    }
        //    base.OnConfiguring(optionsBuilder);
        //}

        public DbSet<User>? Users { get; set; }
        public DbSet<Todo>? Todos { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<Photo>? Photos { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Album>? Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========== CONFIGURACIÓN DE USER ==========
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(200);
                entity.Property(e => e.UserName).HasMaxLength(100);
                entity.Property(e => e.WebSite).HasMaxLength(200);

                // Índices
                entity.HasIndex(e => e.UserName);
               

                // Address como ValueObject (Owned Type)
                entity.OwnsOne(u => u.Address, address =>
                {
                    address.Property(a => a.Street).HasColumnName("Address_Street").HasMaxLength(200);
                    address.Property(a => a.Suite).HasColumnName("Address_Suite").HasMaxLength(100);
                    address.Property(a => a.City).HasColumnName("Address_City").HasMaxLength(100);
                    address.Property(a => a.ZipCode).HasColumnName("Address_ZipCode").HasMaxLength(20);

                    // Geo dentro de Address
                    address.OwnsOne(a => a.Geo, geo =>
                    {
                        geo.Property(g => g.Lat).HasColumnName("Address_Geo_Lat").HasMaxLength(50);
                        geo.Property(g => g.Lng).HasColumnName("Address_Geo_Lng").HasMaxLength(50);
                    });
                });

                // Email como ValueObject
                entity.OwnsOne(u => u.Email, email =>
                {
                    email.Property(e => e.Direction)
                          .HasColumnName("Email")
                          .HasMaxLength(200)
                          .IsRequired(false);

                    email.HasIndex(e => e.Direction);
                });

                // Phone como ValueObject
                entity.OwnsOne(u => u.Phone, phone =>
                {
                    phone.Property(p => p.Number)
                          .HasColumnName("Phone")
                          .HasMaxLength(20)
                          .IsRequired(false);
                });

                // Company como ValueObject
                entity.OwnsOne(u => u.Company, company =>
                {
                    company.Property(c => c.Name).HasColumnName("Company_Name").HasMaxLength(200);
                    company.Property(c => c.CatchPhrase).HasColumnName("Company_CatchPhrase").HasMaxLength(500);
                    company.Property(c => c.Bs).HasColumnName("Company_Bs").HasMaxLength(200);
                });

                // Relaciones
                entity.HasMany(u => u.Posts)
                      .WithOne(p => p.User)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Albums)
                      .WithOne(a => a.User)
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Todos)
                      .WithOne(t => t.User)
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ========== CONFIGURACIÓN DE POST ==========
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasMaxLength(500);
                entity.Property(e => e.Body).HasColumnType("text");
                entity.HasIndex(e => e.UserId);

                // Relación con Comments
                entity.HasMany(p => p.Comments)
                      .WithOne(c => c.Post)
                      .HasForeignKey(c => c.PostId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ========== CONFIGURACIÓN DE COMMENT ==========
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(200);
                entity.Property(e => e.Body).HasColumnType("text");
                entity.HasIndex(e => e.PostId);

                // Email como ValueObject
                entity.OwnsOne(c => c.Email, email =>
                {
                    email.Property(e => e.Direction)
                          .HasColumnName("Email")
                          .HasMaxLength(200);
                });
            });

            // ========== CONFIGURACIÓN DE ALBUM ==========
            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasMaxLength(500);
                entity.Property(e => e.UserId);
                entity.HasIndex(e => e.UserId);

                // Relación con Photos
                entity.HasMany(a => a.Photos)
                      .WithOne(p => p.Album)
                      .HasForeignKey(p => p.AlbumId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ========== CONFIGURACIÓN DE PHOTO ==========
            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasMaxLength(500);
                entity.Property(e => e.Url).HasMaxLength(500);
                entity.Property(e => e.ThumbnailUrl).HasMaxLength(500);
                entity.HasIndex(e => e.AlbumId);
            });

            // ========== CONFIGURACIÓN DE TODO ==========
            modelBuilder.Entity<Todo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasMaxLength(500);
                entity.Property(e => e.Completed);
                entity.HasIndex(e => e.UserId);
            });
        }

    }
}
