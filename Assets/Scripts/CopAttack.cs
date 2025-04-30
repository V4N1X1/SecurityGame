using UnityEngine;
using System.Collections;

public class CopAttack : MonoBehaviour
{
    public GameObject cop;
    private Animator copAnimation;
    public float rayDistance = 1f;  // Raycast uzaklığı
    public LayerMask npcLayer;      // NPC'lerin bulunduğu katman maskesi
    private Camera mainCamera;

    public int loseScore = 0;  // Skor değeri (illegal olmayan NPC'lere vuruldukça artacak)
    public GameObject inventoryPanel; // panel objesini buraya atayacaksın

    public float attackCooldown = 0.5f; // 🕒 Saldırı cooldown süresi (saniye)
    private bool canAttack = true;

    void Start()
    {
        copAnimation = cop.GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (inventoryPanel != null && inventoryPanel.activeSelf) return;

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        canAttack = false;

        if (cop != null)
        {
            copAnimation = cop.GetComponent<Animator>();
            copAnimation.SetTrigger("Attack"); // Trigger kullanıldı
        }

        PerformRaycast();

        yield return new WaitForSeconds(0.5f);
        EndAttack();

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void EndAttack()
    {
        ResetCopRotation();
    }

    public void ResetCopRotation()
    {
        cop.transform.localRotation = Quaternion.Euler(-60f, -30f, 0f);
    }

    void PerformRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, rayDistance, npcLayer))
        {
            if (hit.collider.CompareTag("NPC"))
            {
                NPCData npcData = hit.collider.GetComponent<NPCData>();
                if (npcData != null)
                {
                    if (!npcData.hasIllegalItem)
                    {
                        loseScore++;
                        Debug.Log("Skor arttı: " + loseScore);
                    }

                    npcData.ResetData();

                    NPCBehavior npcBehavior = hit.collider.GetComponent<NPCBehavior>();
                    if (npcBehavior != null)
                    {
                        npcBehavior.ForceExit();
                    }
                    else
                    {
                        Debug.LogError("NPCBehavior component bulunamadı!");
                    }
                }
                else
                {
                    Debug.LogError("NPCData component bulunamadı!");
                }
            }
        }
    }
}
