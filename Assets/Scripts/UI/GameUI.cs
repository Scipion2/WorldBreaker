using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class GameUI : MonoBehaviour
{

    [Header("UI Components")]
    [Space(10)]

        [SerializeField] private TextMeshProUGUI ScoreDisplay;
        [SerializeField] private TextMeshProUGUI LevelDisplay;
        [SerializeField] private TextMeshProUGUI LevelTitleDisplay;
        [SerializeField] private Image BackgroundDisplay;
        [SerializeField] private Sprite BaseBackground;

    //GETTERS

    //SETTERS

        public void SetBackground(Sprite SRC){BackgroundDisplay.sprite=SRC;}//Setter For BackgroundDisplay
        public void SetScore(string ScoreToDisplay){ScoreDisplay.text=ScoreToDisplay;}//Setter For ScoreDisplay
        public void SetLevelTitle(string LevelTitleToDisplay){LevelTitleDisplay.text=LevelTitleToDisplay;}//Setter For LevelTitle
        public void SetLevel(string LevelToDisplay){LevelDisplay.text=LevelToDisplay;}//Setter For LevelDisplay

    //ESSENTIALS

        public void OnEnable()
        {

            SetBackground(BaseBackground);

        }


}
