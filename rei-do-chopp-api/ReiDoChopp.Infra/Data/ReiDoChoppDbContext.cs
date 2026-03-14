using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReiDoChopp.Domain.OrderAdditionalFees.Entities;
using ReiDoChopp.Domain.Orders.Entities;
using ReiDoChopp.Domain.OrdersProducts.Entities;
using ReiDoChopp.Domain.PrintControls.Entities;
using ReiDoChopp.Domain.Products.Entities;
using ReiDoChopp.Domain.RestockingAdditionalFees.Entities;
using ReiDoChopp.Domain.RestockingProducts.Entities;
using ReiDoChopp.Domain.Restockings.Entities;
using ReiDoChopp.Domain.Roles.Entities;
using ReiDoChopp.Domain.Users.Entities;

namespace ReiDoChopp.Infra.Data
{
    public class ReiDoChoppDbContext : IdentityDbContext<
    User,
    Role,
    int,
    IdentityUserClaim<int>,
    UserRole,
    IdentityUserLogin<int>,
    IdentityRoleClaim<int>,
    IdentityUserToken<int>>
    {
        public ReiDoChoppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderAdditionalFee> OrderAdditionalFees { get; set; }
        public DbSet<Restocking> Restockings { get; set; }
        public DbSet<RestockingProduct> RestockingProducts { get; set; }
        public DbSet<RestockingAdditionalFee> RestockingAdditionalFees { get; set; }
        public DbSet<PrintControl> PrintControls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("RDC");

            base.OnModelCreating(modelBuilder);

            // Configurar todos os DateTime/DateTime? para serem tratados como UTC sem conversão
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(
                            new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>(
                                v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                            )
                        );
                    }
                    else if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(
                            new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime?, DateTime?>(
                                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v,
                                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v
                            )
                        );
                    }
                }
            }

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR",
                },
                new Role
                {
                    Id = 2,
                    Name = "Funcionario",
                    NormalizedName = "FUNCIONARIO",
                },
                new Role
                {
                    Id = 3,
                    Name = "Desenvolvedor",
                    NormalizedName = "DESENVOLVEDOR",
                }
            );


            modelBuilder.Entity<Order>(order =>
            {
                order.HasKey(x => x.Id);

                order.HasOne(x => x.Cashier)
                .WithMany()
                .HasForeignKey(x => x.CashierId)
                .OnDelete(DeleteBehavior.Restrict);

                order.HasOne(x => x.Attendant)
                .WithMany()
                .HasForeignKey(x => x.AttendantId)
                .OnDelete(DeleteBehavior.Restrict);

                order.HasMany(o => o.OrderProducts)
                .WithOne(o => o.Order)
                .HasForeignKey(op => op.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

                order.HasMany(o => o.OrderAdditionalFees)
                .WithOne(oaf => oaf.Order)
                .HasForeignKey(oaf => oaf.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<OrderProduct>(orderProduct =>
            {
                orderProduct.HasKey(op => op.Id);

                orderProduct.HasOne(op => op.Product)
                      .WithMany()
                      .HasForeignKey(op => op.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OrderAdditionalFee>(oaf =>
            {
                oaf.HasKey(x => x.Id);

                oaf.HasOne(x => x.Order)
                   .WithMany(o => o.OrderAdditionalFees)
                   .HasForeignKey(x => x.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Restocking>(restocking =>
            {
                restocking.HasKey(x => x.Id);

                restocking.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                restocking.HasMany(r => r.RestockingProducts)
                .WithOne(r => r.Restocking)
                .HasForeignKey(rp => rp.RestockingId)
                .OnDelete(DeleteBehavior.Cascade);

                restocking.HasMany(o => o.RestockingProducts)
                .WithOne(oaf => oaf.Restocking)
                .HasForeignKey(oaf => oaf.RestockingId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<RestockingProduct>(restockingProduct =>
            {
                restockingProduct.HasKey(rp => rp.Id);

                restockingProduct.HasOne(rp => rp.Product)
                      .WithMany()
                      .HasForeignKey(rp => rp.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RestockingAdditionalFee>(restockingAdditionalFee =>
            {
                restockingAdditionalFee.HasKey(raf => raf.Id);

                restockingAdditionalFee.HasOne(raf => raf.Restocking)
                   .WithMany(o => o.RestockingAdditionalFees)
                   .HasForeignKey(raf => raf.RestockingId)
                   .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<User>()
            .HasMany(e => e.Roles)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
            .HasOne(e => e.Role)
            .WithMany()
            .HasForeignKey(e => e.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); modelBuilder.Entity<User>();
        }
    }
}
