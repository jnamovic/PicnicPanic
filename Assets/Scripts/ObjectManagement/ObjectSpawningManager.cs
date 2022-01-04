using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Malee;

public class ObjectSpawningManager : MonoBehaviour
{
    public GameObject spawnParent;

    public List<ObjectSpawnProperties> objectsToSpawn;

    public bool startTimeOnGameStart = false;

    private float startTime = 0;
    private float currentTime = 0;
    private float elapsedTime = 0;

    void Start() {
        
    }

    void StartTime()
    {
        startTime = Time.time;
        currentTime = Time.time;
    }

    void Update() {
        currentTime = Time.time;
        elapsedTime = currentTime - startTime;

        // every frame, we check if there is objects to Spawn
        foreach (ObjectSpawnProperties o in objectsToSpawn.Where(o => !o.spawned && o.spawnTrigger == SpawnTriggerType.FromGameStart)) {
            if (o.timeDelaySec < elapsedTime)
            {
                spawnNow(o); 
            }
        }
    }

    void spawnNow(ObjectSpawnProperties o)
    {
        Instantiate(o.spawnObject, o.position, Quaternion.Euler(o.rotation), spawnParent.transform);
        if (!o.repeat)
        {
            o.spawned = true;
        }
    }
}
