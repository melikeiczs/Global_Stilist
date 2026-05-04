using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem; // Yeni Input Sistemi için gerekli!

public class MusteriHareketi : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float hiz = 5f;
    public float durmaNoktasiX = 0f;

    [Header("Görsel Bağlantılar")]
    public GameObject balon;
    public GameObject siparisButonu;

    [Header("Efekt Ayarları")]
    public float efektSuresi = 0.3f;

    private bool durduMu = false;
    private bool balonGosterildiMi = false; 
    private bool butonAcildiMi = false;
    private Vector3 balonOrijinalOlcegi;

    void Start()
    {
        // 1. Balon Ayarları
        if (balon != null)
        {
            balonOrijinalOlcegi = balon.transform.localScale;
            balon.SetActive(false);
        }

        // 2. Buton Ayarları
        if (siparisButonu != null)
        {
            siparisButonu.SetActive(false);
        }
    }

    void Update()
    {
        // AŞAMA 1: Müşteri sola doğru yürür
        if (!durduMu)
        {
            if (transform.position.x > durmaNoktasiX)
            {
                transform.Translate(Vector3.left * hiz * Time.deltaTime);
            }
            else
            {
                MusteriyiDurdurVeBalonuAc();
            }
        }

        // AŞAMA 2: Balon çıktıktan sonra tıklama bekle
        if (balonGosterildiMi && !butonAcildiMi)
        {
            // Yeni Input System Tıklama Kontrolü (Mouse veya Dokunma fark etmez)
            if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
            {
                ButonuGetir();
            }
        }
    }

    void MusteriyiDurdurVeBalonuAc()
    {
        durduMu = true;
        // Pozisyonu tam hedefe eşitle
        transform.position = new Vector3(durmaNoktasiX, transform.position.y, transform.position.z);
        
        if (balon != null)
        {
            balon.SetActive(true);
            StartCoroutine(BalonBuyumeEfekti());
            balonGosterildiMi = true;
        }
    }

    void ButonuGetir()
    {
        if (siparisButonu != null)
        {
            siparisButonu.SetActive(true);
            butonAcildiMi = true;
            Debug.Log("Yeni sistemle tıklandı ve buton açıldı!");
        }
    }

    IEnumerator BalonBuyumeEfekti()
    {
        if (balon == null) yield break;

        balon.transform.localScale = Vector3.zero;
        float gecenSure = 0f;

        while (gecenSure < efektSuresi)
        {
            gecenSure += Time.deltaTime;
            float oran = gecenSure / efektSuresi;
            balon.transform.localScale = Vector3.Lerp(Vector3.zero, balonOrijinalOlcegi, oran);
            yield return null;
        }
        
        balon.transform.localScale = balonOrijinalOlcegi;
    }
}