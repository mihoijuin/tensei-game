using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEDirector : MonoBehaviour {


    public AudioClip[] audioClips;

    private AudioSource audioSource;


    public enum SE
    {
        NONE = -1,

        GOODITEM = 0,
        BADITEM,
        DAMAGE,
        SWITCH,

        NUM
    }

    void Start () {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
	}
	

    public void PlaySE(SE se)
    {
        audioSource.clip = audioClips[(int)se];
        audioSource.Play();
    }



}
