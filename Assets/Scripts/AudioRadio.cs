using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioRadio : MonoBehaviour {

    AudioClip[][] radioChSong;
    [SerializeField] AudioSource radioSource;
    GameObject radioButton;
    Sprite[] imgChannels;
    int nChannel;
    int currentChannel;
    int currentSong;
    bool pressed;

    void Start () {
        nChannel = 2;
        radioChSong = new AudioClip[nChannel][];
        for (int i = 0; i < nChannel; ++i) {
            AudioClip[] ch = Resources.LoadAll<AudioClip>("Audio/Radio/Ch" + (i + 1));
            radioChSong[i] = ch;
        }
        radioButton = GameObject.Find("RadioButton");
        imgChannels = new Sprite[nChannel];
        imgChannels = Resources.LoadAll<Sprite>("Sprites/Radio");
        currentChannel = currentSong = 0;
        radioSource.clip = radioChSong[currentChannel][currentSong];
        radioSource.Play();
        radioButton.GetComponent<Image>().sprite = imgChannels[currentChannel];
        pressed = false;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.F) && !pressed) {
            pressed = true;
            ++currentChannel;
            currentChannel %= 3; // 3 is the num of channels
            radioButton.GetComponent<Image>().sprite = imgChannels[currentChannel];
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
        if (pressed && Input.GetKeyUp(KeyCode.F)) pressed = false;
    }
}
