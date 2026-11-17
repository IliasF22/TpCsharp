using Ecole;

namespace TpCsharp.Exercices;

public static class Ex3Collections
{
    public static void Executer()
    {
        // 3.1 — la méthode Mention est dans Ecole/Bareme.cs (réutilisée par Promo)
        Console.WriteLine("--- 3.1 Mention ---");
        foreach (double m in (double[])[17, 14, 12.5, 10, 6])
            Console.WriteLine($"  {m,5} -> {Bareme.Mention(m)}");
        try
        {
            Bareme.Mention(21);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"  21 -> exception : {ex.GetType().Name}");
        }

        Console.WriteLine("--- 3.2 Liste de notes ---");
        StatistiquesListe();

        Console.WriteLine("--- 3.3 / 3.4 Dictionnaire ---");
        Dictionnaire();

        Console.WriteLine("--- 3.5 Filtrage par motif ---");
        object?[] valeurs = [4, 7, "", "bonjour", new List<double> { 1, 2, 3 }, null, 3.14];
        foreach (var v in valeurs)
            Console.WriteLine($"  {v?.GetType().Name ?? "(null)",-8} {(v is System.Collections.IList ? "[1, 2, 3]" : v),-10} -> {Decrire(v)}");
    }

    private static void StatistiquesListe()
    {
        List<double> notes = [12, 15.5, 9, 18];
        double somme = 0, min = double.MaxValue, max = double.MinValue;
        foreach (double n in notes)
        {
            somme += n;
            if (n < min) min = n;
            if (n > max) max = n;
        }
        double moyenne = somme / notes.Count;
        Console.WriteLine($"  Moyenne : {moyenne:F2} ({Bareme.Mention(moyenne)}), min : {min}, max : {max}");
    }

    private static void Dictionnaire()
    {
        Dictionary<string, List<double>> dico = new()
        {
            ["Ada"] = [15, 16, 18],
            ["Alan"] = [9, 12],
            ["Grace"] = [17, 14.5],
        };
        dico["Alan"].Add(13);          // ajout d'une note à un étudiant existant

        foreach (var (nom, notes) in dico)
        {
            double moyenne = MoyenneDe(notes);
            Console.WriteLine($"  {nom,-6} {notes.Count} note(s)  moyenne {moyenne:F2}  {Bareme.Mention(moyenne)}");
        }

        // 3.4 : l'indexeur lève KeyNotFoundException si la clé est absente
        try
        {
            _ = dico["Inconnu"];
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"  dico[\"Inconnu\"] -> {ex.GetType().Name}");
        }

        if (dico.TryGetValue("Inconnu", out var notesInconnu))
            Console.WriteLine($"  Inconnu a {notesInconnu.Count} note(s)");
        else
            Console.WriteLine("  TryGetValue : « Inconnu » n'est pas dans le dictionnaire (pas d'exception)");
    }

    private static double MoyenneDe(List<double> notes)
    {
        if (notes.Count == 0) return 0;
        double somme = 0;
        foreach (double n in notes) somme += n;
        return somme / notes.Count;
    }

    public static string Decrire(object? o) => o switch
    {
        null => "null",
        int n when n % 2 == 0 => "entier pair",
        int => "entier impair",
        string { Length: 0 } => "chaîne vide",
        string s => $"chaîne de {s.Length} caractères",
        List<double> l => $"liste de {l.Count} éléments",
        _ => "autre"
    };
}
