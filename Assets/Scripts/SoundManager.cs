using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> ThrowAudioClips;
    public List<AudioClip> PickUpAudioClips;

    public float lowPitchRange = 0.97f;
    public float highPitchRange = 1.03f;

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickupSound()
    {
        int shouldPlay = Random.Range(0, 4); // returns 0, 1, 2, or 3
        if (PickUpAudioClips.Count > 0 && shouldPlay > 0) // gives 75% chance of playing sound if audioclips not empty
        {
            int randomIndex = Random.Range(0, PickUpAudioClips.Count);
            float randomPitch = Random.Range(lowPitchRange, highPitchRange); // Modifies audio file slighly for varied sounds

            source.Stop();
            source.pitch = randomPitch;
            source.clip = PickUpAudioClips[randomIndex];
            source.Play();
        }

    }

    public void ThrowSound()
    {
        int shouldPlay = Random.Range(0, 4); // returns 0, 1, 2, or 3
        if (ThrowAudioClips.Count > 0 && shouldPlay > 0) // gives 75% chance of playing sound if audioclips not empty
        {
            int randomIndex = Random.Range(0, ThrowAudioClips.Count);
            float randomPitch = Random.Range(lowPitchRange, highPitchRange); // Modifies audio file slighly for varied sounds

            source.Stop();
            source.pitch = randomPitch;
            source.clip = ThrowAudioClips[randomIndex];
            source.Play();
        }
    }
}
