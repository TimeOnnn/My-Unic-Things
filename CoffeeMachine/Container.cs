namespace CoffeeMachine;

/// <summary>Контейнер для ресурса (зёрна, вода, молоко) с ограниченной ёмкостью.</summary>
public class Container
{
    private const int MinCapacity = 1;
    private const int MaxCapacity = 3000;

    /// <summary>Предельное количество ресурса, которое вмещает контейнер.</summary>
    public int Capacity { get; }

    /// <summary>Сколько ресурса лежит в контейнере сейчас (от 0 до Capacity).</summary>
    public int Value { get; private set; }

    public Container(int capacity)
    {
        if (capacity < MinCapacity || capacity > MaxCapacity)
            throw new ArgumentException(
                $"Ёмкость должна быть от {MinCapacity} до {MaxCapacity}.", nameof(capacity));

        Capacity = capacity;
    }

    /// <summary>Загрузить ресурс. Нельзя загрузить больше, чем свободно места.</summary>
    public void LoadResource(int value)
    {
        if (value < 0)
            throw new ArgumentException("Нельзя загрузить отрицательное количество.", nameof(value));

        // Capacity - Value = свободное место (так записано, чтобы не было переполнения int)
        if (value > Capacity - Value)
            throw new InvalidOperationException(
                $"Недостаточно места в контейнере: свободно {Capacity - Value}, пытаемся загрузить {value}.");

        Value += value;
    }

    /// <summary>Забрать ресурс. Нельзя забрать больше, чем есть. Возвращает забранное количество.</summary>
    public int GetResource(int value)
    {
        if (value < 0)
            throw new ArgumentException("Нельзя забрать отрицательное количество.", nameof(value));

        if (value > Value)
            throw new InvalidOperationException(
                $"Недостаточно ресурса в контейнере: есть {Value}, пытаемся забрать {value}.");

        Value -= value;
        return value;
    }
}
