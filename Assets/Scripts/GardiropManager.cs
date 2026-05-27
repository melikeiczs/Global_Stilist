using UnityEngine;

public class GardiropManager : MonoBehaviour
{
    [Header("Manken Bağlantısı")]
    public MankenYonetici mankenYoneticisi; 

    [Header("Satın Alma / Kilit Sistemi")]
    public GameObject satinAlmaPaneli; 

    [Header("Aynı Sahne Panel Yönetimi")]
    [Tooltip("Giydirme yaptığımız tüm arayüzü tutan ana panel")]
    public GameObject gardirobPaneli; 
    
    [Tooltip("Jüri ve puanlamanın olacağı ana panel")]
    public GameObject degerlendirmePaneli;

    private ElbiseVerisi secilenKilitliElbise;

    public void ElbiseSec(ElbiseVerisi secilenElbise)
    {
        if (secilenElbise == null) return;

        // Kilit Kontrolü
        if (secilenElbise.isLocked)
        {
            secilenKilitliElbise = secilenElbise; 
            if (satinAlmaPaneli != null) satinAlmaPaneli.SetActive(true);
            return; 
        }

        // Kıyafet Giydir
        if (mankenYoneticisi != null) mankenYoneticisi.KiyafetGiy(secilenElbise);
    }

    public void KilidiAcVeSatinAl()
    {
        if (secilenKilitliElbise != null)
        {
            secilenKilitliElbise.isLocked = false; 
            if (satinAlmaPaneli != null) satinAlmaPaneli.SetActive(false); 
            if (mankenYoneticisi != null) mankenYoneticisi.KiyafetGiy(secilenKilitliElbise);
        }
    }

    public void SatAlPaneliniKapat() { if (satinAlmaPaneli != null) satinAlmaPaneli.SetActive(false); }

    public void KarakteriDegistir(Sprite yeniKizVucutGorseli)
    {
        if (mankenYoneticisi != null && yeniKizVucutGorseli != null)
            mankenYoneticisi.MankenVucutDegistir(yeniKizVucutGorseli);
    }

    // 🚀 ESKİ IŞINLANMA YERİNE ARTIK BU FONKSİYONU "Tasarımı Bitir" BUTONUNA BAĞLIYORUZ:
    public void DegerlendirmeModunaGec()
    {
        // 1. Giydirme menüsünü gizle
        if (gardirobPaneli != null) gardirobPaneli.SetActive(false);

        // 2. Jüri / Değerlendirme menüsünü ekrana getir
        if (degerlendirmePaneli != null) degerlendirmePaneli.SetActive(true);

        // 3. Jüri sistemi doğrudan mankenYoneticisi.suAnkiElbise üzerinden hangi elbisenin giyildiğine bakabilir!
        Debug.Log($"Aynı sahne içinde değerlendirmeye geçildi. Giymiş olduğu elbise: {mankenYoneticisi.suAnkiElbise.elbiseAdi}");
    }
}