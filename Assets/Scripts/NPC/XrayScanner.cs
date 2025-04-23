using UnityEngine;

public class XRayScanner : MonoBehaviour
{
    public Transform scanPoint;         // Tarama merkezi
    public float scanRadius = 2f;       // Tarama yarýçapý
    public LayerMask npcLayer;          // Sadece NPC katmanýndaki objeleri tarar

    public GameObject greenLight;       // Temizse
    public GameObject redLight;         // Illegal item varsa

    void Update()
    {
        Collider[] npcs = Physics.OverlapSphere(scanPoint.position, scanRadius, npcLayer);
        bool foundIllegal = false;

        foreach (Collider npc in npcs)
        {
            NPCData data = npc.GetComponent<NPCData>();
            if (data != null && data.hasIllegalItem)
            {
                foundIllegal = true;
                break;
            }
        }

        // Iþýklarý güncelle
        greenLight.SetActive(!foundIllegal);
        redLight.SetActive(foundIllegal);
    }

    // Tarama alanýný sahnede çizmek için
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(scanPoint.position, scanRadius);
    }
}
