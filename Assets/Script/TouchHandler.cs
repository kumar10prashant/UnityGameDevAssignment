using System;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    [SerializeField] private Transform selectedTransform;
    [SerializeField] private Camera playerCam;
    [SerializeField] private LayerMask groundLayerMask, Selectble;
    private Rigidbody selectedRb;

    [SerializeField] private float moveForce = 1000f; // Add force magnitude
    [SerializeField] private float maxSpeed = 10f;

    private Vector3 targetPosition;

    private float tileSize = 1f;
    [SerializeField] Vector3 offset;
    [SerializeField] LevelGenerator levelGenerator;

    Tiles selectedTiles;
    private void Start()
    {
        playerCam = Camera.main;
    }

    private void Update()
    {


        if (selectedTransform != null)
        {
            if (Physics.Raycast(GetMouseRay(), out RaycastHit hitInfo, float.MaxValue, groundLayerMask))
            {
                //offset = new Vector3(hitInfo.point.x, 0, hitInfo.point.z) - new Vector3(selectedTransform.position.x, selectedTransform.position.y, selectedTransform.position.z);
                targetPosition = new Vector3(hitInfo.point.x, selectedTransform.position.y, hitInfo.point.z);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(GetMouseRay(), out RaycastHit hitInfo, float.MaxValue, Selectble))
            {
                selectedTransform = hitInfo.collider.transform.parent;
                selectedTiles = selectedTransform.GetComponent<Tiles>();
                selectedRb = selectedTransform.GetComponent<Rigidbody>();
                selectedRb.isKinematic = false;
                SetColliderSize(selectedTransform, new Vector3(0.9f, 1f, 0.9f));
                if (Physics.Raycast(GetMouseRay(), out RaycastHit groundHit, float.MaxValue, groundLayerMask))
                {
                    offset = selectedTransform.position - groundHit.point;
                }
                else
                {
                    offset = Vector3.zero;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (selectedTransform != null)
            {

                CheckTilePos();
                

            }
        }
    }

    public void CheckTilePos()
    {
        selectedRb.isKinematic = true;
        selectedRb.transform.position = new Vector3(Mathf.Round(selectedTransform.position.x / tileSize) * tileSize, selectedTransform.position.y, Mathf.Round(selectedTransform.position.z / tileSize) * tileSize);
        SetColliderSize(selectedTransform,Vector3.one);

        

        float posX = selectedTransform.position.x + selectedTransform.GetComponent<Tiles>().width;
        float posZ = selectedTransform.position.z + selectedTransform.GetComponent<Tiles>().height;

        if (selectedTransform.position.x < 0)
        {
            selectedTransform.transform.position = new Vector3(0, selectedTransform.position.y, selectedTransform.position.z);
        }
        if (selectedTransform.position.z < 0)
        {
            selectedTransform.transform.position = new Vector3(selectedTransform.position.x, selectedTransform.position.y, 0);
        }
        if (posX > levelGenerator.row)
        {
            selectedTransform.transform.position = new Vector3(selectedTransform.position.x - (posX - levelGenerator.row), selectedTransform.position.y, selectedTransform.position.z);
        }
        if (posZ > levelGenerator.col)
        {
            selectedTransform.transform.position = new Vector3(selectedTransform.position.x, selectedTransform.position.y, selectedTransform.position.z - (posZ - levelGenerator.col));
        }
        UpdateTilesPoint();
        selectedTransform = null;
        selectedRb = null;


    }

    private void UpdateTilesPoint()
    {
        foreach(var tiles in selectedTransform.GetComponentsInChildren<TileHandler>())
        {
            levelGenerator.TilesPoint[tiles.previousRow, tiles.previousColumn] = null;
            tiles.previousRow = (int)tiles.transform.position.x;
            tiles.previousColumn = (int)tiles.transform.position.z;
            levelGenerator.TilesPoint[tiles.previousRow, tiles.previousColumn] = tiles.gameObject;
           
        }
    }

    //This function set and reset the size of a collider when you select and deselect the object
    public void SetColliderSize(Transform transform,Vector3 size)
    {
        foreach (var collider in transform.GetComponentsInChildren<BoxCollider>())
        {
            collider.size = size;
        }
    }

    private void FixedUpdate()
    {
        if (selectedTransform != null)
        {
            Vector3 direction = ((targetPosition + offset) - selectedTransform.position).normalized;

            // Apply a force toward the target
            //selectedRb.AddForce(direction * moveForce * Time.fixedDeltaTime);

            if (Vector3.Distance(selectedTransform.position, (targetPosition + offset)) > 0.1f)
            {
                if (selectedTiles.horizontalMove && selectedTiles.verticalMove)
                {
                    selectedRb.linearVelocity = direction * maxSpeed;
                }
                else if (selectedTiles.horizontalMove)
                {
                    selectedRb.linearVelocity = new Vector3(direction.x, 0, 0) * maxSpeed;
                }
                else if (selectedTiles.verticalMove)
                {
                    selectedRb.linearVelocity = new Vector3(0, 0, direction.z) * maxSpeed;
                }

            }
            else
            {
                selectedRb.linearVelocity = Vector3.zero;
            }


            // Clamp the speed to not go crazy fast
            /*  if (selectedRb.linearVelocity.magnitude > maxSpeed)
              {
                  selectedRb.linearVelocity = selectedRb.linearVelocity.normalized * maxSpeed;
              }*/
        }
    }

    private Ray GetMouseRay()
    {
        return playerCam.ScreenPointToRay(Input.mousePosition);
    }
}
