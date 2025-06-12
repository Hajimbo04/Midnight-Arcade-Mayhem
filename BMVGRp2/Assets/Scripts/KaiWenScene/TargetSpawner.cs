using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetSpawnConfig
{
    public string name;
    public GameObject prefab;
    public float spawnInterval = 1f;
    public int maxActive = 5;

    [HideInInspector] public float nextSpawnTime = 0f;
    [HideInInspector] public int activeCount = 0;
}

public class TargetSpawner : MonoBehaviour
{
    public List<TargetSpawnConfig> targets;
    public BoxCollider spawnArea;

    [Header("Global Limit")]
    public int maxTotalActive = 15;
    private int totalActiveCount = 0;

    private bool spawningEnabled = false;

    private void Update()
    {
        if (!spawningEnabled) return;

        foreach (var config in targets)
        {
            if (Time.time >= config.nextSpawnTime &&
                config.activeCount < config.maxActive &&
                totalActiveCount < maxTotalActive)
            {
                SpawnTarget(config);
                config.nextSpawnTime = Time.time + config.spawnInterval;
            }
        }
    }

    public void StartSpawning()
    {
        spawningEnabled = true;
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

            // Register destroy event
            tb.OnDestroyed += () =>
            {
                config.activeCount--;
                totalActiveCount--;
            };
        }

        config.activeCount++;
        totalActiveCount++;
    }

    Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }
}
