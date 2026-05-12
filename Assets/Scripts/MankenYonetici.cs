using UnityEngine;

public class MankenYonetici : MonoBehaviour
{
    [Header("Manken Parçaları")]
    public SpriteRenderer vucutRenderer; // Ana kızın vücut resmi
    public SpriteRenderer kiyafetSlot;   // Kıyafetlerin görüneceği slot (Kızın alt objesi)

    /// <summary>
    /// Butona basıldığında karakteri (kızı) değiştiren fonksiyon.
    /// </summary>
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

    /// <summary>
    /// Butona basıldığında kıyafeti giydiren fonksiyon.
    /// </summary>
    public void KiyafetGiy(Sprite yeniKiyafet)
    {
        if (kiyafetSlot != null)
        {
            kiyafetSlot.sprite = yeniKiyafet;
            kiyafetSlot.color = Color.white; // Varsa eski renk etkilerini temizler
            Debug.Log(yeniKiyafet.name + " giydirildi!");
        }
        else
        {
            Debug.LogError("Hata: KiyafetSlot atanmamış!");
        }
    }
}