using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIFood : MonoBehaviour
{
    public UnityEvent DoTheThing;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoIt() {
        DoTheThing.Invoke();
        Destroy(transform.parent.gameObject);
        //yes
    }

    public void Reset()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        transform.parent = null;
    }


}
