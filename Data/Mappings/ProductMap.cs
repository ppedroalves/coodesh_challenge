using Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Commerce.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();


            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("VARCHAR(100)");

            builder.Property(c => c.Description)
            .IsRequired()
            .HasColumnName("Description")
            .HasColumnType("VARCHAR(100)");

            builder.Property(c => c.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("FLOAT");

            builder.Property(c => c.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("SMALLDATETIME")
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Provider)
                .WithMany(x => x.Products)
                .HasConstraintName("FK_Product_Provider")
                .OnDelete(DeleteBehavior.Cascade);



            builder.HasOne(x => x.Category);
              
        }
    }
}
