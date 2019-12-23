using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using Pixelplacement.TweenSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float yOffset = 1.75f;
    public float xOffset = 1.5f;
    private List<GameObject> cards = new List<GameObject>();

    public List<GameObject> EnemiesB = new List<GameObject>();
    public List<GameObject> EnemiesE = new List<GameObject>();
    public List<GameObject> EnemiesM = new List<GameObject>();
    public List<GameObject> EnemiesH = new List<GameObject>();
    public List<GameObject> ItemsC = new List<GameObject>();
    public List<GameObject> ItemsUC = new List<GameObject>();
    public List<GameObject> ItemsR = new List<GameObject>();
    public List<GameObject> ItemsM = new List<GameObject>();

    public Transform GameField;

    public Collider2D MurderLine;

    public GameObject player;
    private BaseObject playerBase;

    private bool canMove = true;

    private int rowCount = 0;

    private float itemChance = 0.10f;

    // Start is called before the first frame update
    void Start()
    {
        playerBase = player.GetComponent<BaseObject>();
        CreateCards(5);
    }

    private void CreateCards(int rows)
    {
        for (int i = 0; i < rows; i++)
        {
            if(rowCount == 10) // Spwan Bossss!!!
            {
                rowCount = 0;
                GameObject temp = Instantiate(EnemiesB[Random.Range(0, EnemiesB.Count)], new Vector3(0, yOffset * -i + 7.5f, 0), Quaternion.identity, GameField);
                BaseObject tempBase = temp.GetComponent<BaseObject>();
                tempBase.lane = (Lane)0;
                cards.Add(temp);
            }
            else
            {
                for (int j = -1; j < 3 -1; j++)
                {
                    float rand = Random.Range(0f, 1f);
                    GameObject temp;
                    if(rand > itemChance)
                    {
                        itemChance += 0.02f;
                        rand = Random.Range(0f, 1f);
                        if(rand > 0.88f)
                            temp = Instantiate(EnemiesH[Random.Range(0, EnemiesH.Count)], new Vector3(xOffset * j, yOffset * -i + 7.5f, 0), Quaternion.identity, GameField);
                        else if(rand > 0.44f)
                            temp = Instantiate(EnemiesM[Random.Range(0, EnemiesM.Count)], new Vector3(xOffset * j, yOffset * -i + 7.5f, 0), Quaternion.identity, GameField);
                        else
                            temp = Instantiate(EnemiesE[Random.Range(0, EnemiesE.Count)], new Vector3(xOffset * j, yOffset * -i + 7.5f, 0), Quaternion.identity, GameField);
                    }
                    else
                    {
                        itemChance = 0.10f;
                        rand = Random.Range(0f, 1f);
                        if(rand > 0.75f)
                            temp = Instantiate(ItemsC[Random.Range(0, ItemsC.Count)], new Vector3(xOffset * j, yOffset * -i + 7.5f, 0), Quaternion.identity, GameField);
                        else if(rand > 0.50f)
                            temp = Instantiate(ItemsUC[Random.Range(0, ItemsUC.Count)], new Vector3(xOffset * j, yOffset * -i + 7.5f, 0), Quaternion.identity, GameField);
                        else if(rand > 0.25f)
                            temp = Instantiate(ItemsR[Random.Range(0, ItemsR.Count)], new Vector3(xOffset * j, yOffset * -i + 7.5f, 0), Quaternion.identity, GameField);
                        else
                            temp = Instantiate(ItemsM[Random.Range(0, ItemsM.Count)], new Vector3(xOffset * j, yOffset * -i + 7.5f, 0), Quaternion.identity, GameField);
                    }

                    BaseObject tempBase = temp.GetComponent<BaseObject>();
                    tempBase.lane = (Lane)j;
                    cards.Add(temp);
                }
            }
            rowCount++;
        }
    }

    private void MoveCards(int lane, GameObject target)
    {
        canMove = false;
        playerBase.lane = (Lane)lane;
        Tween.Position(player.transform, new Vector3(xOffset * lane, player.transform.position.y, 0), 0.5f, 0, null, Tween.LoopType.None, null, () => 
        {
            playerBase.CallEvents(target.GetComponent<BaseObject>());
            target.GetComponent<BaseObject>().CallEvents(playerBase);
            canMove = true;
            MurderLineDestroy();
        });

        Debug.Log(cards.Count);

        foreach (GameObject o in cards)
        {
            try
            {
                o.GetComponent<BaseObject>().tween = Tween.Position(o.transform, new Vector3(o.transform.position.x, o.transform.position.y - yOffset , 0), 0.5f, 0);
            }
            catch
            {
                Debug.Log("He was already gone.");
            }
        }

        CreateCards(1);

    }

    private void MurderLineDestroy()
    {
        List<Collider2D> result = new List<Collider2D>();
        Physics2D.OverlapCollider(MurderLine, new ContactFilter2D().NoFilter(), result);
        result.ForEach(c => {
            c.GetComponent<BaseObject>().tween.Cancel();
            cards.Remove(c.gameObject);
            Destroy(c.gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canMove && player != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
 
            if(hit.collider != null)
            {
                int dist = (int)hit.collider.GetComponent<BaseObject>().lane - (int)playerBase.lane;
                dist = Mathf.Abs(dist);
                float yDist = hit.collider.transform.position.y - player.transform.position.y;
                yDist = Mathf.Abs(yDist);
                Debug.Log(yDist);
                if(dist >= 0 && dist <= 1 && yDist <= 2.5f && yDist >= 2f)
                    MoveCards((int)hit.collider.GetComponent<BaseObject>().lane, hit.collider.gameObject);
            }
        }
        else if (Input.GetMouseButtonDown(0) && player == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
