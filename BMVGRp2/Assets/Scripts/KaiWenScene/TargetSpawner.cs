using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetSpawnConfig
{
    public string name;                   // Optional: for organization
    public GameObject prefab;            // Target prefab to spawn
    public float spawnInterval = 1f;     // Seconds between spawns
    public int maxActive = 5;            // Max targets active at once
    [HideInInspector] public float nextSpawnTime = 0f;
    [HideInInspector] public int activeCount = 0;
}

public class TargetSpawner : MonoBehaviour
{
    [HideInInspector] public float nextSpawnTime = 0f;
    [HideInInspector] public int activeCount = 0;

    public List<TargetSpawnConfig> targets;
    public BoxCollider spawnArea;

    private void Update()
    {
        foreach (var config in targets)
        {
            if (Time.time >= config.nextSpawnTime && config.activeCount < config.maxActive)
            {
                SpawnTarget(config);
                config.nextSpawnTime = Time.time + config.spawnInterval;
            }
        }
    }

    void SpawnTarget(TargetSpawnConfig config)
    {
        if (config.prefab == null)
        {
            Debug.LogWarning("Tried to spawn a null prefab in TargetSpawner!");
            return;
        }

        Vector3 spawnPos = GetRandomPointInBounds(spawnArea.bounds);
        GameObject target = Instantiate(config.prefab, spawnPos, Quaternion.identity);

        TargetBehavior tb = target.GetComponent<TargetBehavior>();
        if (tb != null)
        {
            tb.Initialize(spawnArea);

            // Register destroy event to decrease count
            tb.OnDestroyed += () => config.activeCount--;
        }

        config.activeCount++;
    }

    Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }
}
