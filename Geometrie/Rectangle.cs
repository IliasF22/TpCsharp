namespace Geometrie;

public class Rectangle : Forme
{
    public Rectangle(double largeur, double hauteur) : this(largeur, hauteur, "Rectangle")
    {
    }

    // Permet à Carre de transmettre son propre nom à Forme.
    protected Rectangle(double largeur, double hauteur, string nom) : base(nom)
    {
        if (!(largeur > 0))
            throw new ArgumentOutOfRangeException(nameof(largeur), largeur, "La largeur doit être strictement positive.");
        if (!(hauteur > 0))
            throw new ArgumentOutOfRangeException(nameof(hauteur), hauteur, "La hauteur doit être strictement positive.");
        Largeur = largeur;
        Hauteur = hauteur;
    }

    public double Largeur { get; }
    public double Hauteur { get; }

    public override double Aire() => Largeur * Hauteur;
    public override double Perimetre() => 2 * (Largeur + Hauteur);
}
