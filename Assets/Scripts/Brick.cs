using System.Collections;
using UnityEngine;

public class Brick : MonoBehaviour
{

    [Header("Components")]
    [Space(10)]

        private Ball ball;
        [SerializeField] private Reward reward;

    [Header("Graphics Components")]
    [Space(10)]

        [SerializeField] private Material litBrick;
        [SerializeField] private Material unlitBrick;

    [Header("Game Data")]
    [Space(10)]

        [Range(1, 3)] [SerializeField] private int lives = 1;
        [SerializeField] private int scoreValue = 10;
        [SerializeField] private bool isBreakable=true; 

    //ESSENTIALS       
    
        void Start()
        {
            // Add this brick instance to the GameManager list
            BrickManager.instance.RegisterBrick(this,isBreakable);
            
            if(isBreakable)
                reward=BrickManager.instance.GetReward();

        }

        void OnCollisionEnter2D(Collision2D other)
        {
            // First check if collision is due to the ball
            ball = other.gameObject.GetComponent<Ball>();
            if (ball && isBreakable)
            {
                // Decrease lives count
                lives--;
                if (lives == 0)
                {
                    // Tells the GameManager this brick has been broken
                    GameManager.instance.RegisterBreak(scoreValue, this);
                    // Release a reward, if applicable
                    if (reward != null)
                    {
                        GameObject.Instantiate(reward, this.transform.position, Quaternion.identity);
                    }
                    // finally, destroy it
                    DestroyBrick();
                }
                // Change from bright to normal material. Swapping materials allows not to instantiate a new material by modyfying it
                else if (unlitBrick)
                {
                    this.GetComponent<SpriteRenderer>().material = unlitBrick;
                }
            }
        }

    //GAME'S UTIL

        void DestroyBrick() 
        {
            Destroy(this.gameObject);
        }

}