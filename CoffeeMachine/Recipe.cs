namespace CoffeeMachine;

/// <summary>Рецепт: сколько ресурсов нужно для приготовления одной порции.</summary>
public class Recipe
{
    public int Water { get; }
    public int Milk { get; }
    public int Beans { get; }

    public Recipe(int water, int milk, int beans)
    {
        if (water < 0)
            throw new ArgumentException("Количество воды не может быть отрицательным.", nameof(water));
        if (milk < 0)
            throw new ArgumentException("Количество молока не может быть отрицательным.", nameof(milk));
        if (beans <= 0)
            throw new ArgumentException("Для кофе нужно положительное количество зёрен.", nameof(beans));

        Water = water;
        Milk = milk;
        Beans = beans;
    }
}
