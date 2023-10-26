using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    Spawnpoint[] spawnpoints;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
	spawnpoints = GetComponentsInChildren<Spawnpoint>();
    }

    // Update is called once per frame
    public Transform GetSpawnpoint() {
	return spawnpoints[Random.Range(0, spawnpoints.Length)].transform;
    }
}
