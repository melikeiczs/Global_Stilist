using UnityEngine;
using UnityEngine.UI;

public class MankenYonetici : MonoBehaviour
{
    [Header("Manken Grafik Bileşenleri")]
    public Image mankenVucutGorseli;  // Manke_Buton'un Image'ı
    public Image mankenElbiseGorseli; // Kiyafet_Slotu'nun Image'ı

    [Header("Projedeki Tüm Elbiseler")]
    public ElbiseVerisi[] tumElbiseler;

    [HideInInspector]
    public ElbiseVerisi suAnkiElbise;

    public void KiyafetGiy(ElbiseVerisi yeniElbise)
    {
        if (yeniElbise == null) 
        {
            Debug.LogWarning("Mankene giydirilmek istenen elbise verisi boş (null)!");
            return;
        }

        // 🚀 KRİTİK NOKTA: Jürinin okuyacağı elbiseyi hafızaya kesin olarak kaydediyoruz
        suAnkiElbise = yeniElbise;

        if (mankenElbiseGorseli != null)
        {
            mankenElbiseGorseli.sprite = yeniElbise.elbiseSprite;
            
            // Başlangıçta şeffaf (Alpha = 0) olan slotun görünürlüğünü %100 açıyoruz
            mankenElbiseGorseli.color = new Color(1f, 1f, 1f, 1f);
            
            // Jürinin kriterleri doğru okuyup okuyamayacağını Console panelinden test etmek için log ekledik:
            Debug.Log($"[MANKEN] {yeniElbise.elbiseAdi} başarıyla giydirildi! Kriterler -> Konsept: {yeniElbise.konsept}, Zaman: {yeniElbise.zaman}, Renk: {yeniElbise.renk}");
        }
    }

    public void MankenVucutDegistir(Sprite yeniVucut)
    {
        if (mankenVucutGorseli != null && yeniVucut != null)
        {
            mankenVucutGorseli.sprite = yeniVucut;
        }
    }

    // 🚀 JÜRİNİN ÇAĞIRDIĞI KRİTİK FONKSİYON
    public ElbiseVerisi GetGiyilenElbise() 
    { 
        if (suAnkiElbise == null)
        {
            Debug.LogWarning("GetGiyilenElbise çağrıldı ancak manken üzerinde şu an hiçbir elbise yok!");
        }
        return suAnkiElbise; 
    }

    public void KiyafetCikar()
    {
        if (mankenElbiseGorseli != null)
        {
            suAnkiElbise = null;
            mankenElbiseGorseli.sprite = null;
            mankenElbiseGorseli.color = new Color(1f, 1f, 1f, 0f); // Tekrar şeffaf yap
            Debug.Log("[MANKEN] Kıyafet çıkarıldı.");
        }
    }
}