using UnityEngine;
using TMPro;

public class ScoreLine : MonoBehaviour
{
    
    [Header("Text Component")]
    [Space(10)]

        [SerializeField] private TextMeshProUGUI Player;
        [SerializeField] private TextMeshProUGUI Score;
        [SerializeField] private TextMeshProUGUI Stage;

    //SETTERS

        public void SetScoreLine(string PlayerSrc,string ScoreSrc,string StageSrc)
        {

            Debug.Log(Player);
            Player.text=PlayerSrc;
            Score.text=ScoreSrc;
            Stage.text=StageSrc;

        }

}
