using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalSaveData
{
    public LocalSaveData(LocalSaveData data)
    {
        if(data != null)
        {
            MasterVolume = data.MasterVolume;
            MusicVolume = data.MusicVolume;
            FXVolume = data.FXVolume;
        }
        else
        {
            MasterVolume = 20;
            MusicVolume = 20;
            FXVolume = 20;
        }
    }
    public float MasterVolume;
    public float MusicVolume;
    public float FXVolume;
}
