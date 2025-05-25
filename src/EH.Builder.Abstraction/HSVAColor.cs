using UnityEngine;
namespace EH.Builder.Abstraction;
// ReSharper disable once InconsistentNaming
public struct HSVAColor(float h, float s, float v, float a)
{
    public float H { get; set; } = h;
    public float S { get; set; } = s;
    public float V { get; set; } = v;
    public float A { get; set; } = a;
    public void Clamp()
    {
        H = Mathf.Clamp01(H);
        S = Mathf.Clamp01(S);
        V = Mathf.Clamp01(V);
        A = Mathf.Clamp01(A);
    }
    public static explicit operator Color(HSVAColor color)
    {
        Color newColor = Color.HSVToRGB(color.H, color.S, color.V);
        newColor.a = color.A;
        return newColor;
    }
    public static explicit operator HSVAColor(Color color)
    {
        Color.RGBToHSV(color, out float h, out float s, out float v);
        return new(h, s, v, color.a);
    }
}