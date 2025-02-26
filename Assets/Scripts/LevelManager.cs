using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    CameraMove cm;
    PlayerController player;
    Ground ground;
    public bool isPlaying = true;
    [SerializeField] Transform spawnArea;
    [SerializeField] Sprite[] objSprites;
    public GameObject spawnObjPrefab;
    public List<SpawnedObj> spawnedObjs;
    public int maxSpawnCount;
    public float minSpawnTime;
    public float maxSpawnTime;
    public float spawnRadius, innerRadius;

    [Header("Progress")]
    public bool isExploding;
    public float curProgress;
    [SerializeField] float pDropSpeed;
    [SerializeField] Image progressBar;
    private void Awake()
    {
        cm = FindFirstObjectByType<CameraMove>();
        player = FindFirstObjectByType<PlayerController>();
        ground = FindFirstObjectByType<Ground>();
    }
    void Start()
    {
        StartCoroutine(SpawnObjs());
    }
    private void FixedUpdate()
    {
        if (!isPlaying) return;
        progressBar.fillAmount = curProgress / 100f;

        if (curProgress > 0)
        {
            curProgress -= Time.fixedDeltaTime * pDropSpeed;
        }
        if (curProgress >= 100f)
        {
            curProgress = 100f;
            StartCoroutine(ExplodeClimaxTime());
        }
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
        if (!isExploding)
        {
            if (spawnedObjs.Count >= maxSpawnCount) return;

            GameObject ga = Instantiate(spawnObjPrefab, transform.position + Vector3.up, spawnObjPrefab.transform.rotation);
            ga.transform.SetParent(spawnArea);
            SpawnedObj obj = ga.GetComponent<SpawnedObj>();

            if (obj != null)
            {
                Sprite randomSprite = objSprites[Random.Range(0, objSprites.Length)];
                obj.Init(randomSprite, GetRandomPosition());
                spawnedObjs.Add(obj);
            }
        }
        else
        {
            GameObject ga = Instantiate(spawnObjPrefab, transform.position + Vector3.up, spawnObjPrefab.transform.rotation);
            ga.transform.SetParent(spawnArea);
            SpawnedObj obj = ga.GetComponent<SpawnedObj>();

            if (obj != null)
            {
                Sprite randomSprite = objSprites[Random.Range(0, objSprites.Length)];
                obj.Init(randomSprite, GetRandomPosition());
            }
        }
    }
    IEnumerator ExplodeClimaxTime()
    {
        for (int i = 0; i < spawnedObjs.Count; i++)
        {
            Destroy(spawnedObjs[i].gameObject);
            spawnedObjs.Remove(spawnedObjs[i]);
        }
        yield return new WaitForSeconds(0.2f);
        cm.canFollow = false;
        player.canAct = false;
        ground.canSpin = false;
        minSpawnTime = 0.02f;
        maxSpawnTime = 0.02f;
        yield return StartCoroutine(cm.Zoom(8f, 35f, 45f, 0.5f));
        yield return new WaitForSeconds(1f);
        player.canAct = true;
        ground.canSpin = true;
        minSpawnTime = 1f;
        maxSpawnTime = 3f;
        yield return StartCoroutine(cm.Zoom(10f, 45f, 35f, 0.3f));
        cm.canFollow = true;
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
/*    private void OnTriggerEnter(Collider other)
    {
        ICollectable collectable = other.GetComponent<ICollectable>();
        if (collectable != null)
        {
            collectable.Collect(transform);
            float v = other.GetComponent<SpawnedObj>().value;
            curProgress += v;
        }
    }*/

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 spawnPos = spawnArea.position;
        Gizmos.DrawWireSphere(spawnPos, spawnRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnPos, innerRadius); 
    }
}
