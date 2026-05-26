using UnityEngine;
using UnityEngine.UI;

public class KiyafetButonu : MonoBehaviour
{
    [Header("Elbise Bilgisi")]
    public ElbiseVerisi bagliElbise; 

    [Header("Görsel Ayarı")]
    public Image butonIkonu; 

    private GardiropManager gardiropManager;

    void Start()
    {
        gardiropManager = Object.FindFirstObjectByType<GardiropManager>();

        if (bagliElbise != null && butonIkonu != null)
        {
            butonIkonu.sprite = bagliElbise.elbiseSprite;
        }

        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(ButonaTiklandi);
        }
    }

    void ButonaTiklandi()
    {
        if (gardiropManager != null && bagliElbise != null)
        {
            gardiropManager.ElbiseSec(bagliElbise);
        }
    }
}