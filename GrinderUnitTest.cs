namespace CoffeeMachine.Tests;

public class GrinderUnitTest
{
    [Fact]
    public void Grind_ReturnsGroundCoffee()
    {
        var grinder = new GrinderUnit();

        var result = grinder.Grind(10);

        Assert.NotNull(result);
    }

    [Fact]
    public void Grind_QuantityIsPreserved()
    {
        var grinder = new GrinderUnit();

        var result = grinder.Grind(10);

        Assert.Equal(10, result.Quantity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Grind_NonPositiveQuantity_ThrowsException(int quantity)
    {
        var grinder = new GrinderUnit();

        Assert.Throws<ArgumentException>(() => grinder.Grind(quantity));
    }
}
