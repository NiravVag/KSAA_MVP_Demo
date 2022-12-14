using KSAA.Domain.Entities;
using KSAA.Domain.Entities.Master;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.DAL
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, long, IdentityUserClaim<long>, ApplicationUserRole, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
         : base(options)
        {
        }
        //DbSet<UserType> tbl_UserType { get; set; }
        DbSet<DocumentType> DocumentTypes { get; set; }
        DbSet<PlantCode> PlantCodes { get; set; }
        DbSet<TaxCode> TaxCodes { get; set; }
        DbSet<CustomerCode> CustomerCodes { get; set; }
        DbSet<VendorCode> VendorCodes { get; set; }
        DbSet<Company> Companies { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<TBTagging> TBTaggings { get; set; }
        DbSet<GLIncome_Mapping> GLIncome_Mappings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


            builder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("Users");
            });

            builder.Entity<ApplicationRole>(b =>
            {
                b.ToTable("Roles");
            });

            builder.Entity<ApplicationUserRole>(b =>
            {
                b.ToTable("UserRoles");
                b.HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);
                b.HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);
            });

            builder.Entity<IdentityUserToken<long>>(b =>
            {
                b.ToTable("UserTokens");
            });

            builder.Entity<IdentityUserClaim<long>>(b =>
            {
                b.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<long>>(b =>
            {
                b.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<long>>(b =>
            {
                b.ToTable("RoleClaims");
            });
            builder.Entity<UserType>(b =>
            {
                b.ToTable("tbl_UserType");
            });

            builder.Entity<Company>(b =>
            {
                b.ToTable("tbl_Company");
            });

            builder.Entity<DocumentType>(b =>
            {
                b.ToTable("tbl_Document_Type");
            });

            builder.Entity<PlantCode>(b =>
            {
                b.ToTable("tbl_Plant_Code");
            });
            builder.Entity<TaxCode>(b =>
            {
                b.ToTable("tbl_Tax_Code");
            });
            builder.Entity<CustomerCode>(b =>
            {
                b.ToTable("tbl_Customer_Code");
            });
            builder.Entity<VendorCode>(b =>
            {
                b.ToTable("tbl_Vendor_Code");
            });
            builder.Entity<Location>(b =>
            {
                b.ToTable("tbl_Location");
            });
            builder.Entity<TBTagging>(b =>
            {
                b.ToTable("tbl_TBTagging");
            });
            builder.Entity<GLIncome_Mapping>(b =>
            {
                b.ToTable("tbl_GL_Income_Mapping");
            });
        }
    }
}
