using System.Globalization;
using TpCsharp.Exercices;

// Affichage à la française (virgule décimale : « 15,50 »)
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");

string? choix = args.Length > 0 ? args[0] : null;
if (choix is null)
{
    Console.WriteLine("""
        TP Introduction à C# — exercices disponibles :
          1   Premier projet (prénom, âge)            [interactif]
          2   Types valeur/référence, null, TryParse   [interactif]
          3   Contrôle et collections
          4   Classe Etudiant et Promo
          5   Héritage et interfaces : les formes
          6   Records et exceptions : fichier de notes
          tout  exercices 2.1, 3, 4, 5 et 6 (non interactifs)
        """);
    Console.Write("Votre choix : ");
    choix = Console.ReadLine();
}

switch (choix?.Trim())
{
    case "1":
        Ex1PremierProjet.Executer();
        break;
    case "2":
        Ex2Types.ValeurOuReference();
        Ex2Types.NullSousControle();
        break;
    case "3":
        Ex3Collections.Executer();
        break;
    case "4":
        Ex4Etudiant.Executer();
        break;
    case "5":
        Ex5Formes.Executer();
        break;
    case "6":
        Ex6Fichier.Executer();
        break;
    case "tout":
        Console.WriteLine("===== Exercice 2.1 ====="); Ex2Types.ValeurOuReference();
        Console.WriteLine("===== Exercice 3 =====");   Ex3Collections.Executer();
        Console.WriteLine("===== Exercice 4 =====");   Ex4Etudiant.Executer();
        Console.WriteLine("===== Exercice 5 =====");   Ex5Formes.Executer();
        Console.WriteLine("===== Exercice 6 =====");   Ex6Fichier.Executer();
        break;
    default:
        Console.Error.WriteLine($"Choix inconnu : « {choix} ». Usage : dotnet run -- [1-6|tout]");
        break;
}
