using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Sahne geçişi için şart

public class SehirButonu : MonoBehaviour
{
    [Header("Şehir Ayarları")]
    public string sehirAdi;       // Müfettişten yaz: "Paris", "New York" vb.
    public string hedefSahneAdi;  // Geçiş yapılacak sahne adı: "ParisGiydirme" vb.
    
    [Header("Kilit Durumu")]
    public bool isLocked = true;  // Bu şehir şu an kilitli mi?
    public GameObject kilitIkonu; // Butonun içindeki kilit resmi (UI Image)

    private Button butonBileseni;

    void Start()
    {
        butonBileseni = GetComponent<Button>();
        
        // Oyun başladığında kilit durumunu kontrol et ve görseli ayarla
        KilitDurumunuGuncelle();
    }

    // 1. KONTROL: Kilit durumuna göre görseli açar veya kapatır
    public void KilitDurumunuGuncelle()
    {
        if (kilitIkonu != null)
        {
            kilitIkonu.SetActive(isLocked); // Kilitliyse ikon görünür, açık if görünmez olur
        }
    }

    // 2. KONTROL: Sahne Geçiş Tetikleyicisi (Butona tıklandığında bu çalışacak)
    public void SehreGit()
    {
        if (isLocked)
        {
            // EĞER KİLİTLİYSE: Sahne geçişine izin verme!
            Debug.Log(sehirAdi + " şehri henüz kilitli! Geçiş yapılamaz.");
            
            // Buraya istersen küçük bir "Kilitli" ses efekti veya ekrana uyarı yazısı ekleyebilirsin.
        }
        else
        {
            // EĞER KİLİT AÇIKSA: Pürüzsüzce diğer sahneye geç
            Debug.Log(sehirAdi + " açık! Sahneye yükleniyor: " + hedefSahneAdi);
            SceneManager.LoadScene(hedefSahneAdi);
        }
    }
}