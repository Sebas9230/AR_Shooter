using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class NPCController : MonoBehaviour
{
    public static int POINTS = 0; // Static variable to store the points
    public float timeSpawn = 3f; // NPC spawn period
    private float nextSpawn; // Time to spawn the next NPC

    [Range(0, 100)]
    public int enemyRate = 50; // Rate of enemy spawn
    public GameObject PrefabEnemy; // Prefab of the enemy NPC
    public GameObject PrefabAlly; // Prefab of the ally NPC

    private ARPlaneManager arPlaneManager;

    void Start()
    {
        // Set the time to spawn the next NPC
        nextSpawn = Time.time + timeSpawn;

        // Get the ARPlaneManager component
        arPlaneManager = FindObjectOfType<ARPlaneManager>();
        if (arPlaneManager == null)
        {
            Debug.LogError("ARPlaneManager not found! Make sure it is added to the AR Session Origin.");
        }
    }

    void Update()
    {
        // Check if it is time to spawn the next NPC. If so, spawn the NPC and set the time to the next one
        if (Time.time > nextSpawn)
        {
            SpawnOnHorizontalPlane();
            nextSpawn = Time.time + timeSpawn;
        }
    }

    public void SpawnOnHorizontalPlane()
    {
        if (arPlaneManager == null || arPlaneManager.trackables.count == 0)
        {
            Debug.LogWarning("No planes detected to spawn NPCs.");
            return;
        }

        // Filter only horizontal planes
        List<ARPlane> horizontalPlanes = new List<ARPlane>();
        foreach (var plane in arPlaneManager.trackables)
        {
            if (plane.alignment == UnityEngine.XR.ARSubsystems.PlaneAlignment.HorizontalUp)
            {
                horizontalPlanes.Add(plane);
            }
        }

        if (horizontalPlanes.Count == 0)
        {
            Debug.LogWarning("No horizontal planes detected for spawning NPCs.");
            return;
        }

        // Get a random horizontal plane
        ARPlane selectedPlane = horizontalPlanes[UnityEngine.Random.Range(0, horizontalPlanes.Count)];

        // Generate a random position within the bounds of the selected plane
        Vector3 randomPoint = GetRandomPointOnPlane(selectedPlane);

        // Instantiate the NPC and determine if it's an enemy or ally
        GameObject NPC = null;
        bool isEnemy = UnityEngine.Random.Range(0, 100) <= enemyRate;

        if (isEnemy)
        {
            NPC = Instantiate(PrefabEnemy, randomPoint, Quaternion.identity);
            LogManager.LogEvent("NPC Appear", $"Enemy spawned at {randomPoint}");
        }
        else
        {
            NPC = Instantiate(PrefabAlly, randomPoint, Quaternion.identity);
            LogManager.LogEvent("NPC Appear", $"Ally spawned at {randomPoint}");
        }

        // Schedule NPC destruction and log its disappearance
        StartCoroutine(DestroyNPCAfterTime(NPC, 3f));
    }

    private Vector3 GetRandomPointOnPlane(ARPlane plane)
    {
        // Get the plane's center and size
        Vector3 planeCenter = plane.transform.position;
        Vector2 planeSize = plane.size;

        // Generate a random position within the plane's bounds
        float randomX = UnityEngine.Random.Range(-planeSize.x / 2, planeSize.x / 2);
        float randomZ = UnityEngine.Random.Range(-planeSize.y / 2, planeSize.y / 2);

        // Return the random point in world space
        return planeCenter + plane.transform.right * randomX + plane.transform.forward * randomZ;
    }

    private IEnumerator DestroyNPCAfterTime(GameObject npc, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (npc != null)
        {
            LogManager.LogEvent("NPC Disappear", $"NPC destroyed: {npc.name}");
            Destroy(npc);
        }
    }
}
