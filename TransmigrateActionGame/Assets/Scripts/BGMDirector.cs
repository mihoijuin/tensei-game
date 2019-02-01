using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMDirector : MonoBehaviour {

    public AudioClip[] audioClips;

    private AudioSource audioSource;

    private enum BGM
    {
        NONE = -1,

        STAGE = 0,
        GAMEOVER,

        NUM
    }

    void Awake () {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
	}


    public void PlayStageMusic()
    {
        audioSource.clip = audioClips[(int)BGM.STAGE];
        audioSource.Play();
    }


    public void PlayGameOverMusic()
    {
        audioSource.clip = audioClips[(int)BGM.GAMEOVER];
        audioSource.Play();
    }


    public void StopBGM()
    {
        audioSource.Stop();
    }
}
