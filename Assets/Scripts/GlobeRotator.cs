using UnityEngine;

public class DunyaKontrol : MonoBehaviour
{
    [Header("Donus Ayarlari")]
    public float otomatikHiz = 2.0f; 
    public float hassasiyet = 5.0f;   

    private bool kullaniciDokunuyor = false;

    // Sadece BIR TANE Update fonksiyonu olmali:
    void Update()
    {
        // 1. Kullanıcı Kontrolü
        if (Input.GetMouseButton(0))
        {
            kullaniciDokunuyor = true;
            float fareX = Input.GetAxis("Mouse X") * hassasiyet;
            transform.Rotate(Vector3.up, -fareX, Space.World);
        }
        else
        {
            kullaniciDokunuyor = false;
        }

        // 2. Otomatik Dönüş
        if (!kullaniciDokunuyor)
        {
            transform.Rotate(Vector3.up, otomatikHiz * Time.deltaTime, Space.World);
        }
    }
}