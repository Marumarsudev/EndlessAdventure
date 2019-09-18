using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float xOffset = -1.5f;
    public float yOffset = -2f;
    public float cardDistance = 1.5f;
    public float itemcount = 0.15f;

    public TextMeshPro scoreText;

    public Camera mainCam;
    public Canvas canvas;

    public GameObject worldObject;

    public List<ObjectScriptable> enemiesE = new List<ObjectScriptable>();
    public List<ObjectScriptable> enemiesM = new List<ObjectScriptable>();
    public List<ObjectScriptable> enemiesH = new List<ObjectScriptable>();
    public List<ObjectScriptable> bosses = new List<ObjectScriptable>();
    public List<ObjectScriptable> itemsHP = new List<ObjectScriptable>();
    public List<ObjectScriptable> itemsHPM = new List<ObjectScriptable>();
    public List<ObjectScriptable> itemsHPH = new List<ObjectScriptable>();
    public List<ObjectScriptable> itemsPOIS = new List<ObjectScriptable>();

    public GameObject player;
    private Object oPlayer;

    private Object targetObject;

    private List<Object> objects = new List<Object>();

    private bool canMove = true;

    private int score = 0;

    int y = 0;


    // Start is called before the first frame update
    void Start()
    {
        oPlayer = player.GetComponent<Object>();
        CreateTiles(5, y);
    }

    void CreateTiles(int amount, int _i)
    {
        for(int i = _i; i < amount + _i; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(i%10==0 && i != 0)
                {
                    if(j == 1)
                    {
                        Object temp = Instantiate(worldObject, new Vector3((j * cardDistance) + xOffset, (i * cardDistance) + yOffset, 0), Quaternion.identity, canvas.transform).GetComponent<Object>();
                        temp.scriptable = Instantiate(bosses[Random.Range(0, bosses.Count)], temp.transform.position, Quaternion.identity, temp.transform);
                        temp.scriptable.Setup();
                        temp.ChangeSprite();
                        temp.scriptable.atk *= 3;
                        temp.scriptable.hp *= 3;
                    }
                }
                else
                {
                    Object temp = Instantiate(worldObject, new Vector3((j * cardDistance) + xOffset, (i * cardDistance) + yOffset, 0), Quaternion.identity, canvas.transform).GetComponent<Object>();
                    if(Random.Range(0f,1f) > itemcount)
                    {
                        float rand = Random.Range(0f,1f);
                        if(rand > 0.9f)
                            temp.scriptable = Instantiate(enemiesH[Random.Range(0, enemiesH.Count)], temp.transform.position, Quaternion.identity, temp.transform);
                        else if(rand > 0.6f)
                            temp.scriptable = Instantiate(enemiesM[Random.Range(0, enemiesM.Count)], temp.transform.position, Quaternion.identity, temp.transform);
                        else
                            temp.scriptable = Instantiate(enemiesE[Random.Range(0, enemiesE.Count)], temp.transform.position, Quaternion.identity, temp.transform);
                    }
                    else
                    {
                        float rand = Random.Range(0f,1f);
                        if(rand > 0.8f)
                            temp.scriptable = Instantiate(itemsHPH[Random.Range(0, itemsHPH.Count)], temp.transform.position, Quaternion.identity, temp.transform);
                        else if(rand > 0.5f)
                            temp.scriptable = Instantiate(itemsHPM[Random.Range(0, itemsHPM.Count)], temp.transform.position, Quaternion.identity, temp.transform);
                        else
                            temp.scriptable = Instantiate(itemsHP[Random.Range(0, itemsHP.Count)], temp.transform.position, Quaternion.identity, temp.transform);
                    }
                    temp.scriptable.Setup();
                    temp.ChangeSprite();
                    objects.Add(temp);
                }
            }
            y++;
        }
    }

    private IEnumerator DestroyLast()
    {
        int count = 0;
        int id = 0;
        while (count < 3)
        {
            if(objects[id])
            {
                DestroyImmediate(objects[id].gameObject);
                count++;
            }
            else
            {
                id++;
            }
            yield return null;
        }
    }

    private void PlayerMove(Vector3 inputPos)
    {
        if(canMove)
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(inputPos), Vector2.zero);
 
            if(hit.collider != null && hit.collider.GetComponent<Object>() && player.transform.position.y + cardDistance >= hit.collider.transform.position.y && player.transform.position.y < hit.collider.transform.position.y && Mathf.Abs(hit.collider.transform.position.x - player.transform.position.x) <= cardDistance)
            {
                canMove = false;
                targetObject = hit.collider.GetComponent<Object>();
                Vector3 targetpos = targetObject.transform.position;
                CreateTiles(1, y);
                int tempscore = targetObject.scriptable.hp;
                Tween.Position(player.transform, targetpos, 0.5f, 0, Tween.EaseIn, Tween.LoopType.None, null, () => {
                    targetObject.CallEntryEvents(oPlayer, targetObject);
                    if(oPlayer)
                    {
                        oPlayer.CallEntryEvents(targetObject, oPlayer);
                        canMove = true;
                        if(oPlayer.scriptable.hp > 0)
                        {
                            score += tempscore;
                            scoreText.text = "Score : " + score.ToString();
                        }
                    }
                    Tween.Position(mainCam.transform, new Vector3(0, player.transform.position.y + 3.5f, -10), 0.5f, 0);
                    if(y >= 10 && y % 2 == 0)
                    {
                        StartCoroutine(DestroyLast());
                    }
                });
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (oPlayer)
            {
                PlayerMove(Input.mousePosition);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else if(Input.touchCount > 0)
        {
            if(oPlayer)
                PlayerMove(Input.GetTouch(0).position);
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
