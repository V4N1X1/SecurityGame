using System.Collections;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    public bool rohan;
    public bool hasIllegalItem;
    public float confessChance; // Ýtiraf etme ihtimali (0.0 - 1.0 arasý deðer)
    public bool Gýrgýz;
    public bool isScanned;
    
    private void Start()
    {
        ResetData();
        
    }

    public void ResetData()
    {
        isScanned = false;
        rohan = false;
        // 20% þansla illegal item var
        hasIllegalItem = Random.value < 0.20f;

        Gýrgýz = Random.value < 1f;

        // Ýtiraf etme ihtimali: 30% ile 80% arasýnda rastgele ayarlanýyor
        confessChance = Random.Range(0.3f, 0.8f);
        
    }
}
