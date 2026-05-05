using UnityEngine;
using UnityEngine.UI;

public class KumasSecici : MonoBehaviour
{
    [Header("Ayarlar")]
    public Image masaImage;      // Masadaki kumaş görseli
    public GameObject secimPaneli; // Butonların olduğu panel

    [Header("Kumaş Görselleri")]
    public Sprite kumas1Sprite;  
    public Sprite kumas2Sprite;  
    public Sprite kumas3Sprite; 

    void Start()
    {
        // Oyun başında masada kumaş görünmesin istiyorsan rengini şeffaf yapabiliriz
        // Veya varsayılan bir "boş masa" sprite'ı koyabilirsin.
    }

    public void Kumas1Sec()
    {
        masaImage.sprite = kumas1Sprite;
        SecimiBitir();
    }

    public void Kumas2Sec()
    {
        masaImage.sprite = kumas2Sprite;
        SecimiBitir();
    }

    public void Kumas3Sec()
    {
        masaImage.sprite = kumas3Sprite;
        SecimiBitir();
    }

    // Seçim yapılınca paneli kapatan ortak fonksiyon
    void SecimiBitir()
    {
        if (secimPaneli != null)
        {
            secimPaneli.SetActive(false); // Paneli ve butonları gizle
        }
        Debug.Log("Kumaş seçildi ve masaya serildi!");
        
        // BURAYA: Bir sonraki aşama (Kamera zoom veya kesim çizgileri) kodu gelecek.
    }
}