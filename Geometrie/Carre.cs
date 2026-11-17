namespace Geometrie;

public sealed class Carre : Rectangle
{
    public Carre(double cote) : base(cote, cote, "Carré")
    {
    }

    public double Cote => Largeur;
}

// Essai demandé en 5.1.4 — décommenter provoque :
// error CS0509: 'SuperCarre' : impossible de dériver du type sealed 'Carre'
// public class SuperCarre : Carre { public SuperCarre() : base(1) { } }
