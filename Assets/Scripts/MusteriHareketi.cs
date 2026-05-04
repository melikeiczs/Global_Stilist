using UnityEngine;
using System.Collections; // Efekt (Coroutine) için gerekli

public class MusteriHareketi : MonoBehaviour
{
    [Header("Ayarlar")]
    
    public float hiz = 5f;
    public float durmaNoktasiX = 0f;

    [Header("Bağlantılar")]
    public GameObject balon; // Hiyerarşideki BAĞIMSIZ balonu buraya sürükle
    public GameObject siparisButonu;

    [Header("Efekt Ayarları")]
    public bool efektliAcilsin = true; // Müfettişten açıp kapatabilirsin
    public float efektSuresi = 0.3f;

    private bool durduMu = false;
    private Vector3 balonOrijinalOlcegi;

    void Start()
    {
        if (balon != null)
        {
            balonOrijinalOlcegi = balon.transform.localScale; // Balonun normal boyutu kaydet
            balon.SetActive(false); // Oyun başı kapalı
        }
    }

    void Update()
    {
        // Kız sağda olduğu sürece sola git
        if (!durduMu && transform.position.x > durmaNoktasiX)
        {
            transform.Translate(Vector3.left * hiz * Time.deltaTime);
        }
        // Kız durma noktasına ulaştığı an
        else if (!durduMu)
        {
            durduMu = true;
            
            // Pozisyonu tam hedefe kitle (kayma olmasın)
            transform.position = new Vector3(durmaNoktasiX, transform.position.y, transform.position.z);
            
            // Balonu tetikle
            BalonuGoster();
        }
    }

    void BalonuGoster()
    {
        if (balon == null) return;

        if (efektliAcilsin)
        {
            // Havalı efekt (büyüyerek gelme) başlasın
            StartCoroutine(BalonBuyumeEfekti());
        }
        else
        {
            // Ya da çat diye aniden çıksın
            balon.SetActive(true);
        }
        if (siparisButonu != null) siparisButonu.SetActive(true);
    }

    // Balonun sıfırdan normal boyuta büyüme efekti
    IEnumerator BalonBuyumeEfekti()
    {
        balon.transform.localScale = Vector3.zero; // Önce balonu noktaya kadar küçült
        balon.SetActive(true); // Aktif et (ama boyutu 0 olduğu için görünmez)

        float gecenSure = 0f;

        while (gecenSure < efektSuresi)
        {
            gecenSure += Time.deltaTime;
            float ilerleme = gecenSure / efektSuresi;

            // Boyutu yumuşak bir şekilde artır (0'dan orijinal boyuta)
            balon.transform.localScale = Vector3.Lerp(Vector3.zero, balonOrijinalOlcegi, ilerleme);
            
            yield return null; // Bir sonraki kareye kadar bekle
        }

        // Garanti olsun diye tam boyuta sabitle
        balon.transform.localScale = balonOrijinalOlcegi;
    }
}