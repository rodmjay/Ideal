#region credits
// ***********************************************************************
// Assembly	: Ideal.Infrastructure
// Author	: Rod Johnson
// Created	: 03-19-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Data.Entity;
using Ideal.Core.Model.Cannabis;
using Ideal.Core.Model.Forms;
using Ideal.Core.Model.Logging;
using Ideal.Identity.Model;

namespace Ideal.Infrastructure.Data
{
    #region

    #endregion
    public partial class DataContext : BaseContext<DataContext>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Effect> Effects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasMany(s => s.Symptoms)
                .WithMany(c => c.Recipes)
                .Map(cs =>
                {
                    cs.MapLeftKey("RecipeId");
                    cs.MapRightKey("SymptomId");
                    cs.ToTable("RecipeSymptom");
                });

            modelBuilder.Entity<Recipe>()
                .HasMany(s => s.Effects)
                .WithMany(c => c.Recipes)
                .Map(cs =>
                {
                    cs.MapLeftKey("RecipeId");
                    cs.MapRightKey("EffectId");
                    cs.ToTable("RecipeEffect");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
