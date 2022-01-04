using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManAnimation : MonoBehaviour
{
    private Animator animator;
    private Transform t;
    [SerializeField]
    private float x = 0;

    private Vector3 hiddenpos = new Vector3(-20, -0.02f, -106);

    private Vector3 right2leftpos = new Vector3(-18, -0.02f, -0.56f);
    private Vector3 right2leftrot = new Vector3(0, -270, 0);

    private Vector3 left2rightpos = new Vector3(20, -0.02f, -0.56f);
    private Vector3 left2rightrot = new Vector3(0, -90, 0);

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        x = t.localPosition.x;
        if (t.localPosition.x < -80)
        {
            t.localPosition = hiddenpos;
            animator.SetTrigger("Waiting");
        }
        if (t.localPosition.x > 80)
        {
            t.localPosition = hiddenpos;
            animator.SetTrigger("Waiting");
        }
    }

    public void Right2Left()
    {
        t.localRotation = Quaternion.Euler(right2leftrot);
        t.localPosition = right2leftpos;
        animator.SetTrigger("Walking");
    }

    public void Left2Right()
    {
        t.localRotation = Quaternion.Euler(left2rightrot);
        t.localPosition = left2rightpos;
        animator.SetTrigger("Walking");
    }
}
