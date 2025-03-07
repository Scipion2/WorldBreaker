using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreRecord : MonoBehaviour
{
   
        [SerializeField] private TextMeshProUGUI Score,Stage;
        private string PlayerName="";

    //SETTERS

        public void SetScore(int ScoreToDisplay){Score.text=ScoreToDisplay.ToString();}//Setter For Score
        public void SetStage(int StageToDisplay){Stage.text=StageToDisplay.ToString();}//Setter For Stage
        public void UpdateName(TMP_InputField src){PlayerName=src.text;}//Setter For PlayerName


        public void Erase()
        {

            PlayerName=""; 
            UIManager.instance.DisplayScoreRecord(false);

        }


        public void SaveScore()
        {

            //

        }

}
