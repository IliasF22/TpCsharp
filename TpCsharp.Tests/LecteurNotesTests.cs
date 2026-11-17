using Ecole;

namespace TpCsharp.Tests;

public class LecteurNotesTests
{
    [Fact]
    public void LireLigne_Valide_RenvoieUneNote()
    {
        var note = LecteurNotes.LireLigne("Lovelace,Ada,Algo,15.5,3", 2);
        Assert.Equal(new Note("Lovelace", "Ada", "Algo", 15.5, 3), note);
    }

    [Theory]
    [InlineData("Lovelace,Ada,BD,abc,2")]
    [InlineData("Hopper,Grace,Algo,25,3")]
    [InlineData("Hopper,Grace,Algo")]
    public void LireLigne_Invalide_LeveNoteInvalideAvecLeNumero(string ligne)
    {
        var ex = Assert.Throws<NoteInvalideException>(() => LecteurNotes.LireLigne(ligne, 7));
        Assert.Equal(7, ex.Ligne);
    }
}
