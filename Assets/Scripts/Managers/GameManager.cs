using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    
    [Header("Game Datas")]
    [Space(10)]
        private int Score=0;
        private int Lives=3;
        private bool isAbleToPlay=false;
        [SerializeField] private float startDelay;
        [SerializeField] private float BallSpacing;
        public enum GameMode{Classic,Arcade}
        private GameMode CurrentGameMode;

    [Header("Game Components")]
    [Space(10)]
        [SerializeField] private List<Ball> PlayerBalls=new List<Ball>();
        [SerializeField] private Transform PadleTransform;
        [SerializeField] private Ball BallPrefab;
        [SerializeField] private Ball CurrentBall;
        [SerializeField] private Transform BallTankOrigin;


    [Header("Audio Components")]
    [Space(10)]    
        private AudioClip breakSound;
        private AudioClip winSound;
        private AudioClip looseSound;
        [SerializeField] private AudioSource audiosource;

    //GETTERS
        public bool GetIsAbleToPlay(){return isAbleToPlay;}//Getter For isAbleToPlay
        public Transform GetPadleTransform(){return PadleTransform;}//Getter For PadleTransform
        public int GetLives(){return Lives;}//Getter For Lives
        public GameMode GetCurrentGameMode(){return CurrentGameMode;}//Getter For CurrentGameMode

    //SETTERS

        public void SetCurrentGameMode(GameMode SRC){CurrentGameMode=SRC;}//Setter For CurrentGameMode

    //ESSENTIALS

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

        public void Update()
        {

            if(Input.GetButtonDown("Cancel"))
            {

                if(UIManager.instance.GetisWindowOpen())
                {

                    UIManager.instance.CloseMenu();

                }else
                {

                    UIManager.instance.OpenMenu();

                }

            }

            if(Input.GetKeyUp(KeyCode.P))
            {

                IncreaseLives();

            }

        }

    //

    public void SaveData()
    {}


    public void ResetGame(int LivesCount) 
    {
        // Show instruction panel
        UIManager.instance.DisplayStartPanel(true);

        Lives=LivesCount;
        // Start game after delay
        Invoke("StartGame", startDelay);
    }

    private void StartGame() 
    {
        isAbleToPlay = true;
        UIManager.instance.DisplayStartPanel(false);

        for(int i=0;i<Lives;++i)
        {

            PlayerBalls.Add(Instantiate(BallPrefab,new Vector2(BallTankOrigin.position.x+BallSpacing*i,BallTankOrigin.position.y),Quaternion.identity,BallTankOrigin));

        }

        CurrentBall=Instantiate(BallPrefab,new Vector2(PadleTransform.position.x,PadleTransform.position.y+0.2f),Quaternion.identity,PadleTransform);
        CurrentBall.SetActiveBall();
        

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
        Score += scoreAdded;
        UIManager.instance.UpdateScoreDisplay(Score);
    }

    void WinLevel() 
    {
        isAbleToPlay = CurrentGameMode==GameMode.Arcade ? true : false;
        CurrentBall.transform.position=new Vector2(PadleTransform.position.x,PadleTransform.position.y+0.2f);
        CurrentBall.transform.parent=PadleTransform;
        CurrentBall.ResetBall();
        if (winSound)
        {
            // PlayOneShot instantiates our audiosource, maybe already playing a break sound
            audiosource.PlayOneShot(winSound);
        }
        UIManager.instance.DisplayWinPanel(true);

    }

    public void IncreaseLives()
    {

        Lives++;
        PlayerBalls.Add(Instantiate(BallPrefab,new Vector2(BallTankOrigin.position.x+BallSpacing*PlayerBalls.Count,BallTankOrigin.position.y),Quaternion.identity,BallTankOrigin));

    }

    public bool LoseLive()
    {

        Lives--;

        if(Lives==0)
        {

            Defeat();
            return false;

        }else
        {

            UpdateLives();
            CurrentBall.transform.parent=PadleTransform;
            return true;

        }

    }

    private void UpdateLives()
    {

        Ball LiveToLose=PlayerBalls[PlayerBalls.Count-1];
        Object.Destroy(LiveToLose.gameObject);
        PlayerBalls.Remove(LiveToLose);

    }

    // Called by Ball.cs
    public void Defeat()
    {
        isAbleToPlay = false;
        if (winSound)
        {
            audiosource.PlayOneShot(looseSound);
        }
        UIManager.instance.DisplayLosePanel(true);
    }

}
