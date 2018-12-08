﻿using System.Collections;
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
    [SerializeField] int ntimes = 5;
    GameObject[] times;
    GameObject day, weatherRain, weatherFog;
    string current_player_name = "none";
    bool pressed, dayChange;


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
        times = new GameObject[ntimes];
        for (int i = 0; i < ntimes; ++i) {
            times[i] = GameObject.Find("Time" + i);
            times[i].SetActive(false);
        }
        for (int i = 0; i < ntimes / 2; ++i) {
            times[i].SetActive(true);
            times[i].GetComponent<TimeObject>().setCurrentPlayerName("MainPlayer" + actual_route);
        }
        day = GameObject.Find("LightController");
        weatherRain = GameObject.Find("RainController");
        weatherFog = GameObject.Find("FogController");
        current_player_name = "MainPlayer" + actual_route;
        pressed = false;
        dayChange = false;
        levelConditions();
        //time = 60.0f;
    }
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        if (time < 0.0f)
            SceneManager.LoadScene(0);  // no temps, perd
        if (Input.GetKeyDown(KeyCode.R) && !pressed) {
            resetRoute();
            pressed = true;
        }
        if (pressed && Input.GetKeyUp(KeyCode.R)) pressed = false;
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
        for (int i = 0; i < ntimes; ++i) {
            times[i].GetComponent<TimeObject>().setCurrentPlayerName("MainPlayer" + actual_route);
            if (actual_route == 4 && i >= ntimes / 2) {
                times[i].SetActive(true);
            }
        }
        levelConditions();
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

    void levelConditions() {
        if (actual_route > 4 && !dayChange) {
            int dayProb = Random.Range(0, 3);
            if (dayProb == 0) {
                day.GetComponent<LightController>().changeDay();
                dayChange = true;
            }
        }
        int rainProb = Random.Range(0, 5);
        int fogProb = Random.Range(0, 5);
        if (rainProb == 0) weatherRain.GetComponent<RainController>().changeRain();
        if (fogProb == 0) weatherFog.GetComponent<FogController>().changeFog();
    }
}
