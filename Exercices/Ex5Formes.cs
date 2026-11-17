using Geometrie;
using Geometrie.Piege;

namespace TpCsharp.Exercices;

public static class Ex5Formes
{
    public static void Executer()
    {
        Console.WriteLine("--- 5.2 Polymorphisme ---");
        List<Forme> formes = [new Cercle(1), new Rectangle(2, 5), new Carre(3), new Cercle(2.5)];
        formes.Sort();                      // utilise CompareTo (aires croissantes)
        foreach (var f in formes)
            f.Dessiner();

        double aireTotale = 0;
        Forme? plusGrandPerimetre = null;
        foreach (var f in formes)
        {
            aireTotale += f.Aire();
            if (plusGrandPerimetre is null || f.Perimetre() > plusGrandPerimetre.Perimetre())
                plusGrandPerimetre = f;
        }
        Console.WriteLine($"  Aire totale : {aireTotale:F2}");
        Console.WriteLine($"  Plus grand périmètre : {plusGrandPerimetre?.Decrire()}");

        Console.WriteLine("--- 5.3 Le piège du virtual ---");
        Cercle c = new Cercle(1);
        Forme f2 = c;
        Console.WriteLine($"  override, via Cercle : {c.Decrire()}");
        Console.WriteLine($"  override, via Forme  : {f2.Decrire()}");

        CercleMasque cm = new CercleMasque();
        FormeNonVirtuelle fm = cm;
        Console.WriteLine($"  new, via Cercle : {cm.Decrire()}");
        Console.WriteLine($"  new, via Forme  : {fm.Decrire()}");

        Console.WriteLine("--- Pour aller plus loin : readonly struct Point2D ---");
        var p = new Point2D(1, 2);
        var q = p;                          // copie
        q = q.Deplacer(10, 10);             // pas de mutation possible : on obtient un nouveau point
        Console.WriteLine($"  p = {p}, q = {q}  -> l'original n'a pas bougé");
        var centre = new Cercle(2) { Centre = new Point2D(3, 4) };
        Console.WriteLine($"  Cercle centré en {centre.Centre}");
    }
}
