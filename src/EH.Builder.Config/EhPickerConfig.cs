using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Config;
public class EhPickerConfig
{
    public DkProperty<Color> BackgroundColor   { get; }      = new(new Color32(30, 30, 30, 255));
    public float             Width             { get; set; } = 22;
    public float             Height            { get; set; } = 22;
    public float             ModalWindowHeight { get; set; } = 200;
    public float             ModalWindowWidth  { get; set; } = 200;
    public float             ModalWindowBorder { get; set; } = 5;
    public float             Border            { get; set; } = 90;
    public float             HuePickerBorder   { get; set; } = 5;
    public float             AlphaPickerBorder { get; set; } = 0.05f;
    public float             MainPickerBorder  { get; set; } = 0.05f;
    public float             PickerOffset      { get; set; } = 10;
}