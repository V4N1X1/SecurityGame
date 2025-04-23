using System.Collections;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;           // NPC prefab'ý
    public Transform[] spawnPoints;        // Spawn noktalarý
    public float spawnInterval = 5f;       // Spawn aralýðý
    public int maxNPCCount = 10;           // Maksimum ayný anda aktif NPC sayýsý

    private int currentNPCCount = 0;       // Þu anki aktif NPC sayýsý

    private void Start()
    {
        StartCoroutine(SpawnNPCs());
    }

    private IEnumerator SpawnNPCs()
    {
        while (true)
        {
            float currentHour = GameTimeManager.instance.GetTime();

            // NPC'leri sabah 9'dan akþam 8'e kadar spawn et
            if (currentHour >= 9f && currentHour < 20f && currentNPCCount < maxNPCCount)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject npc = Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);
                currentNPCCount++;

                // NPC yok olduðunda sayacý güncelle
                NPCBehavior npcScript = npc.GetComponent<NPCBehavior>();
                if (npcScript != null)
                {
                    npcScript.OnNPCDestroyed += () => currentNPCCount = Mathf.Max(0, currentNPCCount - 1);
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}