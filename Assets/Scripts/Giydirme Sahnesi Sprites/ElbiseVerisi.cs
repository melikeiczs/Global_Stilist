using UnityEngine;

[CreateAssetMenu(fileName = "YeniElbiseVerisi", menuName = "Moda Oyunu/Elbise Verisi")]
public class ElbiseVerisi : ScriptableObject
{
    [Header("Kimlik Bilgileri")]
    public string elbiseAdi; 

    [Header("Manken Üzerindeki Ayarlar (Vector2)")]
    // Manken yöneticisinin .x ve .y olarak aradığı koordinat yapıları:
    public Vector2 pozisyonOffset; 
    public Vector2 elbiseBoyutu; 

    [Header("Görsel Ayarlar")]
    public Sprite elbiseButonResmi;
    public Sprite elbiseSprite; // MankenYonetici içindeki 'secilenElbise.elbiseSprite' ile eşitledik!

    [Header("Ekonomi Ayarları")]
    public int fiyat;
    public string paraTuru; // "Altin" veya "Elmas"

    [Header("Değerlendirme Etiketleri")]
    public ElbiseRengi renk;
    public ZamanAyari zaman;
    public ElbiseKonsepti konsept; 
}

public enum ElbiseRengi { Siyah, Mavi, Kirmizi, Beyaz, Pembe, Sari, Mor, Lacivert }
public enum ZamanAyari { Gunduz, Gece }
public enum ElbiseKonsepti { Gundelik, Parti, Ofis, Spor }