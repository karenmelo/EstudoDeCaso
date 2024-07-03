using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EstudoDeCaso.Infra.Context
{
    public class EstudoDeCasoContext : DbContext
    {
        public EstudoDeCasoContext(DbContextOptions<EstudoDeCasoContext> options):base(options) { }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            using var transaction = Database.BeginTransaction();
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EstudoDeCasoContext).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  

        }
    }
}
