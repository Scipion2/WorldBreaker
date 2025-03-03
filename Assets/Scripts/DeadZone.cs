using System.Collections;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private Ball ball;
    [SerializeField] private AudioSource missAudio;

    //Called by a collider only if set as a "trigger"
    void OnTriggerEnter2D(Collider2D other)
    {
        ball = other.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            // Equivalent to Rigidbody.isKinematic=true, but Rigidbody2D works a bit differently!                
            ball.GetComponent<Rigidbody2D>().simulated = false;
            // Resets the ball only if more than 1 life remains
            if (GameManager.instance.GetLives() > 1)
            {
                // get the paddle from the GameManager
                Transform paddle = GameManager.instance.GetPadleTransform();
                // Resets the ball on the paddle 
                ball.transform.position = new Vector2 (paddle.position.x, paddle.transform.position.y+0.25f);
                // parent it until next kick off, as the ball can move with the paddle
                ball.transform.parent = transform;
            }
            // Execute a public method on the ball
            ball.ResetBall();
            // Play loose sound if available
            if (missAudio.clip != null)
            {
                missAudio.Play();
            }
        }
    }
}