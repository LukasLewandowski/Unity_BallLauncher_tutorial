using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D pivot;
    [SerializeField] private float respawnDelay;
    [SerializeField] private float detachDelay;

    private Rigidbody2D currentBallRigidbody;
    private SpringJoint2D currentBallSpringJoint;

    private Camera mainCamera;
    private bool isBallDragged;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        SpawnBall();
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

    private void SpawnBall() {
        GameObject ballInstance = Instantiate(ballPrefab, pivot.position, Quaternion.identity);
        currentBallRigidbody = ballInstance.GetComponent<Rigidbody2D>();
        currentBallSpringJoint = ballInstance.GetComponent<SpringJoint2D>();

        // attach the ball to pivot
        currentBallSpringJoint.connectedBody = pivot;
    }

    private void LaunchBall() {
        currentBallRigidbody.isKinematic = false;
        currentBallRigidbody = null;
        Invoke(nameof(DetachBall), detachDelay);
    }

    private void DetachBall() {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;

        // respawn ball
        Invoke(nameof(SpawnBall), respawnDelay);
    }
}
