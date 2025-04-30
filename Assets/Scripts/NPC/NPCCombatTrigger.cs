using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NPCCombatTrigger : MonoBehaviour
{

    private NPCBehavior npcBehavior;

    private void Start()
    {
        npcBehavior = GetComponent<NPCBehavior>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            NPCBehavior thisNPC = GetComponent<NPCBehavior>();
            NPCBehavior otherNPC = other.GetComponent<NPCBehavior>();

            if (thisNPC != null)
                thisNPC.GoIdle();  // Kendini durdur

            if (otherNPC != null)
                otherNPC.GoIdle(); // Diðer NPC'yi durdur

            Debug.Log($"{gameObject.name} ve {other.name} trigger ile çarpýþtý, idle'a geçtiler.");
        }
    }
}
