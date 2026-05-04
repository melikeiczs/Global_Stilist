using UnityEngine;
using UnityEngine.UI;

public class KumasSecici : MonoBehaviour
{
    public Image masaImage;      // Masa (değişecek olan)
    public Sprite kumas1Sprite;  // Kumaş 1 serilmiş hali
    public Sprite kumas2Sprite;  // Kumaş 2 serilmiş hali
    public Sprite kumas3Sprite; 

    public void Kumas1Sec()
    {
        masaImage.sprite = kumas1Sprite;
    }

    public void Kumas2Sec()
    {
        masaImage.sprite = kumas2Sprite;
    }
    public void Kumas3Sec()
    {
        masaImage.sprite = kumas3Sprite;
    }
}