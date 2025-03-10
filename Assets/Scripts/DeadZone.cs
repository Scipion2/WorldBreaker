using System.Collections;
using UnityEngine;

public class DeadZone : MonoBehaviour
{

    [Header("Components")]
    [Space(10)]

        private Ball ball;
        [SerializeField] private AudioSource missAudio;

    //ESSENTIALS

        //Called by a collider only if set as a "trigger"
        void OnTriggerEnter2D(Collider2D other)
        {
            ball = other.gameObject.GetComponent<Ball>();
            if (ball != null)
            {
                
                ball.ResetBall();
                // Play loose sound if available
                if (missAudio.clip != null)
                {
                    missAudio.Play();
                }
            }
        }
}