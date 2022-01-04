using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FMODManager : MonoBehaviour
{
    public AudioClip Score;
    public AudioClip characterYawn;
    public AudioClip characterAh;
    public AudioClip tutorial;
    public AudioClip glass;
    public AudioClip chips;
    public AudioClip chickenThud;
    public AudioClip hm;
    public AudioClip wobble;
    public AudioClip cheering;
    private float lowPitchRange = 0.97f;
    private float highPitchRange = 1.03f;

    private AudioSource source;


    FMOD.Studio.EventInstance BGM;
    //FMOD.Studio.ParameterInstance Instinct;
    //FMOD.Studio.ParameterInstance Stage;
    FMOD.Studio.ParameterInstance GameStarted;

    private AudioSource character;

    private static FMODManager instance = null;
    public static FMODManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        BGM = FMODUnity.RuntimeManager.CreateInstance("event:/BGM");
        //BGM.getParameter ("Instinct", out Instinct);
        //BGM.getParameter ("Stage", out Stage);
        BGM.getParameter("GameStarted", out GameStarted);

    }
    void Start()
    {
        character = gameObject.AddComponent<AudioSource>();
        source = gameObject.AddComponent<AudioSource>();
        BGM.start();
    }


    public void CharacterYawn()
    {
        character.clip = characterYawn;
        character.Play();
    }

    public void CharacterAh()
    {
        Debug.Log(
            "Fmod ah"
        );
        character.clip = characterAh;
        character.Play();
    }

    public void GameStart()
    {
        GameStarted.setValue(1);
    }

    public void TutorialStart()
    {
        GameStarted.setValue(0);
    }

    public void ScoreSound()
    {
        source.clip = Score;
        source.pitch = Random.Range(lowPitchRange, highPitchRange);
        source.Play();
    }

    public void TutorialSound()
    {
        source.clip = tutorial;
        source.Play();
    }

    public void GlassSound()
    {
        source.clip = glass;
        source.Play();
    }

    public void ChipSound()
    {
        source.clip = chips;
        source.Play();
    }

    public void ThudSound()
    {
        source.clip = chickenThud;
        source.Play();
    }

    public void HMSound()
    {
        source.clip = hm;
        source.Play();
    }

    public void WobbleSound()
    {
        source.clip = wobble;
        source.Play();
    }

    public void CheeringSound()
    {
        source.clip = cheering;
        source.Play();
    }
}