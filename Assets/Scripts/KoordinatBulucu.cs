using UnityEngine;

public class KoordinatBulucu : MonoBehaviour
{
    void Update()
    {
        // Mouse sol tıklandığında
        if (Input.GetMouseButtonDown(0))
        {
            // Kameradan mouse'un olduğu yere bir ışın (Ray) fırlat
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Işın bir objeye (dünya küresine) çarptıysa
            if (Physics.Raycast(ray, out hit))
            {
                // Çarptığı noktanın koordinatlarını konsola yazdır
                Debug.Log("Dünya Üzerinde Tıklanan Nokta -> X: " + hit.point.x + " | Y: " + hit.point.y + " | Z: " + hit.point.z);
                
                // Eğer butonları yerleştireceğin Canvas kürenin içindeyse, yerel (local) koordinat daha çok işe yarar:
                Vector3 yerelPozisyon = hit.transform.InverseTransformPoint(hit.point);
                Debug.Log("Kürenin Kendi İçindeki (Local) Koordinatı -> X: " + yerelPozisyon.x + " | Y: " + yerelPozisyon.y + " | Z: " + yerelPozisyon.z);
            }
        }
    }
}