using UnityEngine;
/// <summary>
/// Represents a color with hue, saturation, and lightness.
/// </summary>
public readonly struct HSL
{
    public Color Color => HSV.FromHSL(this).Color;

    public readonly float H;
    public readonly float S;
    public readonly float L;

    public HSL(float H, float S, float L)
    {
        this.H = H;
        this.S = S;
        this.L = L;
    }

    public static HSL FromColor(Color color)
    {
        var hsv = HSV.FromColor(color);
        return FromHSV(hsv);
    }

    public static HSL FromHSV(HSV hsv)
    {
        // https://en.wikipedia.org/wiki/HSL_and_HSV#HSV_to_HSL
        var H = hsv.H;
        var L = hsv.V * (1 - hsv.S / 2);
        var S = L > 0 && L < 1 ? (hsv.V - L) / Mathf.Min(L, 1 - L) : 0;
        return new HSL(H, S, L);
    }
}