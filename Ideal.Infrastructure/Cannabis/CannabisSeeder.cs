using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Ideal.Core.Model.Cannabis;
using Ideal.Infrastructure.Data;

namespace Ideal.Infrastructure.Cannabis
{
    public class CannabisSeeder
    {
        public void Seed(DataContext context)
        {
            var symptoms = new string[]{
                "Alzheimer's Disease",
                "Amyotrophic Lateral Sclerosis (ALS)",
                "Anxiety",
                "Attention Deficit Disorder (ADD)",
                "Cancer",
                "Chronic Pain",
                "Colitis",
                "Cramping/PMS",
                "Chron's Disease",
                "Depression",
                "Diabetes",
                "Diarrhea",
                "Dystonia",
                "Epilepsy",
                "Fibromyalgia",
                "Glaucoma",
                "Gastrointestinal (GI) disorders",
                "Gliomas (tumors in the brain)",
                "Headache",
                "Hepatitis C",
                "Homeostasis",
                "Human Immunodeficiency Virus (HIV)",
                "Huntington's Disease",
                "Hypertension",
                "Incontinence",
                "Indigestion/Acid Reflux",
                "Inflamation",
                "Inflammatory Bowel disease",
                "Insomnia",
                "Irritable Bowel Syndrome (IBS)",
                "Methicillin-Resistant Staphyloccus Aureus (MRSA)",
                "Migranes",
                "Multiple Sclerosis (MS)",
                "Muscle Spasm",
                "Nausea",
                "Obsessive Compulsive Disorders (OCD)",
                "Opioid addiction",
                "Osteoporosis",
                "Parkinson's Disease",
                "Post Traumatic Stress Disorder (PTSD)",
                "Rheumatoid Arthritis",
                "Sleep Apnea",
                "Stress",
                "Tourettes Syndrome",
                "Traumatic Brain Injury (TBI)"
            };

            var effects = new[]{
                "Happy",
                "Hungry",
                "Relaxed",
                "Sleepy",
                "Increase energy",
                "Appetite Stimulation",
                "Focus"
            };

            for (int i = 0; i < symptoms.Length; i++)
            {
                context.Symptoms.AddOrUpdate(x => x.Id, new Symptom
                {
                    Name = symptoms[i],
                    Id = i,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                });
            }

            for (int i = 0; i < effects.Length; i++)
            {
                context.Effects.AddOrUpdate(x => x.Id, new Effect
                {
                    Name = effects[i],
                    Id = i,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                });
            }

            var recipe = new Recipe
            {
                Id = 1,
                PartsCBD = 1,
                PartsTHC = 1,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            recipe.Effects.Add(context.Effects.Find(1));
            recipe.Effects.Add(context.Effects.Find(2));
            recipe.Effects.Add(context.Effects.Find(4));

            recipe.Symptoms.Add(context.Symptoms.Find(1));
            recipe.Symptoms.Add(context.Symptoms.Find(2));

            context.Recipes.AddOrUpdate(x => x.Id, recipe);

            context.SaveChanges();
        }
    }
}
