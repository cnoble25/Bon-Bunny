using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.InputSystem.UI;


public class MainMenuManager : MonoBehaviour
{
    public GameObject TitleScreenFirstButton, LevelSelectFirstButton, OptionsFirstButton, creditsFirstButton, creditsClosedButton;

    public GameObject Title, LevelSelect, Options, Timer, TimerToggle, credits;

    public List<GameObject> Maps;

    public static bool canAccess;
    
    private FMOD.Studio.EventInstance buttonClick;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SaveGameData.instance.Load();
        Time.timeScale = 1f;
        buttonClick = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/MenuClick");
        LoadSettings();
        ToggleSpeedRunTimer();
    }

    private void Update()
    {
        if (PlayerInputManager.instance.Restart.WasPressedThisFrame())
        {
            canAccess = true;
            print("canAccess");
        }

        if (PlayerInputManager.instance.playerInput.actions["Submit"].WasPressedThisFrame())  
        {
            buttonClick.start();
        }
    }

    public void ToggleSpeedRunTimer()
    {
        
        bool isOn = TimerToggle.GetComponent<UnityEngine.UI.Toggle>().isOn;
        SaveGameData.instance.data.TimerOn = isOn;
        if (isOn)
        {
            Timer.SetActive(true);
        }
        else
        {
            Timer.SetActive(false);
        }
    }

    public void LoadSettings()
    {
        TimerToggle.GetComponent<UnityEngine.UI.Toggle>().isOn = SaveGameData.instance.data.TimerOn;
    }

    public void ResetTimer()
    {
        PlayerMovement.Timer = 0.0f;
    }

    public void ToggleLevelSelect()
    {
        if(!LevelSelect.activeSelf)
        {
            LevelSelect.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(LevelSelectFirstButton);
        }
        else
        {
            LevelSelect.SetActive(false);
        }
    }

    public void ToggleOptions()
    {
        if(!Options.activeSelf)
        {
            Options.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(OptionsFirstButton);
        }
        else
        {
            Options.SetActive(false);
        }
    }

    public void ToggleCredits()
    {
        if (credits.activeSelf)
        {
            credits.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(creditsClosedButton);
        }
        else
        {
            credits.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(creditsFirstButton);
        }
    }

    public void ToggleTitleScreen()
    {
        if(!Title.activeSelf)
        {
            Title.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(TitleScreenFirstButton);
        }
        else
        {
            Title.SetActive(false);
        }
    }

    public void TurnOnMaps(int SelectedMap)
    {
        foreach(GameObject map in Maps)
        {
            map.SetActive(false);
        }
        Maps[SelectedMap].SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(Maps[SelectedMap].transform.GetChild(1).gameObject);
        LevelSelect.SetActive(false);
    }

    public void LoadMap(string levelName)
    {
        Bgm music;
        switch (levelName[1])
        {
            case '1':
                music = Bgm.Space;
                break;
            case '2':
                music = Bgm.Aurora;
                break;
            case '3':
                music = Bgm.Mesosphere;
                break;
            case '4':
                music = Bgm.Sunset;
                break;
            case '5':
                music = Bgm.Cloudy;
                break;
            case '6':
                music = Bgm.Earth;
                break;
            default:
                music = Bgm.Cloudy;
                break;
        }
        BGMManagerScript.S.PlayBGM(music);
        PlayerMovement.Timer = 0.0f;    
        SceneManager.LoadScene(levelName);

    }

    public void TurnOffMaps()
    {
        foreach(GameObject map in Maps)
        {
            map.SetActive(false);
        }
        LevelSelect.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(LevelSelectFirstButton);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}