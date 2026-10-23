namespace CoffeeMachine;

/// <summary>
/// Кофемашина. Снаружи видны только простые операции (загрузить, узнать уровень, сварить);
/// контейнеры и модули помола/варки спрятаны внутри (инкапсуляция).
/// </summary>
public class CoffeeMachine
{
    private const int DefaultWaterCapacity = 2000;
    private const int DefaultMilkCapacity = 1000;
    private const int DefaultBeansCapacity = 500;

    // Набор рецептов задаёт «производитель», потом он не меняется.
    private readonly Dictionary<RecipeName, Recipe> _recipes = new()
    {
        [RecipeName.Espresso] = new Recipe(water: 50, milk: 0, beans: 10),
        [RecipeName.Filter] = new Recipe(water: 200, milk: 0, beans: 15),
        [RecipeName.Cappuccino] = new Recipe(water: 50, milk: 150, beans: 10)
    };

    private readonly GrinderUnit _grinderUnit = new();
    private readonly BrewingUnit _brewingUnit = new();
    private readonly Container _waterContainer;
    private readonly Container _milkContainer;
    private readonly Container _beansContainer;

    public CoffeeMachine(
        int waterCapacity = DefaultWaterCapacity,
        int milkCapacity = DefaultMilkCapacity,
        int beansCapacity = DefaultBeansCapacity)
    {
        _waterContainer = new Container(waterCapacity);
        _milkContainer = new Container(milkCapacity);
        _beansContainer = new Container(beansCapacity);
    }

    public void LoadWater(int value) => _waterContainer.LoadResource(value);
    public void LoadMilk(int value) => _milkContainer.LoadResource(value);
    public void LoadBeans(int value) => _beansContainer.LoadResource(value);

    public int GetWaterLevel() => _waterContainer.Value;
    public int GetMilkLevel() => _milkContainer.Value;
    public int GetBeansLevel() => _beansContainer.Value;

    /// <summary>Возвращает рецепт по названию (чтобы можно было узнать, сколько он требует).</summary>
    public Recipe GetRecipe(RecipeName recipeName)
    {
        if (!_recipes.TryGetValue(recipeName, out var recipe))
            throw new ArgumentException($"Рецепт «{recipeName}» не поддерживается.", nameof(recipeName));

        return recipe;
    }

    /// <summary>Приготовить кофе по рецепту. Если ресурсов не хватает - InvalidOperationException.</summary>
    public Coffee Brew(RecipeName recipeName)
    {
        var recipe = GetRecipe(recipeName);

        // Сначала проверяем ВСЁ, и только потом забираем ресурсы:
        // иначе при нехватке молока вода и зёрна уже были бы потрачены.
        if (_waterContainer.Value < recipe.Water)
            throw new InvalidOperationException(
                $"Недостаточно воды: нужно {recipe.Water}, есть {_waterContainer.Value}.");

        if (_milkContainer.Value < recipe.Milk)
            throw new InvalidOperationException(
                $"Недостаточно молока: нужно {recipe.Milk}, есть {_milkContainer.Value}.");

        if (_beansContainer.Value < recipe.Beans)
            throw new InvalidOperationException(
                $"Недостаточно зёрен: нужно {recipe.Beans}, есть {_beansContainer.Value}.");

        _waterContainer.GetResource(recipe.Water);
        _milkContainer.GetResource(recipe.Milk);
        var beans = _beansContainer.GetResource(recipe.Beans);

        var groundCoffee = _grinderUnit.Grind(beans);
        return _brewingUnit.Brew(groundCoffee, recipeName);
    }
}
