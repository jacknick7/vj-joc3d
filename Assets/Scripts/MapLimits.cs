using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLimits : MonoBehaviour {

    GameObject logic;
    string current_player_name;

    void Start() {
        logic = GameObject.Find("Level Logic");
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == current_player_name) {
            GameObject.Find("WrongWayCanvas").gameObject.GetComponent<WrongWayController>().activateWrongWay();
            logic.GetComponent<LevelLogic>().resetRoute();
        }
    }

    public void setCurrentPlayerName(string name) {
        current_player_name = name;
    }
}
