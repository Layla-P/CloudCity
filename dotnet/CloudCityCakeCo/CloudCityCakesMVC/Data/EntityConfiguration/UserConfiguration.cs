using System.ComponentModel.DataAnnotations.Schema;
using CloudCityCakesMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudCityCakesMVC.Data.EntityConfiguration
{
    public class UserConfiguration
    {
            internal static void Configure(ModelBuilder modelBuilder)
            {
                
                modelBuilder.Entity<User>(b =>
                {
                    b.HasKey(c => c.Id);
                    
                    b.HasIndex(i => i.PhoneNumber)
                        .IsUnique();

                    b.HasIndex(i => i.Email)
                        .IsUnique();
                    
                    #region ---- Entity Main Properties  ----   
                    b.Property(c => c.Name)
                        .IsRequired()
                        .HasMaxLength(128);
                    b.Property(c => c.PhoneNumber)
                        .IsRequired()
                        .HasMaxLength(64);
                    b.Property(c => c.Email)
                        .IsRequired()
                        .HasMaxLength(128);
                    
                    
                    #endregion
                });
            }
        }
    }
