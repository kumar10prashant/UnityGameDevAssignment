using UnityEngine;

public class TilesMatching : MonoBehaviour
{
    public int minHeight;
    public int maxHeight;

    public int  minWidth;
    public int maxWidth;

    [SerializeField]LevelGenerator levelGenerator;

    [SerializeField] bool VerticalCheck, HorizontalCheck;
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
         
        }
    }

    public void MatchTiles()
    {
        GameObject go;
        if (HorizontalCheck)
        {
            for (int i = minHeight; i <= maxHeight; i++)
            {
                if(transform.position.x > 0)
                {
                    go = levelGenerator.TilesPoint[(int)(transform.position.x - 1), i];
                }
                else
                {
                    go = levelGenerator.TilesPoint[(int)(transform.position.x + 1), i];
                   

                }

                if (go != null && go.transform.parent.tag == transform.tag)
                {
                    TileCheck(go);
                    go = null;
                    break;
                }
            }
        }
        else if(VerticalCheck)
        {
            for (int i = minWidth; i <= maxWidth; i++)
            {
                
            
                if(transform.position.z > 0)
                {
                     go = levelGenerator.TilesPoint[i, (int)(transform.position.z - 1)];
                }
                else
                {
                    go = levelGenerator.TilesPoint[i, (int)(transform.position.z + 1)];

                }
                
                if (go != null && go.transform.parent.tag == transform.tag)
                {
                    TileCheck(go);
                    go = null;
                    break;
                }
            }
        }
    }

    public void TileCheck(GameObject tile)
    {
        Debug.Log(tile);
        int Counter = 0;
        int ChildCount = 0;
        Transform parent = tile.transform.parent;
        foreach(var child in parent.GetComponentsInChildren<TileHandler>())
        {
            if (HorizontalCheck)
            {
                for(int i = minHeight;i <= maxHeight; i++)
                {
                    if(child.previousColumn == i) { 
                    
                        Counter++;
                        continue;
                    }
                }
                
            }
            else if (VerticalCheck)
            {
                for(int i = minWidth; i <= maxWidth; i++)
                {
                    if (child.previousRow == i)
                    {
                        Counter++;
                        continue;
                    }
                }
              
            }
            ChildCount++;
        }
        Debug.Log(Counter);
        if(ChildCount == Counter)
        {
            foreach(var child in parent.GetComponentsInChildren<TileHandler>())
            {
                levelGenerator.TilesPoint[child.previousRow, child.previousColumn] = null;
            }
            if (levelGenerator.totalFillPoints > levelGenerator.currentFillPoints)
            {
                parent.transform.position = levelGenerator.finalPoints[levelGenerator.currentFillPoints].position;
                levelGenerator.currentFillPoints++;
            }
        }
    }

}
