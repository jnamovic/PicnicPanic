using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public GameObject allThings;
    private GameObject targets;
    private GameObject humans;
    // Start is called before the first frame update
    void Start()
    {
        StartAllthings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAllthings() {
        Instantiate(allThings);
    }

    public void StopAllThings() {
        Destroy(allThings);
    }

    public void restart() {
        StopAllThings();
        StartAllthings();
    }
}
