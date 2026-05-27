using UnityEngine;
using TMPro;

public class DiyalogYoneticisi : MonoBehaviour
{
    [System.Serializable]
    public class SeviyeKriteri
    {
        [TextArea(3, 5)]
        public string siparisMetni;
        public string hedefKonsept; // Örn: Mini Elbise, Tulum, Uzun Abiye
        public string hedefZaman;   // Örn: Gece, Sokak Partisi, Lüks Düğün
        public string hedefRenk;    // Örn: Kırmızı, Mor, Altın Sarısı
    }

    [Header("UI Elemanlari")]
    [Tooltip("Hierarchy panelindeki 'GirisYazisi' objesini veya direkt 'Mor_Konusma_Balonu'nu buraya surukleyin.")]
    public GameObject konusmaMetniObjesi; 

    [Header("Istanbul Seviye Siparisleri")]
    public SeviyeKriteri[] istanbulSiparisleri = new SeviyeKriteri[3] 
    {
        // Seviye 1
        new SeviyeKriteri { 
            siparisMetni = "Merhaba! İstanbul Boğazı'nda şık bir gece davetine katılacağım. Benim için kırmızı renkte, iddialı bir mini elbise tasarlayabilir misin?",
            hedefKonsept = "Mini Elbise",
            hedefZaman = "Gece",
            hedefRenk = "Kırmızı"
        },
        // Seviye 2
        new SeviyeKriteri { 
            siparisMetni = "Selam! Galata'da çok cool bir sokak partisi var bu akşam. Enerjik hissettirecek mor tonlarında, rahat ama tarz bir tulum dikmeni istiyorum!",
            hedefKonsept = "Tulum",
            hedefZaman = "Sokak Partisi",
            hedefRenk = "Mor"
        },
        // Seviye 3
        new SeviyeKriteri { 
            siparisMetni = "İyi günler! Çırağan Sarayı'nda çok lüks bir düğün davetine davetliyim. Sarayın ruhuna yakışacak altın sarısı renklerinde, görkemli bir uzun abiye tasarlayabilir misin?",
            hedefKonsept = "Uzun Abiye",
            hedefZaman = "Lüks Düğün",
            hedefRenk = "Altın Sarısı"
        }
    };

    private TMP_Text _yaziBileseni; 
    [HideInInspector] public int aktifSeviyeNo = 1; // GardiropManager'ın hangi seviyede olduğumuzu bilmesi için

    void Start()
    {
        if (konusmaMetniObjesi != null)
        {
            _yaziBileseni = konusmaMetniObjesi.GetComponentInChildren<TMP_Text>();
        }

        // Başlangıç olarak Seviye 1'i yüklüyoruz.
        SeviyeyiYukle(1); 
    }

    public void SeviyeyiYukle(int levelNo)
    {
        aktifSeviyeNo = levelNo;
        int index = levelNo - 1;

        if (index >= 0 && index < istanbulSiparisleri.Length)
        {
            if (_yaziBileseni != null)
            {
                _yaziBileseni.text = istanbulSiparisleri[index].siparisMetni;
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

    // O an aktif olan seviyenin hedeflerini GardiropManager'a gönderen yardımcı fonksiyon
    public SeviyeKriteri GetAktifSeviyeKriteri()
    {
        int index = aktifSeviyeNo - 1;
        if (index >= 0 && index < istanbulSiparisleri.Length)
        {
            return istanbulSiparisleri[index];
        }
        return null;
    }
}