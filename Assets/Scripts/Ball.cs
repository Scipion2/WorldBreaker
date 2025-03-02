using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer ballRenderer;
    public float launchSpeed = 1.0f;
    public float maxSpeed = 100f;
    [SerializeField] private AudioSource hitAudio;
    public AudioClip[] hitSounds;
    public AudioClip kickSound;
    [SerializeField] private Rigidbody2D body;    
    private bool needKickOff = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if game is on
        if (GameManager.instance.GetIsAbleToPlay() == false)
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
        //Teacher's Cheat
        if (Input.GetKeyDown(KeyCode.W))
        {
            GameObject GO = GameObject.Instantiate(this.gameObject, this.transform.position, Quaternion.identity);
            GO.GetComponent<Rigidbody2D>().linearVelocity = body.linearVelocity*1.1f;
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
        // Decrease lives count
        //lives--;
        if (//lives == 0) 
        {
            // Tells the GameManager the game is over
            GameManager.instance.LooseLevel();
        }
        else 
        {
            // Reset the ball, changing the sprite to show color change
            ballRenderer.sprite = sprites[sprites.Length-lives];
            needKickOff = true;
        }
    }

    public void IncreaseLife()
    {
        if (lives <= 2)
        {
            lives++;
            // switch the colored sprite, 3 being the max applicable value
            ballRenderer.sprite = sprites[sprites.Length-lives];
        }
    }
}