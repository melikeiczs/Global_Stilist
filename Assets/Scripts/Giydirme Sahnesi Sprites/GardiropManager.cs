using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçişi için şart

public class GardiropManager : MonoBehaviour
{
    [Header("Manken Bağlantısı")]
    [Tooltip("Hiyerarşideki en dışta duran Manken_Canvas objesini buraya sürükleyin.")]
    public MankenYonetici mankenYoneticisi; 

    [Header("Sahne Geçiş Ayarı")]
    [Tooltip("Değerlendir butonuna basınca açılacak sahnenin tam adı")]
    public string degerlendirmeSahneAdi = "DegerlendirmeSahnesi";

    /// <summary>
    /// Elbise butonları veya otomatik buton scriptleri bu fonksiyonu çağırır.
    /// </summary>
    public void ElbiseSec(ElbiseVerisi secilenElbise)
    {
        if (secilenElbise == null)
        {
            Debug.LogWarning("Gardırop Manager: Seçilen elbise verisi boş!");
            return;
        }

        if (mankenYoneticisi != null)
        {
            mankenYoneticisi.KiyafetGiy(secilenElbise);
            Debug.Log($"Gardırop Manager: {secilenElbise.elbiseAdi} başarıyla mankene gönderildi.");
        }
        else
        {
            Debug.LogError("Hata: Gardırop Manager üzerinde 'Manken Yoneticisi' yuvası BOŞ!");
        }
    }

    /// <summary>
    /// Jüriye git butonuna bağlayacağın fonksiyon.
    /// </summary>
    public void MankeniKaydetVeIsınla()
    {
        if (mankenYoneticisi != null)
        {
            SceneManager.LoadScene(degerlendirmeSahneAdi);
        }
        else
        {
            Debug.LogError("Hata: Manken bulunamadığı için sahne geçişi engellendi.");
        }
    }
}