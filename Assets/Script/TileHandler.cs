using UnityEngine;

public class TileHandler : MonoBehaviour
{
    public int previousRow;
    public int previousColumn;

    public void Start()
    {
        previousRow = (int)transform.position.x;
        previousColumn = (int)transform.position.z;

        

    }
}
