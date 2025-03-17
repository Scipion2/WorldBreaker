using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] private int X_Origin,Y_Origin;
    [SerializeField] private int X_Max,Y_Max;
    [SerializeField] private float BrickWidth,BrickHeigth;
    [Range(0f, 1f)][SerializeField] private float SpawnRate;
    [SerializeField] private Transform Parent;
    [SerializeField] public enum Difficulty{Easy,Medium,Hard,Extreme}
    private Difficulty CurrentDifficulty=Difficulty.Easy;
    [SerializeField] private int    EASY_X_ORIGIN=0,EASY_X_MAX=0,EASY_Y_ORIGIN=0,EASY_Y_MAX=0,
                                    MEDIUM_X_ORIGIN=0,MEDIUM_X_MAX=0,MEDIUM_Y_ORIGIN=0,MEDIUM_Y_MAX=0,
                                    HARD_X_ORIGIN=0,HARD_X_MAX=0,HARD_Y_ORIGIN=0,HARD_Y_MAX=0,
                                    EXTREME_X_ORIGIN=0,EXTREME_X_MAX=0,EXTREME_Y_ORIGIN=0,EXTREME_Y_MAX=0;


    public Difficulty GetDifficulty()
    {

        return CurrentDifficulty;

    }

    public void SetDifficulty(Difficulty DifficultyToApply)
    {

        switch (DifficultyToApply)
        {

            case Difficulty.Easy :

                    X_Origin=EASY_X_ORIGIN;
                    X_Max=EASY_X_MAX;
                    Y_Origin=EASY_Y_ORIGIN;
                    Y_Max=EASY_Y_MAX;

                break;

            case Difficulty.Medium :

                    X_Origin=MEDIUM_X_ORIGIN;
                    X_Max=MEDIUM_X_MAX;
                    Y_Origin=MEDIUM_Y_ORIGIN;
                    Y_Max=MEDIUM_Y_MAX;

                break;

            case Difficulty.Hard :

                    X_Origin=HARD_X_ORIGIN;
                    X_Max=HARD_X_MAX;
                    Y_Origin=HARD_Y_ORIGIN;
                    Y_Max=HARD_Y_MAX;
                    SpawnRate=0.5f;

                break;

            case Difficulty.Extreme :

                    Debug.Log("Here");
                    X_Origin=EXTREME_X_ORIGIN;
                    X_Max=EXTREME_X_MAX;
                    Y_Origin=EXTREME_Y_ORIGIN;
                    Y_Max=EXTREME_Y_MAX;
                    SpawnRate=0.7f;

            break;

            default :
                break;

        }

    }

    public void SpawnMap()
    {

        switch (Random.Range(0,100)%4)
        {

            case 0 :
                SpawnRandomly();
                break;

            case 1 :
                SpawnMirror();
                break;

            case 2 :
                SpawnDobbleMirror();
                break;

            default :
                SpawnByQuantity(Random.Range(4,15));
                break;

        }

        /*if(BrickManager.instance.isEmpty())
            SpawnMap();*/

    }

    [ContextMenu("Spawn Quantity")]
    public void SpawnByQuantity(int NumberOfBricksToSpawn)
    {

        BrickManager.instance.ClearLevel();
        for(int i=0;i<NumberOfBricksToSpawn;)
        {

            for(int x=X_Origin;x<X_Max;++x)
            {

                for(int y=Y_Origin;y<Y_Max;++y)
                {

                    int Seed=Random.Range(0,101);

                    if(SpawnRate>(float)Seed/100)
                    {

                        Instantiate(BrickManager.instance.GetRandomBrick(),new Vector2(x*BrickWidth,y*BrickHeigth),Quaternion.identity,Parent);
                        ++i;

                    }

                }

            }

        }

        

    }

    [ContextMenu("Spawn Mirror")]
    public void SpawnMirror()
    {

        BrickManager.instance.ClearLevel();

        for(int x=-Mathf.Abs(X_Origin-X_Max)/2;x<0;++x)
        {


            for(int y=Y_Origin;y<Y_Max;++y)
            {

                int Seed=Random.Range(0,101);

                if(SpawnRate>(float)Seed/100)
                {

                    GameObject brickToSummon=BrickManager.instance.GetRandomBrick();
                    Instantiate(brickToSummon,new Vector2(x*BrickWidth,y*BrickHeigth),Quaternion.identity,Parent);
                    Instantiate(brickToSummon,new Vector2(-x*BrickWidth,y*BrickHeigth),Quaternion.identity,Parent);

                }

            }

        }

    }
    
    [ContextMenu("Spawn Dobble Mirror")]
    public void SpawnDobbleMirror()
    {

         BrickManager.instance.ClearLevel();

        for(int x=-Mathf.Abs(X_Origin-X_Max)/2;x<0;++x)
        {


            for(int y=-Mathf.Abs(Y_Origin-Y_Max)/2;y<0;++y)
            {

                int Seed=Random.Range(0,101);

                if(SpawnRate>(float)Seed/100)
                {

                    GameObject brickToSummon=BrickManager.instance.GetRandomBrick();
                    Instantiate(brickToSummon,new Vector2(x*BrickWidth,y*BrickHeigth),Quaternion.identity,Parent);
                    Instantiate(brickToSummon,new Vector2(-x*BrickWidth,y*BrickHeigth),Quaternion.identity,Parent);
                    Instantiate(brickToSummon,new Vector2(x*BrickWidth,-y*BrickHeigth),Quaternion.identity,Parent);
                    Instantiate(brickToSummon,new Vector2(-x*BrickWidth,-y*BrickHeigth),Quaternion.identity,Parent);

                }

            }

        }

    }

    [ContextMenu("Spawn Random")]
    public void SpawnRandomly()
    {

        BrickManager.instance.ClearLevel();
        for(int x=X_Origin;x<X_Max;++x)
        {


            for(int y=Y_Origin;y<Y_Max;++y)
            {

                int Seed=Random.Range(0,101);

                if(SpawnRate>(float)Seed/100)
                {

                    Instantiate(BrickManager.instance.GetRandomBrick(),new Vector2(x*BrickWidth,y*BrickHeigth),Quaternion.identity,Parent);

                }

            }

        }

    }

}
