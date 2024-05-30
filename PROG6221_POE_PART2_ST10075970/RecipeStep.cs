using System;

namespace PROG6221_POE_PART2_ST10075970
{
    public class RecipeStep
    {
        public string Description { get; set; } // Description of the recipe step

        // Constructor to initialize the RecipeStep object
        public RecipeStep(string description)
        {
            Description = description;
        }

        // Override ToString method to return a string representation of the RecipeStep
        public override string ToString()
        {
            return Description;
        }
    }
}