using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This is an enum type, a collection of different states or conditions, in a human friendly format.
// It is declared aside the Paddle class is this usecase, as to be available for other classes as well.
public enum RewardTypes
{
    Points = 0,
    Life = 1,
    Width = 2,       
    Weapon = 3
}

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    private float horizontal;
    public float speed = 1.0f,AnglePower=10f;
    [SerializeField] private Rigidbody2D paddle;

    void FixedUpdate() // Physics callback
    {
        // Check if game is off
        if (GameManager.instance.GetIsAbleToPlay() == false)
        {
            // we quit that block code and don't execute next lines
            return;
        }
        horizontal=Input.GetAxis("Horizontal");
        paddle.AddRelativeForce(Vector2.right*horizontal*speed*Time.fixedDeltaTime);

    }

    void OnCollisionEnter2D(Collision2D other) // Called if this collided with another collider
    {
        
        if(other!=null)
        {

            float angle=Vector2.SignedAngle(this.transform.position,other.gameObject.transform.position);
            other.rigidbody.AddForce(Vector2.right*angle*AnglePower);

        }else Debug.Log("WTF");

    }

    // Public method called by Reward.cs, requiring a RewardTypes
    public void Reward(RewardTypes type)
    {
        switch(type)
        {           
            case RewardTypes.Points:
                GameManager.instance.IncreaseScore(100); // Do whatever you want for that case
                break; // break keyword tells the enum to stop the loop search, if the item as been found 

            case RewardTypes.Life:
                
                break;

            case RewardTypes.Width:
                StartCoroutine("WidthTime");
                break;
            
            case RewardTypes.Weapon: 
                Debug.Log("Weapon reward: to be implemented...");
                break;                             
        }
    }

    IEnumerator WidthTime() 
    {
        // Creates a time counter at the beginning of the coroutine, here the duration is hard coded to 15 seconds.
        float counter = 15.0f;
        // Caching of some components used only here after
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        CapsuleCollider2D collider = this.GetComponent<CapsuleCollider2D>();
        // Remember the original size
        float colliderX = collider.size.x;
        
        // while loop, requires an argument that can become false for exiting the loop
        while (counter >= 0)
        {
            // So decrease the time counter toward 0.
            counter -= Time.deltaTime;
            // The current paddle sprite width is 1.28, same for the capsule collider, so let's take this as a duration... 
            if (counter >= colliderX)
            {
                // Increase size (instead of changing scale that might affect the ball too!)
                if (collider.size.x < (colliderX * 2.0f))
                {
                    collider.size = new Vector2(collider.size.x+Time.deltaTime, collider.size.y);
                    // This works only if the SpriteRenderer is set on "Sliced" or "Tiled" mode! 
                    renderer.size += new Vector2(Time.deltaTime, 0.0f);                    }
            }
            else 
            {
                // Decrease size during the last second of the coroutine
                if (collider.size.x > colliderX)
                {
                    collider.size = new Vector2(collider.size.x-Time.deltaTime, collider.size.y);
                    renderer.size -= new Vector2(Time.deltaTime, 0.0f);
                }
            }
            // return argument of the coroutine, to tell when to pause the loop. "Null" means at each Update
            yield return null;
        }
        // Last, before exiting the coroutine, get rid of the decimals due to Time.deltaTime
        collider.size = renderer.size = new Vector2 (colliderX, collider.size.y);
        
    }
}