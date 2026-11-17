namespace Ecole;

public class NoteInvalideException : Exception
{
    public NoteInvalideException(string message, int ligne)
        : base($"ligne {ligne} : {message}")
    {
        Ligne = ligne;
    }

    public NoteInvalideException(string message, int ligne, Exception inner)
        : base($"ligne {ligne} : {message}", inner)
    {
        Ligne = ligne;
    }

    /// <summary>Numéro de la ligne fautive dans le fichier (l'en-tête est la ligne 1).</summary>
    public int Ligne { get; }
}
