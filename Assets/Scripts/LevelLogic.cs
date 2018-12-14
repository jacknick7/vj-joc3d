using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLogic : MonoBehaviour {

    int level;
    [SerializeField] int max_routes = 10;
    [SerializeField] int max_levels = 3;
    [SerializeField] int scene_menu = 0;
    [SerializeField] int scene_win = 4;
    [SerializeField] int scene_lose = 5;
    int actual_route;
    float time;
    float old_time;
    GameObject[] vehicles;
    GameObject[] destinations;
    GameObject current_camera;
    GameObject characterUI;
    GameObject maplimits;
    int ntimes = 10;
    GameObject[] times;
    GameObject day, weatherRain, weatherFog;
    GameObject load;
    string current_player_name = "none";
    bool pressed, dayChange;


    // Use this for initialization
    void Start () {
        //max_rutes = 9;
        level = GlobalVars.Level;
        time = GlobalVars.Time;
        old_time = time;
        vehicles = new GameObject[max_routes];
        destinations = new GameObject[max_routes];
        actual_route = 0;
        GlobalVars.Route = actual_route;
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
        load = GameObject.Find("LoadingCanvas");
        load.SetActive(false);
        current_player_name = "MainPlayer" + actual_route;
        pressed = false;
        dayChange = false;
        roundConditions();
        //time = 60.0f;
    }
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        if (time < 0.0f)
            SceneManager.LoadScene(scene_lose);  // no temps, perd
        if (Input.GetKeyDown(KeyCode.R) && !pressed) {
            resetRoute();
            pressed = true;
        }
        if (pressed && Input.GetKeyUp(KeyCode.R)) pressed = false;
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(scene_menu);
    }

    public void incTime(float extra_time) {
        Debug.Log(extra_time);
        time += extra_time;
        old_time += extra_time;
    }

    public void newRoute() {
        for (int i = 0; i <= actual_route; ++i){
            vehicles[i].GetComponent<CarController>().setCarStatusAndReset(2);
        }
        destinations[actual_route].SetActive(false);
        if (actual_route + 1 == max_routes) {// tots els recorreguts complerts, carrega nou lvl o guanya
            GlobalVars.Time = time;
            if (level < max_levels) {
                GlobalVars.Level = GlobalVars.Level + 1;
                load.SetActive(true);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
                SceneManager.LoadScene(scene_win);
        }
        else
        {
            actual_route++;
            GlobalVars.Route = actual_route;
            vehicles[actual_route].GetComponent<CarController>().setCarStatus(1);
            destinations[actual_route].SetActive(true);
            current_camera.GetComponent<CameraController>().player = vehicles[actual_route];
            characterUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UICharacter-" + level + '-' + (actual_route + 1));
            maplimits.GetComponent<MapLimits>().setCurrentPlayerName("MainPlayer" + actual_route);
            for (int i = 0; i < ntimes; ++i)
            {
                times[i].GetComponent<TimeObject>().setCurrentPlayerName("MainPlayer" + actual_route);
                if (actual_route == 4 && i >= ntimes / 2)
                {
                    times[i].SetActive(true);
                }
            }
            roundConditions();
            current_player_name = "MainPlayer" + actual_route;
            old_time = time;
        }
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

    void roundConditions() {
        if(level == 1) {
            if(actual_route <= (max_routes/2)){
                day.GetComponent<LightController>().setDay(true);
                weatherRain.GetComponent<RainController>().setRain(false);
                weatherFog.GetComponent<FogController>().setFog(false);
            }
            else{
                day.GetComponent<LightController>().setDay(0.15f *(max_routes-actual_route-1));
            }
        }
        else if(level == 2){
            day.GetComponent<LightController>().setDay(false);
            if(actual_route <= (max_routes / 2)) {
                weatherRain.GetComponent<RainController>().setRain(false);
            }
            else{
                weatherRain.GetComponent<RainController>().setRain(true);
                day.GetComponent<LightController>().setDay(0.15f * (actual_route - (max_routes / 2)));
            }
            weatherFog.GetComponent<FogController>().setFog(false);
        }
        else if(level == 3){
            if (actual_route <= (max_routes / 3)){
                weatherRain.GetComponent<RainController>().setRain(true);
            }
            else{
                if (actual_route <= 3*(max_routes / 4)){
                    weatherRain.GetComponent<RainController>().setRain(false);
                }
                else{
                    weatherRain.GetComponent<RainController>().setRain(true);
                }
                weatherFog.GetComponent<FogController>().setFog(true);
            }

        }
        /*if (actual_route > 4 && !dayChange) {
            int dayProb = Random.Range(0, 3);
            if (dayProb == 0) {
                day.GetComponent<LightController>().changeDay();
                dayChange = true;
            }
        }
        int rainProb = Random.Range(0, 5);
        int fogProb = Random.Range(0, 5);
        if (rainProb == 0) weatherRain.GetComponent<RainController>().changeRain();
        if (fogProb == 0) weatherFog.GetComponent<FogController>().changeFog();*/
    }
}
