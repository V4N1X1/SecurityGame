using UnityEngine;

public class ScannerTrigger : MonoBehaviour
{
    public Camera cam;
    public LayerMask npcLayer;
    public Light scannerLight;

    private float lightTimer = 0f;
    private float lightDuration = 0.5f; // 💡 Işık 0.5 saniye yanacak

    void Start()
    {
        if (scannerLight != null)
            scannerLight.enabled = false;
    }

    void Update()
    {
        // Işık yanıyorsa süresini azalt
        if (scannerLight != null && scannerLight.enabled)
        {
            lightTimer -= Time.deltaTime;
            if (lightTimer <= 0f)
            {
                scannerLight.enabled = false;
            }
        }

        // Tarama işlemi
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, npcLayer))
            {
                NPCBehavior npc = hit.collider.GetComponent<NPCBehavior>();
                NPCData npcData = hit.collider.GetComponent<NPCData>();

                if (npc != null && npcData != null)
                {
                    if (npcData.isScanned)
                    {
                        Debug.Log($"{hit.collider.name} zaten tarandı. Atlanıyor.");
                    }
                    else
                    {
                        npc.ScannerInteraction(cam.transform);
                        npcData.isScanned = true;

                        if (npcData.hasIllegalItem)
                        {
                            if (scannerLight != null)
                            {
                                scannerLight.color = Color.red;
                                scannerLight.enabled = true;
                                lightTimer = lightDuration;
                            }
                        }
                        else
                        {
                            if (scannerLight != null)
                            {
                                scannerLight.enabled = false;
                            }
                        }
                    }
                }
            }
        }
    }
}
