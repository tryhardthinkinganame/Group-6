using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class MenuPlayer : MonoBehaviour
{

    private Rigidbody rb;

    [Tooltip("How fast the ball moves left/right")]
    public float dodgeSpeed = 5;

    [Tooltip("How fast the ball moves forwards automatically")]
    [Range(0, 10)]
    public float rollSpeed = 5;

    public enum MobileHorizMovement
    {
        Accelerometer,
        ScreenTouch
    }
    public MobileHorizMovement horizMovement = MobileHorizMovement.Accelerometer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        var horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;

#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR

        // Check if we're moving to the side
        horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;

        // If the mouse is held down (or the screen is tapped on Mobile)
        if (Input.GetMouseButton(0))
        {
            horizontalSpeed =
            CalculateMovement(Input.mousePosition);
        }

        // Check if we are running on a mobile device
#elif UNITY_IOS || UNITY_ANDROID

        if(horizMovement == MobileHorizMovement.Accelerometer)
         {
         // Move player based on direction of the accelerometer
         horizontalSpeed = Input.acceleration.x * dodgeSpeed;
         }


         // Check if Input has registered more than zero touches

         if (Input.touchCount > 0)
         {

             if (horizMovement == MobileHorizMovement.ScreenTouch)
             {
             //Store the first touch detected.
             Touch touch = Input.touches[0];
             horizontalSpeed = CalculateMovement(touch.position);
             }

         }
#endif

        rb.AddForce(horizontalSpeed, 0, rollSpeed);
    }

    private float CalculateMovement(Vector3 pixelPos)
    {
        var worldPos = Camera.main.ScreenToViewportPoint(pixelPos);
        float xMove = 0;

        if (worldPos.x < 0.5f)
        {
            xMove = -1;
        }
        else
        {
            xMove = 1;
        }
        return xMove * dodgeSpeed;
    }

}
