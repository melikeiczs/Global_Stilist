using UnityEngine;
using TMPro; // Yazıları güncellemek için şart

public class GardiropManager : MonoBehaviour
{
    [Header("Manken Bağlantısı")]
    public MankenYonetici mankenYoneticisi; 

    [Header("Satın Alma / Kilit Paneli")]
    public GameObject satinAlmaPaneli; 
    public TextMeshProUGUI satinAlmaAciklamaText; // 🚨 YENİ: "Bu elbiseyi almak ister misiniz?" yazacak olan yer

    [Header("Ekonomi Sistemi")]
    public int oyuncuParasi = 1000; 
    public int elbiseFiyati = 180; 
    public TextMeshProUGUI paraText; 

    [Header("Aynı Sahne Panel Yönetimi")]
    public GameObject gardirobPaneli; 
    public GameObject degerlendirmePaneli;

    private ElbiseVerisi secilenKilitliElbise;

    void Start()
    {
        ParaYazisiniGuncelle();
    }

    public void ElbiseSec(ElbiseVerisi secilenElbise)
    {
        if (secilenElbise == null) return;

        // Kilit Kontrolü
        if (secilenElbise.isLocked)
        {
            secilenKilitliElbise = secilenElbise; 
            
            // 🚀 SİHİRLİ DOKUNUŞ: Satın alma panelindeki yazıyı tıklanan elbiseye göre güncelliyoruz
            if (satinAlmaAciklamaText != null)
            {
                satinAlmaAciklamaText.text = $"Kıyafeti {elbiseFiyati} altın karşılığında açmak ister misiniz?";
            }

            if (satinAlmaPaneli != null) satinAlmaPaneli.SetActive(true);
            return; 
        }

        // Kıyafet Giydir
        if (mankenYoneticisi != null) mankenYoneticisi.KiyafetGiy(secilenElbise);
    }

    public void KilidiAcVeSatinAl()
    {
        if (secilenKilitliElbise != null)
        {
            if (oyuncuParasi >= elbiseFiyati)
            {
                oyuncuParasi -= elbiseFiyati; 
                ParaYazisiniGuncelle(); 

                secilenKilitliElbise.isLocked = false; 
                
                if (satinAlmaPaneli != null) satinAlmaPaneli.SetActive(false); 
                if (mankenYoneticisi != null) mankenYoneticisi.KiyafetGiy(secilenKilitliElbise);
                
                Debug.Log($"Satın alma başarılı! Kalan Para: {oyuncuParasi}");
            }
            else
            {
                // Eğer oyuncunun parası yetmiyorsa açıklamayı değiştirelim:
                if (satinAlmaAciklamaText != null)
                {
                    satinAlmaAciklamaText.text = "Yetersiz altın! Bu elbiseyi satın alamazsınız.";
                }
                Debug.LogWarning("Yetersiz bakiye!");
            }
        }
    }

    public void SatAlPaneliniKapat() { if (satinAlmaPaneli != null) satinAlmaPaneli.SetActive(false); }

    public void ParaYazisiniGuncelle()
    {
        if (paraText != null)
        {
            paraText.text = oyuncuParasi.ToString();
        }
    }

    public void DegerlendirmeModunaGec()
    {
        if (gardirobPaneli != null) gardirobPaneli.SetActive(false);
        if (degerlendirmePaneli != null) degerlendirmePaneli.SetActive(true);
    }
}