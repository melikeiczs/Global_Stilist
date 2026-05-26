using UnityEngine;
using UnityEngine.UI; // UI Elemanlarını (Image) kontrol edebilmek için şart!

public class MankenYonetici : MonoBehaviour
{
    [Header("Manken Parçaları (UI Sürümü)")]
    public Image vucutRenderer; // Kızın çıplak resmi (Manken_Buton)
    public Image kiyafetSlot;   // Elbisenin giydirileceği yer (Kiyafet_Slotu)

    // GardiropManager veya DegerlendirmeYonetici'nin elbiseyi okuyabilmesi için hafıza değişkeni:
    private ElbiseVerisi uzerindekiElbise; 

    void Start()
    {
        // UI Objesi (Canvas) de olsa sahneler arası ışınlanmasını sağlıyoruz.
        // NOT: Manken_Canvas hiyerarşide en dışta (bağımsız) durmalıdır.
        DontDestroyOnLoad(gameObject);
    }

    public void KarakterDegistir(Sprite yeniKarakterGorseli)
    {
        if (vucutRenderer != null && yeniKarakterGorseli != null)
        {
            vucutRenderer.sprite = yeniKarakterGorseli;
            Debug.Log("Manken Karakteri Değişti: " + yeniKarakterGorseli.name);
        }
        else
        {
            Debug.LogError("Hata: VucutRenderer atanmamış veya Yeni Görsel Eksik!");
        }
    }

    public void KiyafetGiy(ElbiseVerisi secilenElbise)
    {
        if (secilenElbise == null)
        {
            Debug.LogWarning("Mankene giydirilmeye çalışılan elbise verisi boş!");
            return;
        }

        // Seçilen elbiseyi hafızaya kaydediyoruz (Jüri sahnesinde okumak için)
        uzerindekiElbise = secilenElbise;

        if (kiyafetSlot != null && secilenElbise.elbiseSprite != null)
        {
            // 1. Elbise görselini slota yerleştir
            kiyafetSlot.sprite = secilenElbise.elbiseSprite;
            
            // 2. Başlangıçta şeffaf (Alpha: 0) yaptığımız slotu tamamen görünür (Alpha: 1) yap!
            kiyafetSlot.color = Color.white; 
            
            // 3. UI için Pozisyon hizalaması (RectTransform offset değerleri)
            kiyafetSlot.rectTransform.anchoredPosition = new Vector2(secilenElbise.pozisyonOffset.x, secilenElbise.pozisyonOffset.y);
            
            // 4. UI için Boyutlandırma (Genişlik ve Yükseklik)
            kiyafetSlot.rectTransform.localScale = new Vector3(secilenElbise.elbiseBoyutu.x, secilenElbise.elbiseBoyutu.y, 1f);
            
            Debug.Log($"{secilenElbise.elbiseAdi} UI üzerinde başarıyla giydirildi, hizalandı ve ölçeklendi!");
        }
        else
        {
            Debug.LogError("Hata: KiyafetSlot atanmamış veya Elbise Sprite'ı eksik!");
        }
    }

    // --- DIŞARIDAN OKUMA KAPISI ---
    // Jüri sahnesindeki DegerlendirmeYonetici bu fonksiyon sayesinde kızın üzerindeki elbiseyi canlı olarak çeker.
    public ElbiseVerisi GetGiyilenElbise()
    {
        return uzerindekiElbise;
    }
}