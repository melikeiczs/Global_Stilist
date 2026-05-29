using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GardiropManager : MonoBehaviour
{
    [System.Serializable]
    public class SeviyeKriteri
    {
        public int siparisID; 
        public string hedefKonsept; 
        public string hedefZaman;   
        public string hedefRenk;    
    }

    [Header("Sistem Baglantilari")]
    public MankenYonetici mankenYoneticisi; 

    [Header("Sahneye Ozel Seviye Kriter Listesi")]
    public SeviyeKriteri[] seviyeHedefleri = new SeviyeKriteri[5]
    {
        new SeviyeKriteri { siparisID = 1, hedefKonsept = "Uzun", hedefZaman = "Isiltili", hedefRenk = "Mor" },
        new SeviyeKriteri { siparisID = 2, hedefKonsept = "Mini", hedefZaman = "Bel Tokali", hedefRenk = "Siyah" },
        new SeviyeKriteri { siparisID = 3, hedefKonsept = "Mini", hedefZaman = "Askili", hedefRenk = "Mor" },
        new SeviyeKriteri { siparisID = 4, hedefKonsept = "Mini", hedefZaman = "Capraz Omuz", hedefRenk = "Kirmizi" },
        new SeviyeKriteri { siparisID = 5, hedefKonsept = "Mini", hedefZaman = "Halter Yaka", hedefRenk = "Mor" }
    };

    [Header("Satin Alma Paneli")]
    public GameObject satinAlmaPaneli; 
    public TextMeshProUGUI satinAlmaAciklamaText; 

    [Header("Ekonomi Sistemi")]
    public TextMeshProUGUI paraText; 

    [Header("Panel Yonetimi")]
    public GameObject gardirobPaneli; 
    public GameObject degerlendirmePaneli;
    public GameObject altinlariToplaButonu; 

    [Header("Degerlendirme Ekrani UI")]
    public TextMeshProUGUI skorText; 
    public TextMeshProUGUI kazanilanAltinText; 
    public UnityEngine.UI.Image[] yildizGorselleri = new UnityEngine.UI.Image[3]; 
    public Sprite doluYildizSprite;  
    public Sprite bosYildizSprite;   

    [Header("Juri Beklentisi UI Yazilari")]
    public TextMeshProUGUI juriKonseptText;
    public TextMeshProUGUI juriZamanText;
    public TextMeshProUGUI juriRenkText;

    private ElbiseVerisi secilenKilitliElbise;
    private int geciciKazanilanAltin = 0; 

    void Start()
    {
        if (!PlayerPrefs.HasKey("AktifSiparisID") || PlayerPrefs.GetInt("AktifSiparisID") <= 0)
        {
            PlayerPrefs.SetInt("AktifSiparisID", 1);
        }

        if (!PlayerPrefs.HasKey("OyuncuParasi"))
        {
            PlayerPrefs.SetInt("OyuncuParasi", 1000);
        }
        PlayerPrefs.Save();
        
        ParaYazisiniGuncelle();
        if (altinlariToplaButonu != null) altinlariToplaButonu.SetActive(false); 
    }

    public void ElbiseSec(ElbiseVerisi secilenElbise)
    {
        if (secilenElbise == null) return;
        if (secilenElbise.isLocked)
        {
            secilenKilitliElbise = secilenElbise; 
            if (satinAlmaAciklamaText != null)
                satinAlmaAciklamaText.text = $"{secilenElbise.elbiseAdi} isimli kiyafeti {secilenElbise.elbiseFiyati} altina acmak ister misiniz?";
            if (satinAlmaPaneli != null) satinAlmaPaneli.SetActive(true);
            return; 
        }
        if (mankenYoneticisi != null) 
        {
            mankenYoneticisi.KiyafetGiy(secilenElbise);
            ElbiseUIBoyutunuUygula(secilenElbise);
        }
    }

    private void ElbiseUIBoyutunuUygula(ElbiseVerisi elbise)
    {
        if (mankenYoneticisi == null || elbise == null) return;
        
        UnityEngine.UI.Image elbiseGorseli = mankenYoneticisi.GetMankenElbiseGorseli();
        if (elbiseGorseli != null)
        {
            RectTransform rect = elbiseGorseli.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.anchoredPosition = elbise.pozisyonOffset;
                rect.localScale = new Vector3(elbise.elbiseBoyutu.x, elbise.elbiseBoyutu.y, 1f);
            }
        }
    }

    // 🚀 YENİ EKLENEN MANKEN SEÇME FONKSİYONU
    public void MankenSec(MankenVerisi secilenManken)
    {
        if (secilenManken == null) return;
        
        if (mankenYoneticisi != null)
        {
            mankenYoneticisi.MankenDegistir(secilenManken);
        }
    }

    public void KilidiAcVeSatinAl()
    {
        int oyuncuParasi = PlayerPrefs.GetInt("OyuncuParasi", 1000);
        if (secilenKilitliElbise != null && oyuncuParasi >= secilenKilitliElbise.elbiseFiyati)
        {
            oyuncuParasi -= secilenKilitliElbise.elbiseFiyati; 
            PlayerPrefs.SetInt("OyuncuParasi", oyuncuParasi);
            ParaYazisiniGuncelle(); 
            secilenKilitliElbise.isLocked = false; 
            if (satinAlmaPaneli != null) satinAlmaPaneli.SetActive(false); 
            if (mankenYoneticisi != null) 
            {
                mankenYoneticisi.KiyafetGiy(secilenKilitliElbise);
                ElbiseUIBoyutunuUygula(secilenKilitliElbise);
            }
        }
    }

    public void SatAlPaneliniKapat() 
    { 
        if (satinAlmaPaneli != null) satinAlmaPaneli.SetActive(false); 
    }

    public void ParaYazisiniGuncelle()
    {
        if (paraText != null) paraText.text = PlayerPrefs.GetInt("OyuncuParasi", 1000).ToString();
    }

    public void DegerlendirmeModunaGec()
    {
        if (gardirobPaneli != null) gardirobPaneli.SetActive(false);
        if (degerlendirmePaneli != null) degerlendirmePaneli.SetActive(true);

        for (int i = 0; i < yildizGorselleri.Length; i++)
        {
            if (yildizGorselleri[i] != null && bosYildizSprite != null) yildizGorselleri[i].sprite = bosYildizSprite;
        }
        
        if (skorText != null) skorText.text = "Juri kararini bekliyor...";
        if (kazanilanAltinText != null) kazanilanAltinText.text = "";
        
        int aktifSiparisID = PlayerPrefs.GetInt("AktifSiparisID", 1);
        SeviyeKriteri aktifHedef = null;

        foreach (var hedef in seviyeHedefleri)
        {
            if (hedef.siparisID == aktifSiparisID)
            {
                aktifHedef = hedef;
                break;
            }
        }

        if (aktifHedef == null && seviyeHedefleri.Length > 0) aktifHedef = seviyeHedefleri[0];

        if (aktifHedef != null)
        {
            if (juriKonseptText != null) juriKonseptText.text = "Aranan Konsept: " + aktifHedef.hedefKonsept;
            if (juriZamanText != null) juriZamanText.text = "Aranan Tarz: " + aktifHedef.hedefZaman;
            if (juriRenkText != null) juriRenkText.text = "Aranan Renk: " + aktifHedef.hedefRenk;
        }

        JuriDegerlendir();
    }

    public void JuriDegerlendir()
    {
        if (mankenYoneticisi == null) return;

        ElbiseVerisi giyilenElbise = mankenYoneticisi.GetGiyilenElbise();
        if (giyilenElbise == null)
        {
            if (skorText != null) skorText.text = "Manken bos! Elbise secmelisiniz.";
            return;
        }

        int aktifSiparisID = PlayerPrefs.GetInt("AktifSiparisID", 1);
        SeviyeKriteri aktifHedef = null;

        foreach (var hedef in seviyeHedefleri)
        {
            if (hedef.siparisID == aktifSiparisID)
            {
                aktifHedef = hedef;
                break;
            }
        }

        if (aktifHedef == null && seviyeHedefleri.Length > 0) aktifHedef = seviyeHedefleri[0];
        if (aktifHedef == null) return;

        int kazanilanYildizSayisi = 0;

        string elbiseKonsept = TemizMetin(giyilenElbise.konsept);
        string hedefKonsept = TemizMetin(aktifHedef.hedefKonsept);

        string elbiseZaman = TemizMetin(giyilenElbise.zaman);
        string hedefZaman = TemizMetin(aktifHedef.hedefZaman);

        string elbiseRenk = TemizMetin(giyilenElbise.renk);
        string hedefRenk = TemizMetin(aktifHedef.hedefRenk);

        if (elbiseKonsept == hedefKonsept) { YildiziDoldur(0); kazanilanYildizSayisi++; }
        if (elbiseZaman == hedefZaman) { YildiziDoldur(1); kazanilanYildizSayisi++; }
        if (elbiseRenk == hedefRenk) { YildiziDoldur(2); kazanilanYildizSayisi++; }

        geciciKazanilanAltin = kazanilanYildizSayisi * 50; 

        if (skorText != null) skorText.text = $"TEBRİKLER! {kazanilanYildizSayisi} / 3 YILDIZ ALDINIZ";
        if (kazanilanAltinText != null) kazanilanAltinText.text = $"+{geciciKazanilanAltin} Altin Kazandin!";

        if (altinlariToplaButonu != null) altinlariToplaButonu.SetActive(true);
    }

    private string TemizMetin(string text)
    {
        if (string.IsNullOrEmpty(text)) return "";
        
        string temiz = text.Trim().ToLower();
        
        temiz = temiz.Replace("ı", "i")
                     .Replace("ş", "s")
                     .Replace("ğ", "g")
                     .Replace("ç", "c")
                     .Replace("ö", "o")
                     .Replace("ü", "u");
                     
        return temiz;
    }

    private void YildiziDoldur(int yildizIndex)
    {
        if (yildizIndex >= 0 && yildizIndex < yildizGorselleri.Length && yildizGorselleri[yildizIndex] != null && doluYildizSprite != null)
        {
            yildizGorselleri[yildizIndex].sprite = doluYildizSprite;
        }
    }

    public void AltinlariToplaVeDona()
    {
        int mevcutPara = PlayerPrefs.GetInt("OyuncuParasi", 1000);
        mevcutPara += geciciKazanilanAltin;
        PlayerPrefs.SetInt("OyuncuParasi", mevcutPara);

        int aktifSiparisID = PlayerPrefs.GetInt("AktifSiparisID", 1);
        string tamamlananlar = PlayerPrefs.GetString("Istanbul_Tamamlanan_IDler", "");
        tamamlananlar += aktifSiparisID.ToString() + ",";
        PlayerPrefs.SetString("Istanbul_Tamamlanan_IDler", tamamlananlar);

        int toplamSiparis = PlayerPrefs.GetInt("Istanbul_Tamamlanan_Siparis", 0);
        toplamSiparis++;
        PlayerPrefs.SetInt("Istanbul_Tamamlanan_Siparis", toplamSiparis);
        
        PlayerPrefs.Save();

        SceneManager.LoadScene("Şehirler"); 
    }
}