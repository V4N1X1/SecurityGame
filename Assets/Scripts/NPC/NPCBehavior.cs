using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehavior : MonoBehaviour
{
    public System.Action OnNPCDestroyed; // NPC yok olduðunda çaðrýlacak olay
    public Transform exitPoint;         // Çýkýþ noktasý
    public float roamRadius = 15f;      // Dolaþma yarýçapý
    public int roamLimit = 3;           // Kaç kez dolaþacak
    public Transform scanner;           // XRay cihazýnýn hedef noktasý

    private NavMeshAgent agent;
    private Vector3 spawnPosition;
    private Coroutine roamingRoutine;
    private bool goingToExit = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spawnPosition = transform.position;
        roamingRoutine = StartCoroutine(RoamAndExit());
    }

    private void Update()
    {
        if (!goingToExit && GameTimeManager.instance != null && GameTimeManager.instance.GetHour() >= 20)
        {
            goingToExit = true;

            if (roamingRoutine != null)
                StopCoroutine(roamingRoutine);

            StartCoroutine(GoToExitAndDestroy());
        }
    }

    private IEnumerator RoamAndExit()
    {
        if (scanner != null)
        {
            yield return MoveToDestination(scanner.position);
            yield return new WaitForSeconds(0.5f);
        }

        for (int roamCount = 0; roamCount < roamLimit; roamCount++)
        {
            Vector3 randomDestination = GetRandomNavMeshPosition(spawnPosition, roamRadius);
            while (Vector3.Distance(randomDestination, spawnPosition) < 15f)
            {
                randomDestination = GetRandomNavMeshPosition(spawnPosition, roamRadius);
            }

            yield return MoveToDestination(randomDestination);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }

        yield return GoToExitAndDestroy();
    }

    private IEnumerator GoToExitAndDestroy()
    {
        if (exitPoint != null)
        {
            yield return MoveToDestination(exitPoint.position);
        }

        OnNPCDestroyed?.Invoke();
        Destroy(gameObject);
    }

    private IEnumerator MoveToDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
        while (agent.pathPending || agent.remainingDistance > 0.1f)
        {
            yield return null;
        }
    }

    private Vector3 GetRandomNavMeshPosition(Vector3 origin, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += origin;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, NavMesh.AllAreas);
        return hit.position;
    }
}
