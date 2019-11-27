using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform target;
    public int total = 5;
    int count = 0;
    public float spawnTime = 5f;
    private float spawnTimer = 0;

    private bool isSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        count = total;
        Debug.Log(gameObject.name + " Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning)
        {
            if (Global.isBattling)
            {
                count = total;
                spawnTimer = spawnTime;
                isSpawning = true;
            }
        }
        else if (count > 0)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnTime)
            {
                GameObject clone = Instantiate(enemyPrefab, transform) as GameObject;
                clone.GetComponent<EnemyBehaviour>().target = target;
                --count;
                spawnTimer = 0;
            }
        }

    }
}
