using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectPrefab;
    [SerializeField] [Range(1, 50)] private int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] private float spawnTimer = 1f;

    private GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(gameObjectPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void EnableObjectInPool()
    {
        foreach (GameObject nextObject in pool)
        {
            if (!nextObject.activeInHierarchy)
            {
                nextObject.SetActive(true);
                return;
            }
        }
    }
}
