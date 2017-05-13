using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    private Level CurrentLevel;
    private float StartingTime;
    private GameObject clone;
    private Camera MainCamera;
    private BaseEnemyScript Enemy;

    public int Gold { get; set; }
    public GameObject turretContainer;
    public GameObject[] enemyContainer;
    public Text currentLevelIndex;
    public Text goldAmountText;
    public bool enemySpawned;

    void Start()
    {
        Instance = this;
        CurrentLevel = DataHelper.Instance.levels[DataHelper.Instance.CurrentLevel];
        Gold += CurrentLevel.StartingGold;
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        //Game UI
        currentLevelIndex.text = CurrentLevel.levelName;

        UnlockTurrets();
        UpdateGoldText();
        SceneManager.LoadScene(DataHelper.Instance.CurrentLevel.ToString(), LoadSceneMode.Additive);

        StartingTime = Time.time;
    }

    private void Update()
    {
        float gameDuration = Time.time - StartingTime;
        for (int i = 0; i < CurrentLevel.objects.Count; i++)
        {
            if (CurrentLevel.objects[i].time < gameDuration)
            {
                clone = Instantiate(enemyContainer[1], new Vector3(CurrentLevel.objects[i].positionX + 0.5f, 0f, CurrentLevel.objects[i].positionZ + 0.5f), Quaternion.identity) as GameObject;
                enemySpawned = true;
                CurrentLevel.objects.Remove(CurrentLevel.objects[i]);
            }
        }

        if (GetClickedEnemy() != null)
        {
            DestroyEnemyByClicking(GetClickedEnemy());
        }
    }

    private void UnlockTurrets()
    {
        int i = 0;
        foreach (Transform t in turretContainer.transform)
        {
            bool activeButton = ((CurrentLevel.UnlockedTowers) & (1 << i)) != 0;
            //Debug.Log (activeButton);
            t.GetComponent<Button>().interactable = activeButton;
            i++;
        }
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void UpdateGoldText()
    {
        goldAmountText.text = Gold.ToString();
    }

    public void DestroyEnemyByClicking(BaseEnemyScript enemy)
    {
        if (enemy.Life > 10)
            enemy.Life -= 10;
        if (enemy.Life <= 0)
        {
            Destroy(enemy.gameObject);
        }
        Debug.Log(enemy.Life);
    }

    public BaseEnemyScript GetClickedEnemy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Enemy")))
            {
                if (hit.rigidbody != null)
                {
                    return hit.collider.gameObject.GetComponent<BaseEnemyScript>();
                }
            }
        }
        return null;
    }
}
