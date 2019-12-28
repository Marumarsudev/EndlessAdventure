using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogLevel : MonoBehaviour
{
    private int FogAmount = 0;

    public SpriteRenderer Fog1, Fog2, Fog3;

    public void ChangeFog()
    {
        FogAmount++;
        if (FogAmount > 3)
        {
            FogAmount = 0;
        }

        switch(FogAmount)
        {
            case 0:
                Fog1.gameObject.SetActive(false);
                Fog2.gameObject.SetActive(false);
                Fog3.gameObject.SetActive(false);
            break;

            case 1:
                Fog1.gameObject.SetActive(true);
                Fog2.gameObject.SetActive(false);
                Fog3.gameObject.SetActive(false);
            break;

            case 2:
                Fog1.gameObject.SetActive(true);
                Fog2.gameObject.SetActive(true);
                Fog3.gameObject.SetActive(false);
            break;

            case 3:
                Fog1.gameObject.SetActive(true);
                Fog2.gameObject.SetActive(true);
                Fog3.gameObject.SetActive(true);
            break;
        }
    }
}
