using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[SelectionBase]
[DisallowMultipleComponent]
public sealed class LevelGenerator : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    public GameObject initialPrefab;
    public Transform spawnPoint;
    public Transform root;
    private GameObject lastSpawnedPrefab;
    public float speed = 5;
    public float spawnOffset = 40f;
    public float despawnOffset = 20f;
    public int maxBlocks = 20;
    public List<GameObject> pool = new();
    public bool isInit = false;

    // private void Start()
    // {
    //     lastSpawnedPrefab = Instantiate(initialPrefab, spawnPoint.position, Quaternion.identity, root);
    //     pool.Add(lastSpawnedPrefab);
    //     StartCoroutine(Initialize());
    //     StartCoroutine(SpawnNextPrefabCollider());
    // }

    private void Start()
    {
        lastSpawnedPrefab = Instantiate(initialPrefab, spawnPoint.position, Quaternion.identity, root);
        pool.Add(lastSpawnedPrefab);
        StartCoroutine(Initialize());
    }
    
    private void FixedUpdate()
    {
        foreach (var block in pool)
        {
            if (block == null)
                continue;

            Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
            Vector2 targetPosition = rb.position + Vector2.left * (speed * Time.fixedDeltaTime);
            rb.MovePosition(targetPosition);
        }
    }
    
    private IEnumerator UpdateBlockPosition()
    {
        while (true)
        {
            for (var i = pool.Count - 1; i >= 0; i--)
            {
                var block = pool[i];

                if (block == null)
                    continue;

                block.transform.Translate(Vector3.left * (speed * Time.fixedDeltaTime));

                if (!(block.transform.position.x + despawnOffset < -spawnOffset))
                    continue;

                DespawnBlock(block);
                break;
            }

            yield return new WaitForFixedUpdate();
        }
    }


    // private void Update()
    // {
    //     for (var i = pool.Count - 1; i >= 0; i--)
    //     {
    //         var block = pool[i];
    //
    //         if (block == null)
    //             continue;
    //
    //         block.transform.Translate(Vector3.left * (speed * Time.deltaTime));
    //
    //         if (!(block.transform.position.x + despawnOffset < -spawnOffset))
    //             continue;
    //
    //         DespawnBlock(block);
    //         break;
    //     }
    // }

    private IEnumerator Initialize()
    {
        while (pool.Count < maxBlocks)
        {
            var lastCollider = lastSpawnedPrefab.GetComponent<BoxCollider2D>();
            var lastRightEdge = lastCollider.bounds.max.x;
            var nextPrefabIndex = Random.Range(0, platformPrefabs.Length);
            var nextPrefab = Instantiate(platformPrefabs[nextPrefabIndex], Vector3.zero, Quaternion.identity, root);
            pool.Add(nextPrefab);
            var nextCollider = nextPrefab.GetComponent<BoxCollider2D>();
            var lastTopEdge = lastCollider.bounds.max.y;
            var nextTopEdge = nextCollider.bounds.max.y;
            var yOffset = lastTopEdge - nextTopEdge;
            var xOffset = lastRightEdge - nextCollider.bounds.min.x;
            nextPrefab.transform.position += new Vector3(xOffset, yOffset, 0f);
            lastSpawnedPrefab = nextPrefab;

            yield return new WaitForSeconds(0.3f);
        }

        isInit = true;
    }

    private IEnumerator SpawnNextPrefabCollider()
    {
        while (true)
        {
            if (isInit && pool.Count < maxBlocks)
            {
                var lastCollider = lastSpawnedPrefab.GetComponent<BoxCollider2D>();
                var lastRightEdge = lastCollider.bounds.max.x;
                var nextPrefabIndex = Random.Range(0, platformPrefabs.Length);
                var nextPrefab = Instantiate(platformPrefabs[nextPrefabIndex], Vector3.zero, Quaternion.identity,
                    root);
                pool.Add(nextPrefab);
                var nextCollider = nextPrefab.GetComponent<BoxCollider2D>();
                var lastTopEdge = lastCollider.bounds.max.y;
                var nextTopEdge = nextCollider.bounds.max.y;
                var yOffset = lastTopEdge - nextTopEdge;
                var xOffset = lastRightEdge - nextCollider.bounds.min.x;
                nextPrefab.transform.position += new Vector3(xOffset, yOffset, 0f);
                lastSpawnedPrefab = nextPrefab;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void DespawnBlock(GameObject block)
    {
        Destroy(block);
        pool.Remove(block);
    }
}