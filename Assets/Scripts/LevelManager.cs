using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool isPlaying = true;
    [SerializeField] Transform spawnArea;
    [SerializeField] Sprite[] objSprites;
    public GameObject spawnedObj;
    public float minSpawnTime;
    public float maxSpawnTime;
    public float spawnRadius, innerRadius;
    void Start()
    {
        StartCoroutine(SpawnObjs());
    }
    IEnumerator SpawnObjs()
    {
        while (isPlaying)
        {
            SpawnObj();
            float randomInterval = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    void SpawnObj()
    {
        GameObject ga = Instantiate(spawnedObj, transform.position + Vector3.up, spawnedObj.transform.rotation);
        ga.transform.SetParent(spawnArea);
        SpawnedObj obj = ga.GetComponent<SpawnedObj>();

        if (obj != null)
        {
            Sprite randomSprite = objSprites[Random.Range(0, objSprites.Length)];
            obj.Init(randomSprite, GetRandomPosition());
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 spawnPos = Vector3.zero;
        bool validPosition = false;

        while (!validPosition)
        {
            float angle = Random.Range(0f, 2f * Mathf.PI); 
            float radius = Random.Range(innerRadius, spawnRadius);  

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            spawnPos = new Vector3(spawnArea.position.x + x, spawnArea.position.y, spawnArea.position.z + y);

            float distanceToCenter = Vector3.Distance(spawnArea.position, spawnPos);
            if (distanceToCenter >= innerRadius)
            {
                validPosition = true;  
            }
        }

        return spawnPos;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 spawnPos = spawnArea.position;
        Gizmos.DrawWireSphere(spawnPos, spawnRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnPos, innerRadius); 
    }
}
