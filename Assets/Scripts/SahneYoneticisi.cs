using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçişleri için zorunlu kütüphane

public class SahneYneticisi : MonoBehaviour
{
    // Singleton Yapısı: Sahneler arası geçişte bu yöneticinin tek bir kopyası olsun istersen
    public static SahneYneticisi Instance;

    void Awake()
    {
        // Eğer sahneler arasında bu objenin yok olmasını istemiyorsan bu yapıyı kullanabilirsin.
        // Şimdilik basit tutmak adına sadece referans eşitliyoruz.
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [Header("Genel Sahne İsimleri")]
    public string anaHaritaSahneAdi = "Şehirler"; // Ana harita sahnenin adı

    /// <summary>
    /// İsmi verilen herhangi bir sahneye doğrudan geçiş yapar.
    /// (Örn: "Oyuna Başla" butonu veya haritadaki açık şehirler için kullanılabilir)
    /// </summary>
    /// <param name="sahneAdi">Geçilecek sahnenin tam adı</param>
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
    /// Geri Dön butonuna doğrudan bu fonksiyonu bağlayabilirsin.
    /// </summary>
    public void HaritayaGeriDon()
    {
        Debug.Log("Ana dünya haritasına geri dönülüyor...");
        SceneManager.LoadScene(anaHaritaSahneAdi);
    }

    /// <summary>
    /// Oyundan tamamen çıkış yapmak için (Ana menüdeki Çıkış butonu için)
    /// </summary>
    public void OyundanCik()
    {
        Debug.Log("Oyundan çıkılıyor...");
        Application.Quit();
    }
}