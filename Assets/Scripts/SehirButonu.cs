using UnityEngine;
using UnityEngine.SceneManagement;

public class SehirButonu : MonoBehaviour
{
    [Header("Şehir Ayarları")]
    public string gitmekIstedigimSahneAdi;
    public bool kilitliMi = true; // Tikli olursa kilitli başlar

    [Header("UI Elemanları")]
    public GameObject kilitGorseliObjesi; // Sahnede butonun içine attığın kilit resmi

    void Start()
    {
        // Oyun açıldığında kilidin açık mı kapalı mı olduğunu görsel olarak güncelle
        KilitDurumunuGuncelle();
    }

    public void KilitDurumunuGuncelle()
    {
        if (kilitGorseliObjesi != null)
        {
            // Eğer şehir kilitliyse kilit resmi görünür (true), açık ise gizlenir (false)
            kilitGorseliObjesi.SetActive(kilitliMi);
        }
    }

    // Butona tıklandığında bu fonksiyon çalışacak
    public void SehreGit()
    {
        if (!kilitliMi)
        {
            // Kilit açık ise o şehre ait giydirme sahnesini yükle
            SceneManager.LoadScene(gitmekIstedigimSahneAdi);
        }
        else
        {
            Debug.Log(gameObject.name + " şehri henüz kilitli! Önceki bölümleri tamamla.");
        }
    }
}