using System.Collections;
using UnityEngine;

public class Reward : MonoBehaviour
{

    [Header("Components")]
    [Space(10)]

        [SerializeField] private RewardTypes reward; // Creates a variable as a dropdown of types, as defined in Paddle.cs
        [SerializeField] private GameObject RewardIcon;

    [Header("Collide Target")]
    [Space(10)]

        private Paddle paddle;

    //ESSENTIALS

        //Called by a collider only if set as a "trigger".
        void OnTriggerEnter2D(Collider2D other)
        {
            // Check if collided with the paddle
            paddle = other.gameObject.GetComponent<Paddle>();            
            if (paddle != null)
            {
                // Pass the reward type to the paddle
                paddle.Reward(reward);
                // We can destroy this gameObject right away
                Destroy(this.gameObject);
            }
            // If missed, the falling reward will collide the deadzone. Destroy it instead of falling for ever...
            if (other.GetComponent<DeadZone>())
            {
                Destroy(this.gameObject);
            }
        }

}