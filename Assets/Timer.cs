using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public int timeRemaining = 150;
    public UnityEvent onTimerEnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void StartTimer()
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1;
    }

    IEnumerator LoseTime()
    {
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            timeRemaining--;
            //GetComponent<TextMeshPro>().text = ("" + timeRemaining);

            if (timeRemaining > 0 && timeRemaining < 30) {
                //GetComponent<TextMeshPro>().color = new Color(180.0f, 0, 0);
            }
        }
        
        onTimerEnd.Invoke();
    }

}
