using UnityEngine;
using UnityEngine.UI;

public class MankenYonetici : MonoBehaviour
{
    [Header("Manken Grafik Bileşenleri")]
    public Image mankenVucutGorseli;  // KIZIN KENDİ GÖRSELİ (Manke_Buton'un Image'ı)
    public Image mankenElbiseGorseli; // ELBİSE SLOTU GÖRSELİ (Kiyafet_Slotu'nun Image'ı)

    [Header("Projedeki Tüm Elbiseler")]
    public ElbiseVerisi[] tumElbiseler;

    [HideInInspector]
    public ElbiseVerisi suAnkiElbise;

    public void KiyafetGiy(ElbiseVerisi yeniElbise)
    {
        if (yeniElbise == null) return;
        suAnkiElbise = yeniElbise;

        if (mankenElbiseGorseli != null)
        {
            // 1. Resmi yeni elbisenin sprite'ı ile değiştir
            mankenElbiseGorseli.sprite = yeniElbise.elbiseSprite;

            // 🚀 2. SİHİRLİ DOKUNUŞ: Başlangıçta 0 (şeffaf) olan Alpha değerini %100 görünür yapar
            // (1f, 1f, 1f, 1f) -> Kırmızı, Yeşil, Mavi ve Alpha değerlerinin hepsini maksimuma çeker.
            mankenElbiseGorseli.color = new Color(1f, 1f, 1f, 1f);
            
            Debug.Log($"{yeniElbise.elbiseAdi} giydirildi ve görünürlüğü kodla açıldı!");
        }
    }

    // Alttaki barda bir kıza basıldığında vücut resmini değiştiren fonksiyon
    public void MankenVucutDegistir(Sprite yeniVucut)
    {
        if (mankenVucutGorseli != null && yeniVucut != null)
        {
            mankenVucutGorseli.sprite = yeniVucut;
        }
    }

    // İleride jüri panelinde hangi elbisenin giyildiğini sorgulamak için kullanılacak fonksiyon
    public ElbiseVerisi GetGiyilenElbise() 
    { 
        return suAnkiElbise; 
    }

    // 🎨 İSTEĞE BAĞLI: Eğer gardırobu sıfırlamak veya elbiseyi kızın üstünden tamamen çıkarmak istersen kullanabileceğin fonksiyon
    public void KiyafetCikar()
    {
        if (mankenElbiseGorseli != null)
        {
            suAnkiElbise = null;
            mankenElbiseGorseli.sprite = null;
            // Slotu tekrar tamamen şeffaf (görünmez) yaparız
            mankenElbiseGorseli.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}