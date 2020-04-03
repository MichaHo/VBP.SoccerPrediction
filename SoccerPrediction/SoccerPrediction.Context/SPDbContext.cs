using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using SoccerPrediction.Model;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerPrediction.Context
{
    public class SPDbContext : DbContext
    {
        #region Private Member

        private readonly string dbFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"SPDataBase.db");
        
        #endregion

        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[] { new DbLoggerProvider(s => File.AppendAllText("Log.txt", s)) });

        #region Constructor

        public SPDbContext()
        {

        }

        public SPDbContext(DbContextOptions options) : base(options)
        {

        }

        #endregion

        #region DB Sets

        public virtual DbSet<AccessData> AccessDatas { get; set; }
        public virtual DbSet<Encounter> Encounters { get; set; }
        public virtual DbSet<GameDay> GameDays { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Prediction> Predictions { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        #endregion

        #region Overrides

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source={dbFile}");
            }
        }

        #endregion

        #region Helper Methods


        public override int SaveChanges()
        {
            UpdateEntitysBeforeSaving();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateEntitysBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        private void UpdateEntitysBeforeSaving()
        {

            var objectStateEntries = ChangeTracker.Entries()
                .Where(e => e.Entity is ModelBase && e.State != EntityState.Detached && e.State != EntityState.Unchanged).ToList();

            foreach (var entry in objectStateEntries)
            {
                if (!(entry.Entity is ModelBase entityBase)) continue;
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        if (entityBase is ILogicalDelete delete)
                        {
                            entry.State = EntityState.Modified;
                            delete.DeletedTimestamp = DateTime.Now;
                            delete.DeletedFlag = true;
                            entry.Property(nameof(delete.DeletedTimestamp)).IsModified = true;
                            entry.Property(nameof(delete.DeletedFlag)).IsModified = true;
                        }
                        break;
                    case EntityState.Modified:

                        if (entityBase is ILogicalTimestamp timestamp)
                        {
                            timestamp.LastUpdateTimestamp = DateTime.Now;
                            entry.Property(nameof(timestamp.LastUpdateTimestamp)).IsModified = true;
                        }
                        if (entityBase is ILogicalDelete deleted)
                        {
                            string originalvalue = entry.Property(nameof(deleted.DeletedFlag)).OriginalValue.ToString();
                            string currentValue = entry.Property(nameof(deleted.DeletedFlag)).CurrentValue.ToString();
                            bool.TryParse(originalvalue, out var result);
                            bool.TryParse(currentValue, out var result1);
                            if (result != result1)
                            {
                                deleted.DeletedTimestamp = DateTime.Now;
                                entry.Property(nameof(deleted.DeletedTimestamp)).IsModified = true;
                            }
                        }
                        break;
                    case EntityState.Added:
                        if (entityBase is ILogicalTimestamp logicalTimestamp)
                        {
                            logicalTimestamp.CreationTimestamp = DateTime.Now;
                            logicalTimestamp.LastUpdateTimestamp = DateTime.Now;
                            entry.Property(nameof(logicalTimestamp.CreationTimestamp)).IsModified = true;
                            entry.Property(nameof(logicalTimestamp.LastUpdateTimestamp)).IsModified = true;
                        }
                        break;
                }

            }

        }

        public void DetachAll()
        {
            foreach (EntityEntry entityEntry in ChangeTracker.Entries().ToArray())
            {
                if (entityEntry.Entity != null)
                {
                    entityEntry.State = EntityState.Detached;
                }
            }
        }

        #endregion
    }
}
