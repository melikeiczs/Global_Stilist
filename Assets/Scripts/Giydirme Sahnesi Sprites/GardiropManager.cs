using UnityEngine;
using UnityEngine.SceneManagement;

public class GardiropManager : MonoBehaviour
{
    [Header("Manken Bağlantısı")]
    public MankenYonetici mankenYoneticisi; 

    [Header("Satın Alma / Kilit Sistemi")]
    [Tooltip("Sahnedeki Satın Alma/Kilit Panelini (Satinalmapaneli) buraya sürükleyin.")]
    public GameObject satinAlmaPaneli; 

    [Header("Sahne Geçiş Ayarı")]
    public string degerlendirmeSahneAdi = "DegerlendirmeSahnesi";

    // Panelden onay gelmesi için hafızada tutulan geçici kilitli elbise
    private ElbiseVerisi secilenKilitliElbise;

    public void ElbiseSec(ElbiseVerisi secilenElbise)
    {
        if (secilenElbise == null) return;

        // 🚨 KİLİT KONTROLÜ: Elbise kilitli mi?
        if (secilenElbise.isLocked)
        {
            Debug.Log($"{secilenElbise.elbiseAdi} KİLİTLİ! Satın alma paneli açılıyor...");
            secilenKilitliElbise = secilenElbise; 
            
            if (satinAlmaPaneli != null)
            {
                satinAlmaPaneli.SetActive(true); // Satın alma uyarısını ekrana getir!
            }
            return; // Kilitli olduğu için aşağı geçmeyi engelle (giydirme)!
        }

        // ✅ KİLİTSİZ DURUM: Normalce giydir
        if (mankenYoneticisi != null)
        {
            mankenYoneticisi.KiyafetGiy(secilenElbise);
        }
    }

    /// <summary>
    /// Satın Alma Panelindeki "SATIN AL" butonuna bağlanacak ana fonksiyon
    /// </summary>
    public void KilidiAcVeSatinAl()
    {
        if (secilenKilitliElbise != null)
        {
            // Kilidi tamamen kaldır
            secilenKilitliElbise.isLocked = false; 
            
            if (satinAlmaPaneli != null) 
                satinAlmaPaneli.SetActive(false); // Paneli kapat
            
            // Otomatik olarak elbiseyi giydir
            ElbiseSec(secilenKilitliElbise); 
            Debug.Log($"{secilenKilitliElbise.elbiseAdi} kilidi GardiropManager tarafından açıldı.");
        }
    }

    /// <summary>
    /// Satın Alma Panelindeki "KAPAT / İPTAL" butonuna bağlanacak fonksiyon
    /// </summary>
    public void SatAlPaneliniKapat()
    {
        if (satinAlmaPaneli != null)
        {
            satinAlmaPaneli.SetActive(false);
        }
    }

    public void MankeniKaydetVeIsınla()
    {
        if (mankenYoneticisi != null) SceneManager.LoadScene(degerlendirmeSahneAdi);
    }
}