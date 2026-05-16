using UnityEngine;
using UnityEngine.SceneManagement; // Sahneleri yönetmek için bu kütüphane şart!

public class SahneYoneticisi : MonoBehaviour
{
    /// <summary>
    /// Butona basıldığında hedef sahneye geçişi sağlar.
    /// </summary>
    /// <param name="sahneAdi">Gidilecek sahnenin tam adı</param>
    public void SahneyeGit(string sahneAdi)
    {
        // Sahneyi yükle
        SceneManager.LoadScene(sahneAdi);
        Debug.Log(sahneAdi + " sahnesine gidiliyor...");
    }
}