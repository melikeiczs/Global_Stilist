using UnityEngine;

[CreateAssetMenu(fileName = "YeniElbiseVerisi", menuName = "Trend Tailor/Elbise Verisi")]
public class ElbiseVerisi : ScriptableObject
{
    [Header("Görsel Ayarları")]
    public Sprite elbiseSprite;

    [Header("Kilit / Satın Alma Sistemi")]
    public bool isLocked = false;
    public int elbiseFiyati = 100;

    [Header("UI Pozisyon ve Boyut Ayarları")]
    public Vector2 pozisyonOffset = Vector2.zero; 
    public Vector2 elbiseBoyutu = Vector2.one; 

    [Header("Genel Bilgiler")]
    public string elbiseAdi;
    public int elbisePuani;

    [Header("Jüri / Değerlendirme Ayarları")]
    public string konsept; 
    public string zaman; // 🚀 FLOAT YERİNE STRING YAPTIK! Artık "Gece" yazabilirsin.
    public string renk; 
}