using UnityEngine;
using UnityEngine.SceneManagement; // Sahne degistirmek icin bu sart!

public class SeviyeGecis : MonoBehaviour
{
    public void DegerlendirmeSahnesineGit()
    {
        // Build Settings'e ekledigimiz sahnenin adini yaziyoruz
        SceneManager.LoadScene("DegerlendirmeSahnesi");
    }
}