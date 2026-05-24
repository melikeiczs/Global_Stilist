using UnityEngine;
using TMPro;

public class DiyalogYoneticisi : MonoBehaviour
{
    [Header("UI Elemanlari")]
    [Tooltip("Hierarchy panelindeki 'GirisYazisi' objesini veya direkt 'Mor_Konusma_Balonu'nu buraya surukleyin.")]
    public GameObject konusmaMetniObjesi; 

    private TMP_Text _yaziBileseni; 

    [Header("Istanbul Seviye Siparisleri")]
    [TextArea(3, 5)]
    public string[] istanbulSiparisleri = new string[3] 
    {
        // Seviye 1 -> Kriterler: Boğaz (Mekan), Kırmızı (Renk), Mini Elbise (Tür)
        "Merhaba! İstanbul Boğazı'nda şık bir gece davetine katılacağım. Benim için kırmızı renkte, iddialı bir mini elbise tasarlayabilir misin?",
        
        // Seviye 2 -> Kriterler: Galata (Mekan), Mor (Renk), Tulum (Tür)
        "Selam! Galata'da çok cool bir sokak partisi var bu akşam. Enerjik hissettirecek mor tonlarında, rahat ama tarz bir tulum dikmeni istiyorum!",
        
        // Seviye 3 -> Kriterler: Çırağan Sarayı (Mekan), Altın Sarısı (Renk), Uzun Abiye (Tür)
        "İyi günler! Çırağan Sarayı'nda çok lüks bir düğün davetine davetliyim. Sarayın ruhuna yakışacak altın sarısı renklerinde, görkemli bir uzun abiye tasarlayabilir misin?"
    };

    void Start()
    {
        // Obje suruklendiyse icinde veya altindaki cocuklarda TextMeshPro bileseni arar
        if (konusmaMetniObjesi != null)
        {
            _yaziBileseni = konusmaMetniObjesi.GetComponentInChildren<TMP_Text>();
        }

        // TEST AMACLI: Sahne acildiginda direkt 1. Seviyeyi yukler.
        // Diger seviyeleri test etmek icin buradaki 1 sayisini 2 veya 3 yapabilirsin.
        SeviyeyiYukle(1); 
    }

    /// <summary>
    /// Belirtilen seviyenin siparis diyalogunu ekrana basar.
    /// </summary>
    /// <param name="levelNo">Yuklenmek istenen seviye numarasi (1, 2 veya 3)</param>
    public void SeviyeyiYukle(int levelNo)
    {
        // Yazilim dillerinde listeler 0'dan baslar (Level 1 = Index 0)
        int index = levelNo - 1;

        // Listenin sinirlari icinde miyiz kontrolü (Hata onleyici)
        if (index >= 0 && index < istanbulSiparisleri.Length)
        {
            if (_yaziBileseni != null)
            {
                _yaziBileseni.text = istanbulSiparisleri[index];
            }
            else
            {
                Debug.LogError("Hata: Konusma Metni Objesi kisminda TextMeshPro bileseni bulunamadi!");
            }
        }
        else
        {
            Debug.LogError("Hata: " + levelNo + ". seviye icin tanimlanmis bir diyalog yok!");
        }
    }
}