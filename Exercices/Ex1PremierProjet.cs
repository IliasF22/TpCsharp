namespace TpCsharp.Exercices;

public static class Ex1PremierProjet
{
    public static void Executer()
    {
        Console.Write("Votre prénom : ");
        string prenom = Console.ReadLine() ?? "inconnu";

        Console.Write("Votre année de naissance : ");
        int annee = int.Parse(Console.ReadLine() ?? "");

        int age = DateTime.Today.Year - annee;
        Console.WriteLine($"Bonjour {prenom}, vous avez {age} ans (ou les aurez cette année).");
    }
}
