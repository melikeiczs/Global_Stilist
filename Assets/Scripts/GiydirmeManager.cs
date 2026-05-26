using UnityEngine.SceneManagement;
using UnityEngine;

public class GiydirmeManager : MonoBehaviour
{
    // Bu değerleri giydirme yaparken güncellediğini varsayıyoruz
    public int aktifKizID;
    public int aktifElbiseID;
    
    public void DegerlendirmeyeGit()
    {
        // 1. Verileri köprüye (OyunVerileri) kopyala
        OyunVerileri.secilenKizID = aktifKizID;
        OyunVerileri.secilenElbiseID = aktifElbiseID;
        // Diğer eşyaları da buraya ekle...

        // 2. Yeni sahneyi yükle
        SceneManager.LoadScene("DegerlendirmeSahnesi"); 
    }
}