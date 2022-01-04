using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectSpawnProperties
{
    public GameObject spawnObject;
    public Vector3 position;
    public Vector3 rotation;
    
    public SpawnTriggerType spawnTrigger;
    public GameObject afterObject;

    public float timeDelaySec;
    public float scoreDelay;

    public bool repeat;
    public bool spawned = false;

    private float lastTimeSec = 0;
    private float lastScore = 0;
}

public enum SpawnTriggerType {
        FromGameStart,
        AfterPreviousObject,
        AfterSelectedObject
}