using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    public float rayDistance = 5f; //Raycast uzaklýðý
    public LayerMask interactableLayer;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            PerformRaycast();
        }
    }

    void PerformRaycast()
    {
        RaycastHit hit;

        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward,out hit, rayDistance, interactableLayer))
        {
            Interactable currentInteractable = hit.collider.GetComponent<Interactable>();

            if (currentInteractable != null)
            {
                currentInteractable.OnInteract();
            }

        }
    }
}
