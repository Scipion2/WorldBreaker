using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] private int X_Origin,Y_Origin;
    [SerializeField] private int X_Max,Y_Max;
    [SerializeField] private float BrickWidth,BrickHeigth;
    [Range(0f, 1f)][SerializeField] private float SpawnRate;
    [SerializeField] private Transform Parent;


    [ContextMenu("Spawn Quantity")]
    public void SpawnByQuantity(int NumberOfBricksToSpawn)
    {

        BrickManager.instance.ClearLevel();

        for(int i=0;i<NumberOfBricksToSpawn;++i)
        {

            for(int x=X_Origin;x<X_Max;++x)
            {

                for(int y=Y_Origin;y<Y_Max;++y)
                {

                    int Seed=Random.Range(0,101);

                    if(SpawnRate>(float)Seed/100)
                        Instantiate(BrickManager.instance.GetRandomBrick(),new Vector2(x*BrickWidth,y*BrickHeigth),Quaternion.identity,Parent);

                }

            }

        }

        

    }

    [ContextMenu("Spawn Mirror")]
    public void SpawnMirror()
    {

        BrickManager.instance.ClearLevel();

        for(int x=X_Origin;x<X_Max/2-1;++x)
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

        for(int x=X_Origin;x<X_Max/2-1;++x)
        {


            for(int y=Y_Origin;y<Y_Max/2-1;++y)
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
