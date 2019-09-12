using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Camera mainCam;
    public Canvas canvas;

    public GameObject worldObject;

    public List<ObjectScriptable> objectReference = new List<ObjectScriptable>();

    private List<Object> objects = new List<Object>();


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                Object temp = Instantiate(worldObject, new Vector3((j * 1.5f) - 1.5f, (i * 1.5f) -1.5f, 0), Quaternion.identity, canvas.transform).GetComponent<Object>();
                temp.scriptable = objectReference[Random.Range(0, objectReference.Count)];
                temp.scriptable.Setup();
                if(i%3==0 && i != 0)
                {
                    if(j == 1)
                    {
                        Debug.Log("Neekeri neekeri neekeri");
                        temp.scriptable.atk *= 3;
                        temp.scriptable.hp *= 3;
                    }
                }
                temp.CallEntryEvents();
                objects.Add(temp);
            }
        }
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
 
            if(hit.collider != null && hit.collider.GetComponent<Object>())
            {
                Debug.Log ("Target Name: " + hit.collider.GetComponent<Object>().scriptable.oName);
            }
        }
    }

}
