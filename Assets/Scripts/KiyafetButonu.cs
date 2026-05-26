using UnityEngine;
using UnityEngine.UI;

public class KiyafetButonu : MonoBehaviour
{
    [Header("Kıyafet Ayarları")]
    public ElbiseVerisi elbiseVerisi;       // Her kıyafetin benzersiz numarası (0, 1, 2...)
    public int fiyat;           // Kıyafetin fiyatı
    public string paraTuru;     // "Altin" veya "Elmas"
    public bool kilidiAcikMi;    // Bu kıyafet başlangıçta açık mı?

    [Header("Görsel Referans")]
    public GameObject kilitGorseli; // Butonun içindeki kilit resmi

    private GardiropManager manager;

    void Start()
    {
        // Sahnedeki manager'ı otomatik bul
        manager = FindObjectOfType<GardiropManager>();

        // Eğer kıyafetin kilidi zaten açıksa, kilit görselini gizle
        if (kilidiAcikMi == true)
        {
            kilitGorseli.SetActive(false);
        }
    }

    // Butona tıklandığında bu fonksiyon çalışacak
    public void ButonaTiklandi()
{
    if (kilidiAcikMi == true)
    {
        // Doğrudan bu butonu gönderiyoruz
        manager.MankeneGiy(this);
    }
    else
    {
        manager.SatAlmaPaneliniGoster(this);
    }
}
}