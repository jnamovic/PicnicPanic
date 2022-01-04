using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public Transform goal;
    public bool isEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        // Destroy everything that enters the trigger
        //Destroy(collision.gameObject);
        
        MoveTo moveTo = other.transform.GetComponent<MoveTo>();
        if (isEnd)
        {
            UnityEngine.AI.NavMeshAgent agent = other.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
            var rigidbody = other.transform.GetComponent<Rigidbody>();
            Destroy(agent);
            Destroy(moveTo);
            rigidbody.isKinematic = false;
            transform.Translate(other.gameObject.GetComponent<MeshCollider>().bounds.size.x , 0, 0);
        }
        else
        {
            if(moveTo != null) {
                moveTo.goal = goal;
            }
        }
       
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    // Destroy everything that enters the trigger
    //    Destroy(other.gameObject);
    //}
}
