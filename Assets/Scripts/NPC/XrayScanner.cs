using UnityEngine;

public class XRayScanner : MonoBehaviour
{
    public Transform scanPoint;         // Tarama merkezi
    public float scanRadius = 2f;       // Tarama yar��ap�
    public LayerMask npcLayer;          // Sadece NPC katman�ndaki objeleri tarar

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

        // I��klar� g�ncelle
        greenLight.SetActive(!foundIllegal);
        redLight.SetActive(foundIllegal);
    }

    // Tarama alan�n� sahnede �izmek i�in
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(scanPoint.position, scanRadius);
    }
}
