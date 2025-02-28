using System.Collections;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Brick : MonoBehaviour
{
    private Ball ball;
    [Range(1, 3)] public int lives = 1;
    public int scoreValue = 10;
    public Material litBrick;
    public Material unlitBrick;
    [Tooltip("Optional")] public Reward reward;
    [SerializeField] private bool isBreakable=true;        
    
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

    void DestroyBrick() 
    {
        Destroy(this.gameObject);
    }

}