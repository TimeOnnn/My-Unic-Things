namespace CoffeeMachine.Tests;

public class CoffeeMachineTest
{
    // Вспомогательный метод: машина, в которую уже загружены ресурсы.
    private static CoffeeMachine CreateLoadedMachine(int water = 1000, int milk = 1000, int beans = 500)
    {
        var machine = new CoffeeMachine();
        machine.LoadWater(water);
        machine.LoadMilk(milk);
        machine.LoadBeans(beans);
        return machine;
    }

    // ---------- Загрузка и уровни ----------

    [Fact]
    public void NewMachine_AllLevelsAreZero()
    {
        var machine = new CoffeeMachine();

        Assert.Equal(0, machine.GetWaterLevel());
        Assert.Equal(0, machine.GetMilkLevel());
        Assert.Equal(0, machine.GetBeansLevel());
    }

    [Fact]
    public void LoadWater_LevelIncreases()
    {
        var machine = new CoffeeMachine();

        machine.LoadWater(500);

        Assert.Equal(500, machine.GetWaterLevel());
    }

    [Fact]
    public void LoadMilk_LevelIncreases()
    {
        var machine = new CoffeeMachine();

        machine.LoadMilk(300);

        Assert.Equal(300, machine.GetMilkLevel());
    }

    [Fact]
    public void LoadBeans_LevelIncreases()
    {
        var machine = new CoffeeMachine();

        machine.LoadBeans(200);

        Assert.Equal(200, machine.GetBeansLevel());
    }

    [Fact]
    public void LoadWater_MoreThanContainerCapacity_ThrowsException()
    {
        var machine = new CoffeeMachine(waterCapacity: 100, milkCapacity: 100, beansCapacity: 100);

        Assert.Throws<InvalidOperationException>(() => machine.LoadWater(101));
    }

    // ---------- Приготовление кофе ----------

    [Theory]
    [InlineData(RecipeName.Espresso)]
    [InlineData(RecipeName.Filter)]
    [InlineData(RecipeName.Cappuccino)]
    public void Brew_ReturnsCoffeeWithRequestedRecipe(RecipeName recipeName)
    {
        var machine = CreateLoadedMachine();

        var coffee = machine.Brew(recipeName);

        Assert.NotNull(coffee);
        Assert.Equal(recipeName, coffee.Recipe);
    }

    [Theory]
    [InlineData(RecipeName.Espresso)]
    [InlineData(RecipeName.Filter)]
    [InlineData(RecipeName.Cappuccino)]
    public void Brew_ConsumesResourcesAccordingToRecipe(RecipeName recipeName)
    {
        // Arrange
        const int water = 1000, milk = 1000, beans = 500;
        var machine = CreateLoadedMachine(water, milk, beans);
        var recipe = machine.GetRecipe(recipeName);

        // Act
        machine.Brew(recipeName);

        // Assert
        Assert.Equal(water - recipe.Water, machine.GetWaterLevel());
        Assert.Equal(milk - recipe.Milk, machine.GetMilkLevel());
        Assert.Equal(beans - recipe.Beans, machine.GetBeansLevel());
    }

    [Fact]
    public void Brew_ExactlyEnoughResources_Success()
    {
        var machine = new CoffeeMachine();
        var recipe = machine.GetRecipe(RecipeName.Cappuccino);
        machine.LoadWater(recipe.Water);
        machine.LoadMilk(recipe.Milk);
        machine.LoadBeans(recipe.Beans);

        machine.Brew(RecipeName.Cappuccino);

        Assert.Equal(0, machine.GetWaterLevel());
        Assert.Equal(0, machine.GetMilkLevel());
        Assert.Equal(0, machine.GetBeansLevel());
    }

    [Fact]
    public void Brew_TwiceInARow_ConsumesResourcesTwice()
    {
        var machine = CreateLoadedMachine(water: 1000, milk: 1000, beans: 500);
        var recipe = machine.GetRecipe(RecipeName.Espresso);

        machine.Brew(RecipeName.Espresso);
        machine.Brew(RecipeName.Espresso);

        Assert.Equal(1000 - 2 * recipe.Water, machine.GetWaterLevel());
        Assert.Equal(500 - 2 * recipe.Beans, machine.GetBeansLevel());
    }

    [Fact]
    public void Brew_EmptyMachine_ThrowsException()
    {
        var machine = new CoffeeMachine();

        Assert.Throws<InvalidOperationException>(() => machine.Brew(RecipeName.Espresso));
    }

    // Капучино = 50 воды, 150 молока, 10 зёрен.
    // В каждой строке ровно одного ресурса не хватает на единицу.
    [Theory]
    [InlineData(49, 1000, 500)]   // мало воды
    [InlineData(1000, 149, 500)]  // мало молока
    [InlineData(1000, 1000, 9)]   // мало зёрен
    public void Brew_NotEnoughOneResource_ThrowsExceptionAndConsumesNothing(int water, int milk, int beans)
    {
        // Arrange
        var machine = CreateLoadedMachine(water, milk, beans);

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() => machine.Brew(RecipeName.Cappuccino));

        // Ничего не должно быть потрачено, даже ресурсы, которых хватало
        Assert.Equal(water, machine.GetWaterLevel());
        Assert.Equal(milk, machine.GetMilkLevel());
        Assert.Equal(beans, machine.GetBeansLevel());
    }

    [Fact]
    public void Brew_UnknownRecipe_ThrowsException()
    {
        var machine = CreateLoadedMachine();

        Assert.Throws<ArgumentException>(() => machine.Brew((RecipeName)999));
    }
}
