using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tile
{
    public Vector2 position { get; set; }
    public BaseTurret Turret { get; set; }
    public bool Occupied { get; set; }
}

public class GameGridScript : MonoBehaviour
{
    public static GameGridScript Instance { get; set; }
    private const int AMT_TILES_X = 16;
    private const int AMT_TILES_Y = 8;
    private const float TILE_SIZE = 1.0f;
    private int SelectedTurretIndex;
    private bool IsSelectingTurret;

    public Tile[,] grid;
    public GameObject[] turretPrefab;

    private void Start()
    {
        grid = new Tile[AMT_TILES_X, AMT_TILES_Y];
        for (int i = 0; i < AMT_TILES_X; i++)
        {
            for (int j = 0; j < AMT_TILES_Y; j++)
            {
                grid[i, j] = new Tile() { position = new Vector2(i, j), Turret = null };
            }
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 30.0f, LayerMask.GetMask("GameGrid")))
            {
                int x = (int)hit.point.x;
                int y = (int)hit.point.z;

                if (IsSelectingTurret)
                {
                    Tile t = SelectGridTile(x, y);

                    if (t.position.x < 2 && t.Occupied == false)
                    {
                        GameObject go = Instantiate(turretPrefab[SelectedTurretIndex]) as GameObject;
                        Vector3 posX = new Vector3(1 * x + 0.25f, 0, 0);
                        Vector3 posY = new Vector3(0, 0, 1 * y + 0.5f);
                        go.transform.position = posX + posY;
                        
                        var turretSpawner = new BaseTurretSpawner();
                        
                        if(GameObject.FindGameObjectWithTag("Enemy") != null)
                        {
                            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                            {
                                var en = enemy.GetComponent<Enemy1Script>();
                                turretSpawner.TurretSpawned += en.OnTurretSpawned;
                                turretSpawner.Spawn(go.GetComponent<BaseTurret>());
                                turretSpawner.TurretSpawned -= en.OnTurretSpawned;
                            }
                        }

                        t.Occupied = true;
                        t.Turret = go.GetComponent<BaseTurret>();

                        int cost = int.Parse(GameManager.Instance.turretContainer.transform.GetChild(SelectedTurretIndex).GetComponentInChildren<Text>().text);
                        GameManager.Instance.Gold -= cost;
                        GameManager.Instance.UpdateGoldText();

                        IsSelectingTurret = false;
                        SelectedTurretIndex = -1;
                    }
                    else
                    {
                        IsSelectingTurret = false;
                        SelectedTurretIndex = -1;
                    }
                }
            }
        }
        for (int i = 0; i < AMT_TILES_X + 1; i++)
        {
            Vector3 startPos = Vector3.right * i;
            Debug.DrawLine(startPos, startPos + Vector3.forward * AMT_TILES_Y);
        }

        for (int j = 0; j < AMT_TILES_Y + 1; j++)
        {
            Vector3 startPos = Vector3.forward * j;
            Debug.DrawLine(startPos, startPos + Vector3.right * AMT_TILES_X);
        }
    }
    private Tile SelectGridTile(int x, int y)
    {
        return grid[x, y];
    }
    public void SelectTurret(int index)
    {
        //enough money?
        int cost = int.Parse(GameManager.Instance.turretContainer.transform.GetChild(index).GetComponentInChildren<Text>().text);
        if (cost <= GameManager.Instance.Gold)
        {
            IsSelectingTurret = true;
            SelectedTurretIndex = index;
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    }

    public void SpawnObjects(int index, int laneIndex)
    {

    }
}
