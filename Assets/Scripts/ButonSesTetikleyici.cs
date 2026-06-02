using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))] // Bu kodun eklendiği objede kesinlikle buton olmak zorunda
public class ButonSesTetikleyici : MonoBehaviour
{
    void Start()
    {
        Button buton = GetComponent<Button>();
        if (buton != null)
        {
            // Butona tıklandığında müzik yöneticisindeki o genel sesi çalmasını söylüyoruz
            buton.onClick.AddListener(SesiCal);
        }
    }

    void SesiCal()
    {
        // Sahnedeki müzik yöneticisini bulup içindeki ses çalma fonksiyonunu tetikliyoruz
        MuzikYoneticiKodu muzikMudur = Object.FindFirstObjectByType<MuzikYoneticiKodu>();
        if (muzikMudur != null)
        {
            muzikMudur.GenelButonSesiCal();
        }
    }
}  