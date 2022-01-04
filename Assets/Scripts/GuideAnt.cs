using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideAnt : MonoBehaviour
{
    public GameObject antBase;
    public int antsPerLine = 10;
    public float speed = 1.0f;

    private GameObject topAnt;
    private List<GameObject> ants = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        
        topAnt = transform.GetChild(0).gameObject;
        Transform antTrans = topAnt.transform;
        //establishes the line
        for (int antNum = 0; antNum < antsPerLine; antNum++)
        {
            
            GameObject newAnt = CreateAnt(antTrans, antNum);
            ants.Add(newAnt);
            
        }

            
        }
    

    // Update is called once per frame
    void Update()
    {
        Transform antTrans = topAnt.transform;
        for (int antNum = 0; antNum < ants.Count; antNum++)
        {
            float antZ = -antNum * 0.75f + .75f;
            float antX = antTrans.position.x;
            float antY = antTrans.position.y;
            GameObject loopAnt = ants[antNum];

            
            int lastAnt = ants.Count + 1;
            if (loopAnt.name.Equals("ThrownAnt"))
            {
                ants.Remove(loopAnt);
                loopAnt.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                ants.Add(CreateAnt(antTrans, ants.Count+1.5f));
            }
            else
            {
                float step = speed * Time.deltaTime;
                //Debug.Log("i am ant " + antNum + " and i am moving to " + antZ);
                loopAnt.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                loopAnt.transform.position = Vector3.MoveTowards(loopAnt.transform.position, new Vector3 (antX, antY,antZ ), step);
                loopAnt.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX| RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

            }
                
            


        }
    }

    public GameObject CreateAnt(Transform antTransform, float linePlace)
    {
        float antZ = -linePlace * 0.75f + .75f;
        GameObject newAnt = Instantiate(antBase);
        newAnt.transform.position = new Vector3(antTransform.position.x, antTransform.position.y, antTransform.position.z + antZ);
        newAnt.transform.rotation = antTransform.rotation;
        newAnt.transform.SetParent(this.transform);
        newAnt.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        return newAnt;
    }


}
