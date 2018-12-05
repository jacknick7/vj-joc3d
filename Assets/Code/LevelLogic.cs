using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour {

    [SerializeField] int max_rutes = 10;
    int actual_rute;
    [SerializeField] float time = 60.0f;
    GameObject[] vehicles;
    GameObject[] destinations;
    GameObject current_camera;
    string current_player_name = "none";


	// Use this for initialization
	void Start () {
        //max_rutes = 9;
        vehicles = new GameObject[max_rutes];
        destinations = new GameObject[max_rutes];
        actual_rute = 0;
        for (int i = 0; i < max_rutes; ++i) {
            vehicles[i] = GameObject.Find("MainPlayer" + i);
            destinations[i] = GameObject.Find("Destination" + i);
            vehicles[i].SetActive(false);
            destinations[i].SetActive(false);
        }
        vehicles[actual_rute].SetActive(true);
        destinations[actual_rute].SetActive(true);
        current_camera = GameObject.Find("Main Camera");
        current_camera.GetComponent<CameraController>().player = vehicles[actual_rute];
        current_player_name = "MainPlayer" + actual_rute;
        //time = 60.0f;
    }
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        if (time < 0.0f)
            SceneManager.LoadScene(0);  // no temps, perd
    }

    public void incTime(float extra_time) {
        Debug.Log(extra_time);
        time += extra_time;
    }

    public void newRoute() {
        vehicles[actual_rute].SetActive(false);
        destinations[actual_rute].SetActive(false);
        actual_rute++;
        if (actual_rute == max_rutes)
            SceneManager.LoadScene(0);  // tots els recorreguts complerts, carrega nou lvl o guanya
        vehicles[actual_rute].SetActive(true);
        destinations[actual_rute].SetActive(true);
        current_camera.GetComponent<CameraController>().player = vehicles[actual_rute];
        current_player_name = "MainPlayer" + actual_rute;
    }

    public string getCurrentPlayerName() {
        return current_player_name;
    }
}
