using UnityEngine;
using UnityEngine.SceneManagement; // Sahneler arası geçişi sağlayan kütüphane

public class MenuManager : MonoBehaviour
{
    // Butona basıldığında çalışacak olan fonksiyon
    public void SahneyeGec()
    {
        // "ShopScene" yazan yere gitmek istediğin sahnenin tam adını yazmalısın.
        // Eğer sahnenin adını farklı kaydettiysen, buradaki ismi de ona göre değiştir.
        SceneManager.LoadScene("ShopScene"); 
    }
}
