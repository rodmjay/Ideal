using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Ideal.Core.Model.Cannabis
{
    public class Symptom : DomainObject
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; } 
    }

    public class Effect : DomainObject
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; } 
    }

    public class Recipe : DomainObject
    {
        private ICollection<Effect> _effects;
        private ICollection<Symptom> _symptoms;

        [Required]
        public int PartsCBD { get; set; }

        [Required]
        public int PartsTHC { get; set; }

        public virtual ICollection<Symptom> Symptoms
        {
            get { return _symptoms ?? (_symptoms = new Collection<Symptom>()); }
            set { _symptoms = value; }
        }

        public virtual ICollection<Effect> Effects
        {
            get { return _effects ?? (_effects = new Collection<Effect>()); }
            set { _effects = value; }
        }
    }
}
