using Ecole;

namespace TpCsharp.Tests;

public class MentionTests
{
    [Theory]
    [InlineData(20, "Très bien")]
    [InlineData(16, "Très bien")]
    [InlineData(15.99, "Bien")]
    [InlineData(14, "Bien")]
    [InlineData(12, "Assez bien")]
    [InlineData(10, "Passable")]
    [InlineData(9.99, "Ajourné")]
    [InlineData(0, "Ajourné")]
    public void Mention_SelonLaMoyenne(double moyenne, string attendue)
    {
        Assert.Equal(attendue, Bareme.Mention(moyenne));
    }

    [Theory]
    [InlineData(-0.5)]
    [InlineData(20.01)]
    public void Mention_HorsBornes_LeveException(double moyenne)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Bareme.Mention(moyenne));
    }
}
