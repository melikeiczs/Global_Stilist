using UnityEngine;
using UnityEngine.SceneManagement;

public class GardiropManager : MonoBehaviour
{
    [Header("Manken Bağlantısı")]
    public MankenYonetici mankenYoneticisi; 

    [Header("Satın Alma / Kilit Sistemi")]
    public GameObject satinAlmaPaneli; 

    [Header("Sahne Geçiş Ayarı")]
    public string degerlendirmeSahneAdi = "DegerlendirmeSahnesi";

    private ElbiseVerisi secilenKilitliElbise;
    private ElbiseVerisi giyilenSonElbise;

    // --- 🚨 1. İSTEK: KILİTLİYSE PANEL AÇMA SİSTEMİ ---
    public void ElbiseSec(ElbiseVerisi secilenElbise)
    {
        if (secilenElbise == null) return;

        if (secilenElbise.isLocked)
        {
            secilenKilitliElbise = secilenElbise; 
            if (satinAlmaPaneli != null)
            {
                satinAlmaPaneli.SetActive(true); // Panel açılır
            }
            return; // Giydirmeden burada durdurur!
        }

        // Kilitli değilse giydir
        giyilenSonElbise = secilenElbise;
        if (mankenYoneticisi != null)
        {
            mankenYoneticisi.KiyafetGiy(secilenElbise);
        }
    }

    public void KilidiAcVeSatinAl()
    {
        if (secilenKilitliElbise != null)
        {
            secilenKilitliElbise.isLocked = false; 
            if (satinAlmaPaneli != null) satinAlmaPaneli.SetActive(false); 
            
            giyilenSonElbise = secilenKilitliElbise;
            if (mankenYoneticisi != null) mankenYoneticisi.KiyafetGiy(secilenKilitliElbise);
        }
    }

    public void SatAlPaneliniKapat() { if (satinAlmaPaneli != null) satinAlmaPaneli.SetActive(false); }

    // --- 🚀 2. İSTEK: KARAKTER SEÇME SİSTEMİ ---
    // Alttaki kız butonları bu fonksiyonu çağıracak
    public void KarakteriDegistir(Sprite yeniKizVucutGorseli)
    {
        if (mankenYoneticisi != null && yeniKizVucutGorseli != null)
        {
            mankenYoneticisi.MankenVucutDegistir(yeniKizVucutGorseli);
        }
    }

    public void MankeniKaydetVeIsınla()
    {
        if (giyilenSonElbise != null)
        {
            PlayerPrefs.SetString("SecilenElbise", giyilenSonElbise.elbiseAdi);
        }
        // Hangi kızın seçildiğini de jüri sahnesine taşımak istersen buraya ekleme yapabiliriz
        PlayerPrefs.Save();
        SceneManager.LoadScene(degerlendirmeSahneAdi);
    }
}