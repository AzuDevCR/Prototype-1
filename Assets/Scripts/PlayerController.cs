using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float horizontalInput;
    [SerializeField] private float forwardInput;
    [SerializeField] private float horsePower;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometer;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;

    Rigidbody playerRb;

    Camera mainCam;
    Camera secondaryCam;

    private void Start() {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        secondaryCam = GameObject.Find("Secondary Camera").GetComponent<Camera>();
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //We get the Input
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        if (IsOnGround()) {
            //We aplly that input into the actual movement
            //transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            playerRb.AddRelativeForce(Vector3.forward * horsePower * forwardInput);
            //transform.Rotate(Vector3.up, turnSpeed * horizontalInput * forwardInput * Time.deltaTime);
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

            speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 3.6f); //2.237f for miles
            speedometer.SetText("Speed: " + speed + " Kmh");
        }
        
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            CameraChange();
        }
    }

    void CameraChange() {
        if (mainCam.isActiveAndEnabled) {
            secondaryCam.gameObject.SetActive(true);
            mainCam.gameObject.SetActive(false);
        }
        else {
            secondaryCam.gameObject.SetActive(false);
            mainCam.gameObject.SetActive(true);
        }
    }

    bool IsOnGround() {
        wheelsOnGround = 0;
        foreach(WheelCollider wheel in allWheels) {
            if (wheel.isGrounded) {
                wheelsOnGround++;
            }
        }
        return (wheelsOnGround == 4) ? true : false;
    }
}
