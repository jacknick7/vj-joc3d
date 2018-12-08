using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLogic : MonoBehaviour {

    [SerializeField] int level = 3;
    [SerializeField] int max_routes = 10;
    int actual_route;
    [SerializeField] float time = 60.0f;
    float old_time;
    GameObject[] vehicles;
    GameObject[] destinations;
    GameObject current_camera;
    GameObject characterUI;
    GameObject maplimits;
    string current_player_name = "none";
    int tmp = 0;


	// Use this for initialization
	void Start () {
        //max_rutes = 9;
        old_time = time;
        vehicles = new GameObject[max_routes];
        destinations = new GameObject[max_routes];
        actual_route = 0;
        for (int i = 0; i < max_routes; ++i) {
            vehicles[i] = GameObject.Find("MainPlayer" + i);
            vehicles[i].GetComponent<CarController>().setCarStatus(0);
            destinations[i] = GameObject.Find("Destination" + i);
            destinations[i].SetActive(false);
        }
        vehicles[actual_route].GetComponent<CarController>().setCarStatus(1);
        destinations[actual_route].SetActive(true);
        current_camera = GameObject.Find("Main Camera");
        current_camera.GetComponent<CameraController>().player = vehicles[actual_route];
        characterUI = GameObject.Find("Character");
        characterUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UICharacter-" + level + '-' + (actual_route + 1));
        maplimits = GameObject.Find("MapLimits");
        maplimits.GetComponent<MapLimits>().setCurrentPlayerName("MainPlayer" + actual_route);
        current_player_name = "MainPlayer" + actual_route;
        //time = 60.0f;
    }
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        if (time < 0.0f)
            SceneManager.LoadScene(0);  // no temps, perd
        if (Input.GetAxis("Reset") != 0 && tmp == 0) {
            resetRoute();
            tmp = 70;
        }
        if (tmp > 0) --tmp;
    }

    public void incTime(float extra_time) {
        Debug.Log(extra_time);
        time += extra_time;
    }

    public void newRoute() {
        for (int i = 0; i <= actual_route; ++i){
            vehicles[i].GetComponent<CarController>().setCarStatusAndReset(2);
        }
        destinations[actual_route].SetActive(false);
        actual_route++;
        if (actual_route == max_routes)
            SceneManager.LoadScene(0);  // tots els recorreguts complerts, carrega nou lvl o guanya
        vehicles[actual_route].GetComponent<CarController>().setCarStatus(1);
        destinations[actual_route].SetActive(true);
        current_camera.GetComponent<CameraController>().player = vehicles[actual_route];
        characterUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UICharacter-" + level + '-' + (actual_route + 1));
        maplimits.GetComponent<MapLimits>().setCurrentPlayerName("MainPlayer" + actual_route);
        current_player_name = "MainPlayer" + actual_route;
        old_time = time;
    }

    public void resetRoute() {
        for (int i = 0; i <= actual_route; ++i) {
            vehicles[i].GetComponent<CarController>().setCarStatusAndReset(2);
        }
        // aixo es una cutrada PUTO UNITY DE MERDA
        vehicles[actual_route].GetComponent<CarController>().setCarStatusAndReset(0);
        vehicles[actual_route].GetComponent<CarController>().setCarStatusAndReset(1);
        old_time -= 1.0f; 
        time = old_time;
    }

    public string getCurrentPlayerName() {
        return current_player_name;
    }

    public int getMaxRoutes() {
        return max_routes;
    }

    public GameObject getCurrentCar() {
        return vehicles[actual_route];
    }

    public GameObject getCurrentDestination() {
        return destinations[actual_route];
    }

    public float getTime() {
        return time;
    }
}
