using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ideal.Core.Model.Cannabis;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Ideal.Core.Tests.Model
{
    [TestFixture]
    public class CannabisTests
    {
        [TestFixture]
        public class RecipeTests
        {
            [Test]
            public void CanAddEffectToRecipe()
            {
                var effect = new Effect{
                    Name = "Dry mouth"
                };

                var recipe = new Recipe();
                recipe.Effects.Add(effect);

                Assert.IsTrue(recipe.Effects.Count>0);
            }

            [Test]
            public void CanAddSymptomToRecipe()
            {
                var symptom = new Symptom{
                    Name = "Dry mouth"
                };

                var recipe = new Recipe();
                recipe.Symptoms.Add(symptom);

                Assert.IsTrue(recipe.Symptoms.Count > 0);
            }
        }
    }
}
