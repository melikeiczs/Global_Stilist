using UnityEngine;
using UnityEngine.UI; // Eski Text bileşeni için
using TMPro;        // TextMeshPro bileşeni için

public class ParaSistemi : MonoBehaviour
{
    [Header("Gardirop Manager Bağlantısı")]
    [Tooltip("Hiyerarşideki GardiropManager objesini buraya sürükleyin.")]
    public GardiropManager gardiropManager;

    [Header("Para ve Elmas Ayarları")]
    public int toplamPara = 1000;  
    public int toplamElmas = 10;   

    [Header("UI TextMeshPro Bileşenleri")]
    public TextMeshProUGUI paraYazisiTMP; 
    public TextMeshProUGUI elmasYazisiTMP; 

    [Header("UI Normal Text Bileşenleri")]
    public Text paraYazisiNormal;
    public Text elmasYazisiNormal;

    void Start()
    {
        ArayuzuGuncelle();
    }

    /// <summary>
    /// Satın alma butonunun tetikleyeceği ana fonksiyon
    /// </summary>
    public void KilidiAc()
    {
        // Elbisenin fiyat ayarları (İleride bunu elbiseye göre de dinamik yapabiliriz)
        int altinFiyati = 100; 
        int elmasFiyati = 2; 

        // 1. KONTROL: Oyuncunun parası ve elması yetiyor mu?
        if (toplamPara >= altinFiyati && toplamElmas >= elmasFiyati)
        {
            // 2. İŞLEM: Parayı ve elması düşür
            toplamPara -= altinFiyati;
            toplamElmas -= elmasFiyati;
            
            // 3. İŞLEM: Ekrandaki yazıları güncelle
            ArayuzuGuncelle(); 
            Debug.Log($"[PARA SİSTEMİ] Satın alma başarılı! Kalan Altın: {toplamPara} | Elmas: {toplamElmas}");

            // 4. İŞLEM: GardiropManager'a kilidi açtır ve paneli kapattır!
            if (gardiropManager != null)
            {
                gardiropManager.KilidiAcVeSatinAl();
            }
            else
            {
                Debug.LogWarning("[PARA SİSTEMİ] GardiropManager referansı eksik! Panel kapatılamadı.");
            }
        }
        else
        {
            Debug.LogWarning("[PARA SİSTEMİ] Yetersiz altın veya elmas! Satın alma iptal edildi.");
            // İstersen buraya paranın yetmediğini belirten görsel bir uyarı kodu ekleyebilirsin.
        }
    }

    void ArayuzuGuncelle()
    {
        if (paraYazisiTMP != null) paraYazisiTMP.text = toplamPara.ToString();
        if (elmasYazisiTMP != null) elmasYazisiTMP.text = toplamElmas.ToString();

        if (paraYazisiNormal != null) paraYazisiNormal.text = toplamPara.ToString();
        if (elmasYazisiNormal != null) elmasYazisiNormal.text = toplamElmas.ToString();
    }
}