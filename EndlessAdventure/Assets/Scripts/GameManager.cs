using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

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

    public Collider2D MurderLine;

    public GameObject player;
    private BaseObject playerBase;
    private Animator playerAnimator;

    public GameObject BackGround1;
    public GameObject BackGround2;

    private GameObject curTarget;

    private bool canMove = true;

    private int rowCount = 0;

    private float itemChance = 0.10f;

    private float difficulty = 0.00f;

    private bool pStart = false;

    private int score = 0;
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI DiedText;

    public GameObject RestartBtn;
    public GameObject MainMenuBtn;

    public SpriteRenderer fadescreen;

    // Start is called before the first frame update
    void Start()
    {
        fadescreen.enabled = true;
        fadescreen.DOFade(0, 2.5f);
        playerAnimator = player.GetComponent<Animator>();
        playerBase = player.GetComponent<BaseObject>();
        playerBase.Attack += EnemyDamaged;
        playerBase.AttackEnd += CombatEnd;
        CreateCards(5);
        UpdateUI();
    }

    private void CreateCards(int rows)
    {
        difficulty += 0.01f;
        if (difficulty >= 0.35f)
            difficulty = 0.35f;
        for (int i = 0; i < rows; i++)
        {
            if(rowCount == 10) // Spwan Bossss!!!
            {
                rowCount = 0;
                GameObject temp = Instantiate(EnemiesB[Random.Range(0, EnemiesB.Count)], new Vector3(0, yOffset * -i + 7.5f, 0), Quaternion.identity);
                BaseObject tempBase = temp.GetComponent<BaseObject>();
                tempBase.lane = (Lane)0;
                tempBase.SetLookDirection(Random.Range(0f,1f) >= 0.5 ? -1 : 1);
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
                        if(rand > 0.88f - difficulty)
                            temp = Instantiate(EnemiesH[Random.Range(0, EnemiesH.Count)], new Vector3(xOffset * j, yOffset * -i + 7.5f, 0), Quaternion.identity);
                        else if(rand > 0.44f - difficulty)
                            temp = Instantiate(EnemiesM[Random.Range(0, EnemiesM.Count)], new Vector3(xOffset * j, yOffset * -i + 7.5f, 0), Quaternion.identity);
                        else
                            temp = Instantiate(EnemiesE[Random.Range(0, EnemiesE.Count)], new Vector3(xOffset * j, yOffset * -i + 7.5f, 0), Quaternion.identity);
                    }
                    else
                    {
                        itemChance = 0.10f;
                        rand = Random.Range(0f, 1f);
                        if(rand > 0.75f)
                            temp = Instantiate(ItemsC[Random.Range(0, ItemsC.Count)], new Vector3(xOffset * j, yOffset * -i + 7.5f, 0), Quaternion.identity);
                        else if(rand > 0.50f)
                            temp = Instantiate(ItemsUC[Random.Range(0, ItemsUC.Count)], new Vector3(xOffset * j, yOffset * -i + 7.5f, 0), Quaternion.identity);
                        else if(rand > 0.25f)
                            temp = Instantiate(ItemsR[Random.Range(0, ItemsR.Count)], new Vector3(xOffset * j, yOffset * -i + 7.5f, 0), Quaternion.identity);
                        else
                            temp = Instantiate(ItemsM[Random.Range(0, ItemsM.Count)], new Vector3(xOffset * j, yOffset * -i + 7.5f, 0), Quaternion.identity);
                    }

                    BaseObject tempBase = temp.GetComponent<BaseObject>();
                    tempBase.lane = (Lane)j;
                    tempBase.SetLookDirection(j);
                    cards.Add(temp);
                }
            }
            rowCount++;
        }
    }

    private void SwitchTarget(GameObject target)
    {
        if(curTarget)
        {
            curTarget.GetComponent<BaseObject>().Attack -= PlayerDamaged;
        }

        curTarget = target;
        curTarget.GetComponent<BaseObject>().Attack += PlayerDamaged;
        curTarget.GetComponent<BaseObject>().Attacked += AttackBack;

        if(curTarget.GetComponent<DestroySelfEvent>())
            curTarget.GetComponent<DestroySelfEvent>().ContinuePlay += CombatEnd;
    }

    private void PlayerDamaged()
    {
        playerAnimator.SetTrigger("Damage");
        curTarget.GetComponent<BaseObject>().CallEvents(playerBase);
        if(player.GetComponent<HealthComponent>().curHealth <= 0 || !player)
        {
            scoretext.transform.DOMove(new Vector3(0,1,0), 5f);
            playerBase.inventory.GetComponent<Transform>().DOMoveY(-10, 5f);
            playerBase.inventory.CanInteract = false;
            fadescreen.DOFade(1f, 5f).OnComplete(() => {
                RestartBtn.SetActive(true);
                MainMenuBtn.SetActive(true);
                MainMenuBtn.GetComponent<Image>().DOFade(1,2f);
                RestartBtn.GetComponent<Image>().DOFade(1,2f);
                RestartBtn.GetComponentInChildren<TextMeshProUGUI>().DOFade(1,2f);
                MainMenuBtn.GetComponentInChildren<TextMeshProUGUI>().DOFade(1,2f);
            });
            DiedText.DOFontSize(150, 5f);
            DiedText.DOFade(1f, 2.5f);
        }
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private void AttackBack()
    {
        if(curTarget.GetComponent<HealthComponent>().curHealth > 0)
        {
            if(!curTarget.GetComponent<SpriteRenderer>().flipX)
                curTarget.GetComponent<SpriteRenderer>().flipX = true;
            else
                curTarget.GetComponent<SpriteRenderer>().flipX = false;
            curTarget.GetComponent<BaseObject>().CallAnimationEvents();
        }
    }

    private void EnemyDamaged()
    {
        playerBase.CallEvents(curTarget.GetComponent<BaseObject>());
        if(curTarget.GetComponent<HealthComponent>().curHealth <= 0)
        {
            score += curTarget.GetComponent<HealthComponent>().maxHealth;
            UpdateUI();
        }
        curTarget.GetComponent<Animator>().SetTrigger("Damage");
    }

    private void UpdateUI()
    {
        scoretext.text = "Score: " + score.ToString();
    }

    private void CombatEnd()
    {
        if(curTarget.GetComponent<HealthComponent>())
        {
            if(curTarget.GetComponent<HealthComponent>().curHealth < 0)
            {
                player.transform.DOMove(new Vector3(xOffset * (int)playerBase.lane, player.transform.position.y, 0), 0.5f, false)
                .OnStart(() => {playerAnimator.SetBool("Moving", true);})
                .OnComplete(() => {
                    playerAnimator.SetBool("Moving", false);
                    canMove = true;
                    MurderLineDestroy();
                });
            }
        }
        else
        {
            player.transform.DOMove(new Vector3(xOffset * (int)playerBase.lane, player.transform.position.y, 0), 0.5f, false)
                .OnStart(() => {playerAnimator.SetBool("Moving", true);})
                .OnComplete(() => {
                    playerAnimator.SetBool("Moving", false);
                    canMove = true;
                    MurderLineDestroy();
                });
        }
    }

    private void StartCombat(bool playerStart)
    {
        player.GetComponent<DamageEventPlayer>().SetDamage(player.GetComponent<HealthComponent>().curHealth);
        if(curTarget.GetComponent<BaseObject>().oType == Type.enemy && !playerStart)
            curTarget.GetComponent<BaseObject>().CallAnimationEvents();
        else if(curTarget.GetComponent<BaseObject>().oType == Type.enemy && playerStart)
        {
            playerAnimator.SetTrigger("Attack");
        }
        else
        {
            curTarget.GetComponent<BaseObject>().CallEvents(playerBase);
        }
    }

    private void MoveCards(int lane, float dir, GameObject target)
    {
        pStart = false;
        canMove = false;
        float spacing = (float)lane * -0.75f;
        if(playerBase.lane == Lane.left && lane == 0 && target.GetComponent<SpriteRenderer>().flipX)
        {
            spacing =  -0.75f;
            pStart = true;
        }
        else if(playerBase.lane == Lane.right && lane == 0 && !target.GetComponent<SpriteRenderer>().flipX)
        {
            spacing =  0.75f;
            pStart = true;
        }
        else if(lane == 0 && target.GetComponent<SpriteRenderer>().flipX)
        {
            spacing =  0.75f;
        }
        else if(lane == 0)
        {
            spacing =  -0.75f;
        }

        if(target.GetComponent<BaseObject>().oType == Type.item)
            spacing = 0;

        playerBase.lane = (Lane)lane;
#region BG Movement
        BackGround1.transform.DOMove(new Vector3(BackGround1.transform.position.x, BackGround1.transform.position.y - yOffset, 0), 0.5f)
        .OnComplete(() => {
            if(BackGround1.transform.position.y <= -11f)
            {
                Debug.Log("BG BG BG");
                BackGround1.transform.position = new Vector3(0,BackGround2.transform.position.y + 13.75f,0);
            }
        });

        BackGround2.transform.DOMove(new Vector3(BackGround2.transform.position.x, BackGround2.transform.position.y - yOffset, 0), 0.5f)
        .OnComplete(() => {
            if(BackGround2.transform.position.y <= -11f)
            {
                Debug.Log("BG BG BG");
                BackGround2.transform.position = new Vector3(0,BackGround1.transform.position.y + 13.75f,0);
            }
        });
#endregion

        player.transform.DOMove(new Vector3(xOffset * lane + spacing, player.transform.position.y, 0), 0.5f)
        .OnStart(() => {playerAnimator.SetBool("Moving", true);})
        .OnComplete(() => {
            playerAnimator.SetBool("Moving", false);
            if(curTarget.GetComponent<SpriteRenderer>().flipX && !pStart)
            {
                player.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if(!pStart)
            {
                player.GetComponent<SpriteRenderer>().flipX = false;
            }
            StartCombat(pStart);
        });

        foreach (GameObject o in cards)
        {
            try
            {
                o.transform.DOMove(new Vector3(o.transform.position.x, o.transform.position.y - yOffset , 0), 0.5f);
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
            cards.Remove(c.gameObject);
            Destroy(c.gameObject);
        });
    }

    private void CheckInput(Vector3 inputPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(inputPos), Vector2.zero);
 
        if(hit.collider != null)
        {
            int dist = (int)hit.collider.GetComponent<BaseObject>().lane - (int)playerBase.lane;
            int dir = dist;
            dist = Mathf.Abs(dist);
            float yDist = hit.collider.transform.position.y - player.transform.position.y;
            yDist = Mathf.Abs(yDist);
            Debug.Log(yDist);
            if(dist >= 0 && dist <= 1 && yDist <= 2.5f && yDist >= 2f && hit.collider.transform.position.y > player.transform.position.y)
            {
                if(dir <= 0)
                {
                    player.GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    player.GetComponent<SpriteRenderer>().flipX = false;
                }
                SwitchTarget(hit.collider.gameObject);
                MoveCards((int)hit.collider.GetComponent<BaseObject>().lane, dir, hit.collider.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canMove && player != null && player.GetComponent<HealthComponent>().curHealth > 0)
        {
            CheckInput(Input.mousePosition);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && canMove && player != null && player.GetComponent<HealthComponent>().curHealth > 0)
            {
                CheckInput(touch.position);
            }
            // else if (touch.phase == TouchPhase.Began && player == null)
            // {
            //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // }
        }
    }

}
