using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject spotLight;
    private bool isOn = false;

    void Start()
    {
        spotLight.SetActive(false);
    }

    void Update()
    {
        if (inventoryPanel != null && inventoryPanel.activeSelf) return;

        if (!isOn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                spotLight.SetActive(true);
                isOn = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                spotLight.SetActive(false);
                isOn = false;
            }
        }
    }

}
