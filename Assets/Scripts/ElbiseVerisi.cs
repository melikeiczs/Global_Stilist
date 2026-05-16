using UnityEngine;

[CreateAssetMenu(fileName = "YeniElbiseVerisi", menuName = "Oyun/Elbise Verisi")]
public class ElbiseVerisi : ScriptableObject
{
    public string elbiseAdi;        
    public Sprite elbiseSprite;   
    public Vector2 pozisyonOffset; 
    
    [Header("Boyut Genişletme ve Uzatma Ayarı")]
    [Tooltip("X = Genişlik (En), Y = Yükseklik (Boy). Orijinal boyut için ikisini de 1 bırakın.")]
    public Vector2 elbiseBoyutu = new Vector2(1f, 1f); 
}