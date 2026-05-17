using UnityEngine;

[ExecuteInEditMode] // Oyunu başlatmadan editörde de düzgün dursun diye
public class KamerayaHizala : MonoBehaviour
{
    private Transform anaKamera;

    void Start()
    {
        if (Camera.main != null)
        {
            anaKamera = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        if (anaKamera != null)
        {
            // Canvas'ın her zaman kameraya dümdüz bakmasını sağlar.
            // Böylece ne dünyaya saplanır ne de eğik durur.
            transform.LookAt(transform.position + anaKamera.forward);
        }
    }
}