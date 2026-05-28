using UnityEngine;

public class MuzikYoneticiKodu : MonoBehaviour
{
    private static MuzikYoneticiKodu instance;

    void Awake()
    {
        // Eğer sahneler arası geçişte zaten çalan bir müzik yöneticisi varsa, ikincisinin oluşmasını engeller
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Bu obje sahneler değişse bile SİLİNMEZ!
        }
        else
        {
            Destroy(gameObject); // Eğer haritadan gardıroba geri dönülürse mükerrer (çift) müziği yok eder
        }
    }
}