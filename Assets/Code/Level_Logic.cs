using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Logic : MonoBehaviour {

    [SerializeField] int max_rutes = 9;
    int actual_rute;
    [SerializeField] float time = 60.0f;


	// Use this for initialization
	void Start () {
        //max_rutes = 9;
        actual_rute = 0;
        //time = 60.0f;
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        if (actual_rute > max_rutes)
            SceneManager.LoadScene(0);  // tots els recorreguts complerts, carrega nou lvl o guanya
        if (time < 0.0f)
            SceneManager.LoadScene(0);  // no temps, perd

        
    }

    public void incTime(float extra_time) {
        Debug.Log(extra_time);
        time += extra_time;
    }
}
