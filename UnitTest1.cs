using System.Collections.Generic;
using NUnit.Framework;
using PROG6221_POE_PART2_ST10075970;
using Assert = NUnit.Framework.Assert;

namespace UnitTest
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            // Initialization code, if needed
        }

        [Test]
        public void TotalCalories_ShouldBeOver300_WhenIngredientsExceed300Calories()
        {
            // Arrange
            var ingredients = new List<Ingredient>
            {
                new Ingredient("Sugar", 100, "grams", 4, "Carbs"), // 400 calories
                new Ingredient("Butter", 50, "grams", 7, "Fats"), // 350 calories
            };
            var steps = new List<RecipeStep>
            {
                new RecipeStep("Mix ingredients"),
                new RecipeStep("Bake at 180 degrees")
            };
            var recipe = new Recipe("Cake", ingredients, steps);

            // Act
            double totalCalories = recipe.TotalCalories;

            // Assert
            Assert.That(totalCalories, Is.GreaterThan(300));
        }

        [Test]
        public void TotalCalories_ShouldBeUnder300_WhenIngredientsDoNotExceed300Calories()
        {
            // Arrange
            var ingredients = new List<Ingredient>
            {
                new Ingredient("Apple", 1, "unit", 95, "Fruits"), // 95 calories
                new Ingredient("Banana", 1, "unit", 105, "Fruits"), // 105 calories
                new Ingredient("Orange", 1, "unit", 62, "Fruits") // 62 calories
            };
            var steps = new List<RecipeStep>
            {
                new RecipeStep("Peel and slice the fruits"),
                new RecipeStep("Mix the fruits in a bowl")
            };
            var recipe = new Recipe("Fruit Salad", ingredients, steps);

            // Act
            double totalCalories = recipe.TotalCalories;

            // Assert
            Assert.That(totalCalories, Is.LessThanOrEqualTo(300));
        }
    }
}
