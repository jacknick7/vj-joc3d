using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Object : MonoBehaviour {

    [SerializeField] float time_increment = 10.0f;
    [SerializeField] string player_name;
    [SerializeField] GameObject logic;
    bool enter = false;

    private void Start() {
        player_name = GameObject.Find("MainPlayer").name;
        logic = GameObject.Find("Level Logic");
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == player_name && !enter) {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            logic.GetComponent<Level_Logic>().incTime(time_increment);
            enter = true;
        }   
    }

    public void OnTriggerExit(Collider other) {
        if (other.gameObject.name == player_name) {
            Destroy(this.gameObject);
        }
    }
}
