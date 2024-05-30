using System;
using System.Collections.Generic;
using System.Linq;

namespace PROG6221_POE_PART2_ST10075970
{
    class Program
    {
        static List<Recipe> recipes = new List<Recipe>(); // List to store multiple recipes

        // Delegate for calorie notification
        public delegate void CalorieNotification(string message);
        public static event CalorieNotification OnCalorieExceed;

        static void Main(string[] args)
        {
            // Subscribe to the calorie notification event
            OnCalorieExceed += message => Console.WriteLine(message);

            Console.ForegroundColor = ConsoleColor.DarkCyan;

            try
            {
                while (true)
                {
                    Console.WriteLine("\nMenu:");
                    Console.WriteLine("1. Create Recipe");
                    Console.WriteLine("2. Display All Recipes");
                    Console.WriteLine("3. Select and Display Recipe");
                    Console.WriteLine("4. Scale Recipe");
                    Console.WriteLine("5. Reset Quantities");
                    Console.WriteLine("6. Clear Recipe");
                    Console.WriteLine("7. Exit");
                    Console.Write("Select an option: ");

                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            CreateRecipe();
                            break;
                        case 2:
                            DisplayAllRecipes();
                            break;
                        case 3:
                            SelectAndDisplayRecipe();
                            break;
                        case 4:
                            ScaleRecipe();
                            break;
                        case 5:
                            ResetQuantities();
                            break;
                        case 6:
                            ClearRecipe();
                            break;
                        case 7:
                            Console.WriteLine("Thank you for using this recipe app! Hope to see you soon for some more delicious recipes ;)");
                            Console.WriteLine("Press any key to exit...");
                            Console.ReadKey();
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please select a number between 1 and 7.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        // Method to create a new recipe
        static void CreateRecipe()
        {
            try
            {
                Console.Write("Enter the recipe name: ");
                string recipeName = Console.ReadLine();

                Console.WriteLine("Enter number of ingredients:");
                if (!int.TryParse(Console.ReadLine(), out int numIngredients) || numIngredients <= 0)
                {
                    Console.WriteLine("Invalid input for number of ingredients.");
                    return;
                }

                List<Ingredient> ingredients = new List<Ingredient>();
                for (int i = 0; i < numIngredients; i++)
                {
                    Console.WriteLine($"Enter details for ingredient {i + 1}:");
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Quantity: ");
                    if (!double.TryParse(Console.ReadLine(), out double quantity) || quantity <= 0)
                    {
                        Console.WriteLine("Invalid input for quantity.");
                        return;
                    }
                    Console.Write("Unit: ");
                    string unit = Console.ReadLine();
                    Console.Write("Calories: ");
                    if (!double.TryParse(Console.ReadLine(), out double calories) || calories < 0)
                    {
                        Console.WriteLine("Invalid input for calories.");
                        return;
                    }
                    Console.Write("Food Group: ");
                    string foodGroup = Console.ReadLine();
                    ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
                }

                Console.WriteLine("Enter number of steps:");
                if (!int.TryParse(Console.ReadLine(), out int numSteps) || numSteps <= 0)
                {
                    Console.WriteLine("Invalid input for number of steps.");
                    return;
                }

                List<RecipeStep> steps = new List<RecipeStep>();
                for (int i = 0; i < numSteps; i++)
                {
                    Console.WriteLine($"Enter step {i + 1}:");
                    string description = Console.ReadLine();
                    steps.Add(new RecipeStep(description));
                }

                Recipe recipe = new Recipe(recipeName, ingredients, steps);
                recipes.Add(recipe);

                double totalCalories = recipe.TotalCalories;
                if (totalCalories > 300)
                {
                    OnCalorieExceed?.Invoke($"Warning: The total calories of the recipe '{recipeName}' exceed 300.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while creating recipe: " + ex.Message);
            }
        }

        // Method to display all recipes
        static void DisplayAllRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.WriteLine("Recipes:");
            foreach (var recipe in recipes.OrderBy(r => r.Name))
            {
                Console.WriteLine(recipe.Name);
            }
        }

        // Method to select and display a recipe
        static void SelectAndDisplayRecipe()
        {
            Console.Write("Enter the name of the recipe to display: ");
            string recipeName = Console.ReadLine();
            Recipe recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

            if (recipe != null)
            {
                recipe.DisplayRecipe();
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }

        // Method to scale an existing recipe
        static void ScaleRecipe()
        {
            Console.Write("Enter the name of the recipe to scale: ");
            string recipeName = Console.ReadLine();
            Recipe recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

            if (recipe != null)
            {
                try
                {
                    Console.WriteLine("Enter scale factor (0.5, 2, or 3):");
                    if (!double.TryParse(Console.ReadLine(), out double scaleFactor) || (scaleFactor != 0.5 && scaleFactor != 2 && scaleFactor != 3))
                    {
                        Console.WriteLine("Invalid scale factor. Please enter 0.5, 2, or 3.");
                        return;
                    }
                    recipe.ScaleRecipe(scaleFactor);

                    Console.WriteLine("\nScaled Recipe:");
                    recipe.DisplayRecipe();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while scaling recipe: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }

        // Method to reset ingredient quantities of a recipe
        static void ResetQuantities()
        {
            Console.Write("Enter the name of the recipe to reset: ");
            string recipeName = Console.ReadLine();
            Recipe recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

            if (recipe != null)
            {
                recipe.ResetQuantities();
                Console.WriteLine("Quantities reset successfully.");
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }

        // Method to clear a recipe
        static void ClearRecipe()
        {
            Console.Write("Enter the name of the recipe to clear: ");
            string recipeName = Console.ReadLine();
            Recipe recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

            if (recipe != null)
            {
                recipe.ClearRecipe();
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }
    }
    //CODE ATTRIBUTION: A. Troelsen and P. Japikse. 2022. Pro C# 10 with .NET 6. 11th ed.West Chester, OH, USA. Apress.
}