using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public GameObject BackGround1;
    public GameObject BackGround2;

    private float yOffset = 0.5f;

    public SpriteRenderer fade;

    public AudioMixer MasterMixer;

    public TextMeshProUGUI MasterText;
    public TextMeshProUGUI MusicText;
    public TextMeshProUGUI FXText;

    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject Credits;

    public LocalSaveData saveData;

    // Start is called before the first frame update
    void Start()
    {
        if(LocalSaveHandler.LoadData("LocalSaveData") != null)
        {
            saveData = LocalSaveHandler.LoadData("LocalSaveData") as LocalSaveData;
            ApplyLoadedDataToMixer();
        }
        else
        {
            saveData = new LocalSaveData(null);
        }
        UpdateSettingsUI();
        MoveBG1();
        MoveBG2();
    }

    public void MainMenuVisible(bool visible)
    {
        MainMenu.SetActive(visible);
    }
    public void SettingsMenuVisible(bool visible)
    {
        SettingsMenu.SetActive(visible);
    }
        public void CreditsVisible(bool visible)
    {
        Credits.SetActive(visible);
    }

    public void SaveLocalData(object data)
    {
        LocalSaveHandler.SaveData(data, "LocalSaveData");
    }

    private void ApplyLoadedDataToMixer()
    {
        MasterMixer.SetFloat("MasterVolume", saveData.MasterVolume);
        MasterMixer.SetFloat("MusicVolume", saveData.MusicVolume);
        MasterMixer.SetFloat("FXVolume", saveData.FXVolume);
    }

    public void AddVolume(string name)
    {
        MasterMixer.GetFloat(name, out float Volume);
        float newVolume = Volume + 5f > 0 ? 0 : Volume + 5f;
        MasterMixer.SetFloat(name, newVolume);

        UpdateSettingsUI();
    }

    public void SubtractVolume(string name)
    {
        MasterMixer.GetFloat(name, out float Volume);
        float newVolume = Volume - 5f < -80 ? -80 : Volume - 5f;
        MasterMixer.SetFloat(name, newVolume);

        UpdateSettingsUI();
    }

    private float NormalizeVolume(float volume)
    {
        return Mathf.Ceil((volume - (-80f)) / (0 - (-80f)) * 100f);
    }

    private void UpdateSettingsUI()
    {
        MasterMixer.GetFloat("MasterVolume", out float MasterVolume);
        MasterMixer.GetFloat("MusicVolume", out float MusicVolume);
        MasterMixer.GetFloat("FXVolume", out float FXVolume);

        saveData.MasterVolume = MasterVolume;
        saveData.MusicVolume = MusicVolume;
        saveData.FXVolume = FXVolume;

        SaveLocalData(saveData);

        MasterText.text = (NormalizeVolume(MasterVolume)).ToString();
        MusicText.text = (NormalizeVolume(MusicVolume)).ToString();
        FXText.text = (NormalizeVolume(FXVolume)).ToString();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        fade.DOFade(1, 0.75f).SetEase(Ease.OutCubic).OnComplete(() => {SceneManager.LoadScene("MainScene");});
    }

    private void MoveBG1()
    {
        BackGround1.transform.DOMove(new Vector3(BackGround1.transform.position.x, BackGround1.transform.position.y - yOffset, 0), 0.5f)
        .OnComplete(() => {
            if(BackGround1.transform.position.y <= -11f)
            {
                BackGround1.transform.position = new Vector3(0,BackGround2.transform.position.y + 13.75f,0);
                MoveBG1();
            }
            else
            {
                MoveBG1();
            }
        });
    }

    private void MoveBG2()
    {
        BackGround2.transform.DOMove(new Vector3(BackGround2.transform.position.x, BackGround2.transform.position.y - yOffset, 0), 0.5f)
        .OnComplete(() => {
            if(BackGround2.transform.position.y <= -11f)
            {
                BackGround2.transform.position = new Vector3(0,BackGround1.transform.position.y + 13.75f,0);
                MoveBG2();
            }
            else
            {
                MoveBG2();
            }
        });
    }
}
