namespace Ecole;

public class Etudiant : IEquatable<Etudiant>
{
    private readonly List<double> _notes = [];
    private readonly List<int> _coefs = [];   // bonus exercice 6 : coefficient de chaque note
    private int _annee;

    public Etudiant(string nom, string prenom, int annee)
    {
        if (string.IsNullOrWhiteSpace(nom))
            throw new ArgumentException("Le nom ne peut pas être vide.", nameof(nom));
        if (string.IsNullOrWhiteSpace(prenom))
            throw new ArgumentException("Le prénom ne peut pas être vide.", nameof(prenom));

        Nom = nom;
        Prenom = prenom;
        Annee = annee;          // passe par le setter, donc par la validation
    }

    public string Nom { get; }
    public string Prenom { get; }
    public DateOnly Naissance { get; init; }

    public int Annee
    {
        get => _annee;
        set
        {
            if (value is < 1 or > 5)
                throw new ArgumentOutOfRangeException(nameof(value), value, "L'année doit être comprise entre 1 et 5.");
            _annee = value;
        }
    }

    public string NomComplet => $"{Prenom} {Nom.ToUpperInvariant()}";

    public int NbNotes => _notes.Count;

    /// <summary>Vue en lecture seule : l'appelant ne peut ni ajouter ni supprimer de note.</summary>
    public IReadOnlyList<double> Notes => _notes.AsReadOnly();

    /// <summary>L1, L2, L3, M1, M2.</summary>
    public string Niveau => Annee <= 3 ? $"L{Annee}" : $"M{Annee - 3}";

    public void AjouterNote(double n) => AjouterNote(n, 1);

    public void AjouterNote(double n, int coef)
    {
        if (double.IsNaN(n) || n < 0 || n > 20)
            throw new ArgumentOutOfRangeException(nameof(n), n, "Une note doit être comprise entre 0 et 20.");
        if (coef < 1)
            throw new ArgumentOutOfRangeException(nameof(coef), coef, "Le coefficient doit être au moins 1.");
        _notes.Add(n);
        _coefs.Add(coef);
    }

    public double Moyenne()
    {
        if (_notes.Count == 0)
            return 0;
        double somme = 0;
        foreach (double n in _notes)
            somme += n;
        return somme / _notes.Count;
    }

    /// <summary>Bonus exercice 6 : moyenne pondérée par les coefficients.</summary>
    public double MoyennePonderee()
    {
        double somme = 0;
        int totalCoefs = 0;
        for (int i = 0; i < _notes.Count; i++)
        {
            somme += _notes[i] * _coefs[i];
            totalCoefs += _coefs[i];
        }
        return totalCoefs == 0 ? 0 : somme / totalCoefs;
    }

    public override string ToString() => $"{NomComplet} ({Niveau}) : {Moyenne():F2}";

    // --- Pour aller plus loin : égalité de valeur (nom, prénom, date de naissance) ---
    public bool Equals(Etudiant? other) =>
        other is not null && Nom == other.Nom && Prenom == other.Prenom && Naissance == other.Naissance;

    public override bool Equals(object? obj) => Equals(obj as Etudiant);

    public override int GetHashCode() => HashCode.Combine(Nom, Prenom, Naissance);
}
