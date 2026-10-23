namespace CoffeeMachine;

/// <summary>Готовый напиток - результат работы модуля варки.</summary>
public class Coffee
{
    public RecipeName Recipe { get; }

    public Coffee(RecipeName recipe)
    {
        Recipe = recipe;
    }
}
