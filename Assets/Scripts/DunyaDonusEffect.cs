using UnityEngine;
using UnityEngine.UI; // RawImage bileşeni için bu satır şart!

public class DunyaDonusEffect : MonoBehaviour
{
    [Header("Dönüş Ayarları")]
    [Range(0, 0.5f)] 
    public float donusHizi = 0.05f; // Dünyanın dönüş hızı

    private RawImage dunyaResmi;
    private float xKayma = 0f;

    void Start()
    {
        // Objedeki RawImage bileşenini buluyoruz
        dunyaResmi = GetComponent<RawImage>();

        if (dunyaResmi == null)
        {
            Debug.LogError("Hata: Bu scripti Raw Image olan objeye (Dunya_Harita) atmalısın!");
        }
    }

    void Update()
    {
        if (dunyaResmi != null)
        {
            // Zamanla X koordinatını artırıyoruz
            xKayma += Time.deltaTime * donusHizi;

            // Eğer değer 1'i geçerse başa sar (Sonsuz döngü için)
            if (xKayma > 1f) xKayma -= 1f;

            // Raw Image'ın uvRect değerini güncelliyoruz
            // (x, y, genişlik, yükseklik)
            dunyaResmi.uvRect = new Rect(xKayma, 0, 1, 1);
        }
    }
}