using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour {
    [HideInInspector]
    public GameManager GameManagerSingleton;

    public int currentLevel = -3;

    public Animator tutorialAnimator;
    public GameObject timer;

    public UnityEvent onGameStart;
    public UnityEvent onLevelNeg2Start;
    public UnityEvent onLevelNeg1Start;
    public UnityEvent onLevel0Start;
    public UnityEvent onLevel1Start;
    public UnityEvent onLevel2Start;
    public UnityEvent onLevel3Start;
    public UnityEvent onLevel4Start;
    public UnityEvent onGameEnd;

    private int level1Count = 0;
    private int level2Count = 0;
    private int level3Count = 0;

    // Start is called before the first frame update
    void Start () {
        GameManagerSingleton = this;
    }

    private void Update () {
        // if (Scoreboard.GetScore() == 1 && currentLevel < -2)
        // {
        //     onLevelNeg2Start?.Invoke();
        //     currentLevel = -2;
        // }
        // else if (Scoreboard.GetScore() == 2 && currentLevel < -1)
        // {
        //     onLevelNeg1Start?.Invoke();
        //     currentLevel = -1;
        // }
        // else if (Scoreboard.GetScore() >= 4 && currentLevel < 2)
        // {
        //     onLevel2Start?.Invoke();
        //     currentLevel = 2;
        // }
    }

    public void StartGame () {
        onGameStart.Invoke();
    }

    public void CompleteLevelNeg3 () {
        if (currentLevel == -3) {
            Debug.Log ("Level -3 Complete, Single Ant on Food");

            currentLevel = -2;
            onLevelNeg2Start?.Invoke();
        }
    }

    public void CompleteLevelNeg2 () {
        if (currentLevel == -2) {
            Debug.Log ("Level -2 Complete, Multiple Ants on Food");

            currentLevel = -1;
            onLevelNeg1Start?.Invoke ();
        }
    }

    public void CompleteLevelNeg1 () {
        if (currentLevel == -1) {
            Debug.Log ("Level -1 Complete, Single Ant on Foot");

            tutorialAnimator.SetBool ("SingleTutorialComplete", true);
            currentLevel = 0;
            onLevel0Start?.Invoke ();
        }
    }

    public void CompleteLevelNeg0 () {
        if (currentLevel == 0) {
            Debug.Log ("Level 0 Complete, Multiple Ants on Foot");

            tutorialAnimator.SetBool ("MultipleTutorialComplete", true);
            FMODManager.Instance.GameStart ();
            timer.GetComponent<Timer> ().StartTimer();
            currentLevel = 1;
            onLevel1Start.Invoke();
        }
    }

    public void ProgressLevel1 () {
        if (currentLevel == 1) {
            level1Count++;
            Debug.Log ("Progress Level 1: " + level1Count);
            if (level1Count == 4) {
                Debug.Log ("Level 1 Complete");
                currentLevel = 2;
                onLevel2Start.Invoke ();
            }
        }
    }

    public void ProgressLevel2 () {
        if (currentLevel == 2) {
            level2Count++;
            Debug.Log ("Progress Level 2: " + level2Count);
            if (level2Count == 5) {
                Debug.Log ("Level 2 Complete");
                currentLevel = 3;
                onLevel3Start.Invoke ();
            }
        }
    }

    public void ProgressLevel3 () {
        if (currentLevel == 3) {
            level3Count++;
            Debug.Log ("Progress Level 3: " + level3Count);
            if (level3Count == 1)
                Debug.Log ("Level 3 Complete");
            currentLevel = 4;
            onLevel4Start.Invoke ();
        }
    }

    public void FinishGame () {
        onGameEnd.Invoke ();
    }

    public void Quit () {
        Application.Quit ();
    }

    public void ScoreboardPopup() {
        Scoreboard.Popup();
    }

    public void DrawArrow () {
        TeleportArc teleportArc = GetComponent<TeleportArc> ();
        Vector3 pointerStart = new Vector3 (10, 10, 10);
        Vector3 arcVelocity = new Vector3 (-0.5f, 7.2f, -7.0f);
        RaycastHit hitInfo;
        teleportArc.Show ();
        teleportArc.SetArcData (pointerStart, arcVelocity, true, false);
        teleportArc.DrawArc (out hitInfo);
        teleportArc.Show ();
    }

    public void CharacterYawn () {
        FMODManager.Instance.CharacterYawn ();
    }

    public void CharacterAh () {
        FMODManager.Instance.CharacterAh ();
    }

    public void CharacterWalkBack()
    {

    }
}