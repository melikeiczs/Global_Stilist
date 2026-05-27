using UnityEngine;
using UnityEngine.UI; // Yıldız resimlerini kontrol etmek için gerekli
using TMPro;

public class GardiropManager : MonoBehaviour
{
    [System.Serializable]
    public class SeviyeKriteri
    {
        public string hedefKonsept; 
        public string hedefZaman;   
        public string hedefRenk;    
    }

    [Header("Sistem Bağlantıları")]
    public MankenYonetici mankenYoneticisi; 

    [Header("Sahneye Özel Seviye Kriter Listesi")]
    public SeviyeKriteri[] seviyeHedefleri = new SeviyeKriteri[3]
    {
        new SeviyeKriteri { hedefKonsept = "Mini Elbise", hedefZaman = "1", hedefRenk = "Kırmızı" },
        new SeviyeKriteri { hedefKonsept = "Tulum", hedefZaman = "2", hedefRenk = "Mor" },
        new SeviyeKriteri { hedefKonsept = "Uzun Abiye", hedefZaman = "3", hedefRenk = "Altın Sarısı" }
    };

    [Header("Satın Alma / Kilit Paneli")]
    public GameObject satinAlmaPaneli; 
    public TextMeshProUGUI satinAlmaAciklamaText; 

    [Header("Ekonomi Sistemi")]
    public int oyuncuParasi = 1000; 
    public int elbiseFiyati = 180; 
    public TextMeshProUGUI paraText; 

    [Header("Panel Yönetimi")]
    public GameObject gardirobPaneli; 
    public GameObject degerlendirmePaneli;

    [Header("Değerlendirme Ekranı UI Elemanları")]
    public TextMeshProUGUI skorText; 
    public TextMeshProUGUI kazanilanAltinText; 

    // 🚀 YENİ: YILDIZ SİSTEMİ İÇİN GEREKLİ UI ELEMANLARI
    [Header("Yıldız UI Bileşenleri")]
    [Tooltip("Değerlendirme panelindeki 3 adet boş yıldızın Image bileşenlerini sırasıyla koyun.")]
    public Image[] yildizGorselleri = new Image[3]; 
    public Sprite doluYildizSprite;  // Parlayan sarı yıldız görseli
    public Sprite bosYildizSprite;   // Sönük/gri yıldız görseli

    private ElbiseVerisi secilenKilitliElbise;

    void Start()
    {
        ParaYazisiniGuncelle();
    }

    public void ElbiseSec(ElbiseVerisi secilenElbise)
    {
        if (secilenElbise == null) return;

        if (secilenElbise.isLocked)
        {
            secilenKilitliElbise = secilenElbise; 
            int gecerliFiyat = secilenElbise.elbiseFiyati > 0 ? secilenElbise.elbiseFiyati : elbiseFiyati;

            if (satinAlmaAciklamaText != null)
                satinAlmaAciklamaText.text = $"{secilenElbise.elbiseAdi} isimli kıyafeti {gecerliFiyat} altına açmak ister misiniz?";
            
            if (satinAlmaPaneli != null) satinAlmaPaneli.SetActive(true);
            return; 
        }

        if (mankenYoneticisi != null) mankenYoneticisi.KiyafetGiy(secilenElbise);
    }

    public void KilidiAcVeSatinAl()
    {
        if (secilenKilitliElbise != null)
        {
            int gecerliFiyat = secilenKilitliElbise.elbiseFiyati > 0 ? secilenKilitliElbise.elbiseFiyati : elbiseFiyati;

            if (oyuncuParasi >= gecerliFiyat)
            {
                oyuncuParasi -= gecerliFiyat; 
                ParaYazisiniGuncelle(); 
                secilenKilitliElbise.isLocked = false; 

                #if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(secilenKilitliElbise);
                #endif

                if (satinAlmaPaneli != null) satinAlmaPaneli.SetActive(false); 
                if (mankenYoneticisi != null) mankenYoneticisi.KiyafetGiy(secilenKilitliElbise);
            }
            else
            {
                if (satinAlmaAciklamaText != null)
                    satinAlmaAciklamaText.text = "Yetersiz altın! Bu elbiseyi satın alamazsınız.";
            }
        }
    }

    public void SatAlPaneliniKapat() { if (satinAlmaPaneli != null) satinAlmaPaneli.SetActive(false); }

    public void ParaYazisiniGuncelle()
    {
        if (paraText != null) paraText.text = oyuncuParasi.ToString();
    }

    // 🚀 1. ADIM: TASARIMI BİTİR'E BASILDIĞINDA SADECE PANELİ DEĞİŞTİRİR VE YILDIZLARI SIFIRLAR
    public void DegerlendirmeModunaGec()
    {
        // Önceki oyundan kalan yıldızlar varsa hepsini boş/sönük yapıyoruz
        for (int i = 0; i < yildizGorselleri.Length; i++)
        {
            if (yildizGorselleri[i] != null && bosYildizSprite != null)
                yildizGorselleri[i].sprite = bosYildizSprite;
        }

        if (skorText != null) skorText.text = "Jüri kararını bekliyor...";
        if (kazanilanAltinText != null) kazanilanAltinText.text = "";

        // Panelleri Değiştir
        if (gardirobPaneli != null) gardirobPaneli.SetActive(false);
        if (degerlendirmePaneli != null) degerlendirmePaneli.SetActive(true);
    }

    // 🚀 2. ADIM: DEĞERLENDİRME PANELİNDEKİ "DEĞERLENDİR" BUTONUNA BASILDIĞINDA ÇALIŞACAK SİSTEM
    public void JuriDegerlendir()
    {
        ElbiseVerisi giyilenElbise = mankenYoneticisi.GetGiyilenElbise();

        if (giyilenElbise == null)
        {
            Debug.LogWarning("Manken üzerinde kıyafet yok!");
            return;
        }

        int aktifSeviyeNo = PlayerPrefs.GetInt("AktifSeviyeNo", 1);
        int index = aktifSeviyeNo - 1;

        if (index < 0 || index >= seviyeHedefleri.Length) return;

        SeviyeKriteri aktifHedef = seviyeHedefleri[index];

        int kazanilanYildizSayisi = 0;

        // 🎯 KRİTER KONTROLLERİ VE YILDIZ HESAPLAMA
        // 1. Kriter: Konsept
        if (giyilenElbise.konsept == aktifHedef.hedefKonsept)
        {
            YildiziDoldur(0); // 1. Yıldızı yak
            kazanilanYildizSayisi++;
        }
        
        // 2. Kriter: Zaman
        if (giyilenElbise.zaman.ToString() == aktifHedef.hedefZaman)
        {
            YildiziDoldur(1); // 2. Yıldızı yak
            kazanilanYildizSayisi++;
        }
        
        // 3. Kriter: Renk
        if (giyilenElbise.renk == aktifHedef.hedefRenk)
        {
            YildiziDoldur(2); // 3. Yıldızı yak
            kazanilanYildizSayisi++;
        }

        // 💰 ÖDÜL SİSTEMİ (Örn: Yıldız başına 50 altın, 3 yıldız = 150 altın)
        int kazanilanAltin = kazanilanYildizSayisi * 50; 
        oyuncuParasi += kazanilanAltin; 
        ParaYazisiniGuncelle(); 

        // 📝 UI METİNLERİNİ GÜNCELLE
        if (skorText != null) skorText.text = $"TEBRİKLER! {kazanilanYildizSayisi} / 3 YILDIZ ALDINIZ";
        if (kazanilanAltinText != null) kazanilanAltinText.text = $"+{kazanilanAltin} Altın Kazandın!";
    }

    // Yardımcı fonksiyon: Belirtilen sıradaki yıldızı parlatır
    private void YildiziDoldur(int yildizIndex)
    {
        if (yildizIndex >= 0 && yildizIndex < yildizGorselleri.Length)
        {
            if (yildizGorselleri[yildizIndex] != null && doluYildizSprite != null)
            {
                yildizGorselleri[yildizIndex].sprite = doluYildizSprite;
            }
        }
    }
}