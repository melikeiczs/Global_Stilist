using UnityEngine;
using UnityEngine.UI;

public class KiyafetButonu : MonoBehaviour
{
    [Header("Elbise Bilgisi")]
    public ElbiseVerisi bagliElbise; 

    [Header("Görsel Ayarı")]
    public Image butonIkonu; 

    [Header("Kilit Ayarı")]
    public GameObject kilitIkonu; // Butonun üzerindeki kilit resmi objesi

    void Start()
    {
        // 1. Kıyafet resmini yükle
        if (bagliElbise != null && butonIkonu != null)
        {
            butonIkonu.sprite = bagliElbise.elbiseSprite;
        }

        // 2. Kilit durumuna göre kilit resmini aç veya kapat
        KilitGorseliniGuncelle();
    }

    // Bu fonksiyonu hem başlangıçta hem de satın alma bittiğinde çağıracağız
    public void KilitGorseliniGuncelle()
    {
        if (bagliElbise != null && kilitIkonu != null)
        {
            // Eğer elbise kilitliyse ikon GÖRÜNÜR, kilitli değilse GÖRÜNMEZ olur.
            kilitIkonu.SetActive(bagliElbise.isLocked);
        }
    }
}