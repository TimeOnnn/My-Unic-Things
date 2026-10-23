namespace CoffeeMachine;

/// <summary>Модуль варки: из молотого кофе делает готовый напиток.</summary>
public class BrewingUnit
{
    public Coffee Brew(GroundCoffee groundCoffee, RecipeName recipe)
    {
        ArgumentNullException.ThrowIfNull(groundCoffee);

        return new Coffee(recipe);
    }
}
