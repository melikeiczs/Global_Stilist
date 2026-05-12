using UnityEngine;

public class DunyaKontrol : MonoBehaviour
{
    public float otomatikHiz = 15f; 
    public float hassasiyet = 5f;

    void Update()
    {
        // 1. OTOMATİK DÖNÜŞ (Her zaman çalışır)
        transform.Rotate(Vector3.up, otomatikHiz * Time.deltaTime, Space.Self);

        // 2. TIKLAYIP SÜRÜKLEME (MacBook Touchpad Sol Tık)
        if (Input.GetMouseButton(0))
        {
            float fareX = Input.GetAxis("Mouse X") * hassasiyet;
            transform.Rotate(Vector3.up, -fareX, Space.World);
        }
    }
}