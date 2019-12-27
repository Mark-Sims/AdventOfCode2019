using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    class Program
    {

        static void Main(string[] args)
        {

            var inputFile = Environment.CurrentDirectory + "//input.txt";

            string[] lines = System.IO.File.ReadAllLines(inputFile);

            ReactionCalculator calc = new ReactionCalculator(lines);
            calc.Calculate();

            Console.WriteLine(calc.RequiredORE);
        }

    }

    class ReactionCalculator
    {
        public long RequiredORE { get; set; }

        Dictionary<string, Reaction> _availableRecipes;
        Dictionary<string, long> _chemicalBank;

        public ReactionCalculator(string[] recipeLines)
        {
            _availableRecipes = new Dictionary<string, Reaction>();
            _chemicalBank = new Dictionary<string, long>();

            foreach (var unparsedReaction in recipeLines)
            {
                var reaction = new Reaction(unparsedReaction);
                _availableRecipes.Add(reaction.OutputChemical, reaction);
            }

            // ORE is free
            _availableRecipes.Add("ORE", new Reaction
            {
                Inputs = new Dictionary<string, long>(),
                OutputChemical = "ORE",
                OutputChemicalAmount = 1
            });

        }

        public void Calculate()
        {
            var chemicalsRequired = new Dictionary<string, long>();
            chemicalsRequired.Add("FUEL", 2144702);
            chemicalsRequired.Add("ORE", 0);

            while (chemicalsRequired.Count > 1)
            {
                // Get any required chemical besides ORE
                var requiredChemical = chemicalsRequired.Where(x => x.Key != "ORE").First();

                chemicalsRequired.Remove(requiredChemical.Key);

                var recipe = _availableRecipes[requiredChemical.Key];
                var multiplier = CalculateRecipeMultiplier(recipe, requiredChemical.Value);

                if (requiredChemical.Key == "ORE")
                {

                }
                if (multiplier > 0)
                {
                    foreach (var requiredReactant in recipe.Inputs)
                    {
                        if (chemicalsRequired.ContainsKey(requiredReactant.Key))
                        {
                            chemicalsRequired[requiredReactant.Key] += requiredReactant.Value * multiplier;
                        }
                        else
                        {
                            chemicalsRequired.Add(requiredReactant.Key, requiredReactant.Value * multiplier);
                        }
                    }
                }
            }

            RequiredORE = chemicalsRequired["ORE"];
        }

        public long CalculateRecipeMultiplier(Reaction recipe, long numOfProductRequired)
        {
            // If we have some of the needed chemical in the bank
            if (_chemicalBank.ContainsKey(recipe.OutputChemical))
            {
                // If we have just enough, or more than enough
                if (_chemicalBank[recipe.OutputChemical] >= numOfProductRequired)
                {
                    // Remove what we used and return 0 to indicate we don't need to undergo this
                    // reaction right now.
                    _chemicalBank[recipe.OutputChemical] -= numOfProductRequired;
                    return 0;
                }
                // If we don't have enough, remove what we have and subtract that amount from
                // the amount we still need.
                else
                {
                    numOfProductRequired -= _chemicalBank[recipe.OutputChemical];
                    _chemicalBank.Remove(recipe.OutputChemical);
                }
            }

            // If the recipe produces more than we need, multiplier is 1, and store the excess in the bank.
            if (numOfProductRequired < recipe.OutputChemicalAmount)
            {
                _chemicalBank.Add(recipe.OutputChemical, recipe.OutputChemicalAmount - numOfProductRequired);
                return 1;
            }
            // If the recipe produces exactly what we need, multiplier is 1 with nothing added to the bank.
            else if (numOfProductRequired == recipe.OutputChemicalAmount)
            {
                return 1;
            }
            // If the recipe produces less that we need, we'll need to perform it multiple times.
            else
            {
                var multiplier = (long)Math.Ceiling((double)numOfProductRequired / recipe.OutputChemicalAmount);
                if (recipe.OutputChemicalAmount * multiplier > numOfProductRequired)
                {
                    _chemicalBank.Add(recipe.OutputChemical, recipe.OutputChemicalAmount * multiplier - numOfProductRequired);
                }
                return multiplier;
            }
        }
    }


    class Reaction
    {
        public Dictionary<string, long> Inputs { get; set; }

        // Checmical reactions only product a single chemical.
        public string OutputChemical;
        public long OutputChemicalAmount;

        public Reaction() { }

        public Reaction(string unparsedReaction)
        {
            Inputs = new Dictionary<string, long>();

            var reactants = unparsedReaction.Split('=')[0].Trim();
            var products = unparsedReaction.Split('>')[1].Trim();

            string[] reactantChemicalsAndAmounts = reactants.Split(',');
            foreach (var chemicalAndAmount in reactantChemicalsAndAmounts)
            {
                var trimmedChemicalAndAmount = chemicalAndAmount.Trim();
                var amount = long.Parse(trimmedChemicalAndAmount.Split(' ')[0]);
                var chemical = trimmedChemicalAndAmount.Split(' ')[1];

                Inputs.Add(chemical, amount);
            }

            OutputChemicalAmount = long.Parse(products.Split(' ')[0]);
            OutputChemical = products.Split(' ')[1];
        }
    }

}
