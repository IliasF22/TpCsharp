namespace Geometrie;

/// <summary>Pour aller plus loin : un point immuable, copié par valeur.</summary>
public readonly struct Point2D(double x, double y)
{
    public double X { get; } = x;
    public double Y { get; } = y;

    public Point2D Deplacer(double dx, double dy) => new(X + dx, Y + dy);

    public override string ToString() => $"({X:F1} ; {Y:F1})";
}
