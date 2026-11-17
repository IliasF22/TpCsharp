using Ecole;

namespace TpCsharp.Exercices;

public static class Ex6Fichier
{
    public static void Executer(string chemin = "notes.csv")
    {
        Console.WriteLine("--- 6.1 Records ---");
        var n1 = new Note("Lovelace", "Ada", "Algo", 15, 3);
        var n2 = new Note("Lovelace", "Ada", "Algo", 15, 3);
        Console.WriteLine($"  n1 == n2 : {n1 == n2}");
        Console.WriteLine($"  {n1}");
        var n3 = n1 with { Matiere = "BD", Valeur = 18 };
        Console.WriteLine($"  copie avec with : {n3}  (n1 inchangé : {n1.Matiere})");

        var e1 = new Etudiant("Lovelace", "Ada", 4) { Naissance = new DateOnly(1815, 12, 10) };
        var e2 = new Etudiant("Lovelace", "Ada", 4) { Naissance = new DateOnly(1815, 12, 10) };
        Console.WriteLine($"  Etudiant e1 == e2 : {e1 == e2} (références)  | e1.Equals(e2) : {e1.Equals(e2)} (IEquatable, bonus)");
        HashSet<Etudiant> ensemble = [e1, e2];
        Console.WriteLine($"  HashSet<Etudiant> contenant e1 et e2 : {ensemble.Count} élément(s)");

        Console.WriteLine("--- 6.5 Fichier absent ---");
        LireEtAfficher("fichier-inexistant.csv");

        Console.WriteLine($"--- 6.4 / 6.6 Lecture de {chemin} ---");
        LireEtAfficher(chemin);
    }

    private static void LireEtAfficher(string chemin)
    {
        List<Note> notes;
        try
        {
            notes = LecteurNotes.LireFichier(chemin);
        }
        catch (FileNotFoundException)
        {
            Console.Error.WriteLine($"  Fichier introuvable : « {chemin} » (dossier courant : {Directory.GetCurrentDirectory()})");
            return;
        }

        var promo = new Promo("M1 Info");
        foreach (var note in notes)
        {
            var etudiant = promo.Chercher(note.Nom);
            if (etudiant is null)
            {
                etudiant = new Etudiant(note.Nom, note.Prenom, 4);
                promo.Inscrire(etudiant);
            }
            etudiant.AjouterNote(note.Valeur, note.Coef);
        }
        promo.AfficherReleve();
        promo.AfficherReleve(ponderee: true);
    }
}
