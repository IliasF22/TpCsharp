namespace Geometrie.Piege;

// Reproduction de l'exercice 5.3 sans casser la vraie hiérarchie :
// Decrire() n'est PAS virtual dans la classe de base, et la classe dérivée la MASQUE avec new.

public class FormeNonVirtuelle
{
    public string Decrire() => "Decrire() de la classe de base (Forme)";
}

public class CercleMasque : FormeNonVirtuelle
{
    // Avec override ici : error CS0506 (le membre hérité n'est pas virtual/abstract/override).
    // Sans new ni override : warning CS0108 (le membre masque le membre hérité ; utilisez new).
    public new string Decrire() => "Decrire() de Cercle (masquage avec new)";
}
