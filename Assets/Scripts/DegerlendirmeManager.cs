using UnityEngine;
using UnityEngine.UI;

public class DegerlendirmeManager : MonoBehaviour
{
    [Header("Görsel Referansları")]
    public Image kizGorseli; // Sahnedeki boş kız resmi objesi
    public Image elbiseGorseli; // Sahnedeki boş elbise resmi objesi

    [Header("Sprite Arşivi")]
    public Sprite[] kizSpriteleri; // Tüm kızların görsellerini buraya sürükle
    public Sprite[] elbiseSpriteleri; // Tüm elbiselerin görsellerini buraya sürükle

    void Start()
    {
        // Sahne başlar başlamaz, köprüdeki verilere bak ve doğru görselleri giydir!
        kizGorseli.sprite = kizSpriteleri[OyunVerileri.secilenKizID];
        
        // Eğer elbise seçildiyse onu da giydir
        elbiseGorseli.sprite = elbiseSpriteleri[OyunVerileri.secilenElbiseID];
        
        // Burada puanlama algoritmanı da çalıştırabilirsin
        PuanlamayiHesapla();
    }
    
    void PuanlamayiHesapla()
    {
        // Puan hesaplama kodları buraya gelecek...
    }
}