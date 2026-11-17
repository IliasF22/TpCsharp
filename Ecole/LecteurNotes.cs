using System.Globalization;

namespace Ecole;

public static class LecteurNotes
{
    public static Note LireLigne(string ligne, int numero)
    {
        string[] champs = ligne.Split(',');
        if (champs.Length != 5)
            throw new NoteInvalideException($"5 champs attendus, {champs.Length} trouvé(s)", numero);

        if (!double.TryParse(champs[3], NumberStyles.Float, CultureInfo.InvariantCulture, out double valeur))
            throw new NoteInvalideException($"« {champs[3]} » n'est pas un nombre", numero);

        if (valeur is < 0 or > 20)
            throw new NoteInvalideException($"la note {valeur} sort de l'intervalle [0, 20]", numero);

        if (!int.TryParse(champs[4], NumberStyles.Integer, CultureInfo.InvariantCulture, out int coef) || coef < 1)
            throw new NoteInvalideException($"coefficient « {champs[4]} » invalide", numero);

        return new Note(champs[0].Trim(), champs[1].Trim(), champs[2].Trim(), valeur, coef);
    }

    /// <summary>Lit le fichier en ignorant l'en-tête ; les lignes invalides sont signalées sur Console.Error.</summary>
    /// <exception cref="FileNotFoundException">si le fichier n'existe pas.</exception>
    public static List<Note> LireFichier(string chemin)
    {
        List<Note> notes = [];
        using var lecteur = new StreamReader(chemin);

        lecteur.ReadLine();             // en-tête
        int numero = 1;
        string? ligne;
        while ((ligne = lecteur.ReadLine()) is not null)
        {
            numero++;
            if (string.IsNullOrWhiteSpace(ligne))
                continue;
            try
            {
                notes.Add(LireLigne(ligne, numero));
            }
            catch (NoteInvalideException ex)
            {
                Console.Error.WriteLine($"[ignorée] {ex.Message}");
            }
        }
        return notes;
    }
}
