namespace CoffeeMachine;

/// <summary>Модуль помола: превращает зёрна в молотый кофе.</summary>
public class GrinderUnit
{
    public GroundCoffee Grind(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Для помола нужно положительное количество зёрен.", nameof(quantity));

        return new GroundCoffee(quantity);
    }
}
