using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManManager : MonoBehaviour
{
    public Animator targetAnimator;

    private bool atTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // start with walking in animation

        // when hit

        // then stop

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("PPOOOOOP");
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("PPOOOOOP");
    }

}
