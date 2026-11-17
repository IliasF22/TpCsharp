namespace Geometrie;

public abstract class Forme : IDessinable, IComparable<Forme>
{
    protected Forme(string nom)
    {
        Nom = nom;
    }

    public string Nom { get; }

    /// <summary>Pour aller plus loin : centre de la forme (origine par défaut).</summary>
    public Point2D Centre { get; init; }

    public abstract double Aire();
    public abstract double Perimetre();

    public virtual string Decrire() => $"{Nom} : aire = {Aire():F2}, périmètre = {Perimetre():F2}";

    public int CompareTo(Forme? other) => other is null ? 1 : Aire().CompareTo(other.Aire());

    public void Dessiner() => Console.WriteLine(Decrire());
}
