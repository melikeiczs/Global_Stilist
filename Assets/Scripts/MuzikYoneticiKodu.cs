using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MuzikYoneticiKodu : MonoBehaviour
{
    private static MuzikYoneticiKodu instance;

    [Header("Genel Ses Ayarlari")]
    public AudioClip genelTiklamaSesi; // Tüm standart butonlarda çalacak olan genel ses

    void Awake()
    {
        // Eğer sahneler arası geçişte zaten çalan bir müzik yöneticisi varsa, ikincisinin oluşmasını engeller
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Bu obje sahneler değişse bile SİLİNMEZ!
        }
        else
        {
            Destroy(gameObject); // Eğer haritadan gardıroba geri dönülürse mükerrer (çift) müziği yok eder
            return; // Kodun aşağıya devam etmesini engellemek için güvenlik amaçlı geri dönüyoruz
        }
    }

    void Start()
    {
        // Oyun ilk açıldığında sahnedeki butonları bul ve sesleri ata
        ButonSesleriniDinamikAta();

        // Her sahne değiştiğinde (örneğin gardıroptan şehir haritasına geçince) yeni gelen butonları da yakalamak için Unity sistemine kayıt oluyoruz
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Bellek sızıntısını (Memory Leak) önlemek için obje yok olursa takibi bırakıyoruz
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Her yeni sahne yüklendiğinde otomatik çalışacak fonksiyon
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ButonSesleriniDinamikAta();
    }

    // Sahnedeki sessiz butonları bulup genel tıklama sesini tanımlayan akıllı motor
    private void ButonSesleriniDinamikAta()
    {
        // Sahnedeki AKTİF olan tüm Buton bileşenlerini otomatik bulur
        Button[] sahnedekiButonlar = FindObjectsByType<Button>(FindObjectsSortMode.None);

        foreach (Button buton in sahnedekiButonlar)
        {
            if (buton == null) continue;

            // ⚠️ ÖNCEKİ SESLERİ KORUMA KONTROLÜ:
            // Gardırop butonları ve satın alma butonlarının kendi özel ses tetikleyicileri (PlayClipAtPoint) 
            // zaten olduğu için onları bu genel sesten muaf tutuyoruz.
            if (buton.name.Contains("Elbise") || buton.name.Contains("Kiyafet") || buton.name.Contains("SatinAl"))
            {
                continue; 
            }

            // Butona mükerrer (üst üste) ses eklememek için önce eski dinleyiciyi temizleyip yenisini ekliyoruz
            buton.onClick.RemoveListener(GenelButonSesiCal);
            buton.onClick.AddListener(GenelButonSesiCal);
        }
    }

    public void GenelButonSesiCal()
    {
        if (genelTiklamaSesi != null)
        {
            // Müziği kesmeyen ve sahne geçişlerinde bellekten otomatik silinen bağımsız ses oynatıcı
            AudioSource.PlayClipAtPoint(genelTiklamaSesi, Camera.main.transform.position);
        }
    }
}