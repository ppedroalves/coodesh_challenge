using Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Commerce.Data.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();


            builder.Property(c => c.CreatedDate)
            .IsRequired()
            .HasColumnName("CreatedDate")
            .HasColumnType("SMALLDATETIME")
            .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.Total)
            .IsRequired()
            .HasColumnName("Total")
            .HasColumnType("FLOAT");

            builder.HasMany(x => x.Itens)
                .WithOne(x => x.Order);


         




        }
    }
}
