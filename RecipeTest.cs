namespace CoffeeMachine.Tests;

public class RecipeTest
{
    [Fact]
    public void RecipeCreated_PropertiesSet()
    {
        var recipe = new Recipe(water: 50, milk: 150, beans: 10);

        Assert.Equal(50, recipe.Water);
        Assert.Equal(150, recipe.Milk);
        Assert.Equal(10, recipe.Beans);
    }

    [Theory]
    [InlineData(-1, 0, 10)]
    [InlineData(50, -1, 10)]
    [InlineData(50, 0, 0)]
    [InlineData(50, 0, -5)]
    public void RecipeInvalidAmounts_ThrowsException(int water, int milk, int beans)
    {
        Assert.Throws<ArgumentException>(() => new Recipe(water, milk, beans));
    }
}
