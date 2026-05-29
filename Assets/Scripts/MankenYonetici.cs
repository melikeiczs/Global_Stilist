using UnityEngine;
using UnityEngine.UI;

public class MankenYonetici : MonoBehaviour
{
    [Header("Manken Grafik Bileşenleri")]
    public Image mankenVucutGorseli;  // Manken_Buton'un Image'ı
    public Image mankenElbiseGorseli; // Kiyafet_Slotu'nun Image'ı

    [Header("Projedeki Tüm Elbiseler")]
    public ElbiseVerisi[] tumElbiseler;

    [HideInInspector]
    public ElbiseVerisi suAnkiElbise;
    
    [HideInInspector]
    public MankenVerisi suAnkiManken; // Yeni eklenen aktif manken verisi

    // 🚀 YENİ MANKEN DEĞİŞTİRME FONKSİYONU
    public void MankenDegistir(MankenVerisi yeniManken)
    {
        if (yeniManken == null) 
        {
            Debug.LogWarning("Değiştirilmek istenen manken verisi boş!");
            return;
        }

        suAnkiManken = yeniManken;

        if (mankenVucutGorseli != null)
        {
            mankenVucutGorseli.sprite = yeniManken.mankenSprite;
            
            // UI Manken Boyutlandırma ve Konumlandırma Ayarı
            RectTransform rect = mankenVucutGorseli.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.anchoredPosition = yeniManken.pozisyonOffset;
                rect.localScale = new Vector3(yeniManken.mankenBoyutu.x, yeniManken.mankenBoyutu.y, 1f);
            }
            
            Debug.Log($"[MANKEN] Gövde {yeniManken.mankenAdi} olarak değiştirildi ve boyutlandırıldı!");
        }
    }

    public void KiyafetGiy(ElbiseVerisi yeniElbise)
    {
        if (yeniElbise == null) return;
        suAnkiElbise = yeniElbise;

        if (mankenElbiseGorseli != null)
        {
            mankenElbiseGorseli.sprite = yeniElbise.elbiseSprite;
            mankenElbiseGorseli.color = new Color(1f, 1f, 1f, 1f);

            RectTransform rect = mankenElbiseGorseli.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.anchoredPosition = yeniElbise.pozisyonOffset;
                rect.localScale = new Vector3(yeniElbise.elbiseBoyutu.x, yeniElbise.elbiseBoyutu.y, 1f);
            }
        }
    }

    public ElbiseVerisi GetGiyilenElbise() { return suAnkiElbise; }
    public Image GetMankenElbiseGorseli() { return mankenElbiseGorseli; }

    public void KiyafetCikar()
    {
        if (mankenElbiseGorseli != null)
        {
            suAnkiElbise = null;
            mankenElbiseGorseli.sprite = null;
            mankenElbiseGorseli.color = new Color(1f, 1f, 1f, 0f); 
        }
    }
}