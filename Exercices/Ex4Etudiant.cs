using Ecole;

namespace TpCsharp.Exercices;

public static class Ex4Etudiant
{
    public static void Executer()
    {
        Console.WriteLine("--- 4.1 Etudiant ---");
        var ada = new Etudiant("Lovelace", "Ada", 4)
        {
            Naissance = new DateOnly(1815, 12, 10)
        };
        ada.AjouterNote(15);
        ada.AjouterNote(16);
        Console.WriteLine(ada);                 // appelle ToString()
        ada.Annee = 5;                          // autorisé
        Console.WriteLine(ada);
        // ada.Nom = "Byron";                   // CS0200 : la propriété ou l'indexeur 'Etudiant.Nom' ne peut pas être assigné, car il est en lecture seule
        // ada.Naissance = new DateOnly(1816, 1, 1); // CS8852 : la propriété init-only 'Etudiant.Naissance' ne peut être assignée que dans un initialiseur d'objet...
        // Console.WriteLine(ada._notes);       // CS0122 : 'Etudiant._notes' est inaccessible en raison de son niveau de protection

        Console.WriteLine($"  Notes (IReadOnlyList) : {string.Join(" ; ", ada.Notes)} — NbNotes = {ada.NbNotes}");

        foreach (Action essai in (Action[])[() => ada.Annee = 6, () => ada.AjouterNote(25), () => _ = new Etudiant("", "X", 1)])
        {
            try { essai(); }
            catch (ArgumentException ex) { Console.WriteLine($"  refusé : {ex.GetType().Name} — {ex.Message.Split(" (")[0]}"); }
        }

        Console.WriteLine("--- 4.2 Promo ---");
        var promo = new Promo("M1 Info");
        Console.WriteLine($"  Major d'une promo vide : {promo.Major()?.NomComplet ?? "null"}");
        promo.Inscrire(ada);

        var alan = new Etudiant("Turing", "Alan", 4) { Naissance = new DateOnly(1912, 6, 23) };
        alan.AjouterNote(12);
        alan.AjouterNote(9.5);
        promo.Inscrire(alan);

        var grace = new Etudiant("Hopper", "Grace", 4) { Naissance = new DateOnly(1906, 12, 9) };
        grace.AjouterNote(17);
        grace.AjouterNote(18);
        promo.Inscrire(grace);

        Console.WriteLine($"  Chercher(\"turing\") : {promo.Chercher("turing")}");
        Console.WriteLine($"  Chercher(\"Babbage\") : {promo.Chercher("Babbage")?.ToString() ?? "null"}");
        promo.AfficherReleve();
    }
}
