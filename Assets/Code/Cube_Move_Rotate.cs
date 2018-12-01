using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Move_Rotate : MonoBehaviour {

    [SerializeField] Vector3 movement = new Vector3(0.01f, 0.0f, 0.0f);

    [SerializeField] float rotateX = 0.2f;
    [SerializeField] float rotateY = 1.0f;
    [SerializeField] float rotateZ = 2.5f;

    float scale = 1.0f;
    [SerializeField] float scaleIncrement = 0.001f;

    [SerializeField] float speed = 1.0f;
    [SerializeField] bool useSpeed = false;

    [SerializeField] Space transformSpace = Space.World;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
        speed = Time.deltaTime;
        Move();
        Rotate();
        Scale();
        scale += scaleIncrement;
	}

    public void Move() {
        Vector3 translation = movement;
        if (useSpeed) translation *= speed;
        transform.Translate(translation, transformSpace);
    }

    public void Rotate() {
        transform.Rotate(new Vector3(rotateX, rotateY, rotateZ), transformSpace);
    }

    public void Scale() {
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
