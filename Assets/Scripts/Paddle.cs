using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardTypes {Points = 0,Life = 1,Width = 2,Weapon = 3}

public class Paddle : MonoBehaviour
{

    [Header("Components")]
    [Space(10)]

        [SerializeField] private Rigidbody2D paddle;
        Vector2 ScreenBounds;

    [Header("Game Datas")]
    [Space(10)]

        private float horizontal;
        [SerializeField] private float speed = 1.0f,AnglePower=10f; 
        public bool isMovingRight=true;       

    //GETTERS

    //ESSENTIALS

        public void Awake()
        {

            GameManager.instance.SetPadle(this.transform);

        }

        void FixedUpdate() // Physics callback
        {

            ScreenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,Camera.main.transform.position.z));
            // Check if game is on
            if (GameManager.instance.GetIsAbleToPlay())
            {

                horizontal=Input.GetAxis("Horizontal");
                this.transform.Translate(new Vector3(horizontal*speed*Time.fixedDeltaTime,0,0));

            }

            
            
        }

        void LateUpdate()
        {

            Vector3 Temp=transform.position;
            Temp.x=Mathf.Clamp(Temp.x, -ScreenBounds.x, ScreenBounds.x);
            transform.position=Temp;

        }

        void OnCollisionEnter2D(Collision2D other) // Called if this collided with another collider
        {
            
            if(other!=null && other.gameObject.tag == "Ball")
            {

                float angle=Vector2.SignedAngle(this.transform.position,other.gameObject.transform.position);
                other.rigidbody.AddForce(Vector2.right*angle*AnglePower);

            }

        }

    //REWARD GESTURE

        // Public method called by Reward.cs, requiring a RewardTypes
        public void Reward(RewardTypes type)
        {
            switch(type)
            {           
                case RewardTypes.Points:
                    GameManager.instance.IncreaseScore(100); // Do whatever you want for that case
                    break; // break keyword tells the enum to stop the loop search, if the item as been found 

                case RewardTypes.Life:
                    GameManager.instance.IncreaseLives();
                    
                    break;

                case RewardTypes.Width:
                    StartCoroutine("WidthTime");
                    break;
                
                case RewardTypes.Weapon: 
                    Debug.Log("Weapon reward: to be implemented...");
                    break; 

                default :
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