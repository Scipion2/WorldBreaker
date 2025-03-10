using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [Header("Components")]
    [Space(10)]

        [SerializeField] private Sprite[] sprites;
        [SerializeField] private SpriteRenderer ballRenderer;
        [SerializeField] private AudioSource hitAudio;
        [SerializeField] private Rigidbody2D body; 

    [Header("Physics Data")]
    [Space(10)]

        public float launchSpeed = 1.0f;
        public float maxSpeed = 100f;

    [Header("Audio Data")]
    [Space(10)]

        public AudioClip[] hitSounds;
        public AudioClip kickSound;

    [Header("Game Data")]
    [Space(10)]

        private bool needKickOff = true;
        private bool isActiveBall=false;


    //SETTERS
        public void SetActiveBall(){isActiveBall=true;}//Setter For isActiveBall

    //ESSENTIALS

        public void Start()
        {

            body.simulated=false;

        }
    
        void FixedUpdate()
        {
            // Check if game is on
            if (isActiveBall == false)
            {
                // Equivalent to Rigidbody.isKinematic=true, but rigidbody2D works a bit differently!
                body.simulated = false;
            }
            // needKickOff is on by default and after reset 
            else if (needKickOff == true && Input.GetButton("Jump"))
            {
                KickBall();
            }

            // limit the velocity
            if (body.linearVelocity.magnitude > maxSpeed)
            {
                body.linearVelocity = body.linearVelocity.normalized * maxSpeed;
            }

        }

        // Called if this collided with another collider
        void OnCollisionEnter2D(Collision2D other)
        {
            // Check if the Audiosource is not null and avoid a call on start up as the ball is on the paddle 
            if (hitAudio && GameManager.instance.GetIsAbleToPlay())
            {
                // Plays a random clip to make it less repetitive
                int randomNumber = Random.Range(0, hitSounds.Length);
                hitAudio.clip = hitSounds[randomNumber];
                hitAudio.Play();
            }
        }

    //GAME'S UTIL

        // Called on start up and after Reset
        public void KickBall()
        {
            if (kickSound)
            {
                hitAudio.clip = kickSound;
                hitAudio.Play();
            }
            // remove the ball from the paddle parent
            body.transform.parent = null;
            // restore physics
            body.simulated = true;
            // give a new velocity to the ball
            body.linearVelocity = Vector2.up*launchSpeed;
            // Make sure we can kick only once
            needKickOff = false;
        }

        // Called by DeadZone.cs
        public void ResetBall()
        {

            if(GameManager.instance.LoseLive())
            {

                needKickOff = true;
                body.simulated=false;
                ballRenderer.color=new Color((float)Random.Range(0,100)/100,(float)Random.Range(0,100)/100,(float)Random.Range(0,100)/100,1f);

            }
                
        }

}