using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLimits : MonoBehaviour {

    GameObject logic;

    void Start() {
        logic = GameObject.Find("Level Logic");
    }

    void OnTriggerEnter(Collider other) {
        string current_player_name = logic.GetComponent<LevelLogic>().getCurrentPlayerName();
        if (other.gameObject.name == current_player_name) {
            other.gameObject.GetComponent<CarController>().setCarStatus(0);
            other.gameObject.GetComponent<CarController>().setCarStatus(1);
        }
    }
}
