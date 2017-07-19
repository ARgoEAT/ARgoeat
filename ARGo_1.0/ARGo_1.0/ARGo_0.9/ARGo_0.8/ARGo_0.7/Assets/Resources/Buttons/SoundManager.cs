using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip left_right_sound;
    public AudioClip confirm_sound;
    public AudioClip finish_shound;
    public AudioClip Error_sound;
    public AudioClip back_sound;

    AudioSource myAudio;

    public static SoundManager instance;

    void Awake()
    {
        if (SoundManager.instance == null)
            SoundManager.instance = this;
    }

	// Use this for initialization
	void Start () {
        myAudio = GetComponent<AudioSource>();
	}

    public void PlaySound(int choiceSound)
    {
        if (choiceSound == 0) // 좌우버튼
            myAudio.PlayOneShot(left_right_sound); // 이 함수가 안에있는 사운드 재생.

        else if (choiceSound == 1)
            myAudio.PlayOneShot(confirm_sound);

        else if (choiceSound == 2)
            myAudio.PlayOneShot(finish_shound);

        else if (choiceSound == 3)
            myAudio.PlayOneShot(Error_sound);

        else if (choiceSound == 4)
            myAudio.PlayOneShot(back_sound);
    }
   

	// Update is called once per frame
	void Update () {
		
	}
}
