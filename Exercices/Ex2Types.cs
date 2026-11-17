namespace TpCsharp.Exercices;

public static class Ex2Types
{
    // 2.1 Valeur ou référence
    public static void ValeurOuReference()
    {
        var c1 = new PointC { X = 1 };
        var c2 = c1;            // copie de la RÉFÉRENCE : c1 et c2 désignent le même objet
        c2.X = 9;
        Console.WriteLine($"classe : c1.X = {c1.X}");

        var s1 = new PointS { X = 1 };
        var s2 = s1;            // copie de la VALEUR : s2 est un second point indépendant
        s2.X = 9;
        Console.WriteLine($"struct : s1.X = {s1.X} (s2.X = {s2.X})");

        string a = "info";
        string b = string.Concat("in", "fo");
        Console.WriteLine(a == b);                       // True : == compare le contenu des string
        Console.WriteLine(ReferenceEquals(a, b));        // False : ce sont deux objets différents (le == de Java)

        Console.WriteLine(0.1 + 0.2 == 0.3);             // False : double est binaire, 0.1 n'est pas représentable
        Console.WriteLine(0.1m + 0.2m == 0.3m);          // True  : decimal est en base 10
    }

    // 2.2 Le null sous contrôle
    public static void NullSousControle()
    {
        // Version de départ (avertissements CS8600 puis CS8602, erreurs avec TreatWarningsAsErrors) :
        // string saisie = Console.ReadLine();
        // Console.WriteLine($"Vous avez tapé {saisie.Length} caractères");

        // Correction 1 : type nullable + ?. et ??
        Console.Write("Tapez un texte : ");
        string? saisie = Console.ReadLine();
        Console.WriteLine($"Vous avez tapé {saisie?.Length ?? 0} caractères");

        // Correction 2 : test is null (le compilateur sait ensuite que saisie2 n'est pas null)
        Console.Write("Tapez un autre texte : ");
        string? saisie2 = Console.ReadLine();
        if (saisie2 is null)
            Console.WriteLine("Entrée standard fermée : rien à compter.");
        else
            Console.WriteLine($"Vous avez tapé {saisie2.Length} caractères");

        int? age = LireAge();
        Console.WriteLine(age is null ? "Pas d'âge saisi." : $"Âge retenu : {age} ans");
    }

    /// <summary>Redemande tant que la saisie n'est pas un entier de 0 à 120 ; null si l'entrée est fermée.</summary>
    public static int? LireAge()
    {
        while (true)
        {
            Console.Write("Votre âge (0 à 120) : ");
            string? texte = Console.ReadLine();
            if (texte is null)
                return null;
            if (int.TryParse(texte, out int age) && age is >= 0 and <= 120)
                return age;
            Console.WriteLine("  Saisie invalide, recommencez.");
        }
    }
}

public class PointC { public int X; }
public struct PointS { public int X; }
