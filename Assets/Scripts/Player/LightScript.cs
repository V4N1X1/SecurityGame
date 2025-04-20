using UnityEngine;

public class LightAndVisibilityScript : MonoBehaviour
{
    [SerializeField] GameObject flashlight; // Fener nesnesi
    [SerializeField] Light flashlightLight; // Fenerin ���k bile�eni
    private bool isLightOn; // I��k durumu
    private bool isVisible; // G�r�n�rl�k durumu

    private void Update()
    {
        // F tu�una bas�ld���nda ����� a�/kapat
        if (Input.GetKeyDown(KeyCode.F))
        {
            isLightOn = !isLightOn;
            flashlightLight.enabled = isLightOn; // I���� a�/kapat
        }

        // 1 tu�una bas�ld���nda fenerin g�r�n�rl���n� a�/kapat
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isVisible = !isVisible;
            flashlight.SetActive(isVisible); // Fenerin g�r�n�rl���n� a�/kapat
        }
    }
}