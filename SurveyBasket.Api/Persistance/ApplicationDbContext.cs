
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;

namespace SurveyBasket.Api.Persistence
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,HttpContextAccessor httpContextAccessor):
		IdentityDbContext<ApplicationUser>(options)
	{
        private readonly HttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public DbSet<Poll> Polls { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditableEntity>(); // SELECT ALL ENTITIES that inherit this class
			foreach(var entityEntry in entries)
			{
				var currentUser = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                if (entityEntry.State == EntityState.Added)
				{
					entityEntry.Property(x => x.CreatedById).CurrentValue = currentUser;

                }
				else
				{
                    entityEntry.Property(x => x.UpdatedById).CurrentValue = currentUser;

                    entityEntry.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow; 


                }
            }
        }
    }
}
