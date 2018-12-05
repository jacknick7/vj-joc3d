using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteCompleted : MonoBehaviour {

    [SerializeField] GameObject logic;

    void Start () {
        logic = GameObject.Find("Level Logic");
    }

    public void OnTriggerEnter(Collider other)
    {
        string current_player_name = logic.GetComponent<LevelLogic>().getCurrentPlayerName();
        if (other.gameObject.name == current_player_name) {
            logic.GetComponent<LevelLogic>().newRoute();
        }
    }
}
