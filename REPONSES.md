# TP Introduction à C# — Réponses

> Environnement : seul le SDK **.NET 11** (11.0.100) est installé sur la machine, le projet vise donc `net11.0` au lieu de `net10.0`. Rien dans le TP ne dépend de cette différence.

## Lancer le projet

```bash
dotnet run -- tout     # exercices 2.1, 3, 4, 5, 6 (non interactifs)
dotnet run -- 1        # exercice 1 (interactif) ; idem 2..6
dotnet test Tp.slnx    # tests xUnit (Pour aller plus loin)
```

Le dossier contient à la fois `TpCsharp.csproj` et `Tp.slnx` : `dotnet build` seul demande de choisir, utiliser `dotnet build TpCsharp.csproj` ou `dotnet build Tp.slnx`.

---

## 1. Premier projet

**Q2 — Version et options du `.csproj`.** `<TargetFramework>net11.0</TargetFramework>` (net10.0 avec un SDK 10). `Nullable` et `ImplicitUsings` sont toutes les deux à `enable` :
- `Nullable` active l'analyse des références null (`string` ≠ `string?`) ;
- `ImplicitUsings` ajoute automatiquement les `using` courants (`System`, `System.IO`, `System.Collections.Generic`, `System.Linq`…).

**Q4 — Le `.dll`.** `bin/Debug/net11.0/` contient `TpCsharp.dll` (le code compilé en IL, langage intermédiaire), `TpCsharp` (petit exécutable lanceur natif), `.pdb` (symboles de débogage), `.deps.json` et `.runtimeconfig.json` (dépendances et version du runtime). Le `.dll` correspond au **`.class` / `.jar`** de Java : du bytecode portable exécuté par une machine virtuelle (le CLR avec compilation JIT, comme la JVM). `dotnet TpCsharp.dll` ≈ `java -jar app.jar`.

**Q5 — `dotnet run hello.cs`** (fichier dans `Hors-projet/`). Pas de `.csproj` : le SDK génère un projet implicite en coulisses, le compile et l'exécute. C'est idéal pour un script ou un essai rapide, mais on ne contrôle pas directement la configuration (dépendances, options) sauf via des directives `#:` en tête de fichier (`#:package`, `#:property`…), et ça ne remplace pas un projet pour du code sur plusieurs fichiers. Le premier lancement est plus lent (génération + compilation, mise en cache ensuite).

## 2. Types, chaînes et null

Sortie obtenue :
```
classe : c1.X = 9
struct : s1.X = 1
True
False
True
```

**Pourquoi `c1.X` et `s1.X` diffèrent ?** `PointC` est une *classe* (type référence) : `c2 = c1` copie la **référence**, les deux variables désignent le même objet, modifier `c2.X` modifie donc `c1.X` → 9. `PointS` est une *struct* (type valeur) : `s2 = s1` copie **toute la valeur**, `s2` est un point indépendant → `s1.X` reste 1.

**`a == b` en Java ?** En Java, `==` entre deux `String` compare les références : `"info"` (littéral internalisé) et le résultat d'une concaténation à l'exécution sont deux objets différents → `false` (il faut `equals`). En C#, `string` **surcharge l'opérateur `==`** pour comparer le contenu → `True`. Pour comparer les références en C# : `ReferenceEquals(a, b)` → `False` (affiché dans le programme).

**Suffixe `m` ?** Il désigne un littéral `decimal` (sinon `0.1` est un `double`). `double` est un flottant binaire : 0,1 n'y est pas représentable exactement, d'où `0.1 + 0.2 == 0.3` → `False`. `decimal` travaille en base 10 sur 128 bits : 0,1 est exact → `True`. Le choix est important dans tout programme **financier / comptable** (montants, factures, taux, banque) où les arrondis binaires sont inacceptables ; `double` convient au calcul scientifique, à la physique, aux graphismes (plus rapide, plus grande plage).

### 2.2 Le null

**Q1 — Avertissements** :
- `CS8600 : Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable` (sur `string saisie = Console.ReadLine();`)
- `CS8602 : Déréférencement d'une éventuelle référence null` (sur `saisie.Length`)

**Q2 — Pourquoi `ReadLine()` peut renvoyer `null` ?** Sa signature est `string? ReadLine()` : elle renvoie `null` quand il n'y a plus rien à lire, c'est-à-dire **fin de flux** — entrée standard fermée par Ctrl+D (Unix/macOS) ou Ctrl+Z (Windows), ou entrée redirigée depuis un fichier/pipe arrivé au bout. Le programme le vérifie : `printf 'salut\n' | dotnet run -- 2` affiche « Entrée standard fermée » à la 2ᵉ lecture.

**Q3 — Corrections** (`Exercices/Ex2Types.cs`) :
```csharp
string? saisie = Console.ReadLine();
Console.WriteLine($"Vous avez tapé {saisie?.Length ?? 0} caractères");

string? saisie2 = Console.ReadLine();
if (saisie2 is null) Console.WriteLine("Entrée standard fermée");
else Console.WriteLine($"Vous avez tapé {saisie2.Length} caractères"); // plus d'avertissement : le compilateur sait que saisie2 n'est pas null
```

**Q4 —** méthode `LireAge()` : boucle avec `int.TryParse(texte, out int age) && age is >= 0 and <= 120`, redemande sinon (renvoie `null` si l'entrée est fermée pour éviter une boucle infinie).

**Q5 — `TreatWarningsAsErrors`.** Tout avertissement devient une **erreur** : la compilation échoue (`error CS8602 …`), aucun `.dll` n'est produit, on ne peut pas lancer le programme tant que le problème de nullabilité n'est pas corrigé. Réglage conservé dans le `.csproj`.

## 3. Contrôle et collections

- **3.1** `Bareme.Mention` (`Ecole/Bareme.cs`) : switch expression avec motifs relationnels (`>= 16 => "Très bien"`, …, `< 0 or > 20 => throw new ArgumentOutOfRangeException(...)`).
- **3.2** `[12, 15.5, 9, 18]` → moyenne **13,62** (Assez bien), min 9, max 18.
- **3.3** Dictionnaire parcouru avec `foreach (var (nom, notes) in dico)` (déconstruction de `KeyValuePair`).
- **3.4 `dico["Inconnu"]`** lève une **`KeyNotFoundException`** (contrairement à Java où `map.get` renvoie `null`). Version sûre :
  ```csharp
  if (dico.TryGetValue("Inconnu", out var notes)) { ... } else { /* absent, pas d'exception */ }
  ```
- **3.5** `Decrire` testée sur 7 valeurs : `4`, `7`, `""`, `"bonjour"`, `List<double>`, `null`, `3.14`.

## 4. Classe Etudiant et propriétés

`Ecole/Etudiant.cs` et `Ecole/Promo.cs`. Sortie : `Ada LOVELACE (M1) : 15,50`.

Erreurs de compilation des lignes commentées :
- `ada.Nom = "Byron";` → **CS0200** : la propriété est en lecture seule ;
- `ada.Naissance = ...;` → **CS8852** : propriété `init` assignable seulement dans un initialiseur d'objet / constructeur.

**`ToString` est `virtual` dans `object` : quel mot-clé ?** `override`. Si on l'oublie, le compilateur émet **CS0114** (« masque le membre hérité `object.ToString()` ; ajoutez `override` … sinon `new` ») — erreur avec `TreatWarningsAsErrors`. Sans ce réglage, la méthode **masque** celle d'`object` au lieu de la redéfinir : `Console.WriteLine(ada)` (qui appelle `ToString()` via une référence `object`) afficherait `Ecole.Etudiant`, pas notre texte.

**Lire `ada._notes` ?** → `error CS0122: 'Etudiant._notes' est inaccessible en raison de son niveau de protection` (champ `private`).

**Pourquoi ne pas exposer `List<double>` en public ?** Le code appelant pourrait faire `Notes.Add(42)`, `Clear()`, `notes[0] = -5`… en contournant la validation de `AjouterNote` (bornes 0–20) : l'objet ne garantit plus ses invariants (encapsulation cassée). Proposition :
```csharp
public IReadOnlyList<double> Notes => _notes.AsReadOnly();
```
`AsReadOnly()` renvoie un wrapper : même un cast vers `List<double>` ne permet pas de modifier la liste.

**4.2 Q3 — retirer `public` devant `class Promo`.** Le programme **compile toujours** : la visibilité par défaut d'une classe de premier niveau est **`internal`** (visible dans tout l'assembly/projet). Cela poserait problème depuis **un autre assembly**, par exemple le projet de tests `TpCsharp.Tests` : il ne verrait plus `Promo` (erreur CS0122), sauf à ajouter `[assembly: InternalsVisibleTo("TpCsharp.Tests")]`. Aussi : une classe `public` ne peut pas exposer un membre public d'un type `internal` (CS0050/CS0051). → `Promo` est laissée `public`.

## 5. Héritage et interfaces

`Geometrie/` : `IDessinable`, `Forme` (abstraite, `IComparable<Forme>`), `Cercle`, `Rectangle`, `Carre` (sealed), `Point2D`.

**5.1 Q4 — dériver de `Carre` sealed :** `error CS0509: 'SuperCarre' : dérivation du type sealed 'Carre' impossible`.
(Remarque : `Carre` appelle `base(cote, cote, "Carré")` via un constructeur `protected` de `Rectangle` pour avoir le nom « Carré » ; avec un simple `base(cote, cote)` il s'afficherait « Rectangle ».)

**5.2 —** Oui, les formes sont triées par aire croissante :
```
Cercle : aire = 3,14, périmètre = 6,28, rayon = 1,00
Carré : aire = 9,00, périmètre = 12,00
Rectangle : aire = 10,00, périmètre = 14,00
Cercle : aire = 19,63, périmètre = 15,71, rayon = 2,50
```
Le texte de `Cercle` contient bien le rayon : l'appel `f.Dessiner()` → `Decrire()` est **virtuel**, c'est la version de `Cercle` qui s'exécute même à travers une référence `Forme`. Aire totale : **41,78** ; plus grand périmètre : **le cercle de rayon 2,5** (15,71).

**5.3 Q1 — `virtual` retiré :** `error CS0506: 'Cercle.Decrire()' : impossible de substituer le membre hérité 'Forme.Decrire()', car il n'est pas marqué comme virtual, abstract ou override`.

**5.3 Q3 — avec `new`** (reproduit dans `Geometrie/Piege.cs`), les deux lignes **ne** sont **pas** identiques :
```
c.Decrire()  -> version de Cercle (avec le rayon)
f.Decrire()  -> version de Forme (sans le rayon)
```
- `override` = **redéfinition** : liaison dynamique, la méthode appelée dépend du **type réel de l'objet** (via la table virtuelle). Même résultat via `Cercle` ou via `Forme`.
- `new` = **masquage** : deux méthodes sans lien qui portent le même nom ; la méthode appelée dépend du **type statique de la variable**, choisi à la compilation. Via `Forme` on obtient celle de `Forme`.
- **En Java**, toutes les méthodes d'instance (non `final`, non `static`, non `private`) sont virtuelles par défaut : `f.decrire()` appellerait la version de `Cercle`, les deux lignes seraient identiques (comportement d'`override`). Java n'a pas d'équivalent du masquage `new` pour les méthodes d'instance. C# fait le choix inverse : non virtuel par défaut, il faut `virtual` + `override` explicitement.

**Q4 —** `virtual`/`override` sont en place dans la vraie hiérarchie.

## 6. Records et exceptions

**Q1 — Records.** `n1 == n2` → **True** : un `record` génère une égalité **par valeur** (`Equals`, `GetHashCode`, `==`, `!=` comparent toutes les propriétés). `ToString()` généré : `Note { Nom = Lovelace, Prenom = Ada, Matiere = Algo, Valeur = 15, Coef = 3 }`. `n1 with { Matiere = "BD", Valeur = 18 }` crée une **copie modifiée**, `n1` reste inchangé (immuable).

**Deux `Etudiant` avec les mêmes données : `==` → False.** `Etudiant` est une classe ordinaire : `==` compare les **références**, et ce sont deux objets distincts en mémoire. (Avec le bonus `IEquatable<Etudiant>`, `e1.Equals(e2)` vaut `True` et le `HashSet` ne garde qu'un élément, mais `==` reste une comparaison de références tant qu'on ne surcharge pas `operator ==`.)

**Q2–Q5 —** `NoteInvalideException` (propriété `Ligne`), `LecteurNotes.LireLigne` (`Split(',')`, `double.TryParse(..., CultureInfo.InvariantCulture, ...)`, bornes 0–20), `LecteurNotes.LireFichier` (`using var lecteur = new StreamReader(...)`, en-tête sauté, erreurs sur `Console.Error`), `FileNotFoundException` attrapée dans `Ex6Fichier` (testée avec `fichier-inexistant.csv`).

**Résultat :**
```
[ignorée] ligne 5 : « abc » n'est pas un nombre
[ignorée] ligne 7 : la note 25 sort de l'intervalle [0, 20]
=== Relevé de la promo M1 Info (3 étudiant(s)) ===
  Ada LOVELACE         M1  1 note(s)   15,00  Bien
  Alan TURING          M1  2 note(s)   10,50  Passable
  Grace HOPPER         M1  1 note(s)   17,00  Très bien
```

**Bonus — moyenne pondérée.** Le calcul se place **dans `Etudiant`** (méthode `MoyennePonderee()`), car c'est lui qui possède les notes : on garde l'encapsulation. Il faut modifier `Etudiant` pour **mémoriser le coefficient de chaque note** : ajout d'un champ privé `_coefs` (parallèle à `_notes`) et d'une surcharge `AjouterNote(double n, int coef)` ; `AjouterNote(double n)` appelle `AjouterNote(n, 1)` pour rester compatible. (Alternative plus propre : stocker directement une `List<Note>` ou une liste de tuples `(Valeur, Coef)`.) Alan : (9×3 + 12×2) / 5 = **10,20**.

## Pour aller plus loin

- **xUnit** : `Tp.slnx` + `TpCsharp.Tests/` — 24 tests passent (`Etudiant` : moyenne sans note, moyenne de plusieurs notes, notes hors bornes, `NomComplet`, année, pondérée, égalité ; `[Theory]`/`[InlineData]` sur `Mention` ; `LecteurNotes`). Le `.csproj` principal exclut le dossier de tests (`<Compile Remove="TpCsharp.Tests/**" />`) puisqu'il est imbriqué dedans.
- **`readonly struct Point2D`** + propriété `Forme.Centre` : copier un point puis appeler `Deplacer` renvoie un nouveau point, l'original ne change jamais (aucun setter possible).
- **`IEquatable<Etudiant>`** : égalité sur nom, prénom, date de naissance ; `Equals(object?)` et `GetHashCode()` (`HashCode.Combine`) redéfinis ; un `HashSet<Etudiant>` contenant deux « mêmes » étudiants n'a qu'un élément.
