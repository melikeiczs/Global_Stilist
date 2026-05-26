using UnityEngine;
using TMPro;

public class DegerlendirmeYonetici : MonoBehaviour
{
    [Header("Jürinin Bu Bölümdeki İstekleri")]
    public string hedefRenk = "Siyah";
    public string hedefZaman = "Gece";
    public string hedefKonsept = "Parti";

    [Header("Arayüz (UI) Elemanları")]
    public TextMeshProUGUI juriIstekYazisi;  // Jürinin ne istediğini yazan yer
    public TextMeshProUGUI oyuncuSecimYazisi;// Oyuncunun ne getirdiğini yazan yer
    public TextMeshProUGUI puanYazisi;        // Toplam puanın yazacağı yer

    void Start()
    {
        // 1. Ekrana jürinin ne istediğini yazdıralım
        if (juriIstekYazisi != null)
        {
            juriIstekYazisi.text = $"Jürinin İsteği: {hedefRenk} | {hedefZaman} | {hedefKonsept}";
        }

        // 2. Diğer sahneden ışınlanıp gelen MankenYonetici objesini sahnede arayıp buluyoruz
        MankenYonetici isınlanıpGelenManken = FindObjectOfType<MankenYonetici>();

        if (isınlanıpGelenManken != null)
        {
            // Mankenin üzerindeki elbiseyi canlı olarak çekiyoruz
            ElbiseVerisi giyilenElbise = isınlanıpGelenManken.GetGiyilenElbise();

            if (giyilenElbise != null)
            {
                // Oyuncunun getirdiği elbiseyi ekranda yazdıralım
                if (oyuncuSecimYazisi != null)
                {
                    oyuncuSecimYazisi.text = $"Senin Kombinin: {giyilenElbise.elbiseAdi} ({giyilenElbise.renk}, {giyilenElbise.zaman}, {giyilenElbise.konsept})";
                }

                // 3. PUANLAMA VAKTİ!
                PuanıHesapla(giyilenElbise.renk.ToString(), giyilenElbise.zaman.ToString(), giyilenElbise.konsept.ToString());
            }
            else
            {
                if (oyuncuSecimYazisi != null) oyuncuSecimYazisi.text = "Manken geldi ama üzerinde kıyafet yok!";
                if (puanYazisi != null) puanYazisi.text = "JÜRI PUANI: 0 / 100";
            }
        }
        else
        {
            Debug.LogError("Hata: Diğer sahneden ışınlanması gereken Manken bulunamadı! Gardırop sahnesinden başlamalısın.");
        }
    }

    void PuanıHesapla(string renk, string zaman, string konsept)
    {
        int toplamPuan = 0;

        // Kriter eşleşmelerine göre puan ekle
        if (renk == hedefRenk) toplamPuan += 30;
        if (zaman == hedefZaman) toplamPuan += 30;
        if (konsept == hedefKonsept) toplamPuan += 30;

        if (toplamPuan == 0) toplamPuan = 10;
        else toplamPuan += 10;

        if (puanYazisi != null)
        {
            puanYazisi.text = $"JÜRI PUANI: {toplamPuan} / 100";
        }
    }
}