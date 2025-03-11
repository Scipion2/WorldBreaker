using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreRecord : MonoBehaviour
{
   
        [SerializeField] private TextMeshProUGUI Score,Stage;
        private string PlayerName="";
        private int score,stage;

    //SETTERS

        public void SetScore(int ScoreToDisplay){Score.text=ScoreToDisplay.ToString(); score=ScoreToDisplay;}//Setter For Score
        public void SetStage(int StageToDisplay){Stage.text=StageToDisplay.ToString();stage=StageToDisplay;}//Setter For Stage
        public void UpdateName(TMP_InputField src){PlayerName=src.text;}//Setter For PlayerName


        public void OnEnable()
        {

            SetScore(GameManager.instance.GetScore());
            SetStage(LevelManager.instance.GetCurrentLevel());

        }

        public void Erase()
        {

            PlayerName="";
            SetScore(0);
            SetStage(0);

        }


        public void SaveScore()
        {

            DataManager.instance.AddScoreLine(PlayerName,score,stage);

        }

}
