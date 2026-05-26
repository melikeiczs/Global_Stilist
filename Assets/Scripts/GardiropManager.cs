using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GardiropManager : MonoBehaviour
{
    [Header("Ekonomi Sistemi")]
    public int oyuncuAltini = 1000;
    public int oyuncuElmasi = 50;

    [Header("Ekranın Üstündeki Para Metinleri")]
    public TextMeshProUGUI ustAltinText;  // Ekranın üstündeki Altın yazısı
    public TextMeshProUGUI ustElmasText;  // Ekranın üstündeki Elmas yazısı

    [Header("Satın Alma Paneli UI Elemanları")]
    public GameObject satinAlmaPaneliObjesi;
    public TextMeshProUGUI panelAciklamaYazisi;

    [Header("Manken Yönetici Referansı")]
    public MankenYonetici mankenYoneticisi;

    private KiyafetButonu secilenButon; // O an hangi kilitli butona basıldı?

    void Start()
    {
        // Oyun açılır açılmaz bendeki paraları ekranın üstüne yazdır
        ParaArayuzunuGuncelle();
    }

    // Parayı ekranda güncelleyen sihirli fonksiyonumuz
    public void ParaArayuzunuGuncelle()
    {
        ustAltinText.text = oyuncuAltini.ToString();
        ustElmasText.text = oyuncuElmasi.ToString();
    }

    // Kilitli bir kıyafete basılınca burası tetiklenir
    public void SatAlmaPaneliniGoster(KiyafetButonu basilanButon)
    {
        secilenButon = basilanButon; // Hangi butona basıldığını aklında tut
        panelAciklamaYazisi.text = secilenButon.fiyat + " " + secilenButon.paraTuru + " karşılığında açmak ister misin?";
        satinAlmaPaneliObjesi.SetActive(true); // Paneli görünür yap
    }

    // Satın Alma Panelindeki "EVET / SATIN AL" butonuna bağlanacak fonksiyon
    public void SatinAlmayiOnayla()
    {
        if (secilenButon.paraTuru == "Altin" && oyuncuAltini >= secilenButon.fiyat)
        {
            oyuncuAltini -= secilenButon.fiyat; // Parayı düş
            ParaArayuzunuGuncelle();           // Ekranda parayı hemen güncelle!
            KilidiGercetenAc();
        }
        else if (secilenButon.paraTuru == "Elmas" && oyuncuElmasi >= secilenButon.fiyat)
        {
            oyuncuElmasi -= secilenButon.fiyat; // Elması düş
            ParaArayuzunuGuncelle();           // Ekranda elması hemen güncelle!
            KilidiGercetenAc();
        }
        else
        {
            Debug.Log("Yetersiz bakiye!");
            // Buraya ileride "Yetersiz Bakiye" uyarısı ekleyebiliriz
        }
    }

    void KilidiGercetenAc()
    {
        secilenButon.kilidiAcikMi = true;
        secilenButon.kilitGorseli.SetActive(false); // Kilidi ekrandan kaldır
        satinAlmaPaneliObjesi.SetActive(false);     // Paneli kapat
        //MankeneGiy(secilenButon.kiyafetID);         // Kıyafeti giydir
    }

    public void PaneliKapat()
    {
        satinAlmaPaneliObjesi.SetActive(false);
    }

    public void MankeneGiy(KiyafetButonu buton)
{
    // Manken yöneticisine sayı (int) yerine, butonun içindeki gerçek veriyi gönderiyoruz
    mankenYoneticisi.KiyafetGiy(buton.elbiseVerisi); 
}
}