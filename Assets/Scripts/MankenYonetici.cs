using UnityEngine;
using UnityEngine.UI;

public class MankenYonetici : MonoBehaviour
{
    [Header("Manken Grafik Bileşenleri")]
    public Image mankenVucutGorseli; // 🚨 KIZIN KENDİ GÖRSELİ (Manke_Buton'un Image'ı)
    public Image mankenElbiseGorseli; // 🚨 ELBİSE SLOTU GÖRSELİ (Kiyafet_Slotu'nun Image'ı)

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
            mankenElbiseGorseli.sprite = yeniElbise.elbiseSprite;
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

    public ElbiseVerisi GetGiyilenElbise() { return suAnkiElbise; }
}