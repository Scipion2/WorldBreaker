using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class ScoreBoard : MonoBehaviour
{

    [SerializeField] private Transform BodyPanel;
    [SerializeField] private GameData ScoreLists;
    [SerializeField] private ScoreLine ScoreLinePrefab;
    private List<ScoreLine> Scores=new List<ScoreLine>();

    public void Start()
    {

        Debug.Log(ScoreLists.Players.Count);

        for(int LineCount=0;LineCount<ScoreLists.Players.Count;++LineCount)
        {

            ScoreLine Temp=Instantiate(ScoreLinePrefab,BodyPanel);
            Temp.SetScoreLine(ScoreLists.Players[LineCount],ScoreLists.PlayerScores[LineCount].ToString(),ScoreLists.PlayerStages[LineCount].ToString());
            Scores.Add(Temp);

        }

    }

}
