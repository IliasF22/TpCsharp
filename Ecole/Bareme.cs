namespace Ecole;

/// <summary>Barème des mentions (exercice 3.1), réutilisé par Promo.</summary>
public static class Bareme
{
    public static string Mention(double moyenne) => moyenne switch
    {
        double.NaN or < 0 or > 20 => throw new ArgumentOutOfRangeException(
            nameof(moyenne), moyenne, "La moyenne doit être comprise entre 0 et 20."),
        >= 16 => "Très bien",
        >= 14 => "Bien",
        >= 12 => "Assez bien",
        >= 10 => "Passable",
        _ => "Ajourné"
    };
}
