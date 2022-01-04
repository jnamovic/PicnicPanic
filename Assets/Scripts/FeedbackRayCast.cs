using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackRayCast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        return;
        RaycastHit hit;
        Container container;
        Food food;
        int antsLeft = -1;

        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow, 1.0f);
        
        if (hit.collider.TryGetComponent<Container>(out container))
        {
            antsLeft = container.maxCarry - container.carryNum;
        }
        else if (hit.collider.TryGetComponent<Food>(out food))
        {
            antsLeft = food.maxCarry - food.carryNum;
        }
        
        //print("Object: " + hit.collider.gameObject.name + " Ants left: " + antsLeft);
        AntDisplay antDisplay = GetComponentInChildren<AntDisplay>();
        antDisplay.DisplayAnts(antsLeft);
    }
}
