using System.Collections;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    public bool hasIllegalItem;

    private void Start()
    {
        // Set hasIllegalItem to true based on a percentage chance
        hasIllegalItem = Random.value < 0.15f; // 15% chance to have an illegal item
    }
}