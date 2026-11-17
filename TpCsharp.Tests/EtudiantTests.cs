using Ecole;

namespace TpCsharp.Tests;

public class EtudiantTests
{
    private static Etudiant NouvelEtudiant() => new("Lovelace", "Ada", 4);

    [Fact]
    public void Moyenne_SansNote_VautZero()
    {
        Assert.Equal(0, NouvelEtudiant().Moyenne());
    }

    [Fact]
    public void Moyenne_PlusieursNotes_EstLaMoyenneArithmetique()
    {
        var e = NouvelEtudiant();
        e.AjouterNote(12);
        e.AjouterNote(15);
        e.AjouterNote(18);
        Assert.Equal(15, e.Moyenne(), precision: 10);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(20.5)]
    [InlineData(double.NaN)]
    public void AjouterNote_HorsBornes_LeveException(double note)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => NouvelEtudiant().AjouterNote(note));
    }

    [Fact]
    public void NomComplet_PrenomPuisNomEnMajuscules()
    {
        Assert.Equal("Ada LOVELACE", NouvelEtudiant().NomComplet);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    public void Annee_HorsBornes_LeveException(int annee)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Etudiant("Turing", "Alan", annee));
    }

    [Fact]
    public void MoyennePonderee_TientCompteDesCoefficients()
    {
        var e = NouvelEtudiant();
        e.AjouterNote(9, 3);
        e.AjouterNote(12, 2);
        Assert.Equal(10.2, e.MoyennePonderee(), precision: 10);
    }

    [Fact]
    public void Equals_MemesNomPrenomNaissance_UnSeulElementDansHashSet()
    {
        var a = new Etudiant("Lovelace", "Ada", 4) { Naissance = new DateOnly(1815, 12, 10) };
        var b = new Etudiant("Lovelace", "Ada", 5) { Naissance = new DateOnly(1815, 12, 10) };
        Assert.True(a.Equals(b));
        Assert.False(a == b);   // == reste une comparaison de références
        Assert.Single(new HashSet<Etudiant> { a, b });
    }
}
