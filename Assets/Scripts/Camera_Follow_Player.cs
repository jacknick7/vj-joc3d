using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow_Player : MonoBehaviour {

    Transform PlayerTrasnsform;

    Vector3 relativeDistancePlayer;
    Vector3 oldPositionPlayer;


    // Use this for initialization
    void Start () {
        PlayerTrasnsform = GameObject.Find("Player").transform;
        if (PlayerTrasnsform == null) Debug.LogError("Error: No player found.");
        else {
            relativeDistancePlayer = PlayerTrasnsform.position - this.transform.position;
            oldPositionPlayer = PlayerTrasnsform.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerTrasnsform == null) Debug.LogError("Error: No player found.");
        else if (oldPositionPlayer != PlayerTrasnsform.position) {
            this.transform.position = PlayerTrasnsform.position - relativeDistancePlayer;
            oldPositionPlayer = PlayerTrasnsform.position;
        }
	}
}
