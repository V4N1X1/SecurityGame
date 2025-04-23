using System.Collections;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;           // NPC prefab'�
    public Transform[] spawnPoints;        // Spawn noktalar�
    public float spawnInterval = 5f;       // Spawn aral���
    public int maxNPCCount = 10;           // Maksimum ayn� anda aktif NPC say�s�

    private int currentNPCCount = 0;       // �u anki aktif NPC say�s�

    private void Start()
    {
        StartCoroutine(SpawnNPCs());
    }

    private IEnumerator SpawnNPCs()
    {
        while (true)
        {
            float currentHour = GameTimeManager.instance.GetTime();

            // NPC'leri sabah 9'dan ak�am 8'e kadar spawn et
            if (currentHour >= 9f && currentHour < 20f && currentNPCCount < maxNPCCount)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject npc = Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);
                currentNPCCount++;

                // NPC yok oldu�unda sayac� g�ncelle
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