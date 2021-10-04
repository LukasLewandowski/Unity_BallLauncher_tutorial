using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // check if user is touching the screen
        if(!Touchscreen.current.primaryTouch.press.isPressed) {
            return;
        }
        // store screen position (pixels) of the touch in variable
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        // convert screen possition to unity world position
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
        Debug.Log(worldPosition);
    }
}
