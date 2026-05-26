using UnityEngine;
using UnityEngine.UI; // Buton ve Image bileşenleri için şart

public class KiyafetButonu : MonoBehaviour
{
    [Header("Elbise Bilgisi")]
    public ElbiseVerisi bagliElbise; // Bu butonun temsil ettiği ScriptableObject

    [Header("Görsel Ayarı")]
    public Image butonIkonu; // Butonun üzerindeki küçük kıyafet resmi

    private GardiropManager gardiropManager;

    void Start()
    {
        // Sahnede bulunan GardiropManager'ı otomatik olarak bulur
        gardiropManager = Object.FindFirstObjectByType<GardiropManager>();

        // Eğer butona elinden bir elbise verisi bağladıysan, butonun ikonu otomatik o elbise olur
        if (bagliElbise != null && butonIkonu != null)
        {
            butonIkonu.sprite = bagliElbise.elbiseSprite;
        }

        // Butona tıklandığında çalışacak fonksiyonu koldan otomatik bağlıyoruz
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(ButonaTiklandi);
        }
    }

    void ButonaTiklandi()
    {
        if (gardiropManager != null && bagliElbise != null)
        {
            // Yeni GardiropManager fonksiyonumuzu tetikliyoruz!
            gardiropManager.ElbiseSec(bagliElbise);
        }
        else
        {
            Debug.LogError("Kıyafet Butonu: GardiropManager veya Bağlı Elbise eksik!");
        }
    }
}