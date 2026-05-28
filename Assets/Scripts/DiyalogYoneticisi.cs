using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DiyalogYoneticisi : MonoBehaviour
{
    [System.Serializable]
    public class SeviyeKriteri
    {
        public int siparisID; 
        [TextArea(3, 5)]
        public string siparisMetni;
    }

    [Header("UI Elemanlari")]
    [Tooltip("Hiyerarşide yeni oluşturduğunuz GirisYazisi (TextMeshPro) nesnesini buraya sürükleyin.")]
    public TextMeshProUGUI diyalogYaziBileseni; // Doğrudan bileşeni istiyoruz ki hata riski sıfırlansın

    [HideInInspector] 
    public List<SeviyeKriteri> istanbulSiparisleri = new List<SeviyeKriteri>();

    void Awake()
    {
        // Önceki denemelerden kalan hatalı hafızayı temizlemek adına İstanbul verilerini sıfırlıyoruz
        PlayerPrefs.DeleteKey("AktifSiparisID");
        PlayerPrefs.Save();

        istanbulSiparisleri.Clear();

        istanbulSiparisleri.Add(new SeviyeKriteri { 
            siparisID = 1, 
            siparisMetni = "Merhaba! Çırağan Sarayı'nda çok lüks bir ödül törenine katılacağım. Sarayın görkemine yakışacak, ışıltılı mor renklerinde şık bir uzun abiye tasarlayabilir misin?" 
        });

        istanbulSiparisleri.Add(new SeviyeKriteri { 
            siparisID = 2, 
            siparisMetni = "Selam! Karaköy'de arkadaşlarımla gece kulübüne eğlenmeye gideceğiz. Siyah renkli, beli tokalı, çok tarz bir mini elbise istiyorum!" 
        });

        istanbulSiparisleri.Add(new SeviyeKriteri { 
            siparisID = 3, 
            siparisMetni = "İyi günler, Ortaköy'de bir doğum günü partisine davetliyim. Yaz akşamına uygun, mor renklerde tatlı bir askılı mini elbise hazırlayabilir misin?" 
        });

        istanbulSiparisleri.Add(new SeviyeKriteri { 
            siparisID = 4, 
            siparisMetni = "Merhaba! Kadıköy'de iddialı bir akşam randevum var. Dikkat çekici, kırmızı renklerde ve çapraz omuz bağlamalı modern bir mini elbise arıyorum." 
        });

        istanbulSiparisleri.Add(new SeviyeKriteri { 
            siparisID = 5, 
            siparisMetni = "Selam! Moda sahilinde şık bir sokak partisi var. Enerjik ve havalı hissettirecek, mor tonlarında halter yaka bir mini elbise dikmeni istiyorum!" 
        });
    }

    void Start()
    {
        SiparisBelirle();
    }

    public void SiparisBelirle()
    {
        int tamamlananSiparisSayisi = PlayerPrefs.GetInt("Istanbul_Tamamlanan_Siparis", 0);

        if (tamamlananSiparisSayisi >= 3)
        {
            PlayerPrefs.SetInt("NewYork_Kilit_Acik", 1); 
            PlayerPrefs.Save();
            
            if (diyalogYaziBileseni != null)
            {
                diyalogYaziBileseni.text = "Tebrikler! İstanbul'daki tüm siparişleri tamamladın. New York haritası açıldı!";
            }
            return;
        }

        string tamamlananlar = PlayerPrefs.GetString("Istanbul_Tamamlanan_IDler", "");
        List<SeviyeKriteri> secilebilirSiparisler = new List<SeviyeKriteri>();
        
        foreach (var siparis in istanbulSiparisleri)
        {
            if (!tamamlananlar.Contains(siparis.siparisID.ToString()))
            {
                secilebilirSiparisler.Add(siparis);
            }
        }

        if (secilebilirSiparisler.Count > 0)
        {
            int rastgeleIndex = Random.Range(0, secilebilirSiparisler.Count);
            SeviyeKriteri secilen = secilebilirSiparisler[rastgeleIndex];

            PlayerPrefs.SetInt("AktifSiparisID", secilen.siparisID);
            PlayerPrefs.Save();

            if (diyalogYaziBileseni != null)
            {
                diyalogYaziBileseni.text = secilen.siparisMetni;
                Debug.Log("Ekrana yazılan sipariş ID: " + secilen.siparisID);
            }
        }
        else
        {
            if (diyalogYaziBileseni != null && istanbulSiparisleri.Count > 0)
            {
                PlayerPrefs.SetInt("AktifSiparisID", istanbulSiparisleri[0].siparisID);
                diyalogYaziBileseni.text = istanbulSiparisleri[0].siparisMetni;
            }
        }
    }
}