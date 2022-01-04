using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{

    public List<GameObject> Throwables;
    private List<Vector3> startPos=new List<Vector3>();
    private List<Quaternion> startRot = new List<Quaternion>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Throwables.Count; i++)
        {
            startPos.Add(Throwables[i].transform.position);
            startRot.Add(Throwables[i].transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("im triggered");
        Debug.Log(other.tag);
        if (other.tag == "Hands") {
            Debug.Log("im hands");
            
            for (int i = 0; i < Throwables.Count; i++) {
                Debug.Log("start: " + Throwables[i].transform.position);
                Debug.Log("goal: " + startPos[i]);
                Throwables[i].transform.position = startPos[i];
                Throwables[i].transform.rotation = startRot[i];
            }
        }
    }
}
