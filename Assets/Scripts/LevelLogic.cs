﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour {

    [SerializeField] int max_routes = 10;
    int actual_route;
    [SerializeField] float time = 60.0f;
    GameObject[] vehicles;
    GameObject[] destinations;
    GameObject current_camera;
    string current_player_name = "none";


	// Use this for initialization
	void Start () {
        //max_rutes = 9;
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
        current_player_name = "MainPlayer" + actual_route;
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
        vehicles[actual_route].GetComponent<CarController>().setCarStatus(2);
        destinations[actual_route].SetActive(false);
        actual_route++;
        if (actual_route == max_routes)
            SceneManager.LoadScene(0);  // tots els recorreguts complerts, carrega nou lvl o guanya
        vehicles[actual_route].GetComponent<CarController>().setCarStatus(1);
        destinations[actual_route].SetActive(true);
        current_camera.GetComponent<CameraController>().player = vehicles[actual_route];
        current_player_name = "MainPlayer" + actual_route;
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
}