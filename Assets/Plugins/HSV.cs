using UnityEngine;

/// <summary>
/// Represents a color with hue, saturation, and value.
/// </summary>
public readonly struct HSV
{
    public Color Color => Color.HSVToRGB(H / 360f, S, V);

    public readonly float H;
    public readonly float S;
    public readonly float V;

    public HSV(float H, float S, float V)
    {
        this.H = H;
        this.S = S;
        this.V = V;
    }

    public static HSV FromColor(Color color)
    {
        Color.RGBToHSV(color, out var H, out var S, out var V);
        return new HSV(H, S, V);
    }

    public static HSV FromHSL(HSL hsl)
    {
        // https://en.wikipedia.org/wiki/HSL_and_HSV#HSL_to_HSV
        var H = hsl.H;
        var V = hsl.L + hsl.S * Mathf.Min(hsl.L, 1 - hsl.L);
        var S = V > 0 ? 2 * (1 - hsl.L / V) : 0;
        return new HSV(H, S, V);
    }    
}