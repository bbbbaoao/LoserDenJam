using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnList = new List<GameObject>();
    //use this to spread each level
    [SerializeField] private Vector2 spawnOffset;

    public void SpawnLevel()
    {

    }

    public GameObject GetRandomLevel()
    {
        int rand = Random.Range(0, spawnList.Count);
        return spawnList[rand];
    }
}
