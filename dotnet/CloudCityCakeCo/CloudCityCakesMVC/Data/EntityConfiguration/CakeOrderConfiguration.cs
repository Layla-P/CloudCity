using CloudCityCakesMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudCityCakesMVC.Data.EntityConfiguration
{
    public class CakeOrderConfiguration
    {
        internal static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CakeOrder>(b =>
            {
                b.HasKey(c => c.Id);
                
                b.Property(c => c.UserId)
                    .IsRequired();
                    
                #region ---- Entity Main Properties  ----   
                b.Property(c => c.Frosting)
                    .IsRequired()
                    .HasMaxLength(64);
                b.Property(c => c.Topping)
                    .IsRequired()
                    .HasMaxLength(64);
                b.Property(c => c.Flavour)
                    .IsRequired()
                    .HasMaxLength(128);

                b.Property(c => c.Price)
                    .HasColumnType("decimal");
                
                b.HasOne(e => e.User)
                    .WithMany(c => c.CakeOrders)
                    .HasForeignKey(t => t.UserId);
                #endregion
             

            });
        }
    }
}