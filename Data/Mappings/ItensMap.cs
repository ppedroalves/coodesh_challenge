using Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Commerce.Data.Mappings
{
    public class ItensMap : IEntityTypeConfiguration<Itens>
    {
        public void Configure(EntityTypeBuilder<Itens> builder)
        {
            builder.ToTable("Itens");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(c => c.Amount)
             .IsRequired()
             .HasColumnName("Amount")
             .HasColumnType("int");

            builder.HasOne(x => x.Product);
       

            builder.HasOne(x => x.Order)
                .WithMany(x => x.Itens);
        }
    }
}
