using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        float spawnValue = Random.value;
        float cumulativeChance = 0f;

        foreach (SpawnableObject obj in objects)
        {
            cumulativeChance += obj.spawnChance;
            if (spawnValue <= cumulativeChance)
            {
                GameObject spawnedObject = Instantiate(obj.prefab);
                spawnedObject.transform.position = transform.position;
                break;
            }
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
