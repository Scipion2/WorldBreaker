using UnityEngine;
using System.Collections.Generic;

public class BrickManager : MonoBehaviour
{
    
    [SerializeField] private List<Brick> LevelBricks = new List<Brick>();
    [SerializeField] private List<Brick> LevelUnbreakableBricks = new List<Brick>();
    [SerializeField] private GameObject[] BricksPrefab;
    [SerializeField] private float[] BricksSpawnWeigth;
    [SerializeField] private Reward[] RewardList;
    [SerializeField] private float[] RewardSpawnWeigth;
    [SerializeField] private float RewardSpawnRate;

    //GETTERS

        public bool isEmpty(){return LevelBricks.Count!=0 ? false : true;}

    public static BrickManager instance;
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

    public void RegisterBrick(Brick brick,bool isBreakable)
    {
        if(isBreakable)
            LevelBricks.Add(brick);
        else
            LevelUnbreakableBricks.Add(brick); 
    }

    public bool Remove(Brick BrickToRemove)
    {

        LevelBricks.Remove(BrickToRemove);
        if(LevelBricks.Count<=0)
            return true;

        return false;

    }

    public void ClearLevel()
    {

        if(LevelBricks.Count!=0)
        {

            LevelBricks.ForEach(Delete);
            LevelBricks.Clear();

        }

        if(LevelUnbreakableBricks.Count!=0)
        {

            LevelUnbreakableBricks.ForEach(Delete);
            LevelUnbreakableBricks.Clear();

        }

    }

    public void Delete(Brick BrickToRemove)
    {

        //LevelBricks.Remove(BrickToRemove);
        if(BrickToRemove!=null)
            Object.Destroy(BrickToRemove.gameObject);

    }

    public GameObject GetRandomBrick()
    {

        float WeigthSum=0;
        for(int i=0;i<BricksSpawnWeigth.Length;WeigthSum+=BricksSpawnWeigth[i++]){}

        for(int i=0;i<BricksPrefab.Length;WeigthSum-=BricksSpawnWeigth[i++])
        {

            if(Random.Range(0,WeigthSum)<BricksSpawnWeigth[i])
            {

                return BricksPrefab[i];

            }

        }

        return BricksPrefab[0];

    }

    public Reward GetReward()
    {

        if((float)Random.Range(0,101)/100>RewardSpawnRate)
            return null;

        float WeigthSum=0;
        for(int i=0;i<RewardSpawnWeigth.Length;WeigthSum+=RewardSpawnWeigth[i++]){}

        for(int i=0;i<RewardList.Length;WeigthSum-=RewardSpawnWeigth[i++])
        {

            if(Random.Range(0,WeigthSum)<RewardSpawnWeigth[i])
            {

                return RewardList[i];

            }

        }

        return null;

    }

}
