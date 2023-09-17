using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody rb;

    float horizontalInput;
    public float horizontalMultiplier = 2;

    private void FixedUpdate()
    {
        Vector3 forwardmove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove =  transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardmove + horizontalMove);
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Update the distance traveled (You can replace this with your own distance calculation)
        distanceTraveled += Time.deltaTime;

        // Update the player's score based on the distance
        UpdateScore();
    }

    private float distanceTraveled = 0.0f; // Total distance traveled by the player
    private int score = 0; // Player's score

    private void UpdateScore()
    {
        // Calculate the score based on the distance traveled (e.g., 10 points per unit of distance)
        int newScore = Mathf.FloorToInt(distanceTraveled * 10);

        // Check if the score has increased
        if (newScore > score)
        {
            score = newScore;
            Debug.Log("Score: " + score);
        }
    }
}
