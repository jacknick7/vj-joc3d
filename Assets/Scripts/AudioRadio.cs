using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRadio : MonoBehaviour {

    AudioClip[][] radioChSong;
    [SerializeField] AudioSource radioSource;
    int nChannel;
    int currentChannel;
    int currentSong;
    int xd;

    void Start () {
        nChannel = 2;
        radioChSong = new AudioClip[nChannel][];
        for (int i = 0; i < nChannel; ++i) {
            AudioClip[] ch = Resources.LoadAll<AudioClip>("Audio/Radio/Ch" + (i + 1));
            radioChSong[i] = ch;
        }
        currentChannel = currentSong = 0;
        radioSource.clip = radioChSong[currentChannel][currentSong];
        radioSource.Play();
    }
	
	void Update () {
        if (Input.GetAxis("Radio") != 0 && xd == 0) {
            xd = 30;
            ++currentChannel;
            currentChannel %= 3; // 3 is the num of channels
            if (currentChannel == 2)
                radioSource.Stop();
            else {
                currentSong = 0;
                radioSource.clip = radioChSong[currentChannel][currentSong];
                radioSource.Play();
            }
        }
        else if (!radioSource.isPlaying && currentChannel != 2) {
            ++currentSong;
            currentSong %= 3; // 3 is the num of songs per channel
            radioSource.clip = radioChSong[currentChannel][currentSong];
            radioSource.Play();
        }
        if (xd > 0) --xd;
    }
}
