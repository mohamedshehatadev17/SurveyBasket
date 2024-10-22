
namespace SurveyBasket.Api.Presistance.EntitiesConfigurations
{
	public class PollConfiguration : IEntityTypeConfiguration<Poll>
	{
		public void Configure(EntityTypeBuilder<Poll> builder)
		{
			builder.HasIndex(x=>x.Title).IsUnique();

			builder.Property(x=>x.Title).HasMaxLength(200);
			builder.Property(x=>x.Summary).HasMaxLength(1500);
		}
	}
}
