using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutWiggle : MonoBehaviour
{

    private Rigidbody body;
    private Vector3 startRot;
    private float wiggleLevel=0;
    private float maxWiggle = 2;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        //startRot = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = startPos;
        /*Vector3 dir = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        dir=dir.normalized*wiggleLevel;
        Debug.Log(dir);
        body.velocity=dir;*/

        if (Random.Range(1, 20) < 2)
        {
            Vector3 dir = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
            dir = dir.normalized * wiggleLevel;
            //Debug.Log(dir);
            body.velocity += dir;
        }
        
    }



    public void IncreaseWiggle() {
        wiggleLevel+=0.5f;
        
    }

    public void SetWiggle(int carrynum, int maxnum) {
        wiggleLevel = maxWiggle * carrynum / maxnum*1.0f;
    }

    public void StopWiggle() {
        wiggleLevel = 0;
    }

}
