using System;

namespace PROG6221_POE_PART2_ST10075970
{
    public class Ingredient
    {
        public string Name { get; set; } // Name of the ingredient
        public double Quantity { get; set; } // Quantity of the ingredient
        public string Unit { get; set; } // Unit of measurement for the ingredient
        public double Calories { get; set; } // Number of calories in the ingredient
        public string FoodGroup { get; set; } // Food group of the ingredient

        // Constructor to initialize the Ingredient object
        public Ingredient(string name, double quantity, string unit, double calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }

        // Override ToString method to return a string representation of the Ingredient
        public override string ToString()
        {
            return $"{Quantity} {Unit} of {Name} ({Calories} calories, {FoodGroup})";
        }
    }
}