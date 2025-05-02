using OG.DataTypes.FontStyle;
namespace OG.DataTypes.Font.Abstraction;
public interface IOgFont
{
    string Name       { get; }
    float  LineHeight { get; }
    int    FontSize   { get; }
    void Prepare(string text, int fontSize, EOgFontStyle fontStyle);
    bool HasCharacter(char character);
    float GetCharacterAdvance(char character);
    void Push();
}