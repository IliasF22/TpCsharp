namespace Ecole;

public class Promo
{
    private readonly List<Etudiant> _etudiants = [];

    public Promo(string nom)
    {
        if (string.IsNullOrWhiteSpace(nom))
            throw new ArgumentException("Le nom de la promo ne peut pas être vide.", nameof(nom));
        Nom = nom;
    }

    public string Nom { get; }
    public int Effectif => _etudiants.Count;

    public void Inscrire(Etudiant e)
    {
        ArgumentNullException.ThrowIfNull(e);
        _etudiants.Add(e);
    }

    public Etudiant? Chercher(string nom)
    {
        foreach (var e in _etudiants)
            if (string.Equals(e.Nom, nom, StringComparison.OrdinalIgnoreCase))
                return e;
        return null;
    }

    public Etudiant? Major()
    {
        Etudiant? major = null;
        foreach (var e in _etudiants)
            if (major is null || e.Moyenne() > major.Moyenne())
                major = e;
        return major;
    }

    public double MoyenneGenerale()
    {
        if (_etudiants.Count == 0)
            return 0;
        double somme = 0;
        foreach (var e in _etudiants)
            somme += e.Moyenne();
        return somme / _etudiants.Count;
    }

    public void AfficherReleve(bool ponderee = false)
    {
        Console.WriteLine($"=== Relevé de la promo {Nom} ({Effectif} étudiant(s)){(ponderee ? " — moyennes pondérées" : "")} ===");
        foreach (var e in _etudiants)
        {
            double moyenne = ponderee ? e.MoyennePonderee() : e.Moyenne();
            Console.WriteLine($"  {e.NomComplet,-20} {e.Niveau}  {e.NbNotes} note(s)  {moyenne,6:F2}  {Bareme.Mention(moyenne)}");
        }
        Console.WriteLine($"  Moyenne générale : {MoyenneGenerale():F2}");
        Console.WriteLine($"  Major : {Major()?.NomComplet ?? "aucun"}");
    }
}
