using UnityEngine;

public class MankenYonetici : MonoBehaviour
{
    [Header("Manken Parçaları")]
    public SpriteRenderer vucutRenderer; 
    public SpriteRenderer kiyafetSlot;   

    public void KarakterDegistir(Sprite yeniKarakterGorseli)
    {
        if (vucutRenderer != null)
        {
            vucutRenderer.sprite = yeniKarakterGorseli;
            Debug.Log("Karakter Değişti: " + yeniKarakterGorseli.name);
        }
        else
        {
            Debug.LogError("Hata: VucutRenderer atanmamış!");
        }
    }

    public void KiyafetGiy(ElbiseVerisi secilenElbise)
    {
        if (secilenElbise == null)
        {
            Debug.LogWarning("Mankene giydirilmeye çalışılan elbise verisi boş!");
            return;
        }

        if (kiyafetSlot != null && secilenElbise.elbiseSprite != null)
        {
            kiyafetSlot.sprite = secilenElbise.elbiseSprite;
            kiyafetSlot.color = Color.white; 
            
            // 1. Pozisyonu offset değerine göre hizala
            kiyafetSlot.transform.localPosition = new Vector3(secilenElbise.pozisyonOffset.x, secilenElbise.pozisyonOffset.y, 0f);
            
            // 2. GÜNCELLENDİ: Genişlik (X) ve Yükseklik (Y) bağımsız olarak uygulanıyor
            kiyafetSlot.transform.localScale = new Vector3(secilenElbise.elbiseBoyutu.x, secilenElbise.elbiseBoyutu.y, 1f);
            
            Debug.Log($"{secilenElbise.elbiseAdi} başarıyla giydirildi, hizalandı ve esnetildi!");
        }
        else
        {
            Debug.LogError("Hata: KiyafetSlot atanmamış veya Elbise Sprite'ı eksik!");
        }
    }
}