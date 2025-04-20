using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

public class NavAIWaypointSystem : MonoBehaviour
{
    [Header("Tag Settings")]
    public string waypointTag = "Waypoint";
    public string exitTag = "Exit";

    [Header("NPC Settings")]
    public float minWaitTime = 3f;
    public float maxWaitTime = 6f;
    private NavMeshAgent agent;
    private Transform exitPoint;
    private Transform currentTarget;
    private int visitedCount = 0;
    private int maxVisits = 3;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitializeWaypointManager();
        FindExitPoint();
        StartCoroutine(GoToWaypoint());
    }

    // Waypoint manager başlatma
    void InitializeWaypointManager()
    {
        if (WaypointManager.AvailableWaypoints.Count == 0)
        {
            WaypointManager.Initialize(GameObject.FindGameObjectsWithTag(waypointTag));
        }
    }

    // Çıkış noktasını bulma
    void FindExitPoint()
    {
        GameObject exitObj = GameObject.FindGameObjectWithTag(exitTag);
        exitPoint = exitObj?.transform ?? throw new NullReferenceException("Exit point not found!");
    }

    // Waypoint'e gitme işlemi
    IEnumerator GoToWaypoint()
    {
        while (visitedCount < maxVisits)
        {
            currentTarget = WaypointManager.GetWaypoint(); // Waypoint manager üzerinden yeni waypoint seçimi

            if (currentTarget == null)
            {
                Debug.Log("No available waypoints, exiting...");
                break;
            }

            agent.SetDestination(currentTarget.position);

            // Hedefe ulaşana kadar bekle
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            // Waypoint'e ulaşıldı, bekleme süresi ekle
            yield return new WaitForSeconds(UnityEngine.Random.Range(minWaitTime, maxWaitTime));

            // Ziyaret edilen olarak işaretle
            visitedCount++;
            WaypointManager.MarkAsVisited(currentTarget);
        }

        // 3 waypoint ziyaret edildikten sonra çıkışa git
        if (exitPoint != null)
        {
            yield return StartCoroutine(GoToExit());
        }
    }

    // Çıkışa gitme işlemi
    IEnumerator GoToExit()
    {
        agent.SetDestination(exitPoint.position);

        while (agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        // NPC çıkışa ulaştığında kendini yok et
        Destroy(gameObject);
    }
}

// Waypoint manager'ı
public static class WaypointManager
{
    public static List<Transform> AvailableWaypoints { get; private set; } = new List<Transform>();
    private static List<Transform> ReservedWaypoints = new List<Transform>();

    public static void Initialize(GameObject[] waypoints)
    {
        AvailableWaypoints = waypoints.Select(go => go.transform).ToList();
        ReservedWaypoints.Clear();
    }

    public static Transform GetWaypoint()
    {
        if (AvailableWaypoints.Count == 0) return null;

        var rand = new System.Random();
        int index = rand.Next(AvailableWaypoints.Count);
        Transform wp = AvailableWaypoints[index];

        AvailableWaypoints.RemoveAt(index);
        ReservedWaypoints.Add(wp);

        return wp;
    }

    public static void ReleaseWaypoint(Transform wp)
    {
        if (ReservedWaypoints.Contains(wp))
        {
            ReservedWaypoints.Remove(wp);
            AvailableWaypoints.Add(wp);
        }
    }

    public static void MarkAsVisited(Transform wp)
    {
        ReservedWaypoints.Remove(wp);
    }
}
