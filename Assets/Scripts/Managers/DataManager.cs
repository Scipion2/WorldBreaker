using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    

    [Header("")]
    [Space(10)]

        [SerializeField] private List<ScoreData> Scores = new List<ScoreData>();
        [SerializeField] private int ScoreNumber=0;
        private const string Quantity="ScoreCount", Player="PlayerName", Stage="StagesNumber", Score="Score";

    //GETTERS

        public int GetScoreCount(){return ScoreNumber;}//Getter For ScoreNumber
        public int GetScore(int ScoreIndex){return Scores[ScoreIndex].GetScore();}//Getter For Scores
        public int GetStage(int StageIndex){return Scores[StageIndex].GetStage();}//Getter For Stages
        public string GetPlayer(int PlayerIndex){return Scores[PlayerIndex].GetPlayer();}//Getter For Players

    //SETTERS

        public void AddScoreLine(string PlayerName,int Score,int Stage)
        {

            ScoreNumber++;
            ScoreData NewScore=new ScoreData(PlayerName,Score,Stage);
            Scores.Add(NewScore);
            Save();
            Debug.Log("Saved !");

        }//Set A New Score On The Data Saved

    //ESSENTIALS

        public void Start()
        {

            UpdateData();

        }

        public static DataManager instance;
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

        public void UpdateData()
        {

            if(PlayerPrefs.HasKey(Quantity))
            {

                ScoreNumber=PlayerPrefs.GetInt(Quantity);

            }else
            {

                PlayerPrefs.SetInt(Quantity, ScoreNumber);

            }

            for(int i=0;i<ScoreNumber;++i)
            {


                Scores.Add(new ScoreData(PlayerPrefs.GetString(Player+i.ToString()),PlayerPrefs.GetInt(Score+i.ToString()),PlayerPrefs.GetInt(Stage+i.ToString())));

            }

        }

    //DATA MANAGEMENT

        public void Save()
        {

            for(int i=PlayerPrefs.GetInt(Quantity);i<ScoreNumber;++i)
            {

                PlayerPrefs.SetString(Player+i,Scores[i].GetPlayer());
                PlayerPrefs.SetInt(Score+i,Scores[i].GetScore());
                PlayerPrefs.SetInt(Stage+i,Scores[i].GetStage());

            }

            PlayerPrefs.SetInt(Quantity,ScoreNumber);

        }

        public void SortByScores()
        {

            

        }

        public void SortByStages()
        {

            //

        }



    public class ScoreData
    {

        private string Player;
        private int Score;
        private int Stage;

        public ScoreData(string PlayerSrc,int ScoreSrc,int StageSrc)
        {

            Player=PlayerSrc;
            Score=ScoreSrc;
            Stage=StageSrc;

        }

        //GETTERS

            public string GetPlayer(){return Player;}
            public int GetScore(){return Score;}
            public int GetStage(){return Stage;}

    }

    [ContextMenu("Clear Save")]
    public void ClearSave()
    {

        PlayerPrefs.DeleteAll();

    }

}
