using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçişleri için zorunlu kütüphane

public class SahneYneticisi : MonoBehaviour
{
    // Singleton Yapısı
    public static SahneYneticisi Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [Header("Genel Sahne İsimleri")]
    public string anaHaritaSahneAdi = "Şehirler"; // Ana harita sahnenin adı
    public string tasarimSahneAdi = "GardirobScene"; // 🚀 YENİ: Tasarım yaptığın gardırop sahnesinin tam adı

    // 🚀 YENİ: SİPARİŞİ AL BUTONUNA BAĞLANACAK FONKSİYON
    /// <summary>
    /// Diyalog sahnesindeki siparişi onaylayıp tasarım sahnesine geçişi sağlar.
    /// </summary>
    public void SiparisiAlVeTasarimaGec()
    {
        // Butona basıldığında PlayerPrefs verilerinin diske yazılmasını garantiye alıyoruz
        PlayerPrefs.Save();
        
        if (!string.IsNullOrEmpty(tasarimSahneAdi))
        {
            Debug.Log("Sipariş alındı, tasarım sahnesine geçiliyor: " + tasarimSahneAdi);
            SceneManager.LoadScene(tasarimSahneAdi);
        }
        else
        {
            Debug.LogError("Hata: Tasarım sahne adı (tasarimSahneAdi) Inspector panelinde boş bırakılmış!");
        }
    }

    /// <summary>
    /// İsmi verilen herhangi bir sahneye doğrudan geçiş yapar.
    /// </summary>
    public void SahneyeGec(string sahneAdi)
    {
        if (!string.IsNullOrEmpty(sahneAdi))
        {
            Debug.Log("Sahneye geçiş yapılıyor: " + sahneAdi);
            SceneManager.LoadScene(sahneAdi);
        }
        else
        {
            Debug.LogWarning("Geçiş yapılmak istenen sahne adı boş bırakılmış!");
        }
    }

    /// <summary>
    /// Giydirme sahnelerinden ana dünya haritasına geri dönmek için kullanılır.
    /// </summary>
    public void HaritayaGeriDon()
    {
        Debug.Log("Ana dünya haritasına geri dönülüyor...");
        SceneManager.LoadScene(anaHaritaSahneAdi);
    }

    /// <summary>
    /// Oyundan tamamen çıkış yapmak için
    /// </summary>
    public void OyundanCik()
    {
        Debug.Log("Oyundan çıkılıyor...");
        Application.Quit();
    }
}