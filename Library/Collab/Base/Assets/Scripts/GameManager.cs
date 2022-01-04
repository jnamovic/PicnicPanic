using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public GameManager GameManagerSingleton;

    public int currentLevel = -1;

    public Animator tutorialAnimator;
    public UnityEvent onLevel1Start;
    public UnityEvent onLevel2Start;

    private int level1Count = -1;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameManagerSingleton = this;
        StartCoroutine("YawnSound");
        //DrawArrow();
    }

    private void Update()
    {
        if(Scoreboard.GetScore() > 4 && currentLevel != 2)
        {
            onLevel2Start?.Invoke();
            currentLevel = 2;
        }
    }

    IEnumerator YawnSound() {
        float time = 2f;
        while(time > 0) {
            time -= Time.deltaTime;
            yield return new WaitForSeconds(0f);
        }
        FMODManager.Instance.CharacterYawn();
    }

    public void CompleteSingleTutorial() {
        if (currentLevel == -1) {
            tutorialAnimator.SetBool("SingleTutorialComplete", true);
            //tutorialAnimator.Play("IdleFootTap", 0, 0);
            Debug.Log("Single Tutorial Complete!");
            currentLevel = 0;
        }
    }

    public void CompleteMultipleTutorial() {
        if (currentLevel == 0) {
            tutorialAnimator.SetBool("MultipleTutorialComplete", true);
            Debug.Log("Multiple Tutorial Complete!");
            onLevel1Start.Invoke();
            currentLevel = 1;
            FMODManager.Instance.GameStart();
        }
    }

    public void ProgressLevel() {
        level1Count++;
        Debug.Log("Progress is " + level1Count);
        if (level1Count == 2) {
            Debug.Log("MOVING ON!");
            onLevel2Start.Invoke();
        }
    }

    public void DrawArrow() {
        /*
        TeleportArc teleportArc = GetComponent<TeleportArc>();
        Vector3 pointerStart = new Vector3(10,10,10);
        Vector3 arcVelocity = new Vector3(-0.5f, 7.2f, -7.0f);
        RaycastHit hitInfo;
        teleportArc.Show();
        teleportArc.SetArcData( pointerStart, arcVelocity, true, false );
	    teleportArc.DrawArc( out hitInfo );
        teleportArc.Show();
        */      
    }

    public void CharacterYawn() {
        FMODManager.Instance.CharacterYawn();
    }

    public void CharacterAh() {
        Debug.Log("gm ah");
        FMODManager.Instance.CharacterAh();
    }
}
