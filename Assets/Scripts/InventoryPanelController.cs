using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class InventoryPanelController : MonoBehaviour
{
    
    public GameObject panel;
    public GameObject playerCamera;
    public GameObject itemPool;
    public Sprite blackSprite;
    public Sprite yellowSprite;
    public List<GameObject> items;
    public List<Button> buttons;
    public float scaleFactor = 1.2f;
    public float animationSpeed = 10f;
    public GameObject cop;  // Bu satýrda kop'un bir referansýný tanýmlýyoruz
    

    private Transform currentHoveredButton;
    private Vector3 defaultScale = Vector3.one;
    private int previousIndex = -1;

    private void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (panel.activeSelf)
        {
            PanelToggle(KeyCode.Escape, false);
        }
        else
        {
            PanelToggle(KeyCode.Tab, true);
        }

        foreach (Button btn in buttons)
        {
            if (btn.transform == currentHoveredButton)
            {
                btn.transform.localScale = Vector3.Lerp(btn.transform.localScale, defaultScale * scaleFactor, Time.deltaTime * animationSpeed);
                btn.GetComponent<Image>().sprite = yellowSprite;
            }
            else
            {
                btn.transform.localScale = Vector3.Lerp(btn.transform.localScale, defaultScale, Time.deltaTime * animationSpeed);
                btn.GetComponent<Image>().sprite = blackSprite;
            }
        }
    }

    public void OnButtonClick(string buttonID)
    {
        switch (buttonID)
        {
            case "InventoryButton0":
                SelectItem("Cop", 0);
                break;

            case "InventoryButton1":
                SelectItem("Fener", 1);
                break;

            case "InventoryButton2":
                SelectItem("Anahtar", 2);
                break;

            case "InventoryButton3":
                SelectItem("Detektör", 3);
                break;

            default:
                Debug.Log("Bilinmeyen buton.");
                break;
        }
    }

    void PanelToggle(KeyCode keyCode, bool isActive)
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (panel != null)
            {
                panel.SetActive(isActive);

                if (isActive)
                {
                    // Panel açýldýðýnda sadece rotayý sýfýrla, pozisyonu deðiþtirme
                    cop.transform.localRotation = Quaternion.Euler(-60f, -30f, 0f); // Rotasyonu sýfýrlama
                    
                }
                
            }
        }
    }


    public void SetHoveredButton(Transform button)
    {
        if (button != currentHoveredButton)
        {
            currentHoveredButton = button;
        }
    }

    void SelectItem(string itemName, int itemIndex)
    {
        // Ayný itemi tekrar seçmeye çalýþýyorsa, fonksiyonu sonlandýr
        if (previousIndex == itemIndex)
        {
            Debug.Log(itemName + " zaten seçili.");
            return; // Ayný itemi tekrar seçmeyi engelliyoruz
        }

        Debug.Log(itemName + " alýndý.");
        if (previousIndex != -1)
        {
            items[previousIndex].SetActive(false);
            items[previousIndex].transform.SetParent(itemPool.transform, false);
            items[previousIndex].transform.localPosition = items[previousIndex].transform.localPosition - new Vector3(0.502507f, -2.7100389f, 2.703206f);
        }

        items[itemIndex].SetActive(true);
        items[itemIndex].transform.SetParent(playerCamera.transform, false);
        items[itemIndex].transform.localPosition = items[itemIndex].transform.localPosition + new Vector3(0.502507f, -2.7100389f, 2.703206f);

        previousIndex = itemIndex;  // Seçilen item'ý kaydediyoruz
        panel.SetActive(false);
    }

}
