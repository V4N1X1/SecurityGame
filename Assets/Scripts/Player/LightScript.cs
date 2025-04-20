using UnityEngine;

public class LightAndVisibilityScript : MonoBehaviour
{
    [SerializeField] GameObject flashlight; // Fener nesnesi
    [SerializeField] Light flashlightLight; // Fenerin ýþýk bileþeni
    private bool isLightOn; // Iþýk durumu
    private bool isVisible; // Görünürlük durumu

    private void Update()
    {
        // F tuþuna basýldýðýnda ýþýðý aç/kapat
        if (Input.GetKeyDown(KeyCode.F))
        {
            isLightOn = !isLightOn;
            flashlightLight.enabled = isLightOn; // Iþýðý aç/kapat
        }

        // 1 tuþuna basýldýðýnda fenerin görünürlüðünü aç/kapat
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isVisible = !isVisible;
            flashlight.SetActive(isVisible); // Fenerin görünürlüðünü aç/kapat
        }
    }
}