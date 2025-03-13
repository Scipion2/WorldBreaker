using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class ScoreBoard : MonoBehaviour
{

    [SerializeField] private Transform BodyPanel;
    [SerializeField] private ScoreLine ScoreLinePrefab;
    private List<ScoreLine> Scores=new List<ScoreLine>();

    public void Start()
    {

        ClearScore();
        UpdateScore();

    }

    public void OnEnable()
    {

        ClearScore();
        UpdateScore();

    }

    public void ClearScore()
    {

        for(int i=0;i<Scores.Count;++i)
            DestroyImmediate(Scores[i].gameObject);

        Scores.Clear();

    }

    public void UpdateScore()
    {

        DataManager.instance.UpdateData();
        int LineCount=DataManager.instance.GetScoreCount();
        for(int i=0;i<Scores.Count;++i)
            DestroyImmediate(Scores[i].gameObject);

        Scores.Clear();

        for(int i=0;i<LineCount;++i)
        {

            ScoreLine Temp=Instantiate(ScoreLinePrefab,BodyPanel);
            Temp.SetScoreLine(DataManager.instance.GetPlayer(i),DataManager.instance.GetScore(i).ToString(),DataManager.instance.GetStage(i).ToString());
            Scores.Add(Temp);

        }

    }

}
