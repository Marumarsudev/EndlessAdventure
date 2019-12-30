using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalSaveData
{
    public LocalSaveData(LocalSaveData data)
    {
        MasterVolume = data.MasterVolume;
        MusicVolume = data.MusicVolume;
        FXVolume = data.FXVolume;
    }
    public float MasterVolume;
    public float MusicVolume;
    public float FXVolume;
}
