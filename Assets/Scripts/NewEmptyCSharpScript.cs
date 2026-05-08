using UnityEngine;

public class DunyaKontrol : MonoBehaviour
{
    [Header("Otomatik Dönüş")]
    public float otomatikHiz = 2f; // Kullanıcı dokunmadığında dönme hızı

    [Header("Kullanıcı Dönüşü")]
    public float hassasiyet = 0.5f; // Touchpad/Mouse sürükleme hızı
    
    private bool kullaniciDokunuyor = false;

    void Update()
    {
        // 1. KULLANICI ETKİLEŞİMİ (Touchpad veya Mouse)
        if (Input.GetMouseButton(0))
        {
            kullaniciDokunuyor = true;
            
            // Farenin/Parmağın yatay hareket miktarını al
            float fareX = Input.GetAxis("Mouse X") * hassasiyet * 10f;
            
            // Dünyayı döndür
            transform.Rotate(Vector3.up, -fareX, Space.World);
        }
        else
        {
            kullaniciDokunuyor = false;
        }

        // 2. OTOMATİK DÖNÜŞ (Kullanıcı dokunmadığında)
        if (!kullaniciDokunuyor)
        {
            transform.Rotate(Vector3.up, otomatikHiz * Time.deltaTime, Space.World);
        }
    }
}