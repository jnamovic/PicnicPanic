using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorItem : MonoBehaviour
{
    [SerializeField]
    public bool isMoving = true;
    [SerializeField]
    public float xMovement = 0;
    [SerializeField]
    public float yMovement = 0;
    [SerializeField]
    public float zMovement = 0;

    private Vector3 direction;

    void Start()
    {
        direction = new Vector3(xMovement, yMovement, zMovement);
    }

    void Update()
    {
        if (isMoving)
        {
            float time = Time.deltaTime;

            transform.position = transform.position - (direction * time);
            /*
            if(this.gameObject.GetComponentInChildren<OVRGrabbable>().isGrabbed) {
                this.isMoving = false;
            }*/
        }
    }
}
