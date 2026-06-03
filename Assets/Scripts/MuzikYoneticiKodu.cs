using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MuzikYoneticiKodu : MonoBehaviour
{
    private static MuzikYoneticiKodu instance;
    [Header("Genel Ses Ayarlari")]
    public AudioClip genelTiklamaSesi;

    void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }
    }

    void Start()
    {
        ButonSesleriniDinamikAta();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => Invoke("ButonSesleriniDinamikAta", 0.1f);

    public void ButonSesleriniDinamikAta()
    {
        Button[] sahnedekiButonlar = Resources.FindObjectsOfTypeAll<Button>();
        foreach (Button buton in sahnedekiButonlar)
        {
            if (buton == null || !buton.gameObject.scene.isLoaded) continue;
            if (buton.name.Contains("Elbise") || buton.name.Contains("Kiyafet") || buton.name.Contains("SatinAl")) continue;

            buton.onClick.RemoveListener(GenelButonSesiCal);
            buton.onClick.AddListener(GenelButonSesiCal);
        }
    }

    public void GenelButonSesiCal()
    {
        if (genelTiklamaSesi != null && Camera.main != null)
            AudioSource.PlayClipAtPoint(genelTiklamaSesi, Camera.main.transform.position);
    }
}