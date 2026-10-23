namespace CoffeeMachine;

/// <summary>Молотый кофе - результат работы модуля помола.</summary>
public class GroundCoffee
{
    public int Quantity { get; }

    public GroundCoffee(int quantity)
    {
        Quantity = quantity;
    }
}
