using UnityEngine;

[CreateAssetMenu(fileName = "YeniMankenVerisi", menuName = "Trend Tailor/Manken Verisi")]
public class MankenVerisi : ScriptableObject
{
    [Header("Manken Görseli")]
    public Sprite mankenSprite;

    [Header("UI Pozisyon ve Boyut Ayarları")]
    public Vector2 pozisyonOffset = Vector2.zero; 
    public Vector2 mankenBoyutu = Vector2.one; 

    [Header("Genel Bilgiler")]
    public string mankenAdi;
    public int mankenID;
}