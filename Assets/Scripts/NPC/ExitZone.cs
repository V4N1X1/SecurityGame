using UnityEngine;

public class ExitZone : MonoBehaviour
{
    GameTimeManager gameTimeManager;

    private void Start()
    {
        gameTimeManager = FindObjectOfType<GameTimeManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        NPCBehavior npc = other.GetComponent<NPCBehavior>();

        if (npc != null && (npc.isDone || gameTimeManager != null && gameTimeManager.currentHour == 0f))
        {
            npc.DeactivateViaTrigger();
        }
    }
}
