using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D currentBallRigidbody;
    [SerializeField] private SpringJoint2D currentBallSpringJoint;
    [SerializeField] private float delayDuration;
    private Camera mainCamera;
    private bool isBallDragged;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBallRigidbody == null) {
            return;
        }
        // check if user is touching the screen
        if(Touchscreen.current.primaryTouch.press.isPressed == false) {
            if (isBallDragged) {
                LaunchBall();
            }
            isBallDragged = false;
            return;
        }
        isBallDragged = true;
        currentBallRigidbody.isKinematic = true;
        // store screen position (pixels) of the touch in variable
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        // convert screen possition to unity world position
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
        // Debug.Log(worldPosition);
        currentBallRigidbody.position = worldPosition;
    }

    private void LaunchBall() {
        currentBallRigidbody.isKinematic = false;
        currentBallRigidbody = null;
        Invoke(nameof(DetachBall), delayDuration);
    }

    private void DetachBall() {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;
    }
}
