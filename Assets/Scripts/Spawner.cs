using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> objectToSpawn = new List<GameObject>();
    public float initialTimeToSpawn;
    public bool isInfinite;
    public bool isRandomized;
    public int numberToSpawn;

    private float _currentTimeToSpawn;

    void Start()
    {
        _currentTimeToSpawn = initialTimeToSpawn;
    }

    void Update()
    {
        if (isInfinite)
        {
            UpdateTimer();
        }
        else if (numberToSpawn > 0)
        {
            UpdateTimerWithLimit();
        }
    }

    void UpdateTimer()
    {
        if (_currentTimeToSpawn > 0)
        {
            _currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnObject();
            RandomTimer();
        }
    }

    void UpdateTimerWithLimit()
    {
        if (_currentTimeToSpawn > 0)
        {
            _currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnObject();
            RandomTimer();
            numberToSpawn--;
        }
    }

    void RandomTimer()
    {
        _currentTimeToSpawn = Random.Range(5, 15);
    }

    void SpawnObject()
    {
        int index = isRandomized ? Random.Range(0, objectToSpawn.Count) : 0;
        if(objectToSpawn.Count > 0)
        {
            Instantiate(objectToSpawn[index], transform.position, Quaternion.identity);
        }
    }
}
