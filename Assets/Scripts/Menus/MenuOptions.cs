using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour {

    public void SwitchMain() {
        GameObject.Find("MenuOptions").SetActive(false);
        transform.parent.Find("MenuMain").gameObject.SetActive(true);
    }
}
