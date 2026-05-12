using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SehirEtkilesim : MonoBehaviour
{
    public GameObject bilgiPaneli;
    public TextMeshProUGUI sehirBaslik;
    public TextMeshProUGUI sehirDetay;

    void Update()
    {
       void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        // 1. ADIM: Mouse pozisyonundan bir lazer fırlat
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 2. ADIM: Bu lazer bir şeye çarptı mı?
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("DÜNYA ÜZERİNDE BİR ŞEYE ÇARPTI: " + hit.collider.gameObject.name);
            
            SehirNoktasi nokta = hit.collider.GetComponent<SehirNoktasi>();
            if (nokta != null)
            {
                Debug.Log("BAŞARILI: Bir şehir noktasına tıkladın!");
                PanelAc(nokta.sehirAdi, nokta.sehirBilgisi);
            }
        }
        else
        {
            Debug.Log("HATA: Lazer hiçbir şeye çarpmadı. Collider'ları kontrol et!");
        }

        // 3. ADIM: UI (Buton) üzerinde miyiz?
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Şu an bir UI (Buton/Panel) elemanının üzerindesin.");
        }
    }
}
    }

    public void PanelAc(string ad, string bilgi)
    {
        bilgiPaneli.SetActive(true);
        sehirBaslik.text = ad;
        sehirDetay.text = bilgi;
    }

    public void ShopSahneneGit() { SceneManager.LoadScene("Scenes/ShopScene"); }
    public void PanelKapat() { bilgiPaneli.SetActive(false); }
}