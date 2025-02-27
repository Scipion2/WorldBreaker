using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Ican.WallBreaker
{
    [RequireComponent(typeof(AudioSource))]
    public class GameManager : MonoBehaviour
    {
        
        [HideInInspector] public bool canPlay;
        [HideInInspector] public Paddle paddle;
        public Ball ball;

        public float startDelay;
        public AudioClip breakSound;
        public AudioClip winSound;
        public AudioClip looseSound;
        private AudioSource audiosource;
        private int score;
        public Text scoreText;
        public GameObject readyPanel;
        public GameObject winPanel;
        public GameObject loosePanel;
        private string initialText;

        public static GameManager instance;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this.gameObject);
        }//Allow To Call This From Any Class

        // Start is called before the first frame update, but we don't know which script will be executed first!
        // So, as the instance can be found by other scripts, this script has been set before default time in Edit>Project Setting>Scripts Execution Order.
        void Start()
        {
            
            audiosource = GetComponent<AudioSource>();
            // Cache the content of the existing text
            initialText = scoreText.text; 
            ResetGame();
        }


        void ResetGame() 
        {
            // Show instruction panel
            readyPanel.SetActive(true);
            // Start game after delay
            Invoke("StartGame", startDelay);
        }

        void StartGame() 
        {
            canPlay = true;
            readyPanel.SetActive(false);
        }


        // Public method to update bricks count, score, called by Brick.cs
        public void RegisterBreak(int value, Brick brick) 
        {

            IncreaseScore(value);

            if (BrickManager.instance.Remove(brick)) 
            {
                WinLevel();
            }
            if (breakSound != null)
            {
                //Charge the audiosource with the right clip and play it
                audiosource.clip = breakSound;
                audiosource.Play();
            }
        }

        // Also called by Reward.cs
        public void IncreaseScore(int scoreAdded)
        {
            score += scoreAdded;
            scoreText.text = initialText + score.ToString();
        }

        void WinLevel() 
        {
            canPlay = false;
            if (winSound)
            {
                // PlayOneShot instantiates our audiosource, maybe already playing a break sound
                audiosource.PlayOneShot(winSound);
            }
            winPanel.gameObject.SetActive(true);

        }

        // Called by Ball.cs
        public void LooseLevel()
        {
            canPlay = false;
            if (winSound)
            {
                audiosource.PlayOneShot(looseSound);
            }
            loosePanel.SetActive(true);
        }
        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}