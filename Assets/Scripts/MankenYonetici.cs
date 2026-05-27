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
        if (yeniElbise == null) return;
        suAnkiElbise = yeniElbise;

        if (mankenElbiseGorseli != null)
        {
            mankenElbiseGorseli.sprite = yeniElbise.elbiseSprite;
            
            // Başlangıçta şeffaf (Alpha = 0) olan slotun görünürlüğünü %100 açıyoruz
            mankenElbiseGorseli.color = new Color(1f, 1f, 1f, 1f);
            
            Debug.Log($"{yeniElbise.elbiseAdi} başarıyla giydirildi ve görünür yapıldı!");
        }
    }

    public void MankenVucutDegistir(Sprite yeniVucut)
    {
        if (mankenVucutGorseli != null && yeniVucut != null)
        {
            mankenVucutGorseli.sprite = yeniVucut;
        }
    }

    public ElbiseVerisi GetGiyilenElbise() 
    { 
        return suAnkiElbise; 
    }

    public void KiyafetCikar()
    {
        if (mankenElbiseGorseli != null)
        {
            suAnkiElbise = null;
            mankenElbiseGorseli.sprite = null;
            mankenElbiseGorseli.color = new Color(1f, 1f, 1f, 0f); // Tekrar şeffaf yap
        }
    }
}