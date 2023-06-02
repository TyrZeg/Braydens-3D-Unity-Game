using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody rb;
    public float jumpForce;

    public int score;

    private bool isGrounded;

    public UI ui;

    // Update is called once per frame
    void Update()
    {

        //Get the horizontal and vertical inputs
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        // set the velocity based on the users inputs
        rb.velocity = new Vector3(x, rb.velocity.y, z);

        // create a copy of the velocity variable
        // set the Y axis to be 0
        Vector3 vel = rb.velocity;
        vel.y = 0;

        // if the player is moving, rotate to face their moving directions
        if (vel.x != 0 || vel.z != 0){
            transform.forward = rb.velocity;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if(transform.position.y <= -10)
        {
            GameOver();
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal == Vector3.up) {
            isGrounded = true;
        }
    }

    public void GameOver() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore(int amount)
    {
        score += amount;
        ui.SetScoreText(score);
    }


}
