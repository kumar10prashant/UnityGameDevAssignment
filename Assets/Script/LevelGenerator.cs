using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] Tiles;
    [SerializeField] GameObject Blocker;

    public int row;
    public int col;


    public GameObject[,] TilesPoint;

    private void Start()
    {

        TilesPoint = new GameObject[row, col];
        TileHandler[] tilesHandler = GameObject.FindObjectsOfType<TileHandler>();
        foreach(TileHandler tileHandler in tilesHandler)
        {
            TilesPoint[tileHandler.previousRow, tileHandler.previousColumn] = tileHandler.gameObject;
           
        }
        GenerateLevel();
       
    }
    private void GenerateLevel()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (i == 0 && j == 0)
                {
                    Instantiate(Blocker, new Vector3(i - 1, 0.5f, j - 1), Quaternion.identity, transform);
                }
                if (i == row - 1 && j == col - 1)
                {
                    Instantiate(Blocker, new Vector3(i + 1, 0.5f, j + 1), Quaternion.identity, transform);
                }
                if (i == row - 1 && j == 0)
                {
                    Instantiate(Blocker, new Vector3(i + 1, 0.5f, j - 1), Quaternion.identity, transform);
                }
                if (i == 0 && j == col - 1)
                {
                    Instantiate(Blocker, new Vector3(i - 1, 0.5f, j + 1), Quaternion.identity, transform);
                }
                if (i == 0)
                {
                    Instantiate(Blocker, new Vector3(i - 1, 0.5f, j), Quaternion.identity, transform);
                }
                if (j == 0)
                {
                    Instantiate(Blocker, new Vector3(i, 0.5f, j - 1), Quaternion.identity, transform);
                }
                if (i == row - 1)
                {
                    Instantiate(Blocker, new Vector3(i + 1, 0.5f, j), Quaternion.identity, transform);
                }
                if (j == col - 1)
                {
                    Instantiate(Blocker, new Vector3(i, 0.5f, j + 1), Quaternion.identity, transform);
                }
                Instantiate(Tiles[(i + j) % 2], new Vector3(i, 0, j), Tiles[(i + j) % 2].transform.rotation, transform);

            }
        }
    }
}
