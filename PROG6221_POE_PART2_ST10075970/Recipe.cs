using System;
using System.Collections.Generic;
using System.Linq;
using PROG6221_POE_PART2_ST10075970;

namespace PROG6221_POE_PART2_ST10075970
{
    public class Recipe
    {
        public string Name { get; set; } // Name of the recipe
        private readonly List<Ingredient> originalIngredients; // List to store original ingredient quantities
        public List<Ingredient> Ingredients { get; set; } // List of ingredients in the recipe
        public List<RecipeStep> Steps { get; set; } // List of steps in the recipe

        // Constructor to initialize the Recipe object
        public Recipe(string name, List<Ingredient> ingredients, List<RecipeStep> steps)
        {
            Name = name;
            Ingredients = ingredients;
            Steps = steps;

            // Store original ingredient quantities
            originalIngredients = ingredients.Select(i => new Ingredient(i.Name, i.Quantity, i.Unit, i.Calories, i.FoodGroup)).ToList();
        }

        // Method to display the recipe
        public void DisplayRecipe()
        {
            if (Ingredients == null || Steps == null)
            {
                Console.WriteLine("No recipe to display.");
                return;
            }

            Console.WriteLine($"Recipe: {Name}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in Ingredients)
            {
                Console.WriteLine(ingredient);
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Steps[i]}");
            }

            double totalCalories = TotalCalories;
            Console.WriteLine($"\nTotal Calories: {totalCalories}");
        }

        // Method to scale the recipe by a given factor
        public void ScaleRecipe(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }

        // Method to reset ingredient quantities to original values
        public void ResetQuantities()
        {
            for (int i = 0; i < Ingredients.Count; i++)
            {
                Ingredients[i].Quantity = originalIngredients[i].Quantity;
            }
        }

        // Method to clear the recipe with confirmation prompt
        public void ClearRecipe()
        {
            Console.WriteLine("Are you sure you want to clear the recipe? (Y/N)");
            var userInput = Console.ReadLine().ToLower();

            if (userInput == "y" || userInput == "yes")
            {
                Ingredients = null;
                Steps = null;
                Console.WriteLine("Recipe cleared successfully.");
            }
            else
            {
                Console.WriteLine("Recipe not cleared.");
            }
        }

        // Property to calculate the total calories of the recipe
        public double TotalCalories => Ingredients.Sum(i => i.Calories * i.Quantity);
    }
    //CODE ATTRIBUTION: A. Troelsen and P. Japikse. 2022. Pro C# 10 with .NET 6. 11th ed.West Chester, OH, USA. Apress.
}