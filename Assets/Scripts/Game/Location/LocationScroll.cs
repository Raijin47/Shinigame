using UnityEngine;

public class LocationScroll : MonoBehaviour
{
    [SerializeField] private Vector2Int playerTilePosition;

    [SerializeField] private float tileSize;

    [SerializeField] private int terrainTileHorizontalCount;
    [SerializeField] private int terrainTileVerticalCount;
    [SerializeField] private int fieldOfVisionHeight;
    [SerializeField] private int fieldOfVisionWidth;

    private Transform playerTransform;
    private GameObject[,] terrainTiles;
    private Vector2Int onTileGridPlayerPosition;
    private Vector2Int currentTilePosition = new Vector2Int(0,0);

    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    private void Start()
    {
        StartTileOnScreen();
        playerTransform = GameManager.instance.playerTransform;
    }
    private void Update()
    {
        playerTilePosition.x = (int)(playerTransform.position.x / tileSize);
        playerTilePosition.y = (int)(playerTransform.position.y / tileSize);

        playerTilePosition.x -= playerTransform.position.x < 0 ? 1 : 0;
        playerTilePosition.y -= playerTransform.position.y < 0 ? 1 : 0;

        if(currentTilePosition != playerTilePosition)
        {
            currentTilePosition = playerTilePosition;

            onTileGridPlayerPosition.x = CalculatePositionOnAxis(onTileGridPlayerPosition.x, true);
            onTileGridPlayerPosition.y = CalculatePositionOnAxis(onTileGridPlayerPosition.y, false);
            UpdateTileOnScreen();
        }
    }
    private void StartTileOnScreen()
    {
        for (int x = -(fieldOfVisionWidth / 2); x <= fieldOfVisionWidth / 2; x++)
        {
            for (int y = -(fieldOfVisionHeight / 2); y <= fieldOfVisionHeight / 2; y++)
            {
                int tileToUpdate_x = CalculatePositionOnAxis(playerTilePosition.x + x, true);
                int tileToUpdate_y = CalculatePositionOnAxis(playerTilePosition.y + y, false);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                Vector3 newPosition = CalculateTilePosition(playerTilePosition.x + x, playerTilePosition.y + y);
                if (newPosition != tile.transform.position)
                {
                    tile.transform.position = newPosition;
                }
            }
        }
    }
    private void UpdateTileOnScreen()
    {
        for(int x = -(fieldOfVisionWidth/2); x <= fieldOfVisionWidth/2; x++)
        {
            for(int y = -(fieldOfVisionHeight / 2); y <= fieldOfVisionHeight/2; y++)
            {
                int tileToUpdate_x = CalculatePositionOnAxis(playerTilePosition.x + x, true);
                int tileToUpdate_y = CalculatePositionOnAxis(playerTilePosition.y + y, false);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                Vector3 newPosition = CalculateTilePosition(playerTilePosition.x + x, playerTilePosition.y + y);
                if(newPosition != tile.transform.position)
                {
                    tile.transform.position = newPosition;
                    terrainTiles[tileToUpdate_x, tileToUpdate_y].GetComponent<LocationTile>().Spawn();
                }
            }
        }
    }
    private Vector3 CalculateTilePosition(int x, int y)
    {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }
    private int CalculatePositionOnAxis(float currentValue, bool horizontal)
    {
        if(horizontal)
        {
            if(currentValue >= 0)
            {
                currentValue = currentValue % terrainTileHorizontalCount;
            }
            else
            {
                currentValue++;
                currentValue = terrainTileHorizontalCount - 1 + currentValue % terrainTileHorizontalCount;
            }
        }
        else
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileVerticalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileVerticalCount - 1 + currentValue % terrainTileVerticalCount;
            }
        }

        return (int)currentValue;
    }
    public void Add(GameObject tileGameObject, Vector2Int tilePos)
    {
        terrainTiles[tilePos.x, tilePos.y] = tileGameObject;
    }
}