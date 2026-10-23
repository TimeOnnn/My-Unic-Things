namespace CoffeeMachine.Tests;

public class BrewingUnitTest
{
    [Fact]
    public void Brew_ReturnsCoffee()
    {
        var brewingUnit = new BrewingUnit();

        var result = brewingUnit.Brew(new GroundCoffee(10), RecipeName.Espresso);

        Assert.NotNull(result);
    }

    [Theory]
    [InlineData(RecipeName.Espresso)]
    [InlineData(RecipeName.Filter)]
    [InlineData(RecipeName.Cappuccino)]
    public void Brew_CoffeeHasRequestedRecipe(RecipeName recipe)
    {
        var brewingUnit = new BrewingUnit();

        var result = brewingUnit.Brew(new GroundCoffee(10), recipe);

        Assert.Equal(recipe, result.Recipe);
    }

    [Fact]
    public void Brew_NullGroundCoffee_ThrowsException()
    {
        var brewingUnit = new BrewingUnit();

        Assert.Throws<ArgumentNullException>(() => brewingUnit.Brew(null!, RecipeName.Espresso));
    }
}
