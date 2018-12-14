using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

struct movement{
    public Vector3 position;
    public Quaternion rotation;

    public movement(Vector3 po, Quaternion ro){
        position = po;
        rotation = ro;
    }
}

public class CarController : MonoBehaviour {
    private int carStatus = 0;
    private Queue<movement> userMovement = new Queue<movement>();
    private Queue<movement> userMovementCurrent = new Queue<movement>();

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    public WheelCollider WheelFL;
    public WheelCollider WheelFR;
    public WheelCollider WheelBL;
    public WheelCollider WheelBR;

    public float topSpeed = 150f;
    public float maxTorque = 65f;
    public float maxSteerAngle = 45f;
    public float currentSpeed = 0f;

    [SerializeField] GameObject smog;

    private float Forward;
    private float Turn;

    float velocity;
    bool reducedVel;
    GameObject audioCrash;

    public void setUserControlled(){
        carStatus = 1;
        this.gameObject.SetActive(true);
    }

    public void setUserRecorded(){
        carStatus = 2;
    }

    void Start(){
        smog.GetComponent<ParticleSystem>().Simulate(0.0f);
        smog.GetComponent<ParticleSystem>().Stop();
        velocity = 5.0f;
        reducedVel = false;
        audioCrash = GameObject.Find("AudioCrash");

        originalPosition = transform.position;
        originalRotation = transform.rotation;
        /*if (carStatus < 1){
            this.gameObject.SetActive(false);
        }
        else */if(carStatus < 2){
            userMovement.Clear();
        }
        //this.gameObject.GetComponent<Rigidbody>().maxAngularVelocity = 1f;
    }

    private void Reset(){
        smog.GetComponent<ParticleSystem>().Simulate(0.0f);
        smog.GetComponent<ParticleSystem>().Stop();
        currentSpeed = 0.0f;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        velocity = 5.0f;
        reducedVel = false;
    }

    void FixedUpdate(){
        if (carStatus == 1){
            Forward = -Input.GetAxis("Vertical");
            Turn = Input.GetAxis("Horizontal");

            WheelFL.steerAngle = maxSteerAngle * Turn;
            WheelFR.steerAngle = maxSteerAngle * Turn;

            currentSpeed = 2 * 22 / 7 * WheelBL.radius * WheelBL.rpm * 60 / 1000;

            print(WheelBL.rpm);

            // print(this.gameObject.GetComponent<Rigidbody>().velocity);
            //if (Input.GetKey(KeyCode.T)) {
            //    if (velocity == 10.0f) velocity = 5.0f;
            //    else if (velocity == 5.0f) velocity = 10.0f;
            //}

            Vector3 vel = this.gameObject.GetComponent<Rigidbody>().velocity;
            if (vel.x < 0.0f && vel.x < -velocity) vel.x = -velocity;
            if (vel.x > 0.0f && vel.x > velocity) vel.x = velocity;
            if (vel.y < 0.0f && vel.y < -velocity) vel.y = -velocity;
            if (vel.y > 0.0f && vel.y > velocity) vel.y = velocity;
            if (vel.z < 0.0f && vel.z < -velocity) vel.z = -velocity;
            if (vel.z > 0.0f && vel.z > velocity) vel.z = velocity;
            this.gameObject.GetComponent<Rigidbody>().velocity = vel;

            if (currentSpeed > -topSpeed) {
                float dir = -1.0f;
                if (Input.GetKey(KeyCode.S)) dir = 1.0f;
                WheelBL.motorTorque = maxTorque * dir;
                WheelBR.motorTorque = maxTorque * dir;
            }

            userMovement.Enqueue(new movement(transform.position, transform.rotation));
        }
        else if(carStatus == 2){
            if (userMovementCurrent.Count > 0){
                transform.position = userMovementCurrent.Peek().position;
                transform.rotation = userMovementCurrent.Peek().rotation;
                if (userMovementCurrent.Count > 1){
                    userMovementCurrent.Dequeue();
                }
            }
        }

    }
    void Update()
    {
        Quaternion flq, Blq, fRq, BRq;
        Vector3 flv, Blv, fRv, BRv;
        WheelFL.GetWorldPose(out flv, out flq);
        WheelBL.GetWorldPose(out Blv, out Blq);
        WheelFR.GetWorldPose(out fRv, out fRq);
        WheelBR.GetWorldPose(out BRv, out BRq);

        if ((Mathf.Abs(Vector3.Dot(transform.up, Vector3.down)) < 0.125f) && (carStatus == 1)){
            GameObject.Find("Level Logic").GetComponent<LevelLogic>().resetRoute();
        }

    }

    public void setCarStatus(int status) {
        carStatus = status;
        gameObject.SetActive(!(status == 0));
        smog.GetComponent<ParticleSystem>().Simulate(0.0f);
        smog.GetComponent<ParticleSystem>().Stop();
    }

    public void setCarStatusAndReset(int status) {
        carStatus = status;
        gameObject.SetActive(!(status == 0));
        Reset();
        if(status == 1){
            userMovement.Clear();
            userMovementCurrent = new Queue<movement>();
        }
        else if (status == 2) {
            userMovementCurrent = new Queue<movement>(userMovement);
        }
        //print("RESETED WITH POSITIONS");
        //print(originalPosition);
    }

    private void OnCollisionEnter(Collision collision) {
        if (carStatus == 1) {
            string name = collision.gameObject.name;
            name = name + "+++++";
            string avoidName1 = name.Substring(0, 4);
            string avoidName2 = name.Substring(0, 5);

            if (avoidName1 != "Cube" && avoidName2 != "Plane")
            {
                if (!smog.GetComponent<ParticleSystem>().IsAlive()) smog.GetComponent<ParticleSystem>().Play();
                audioCrash.GetComponent<AudioSource>().Play();
                if (!reducedVel)
                {
                    reducedVel = true;
                    velocity /= 3;
                }
            }
        }
        else {
            string name = collision.gameObject.name;
            name = name + "+++++";
            string avoidName1 = name.Substring(0, 4);
            string avoidName2 = name.Substring(0, 5);

            if (avoidName1 != "Cube" && avoidName2 != "Plane") {
                if (!smog.GetComponent<ParticleSystem>().IsAlive()) smog.GetComponent<ParticleSystem>().Play();
                audioCrash.GetComponent<AudioSource>().volume = 0.1f;
                audioCrash.GetComponent<AudioSource>().Play();
            }
        }
    }
}
