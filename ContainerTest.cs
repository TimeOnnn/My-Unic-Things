namespace CoffeeMachine.Tests;

public class ContainerTest
{
    // ---------- Требование 1: ёмкость ----------

    [Fact]
    public void ContainerCapacitySet_Success()
    {
        // Arrange
        const int expectedCapacity = 1000;
        var container = new Container(expectedCapacity);

        // Act
        var actualCapacity = container.Capacity;

        // Assert
        Assert.Equal(expectedCapacity, actualCapacity);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3000)]
    public void ContainerBoundaryCapacity_Success(int capacity)
    {
        var container = new Container(capacity);

        Assert.Equal(capacity, container.Capacity);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(3001)]
    public void ContainerInvalidCapacity_ThrowsException(int capacity)
    {
        // Act + Assert
        Assert.Throws<ArgumentException>(() => new Container(capacity));
    }

    // ---------- Требование 2: текущее значение ----------

    [Fact]
    public void ContainerCreated_ValueIsZero()
    {
        var container = new Container(1000);

        Assert.Equal(0, container.Value);
    }

    [Fact]
    public void LoadResource_ValueIncreases()
    {
        var container = new Container(1000);

        container.LoadResource(300);

        Assert.Equal(300, container.Value);
    }

    [Fact]
    public void LoadResourceTwice_ValuesAccumulate()
    {
        var container = new Container(1000);

        container.LoadResource(300);
        container.LoadResource(200);

        Assert.Equal(500, container.Value);
    }

    [Fact]
    public void GetResource_ReturnsTakenAmountAndValueDecreases()
    {
        var container = new Container(1000);
        container.LoadResource(500);

        var taken = container.GetResource(200);

        Assert.Equal(200, taken);
        Assert.Equal(300, container.Value);
    }

    [Fact]
    public void GetResource_AllContent_ValueIsZero()
    {
        var container = new Container(1000);
        container.LoadResource(500);

        container.GetResource(500);

        Assert.Equal(0, container.Value);
    }

    // ---------- Требование 3: нельзя загрузить больше ёмкости ----------

    [Fact]
    public void LoadResource_UpToCapacity_Success()
    {
        var container = new Container(1000);

        container.LoadResource(1000);

        Assert.Equal(1000, container.Value);
    }

    [Theory]
    [InlineData(1001)]
    [InlineData(5000)]
    public void LoadResource_MoreThanCapacity_ThrowsException(int amount)
    {
        var container = new Container(1000);

        Assert.Throws<InvalidOperationException>(() => container.LoadResource(amount));
    }

    [Fact]
    public void LoadResource_MoreThanRemainingSpace_ThrowsException()
    {
        var container = new Container(1000);
        container.LoadResource(700);

        Assert.Throws<InvalidOperationException>(() => container.LoadResource(400));
    }

    [Fact]
    public void LoadResource_Overflow_ValueNotChanged()
    {
        var container = new Container(1000);
        container.LoadResource(700);

        Assert.Throws<InvalidOperationException>(() => container.LoadResource(400));

        Assert.Equal(700, container.Value);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void LoadResource_NegativeAmount_ThrowsException(int amount)
    {
        var container = new Container(1000);

        Assert.Throws<ArgumentException>(() => container.LoadResource(amount));
    }

    // ---------- Требование 4: нельзя забрать больше имеющегося ----------

    [Theory]
    [InlineData(501)]
    [InlineData(1000)]
    public void GetResource_MoreThanAvailable_ThrowsException(int amount)
    {
        var container = new Container(1000);
        container.LoadResource(500);

        Assert.Throws<InvalidOperationException>(() => container.GetResource(amount));
    }

    [Fact]
    public void GetResource_FromEmptyContainer_ThrowsException()
    {
        var container = new Container(1000);

        Assert.Throws<InvalidOperationException>(() => container.GetResource(1));
    }

    [Fact]
    public void GetResource_NotEnough_ValueNotChanged()
    {
        var container = new Container(1000);
        container.LoadResource(500);

        Assert.Throws<InvalidOperationException>(() => container.GetResource(501));

        Assert.Equal(500, container.Value);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void GetResource_NegativeAmount_ThrowsException(int amount)
    {
        var container = new Container(1000);
        container.LoadResource(500);

        Assert.Throws<ArgumentException>(() => container.GetResource(amount));
    }
}
