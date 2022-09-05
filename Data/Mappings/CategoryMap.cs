using Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Commerce.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {


        public void Configure(EntityTypeBuilder<Category> builder)
        {
           builder.ToTable("Category");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("VARCHAR(100)");

            builder.Property(c => c.Slug)
             .HasColumnName("Slug")
             .HasColumnType("VARCHAR(100)");

            builder.Property(c => c.Description)
            .IsRequired()
            .HasColumnName("Description")
            .HasColumnType("VARCHAR(100)");



            builder.Property(c => c.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("SMALLDATETIME")
            .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(c => c.Slug, "IX_Category_Slug")
                .IsUnique();


        }


    }
}
