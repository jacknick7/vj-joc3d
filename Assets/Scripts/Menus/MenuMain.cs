using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour {

    public int LevelOne = 1;

	public void StartLevelOne() {
        GlobalVars.Level = 1;
        GlobalVars.Time = 600;
        GlobalVars.RadioCh = Random.Range(0, 1);
        GlobalVars.RadioSong = Random.Range(0, 2);
        SceneManager.LoadScene(LevelOne);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void SwitchOptions() {
        GameObject.Find("MenuMain").SetActive(false);
        transform.parent.Find("MenuOptions").gameObject.SetActive(true);
    }

    public void SwitchInstructions() {
        GameObject.Find("MenuMain").SetActive(false);
        transform.parent.Find("MenuInstructions").gameObject.SetActive(true);
    }
}
