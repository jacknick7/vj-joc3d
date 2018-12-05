using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

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

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    public int carId = 0;

    public WheelCollider WheelFL;//the wheel colliders
    public WheelCollider WheelFR;
    public WheelCollider WheelBL;
    public WheelCollider WheelBR;

    //public GameObject FL;//the wheel gameobjects
    //public GameObject FR;
    //public GameObject BL;
    //public GameObject BR;

    public float topSpeed = 250f;//the top speed
    public float maxTorque = 200f;//the maximum torque to apply to wheels
    public float maxSteerAngle = 45f;
    public float currentSpeed;
    public float maxBrakeTorque = 2200;


    private float Forward;//forward axis
    private float Turn;//turn axis
    private float Brake;//brake axis

    private Rigidbody rb;//rigid body of car

    public void setUserControlled(){
        carStatus = 1;
        this.gameObject.SetActive(true);
    }

    public void setUserRecorded(){
        carStatus = 2;
    }

    void Start(){
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        /*if (carStatus < 1){
            this.gameObject.SetActive(false);
        }
        else */if(carStatus < 2){
            userMovement.Clear();
        }
    }

    private void Reset(){
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    void FixedUpdate(){
        if (carStatus == 1){
            Forward = Input.GetAxis("Vertical");
            Turn = Input.GetAxis("Horizontal");
            Brake = Input.GetAxis("Jump");

            WheelFL.steerAngle = maxSteerAngle * Turn;
            WheelFR.steerAngle = maxSteerAngle * Turn;

            currentSpeed = 2 * 22 / 7 * WheelBL.radius * WheelBL.rpm * 60 / 1000; //formula for calculating speed in kmph

            if (currentSpeed < topSpeed)
            {
                WheelBL.motorTorque = maxTorque * Forward;//run the wheels on back left and back right
                WheelBR.motorTorque = maxTorque * Forward;
            }//the top speed will not be accurate but will try to slow the car before top speed

            WheelBL.brakeTorque = maxBrakeTorque * Brake;
            WheelBR.brakeTorque = maxBrakeTorque * Brake;
            WheelFL.brakeTorque = maxBrakeTorque * Brake;
            WheelFR.brakeTorque = maxBrakeTorque * Brake;

            userMovement.Enqueue(new movement(transform.position, transform.rotation));
        }
        else if(carStatus == 2){
            if (userMovement.Count > 0)
            {
                transform.position = userMovement.Peek().position;
                transform.rotation = userMovement.Peek().rotation;
                userMovement.Dequeue();
            }
        }

    }
    void Update()//update is called once per frame
    {
        Quaternion flq;//rotation of wheel collider
        Vector3 flv;//position of wheel collider
        WheelFL.GetWorldPose(out flv, out flq);//get wheel collider position and rotation
        //FL.transform.position = flv;
        //FL.transform.rotation = flq;

        Quaternion Blq;//rotation of wheel collider
        Vector3 Blv;//position of wheel collider
        WheelBL.GetWorldPose(out Blv, out Blq);//get wheel collider position and rotation
        //BL.transform.position = Blv;
        //BL.transform.rotation = Blq;

        Quaternion fRq;//rotation of wheel collider
        Vector3 fRv;//position of wheel collider
        WheelFR.GetWorldPose(out fRv, out fRq);//get wheel collider position and rotation
        //FR.transform.position = fRv;
        //FR.transform.rotation = fRq;

        Quaternion BRq;//rotation of wheel collider
        Vector3 BRv;//position of wheel collider
        WheelBR.GetWorldPose(out BRv, out BRq);//get wheel collider position and rotation
                                               //BR.transform.position = BRv;
                                               //BR.transform.rotation = BRq;

        if ((Mathf.Abs(Vector3.Dot(transform.up, Vector3.down)) < 0.125f) && (carStatus == 1)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    public void setCarStatus(int status) {
        carStatus = status;
        gameObject.SetActive(!(status == 0));
    }
}