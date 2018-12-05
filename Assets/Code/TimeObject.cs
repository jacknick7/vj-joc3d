using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour {

    [SerializeField] float time_increment = 10.0f;
    [SerializeField] string current_player_name;
    [SerializeField] GameObject logic;
    bool enter = false;

    void Start() {
        logic = GameObject.Find("Level Logic");
    }

    public void OnTriggerEnter(Collider other) {
        current_player_name = logic.GetComponent<LevelLogic>().getCurrentPlayerName();
        if (other.gameObject.name == current_player_name && !enter) {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            logic.GetComponent<LevelLogic>().incTime(time_increment);
            enter = true;
        }   
    }

    public void OnTriggerExit(Collider other) {
        current_player_name = logic.GetComponent<LevelLogic>().getCurrentPlayerName();
        if (other.gameObject.name == current_player_name) {
            Destroy(this.gameObject);
        }
    }
}
