using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public static bool isnull;

    public GameObject FirstPauseButton, OptionsFirstButton, OptionsClosedButton, FirstDeathButton, FirstWinButton;

    public GameObject TimerToggle;

    private FMOD.Studio.EventInstance buttonClick;
    
    public GameObject PauseScreen;
    public GameObject OptionsScreen;
    public GameObject WinScreen;
    public GameObject DeathScreen;
    public GameObject Timer;
    public string NextLevel;

    private static Bgm music;

    


    void Awake()
    {
        switch (SceneManager.GetActiveScene().name[1])
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
        if(instance == null)
        {
         
                if (BGMManagerScript.S.GetInstantiatedEventName(BGMManagerScript.S.currentlyPlayingSong) !=
                    BGMManagerScript.S.GetInstantiatedEventName(BGMManagerScript.S.GetSongType(music)))
                {
                    print(BGMManagerScript.S.GetInstantiatedEventName(BGMManagerScript.S.currentlyPlayingSong));
                    print( BGMManagerScript.S.GetInstantiatedEventName(BGMManagerScript.S.GetSongType(music)));
                    print("welp fuck");
                    BGMManagerScript.S.PlayBGM(music);
                    if (BGMManagerScript.S.GetInstantiatedEventName(BGMManagerScript.S.cloudyBGMInstance) !=
                        BGMManagerScript.S.GetInstantiatedEventName(BGMManagerScript.S.GetSongType(music)))
                        BGMManagerScript.S.StopBGM(Bgm.Cloudy);
                }
            

            instance = this;
                DontDestroyOnLoad(this);
            
        }
        else
        {
            instance.PauseScreen = PauseScreen;
            instance.OptionsScreen = OptionsScreen;
            instance.WinScreen = WinScreen;
            instance.DeathScreen = DeathScreen;
            instance.FirstDeathButton = FirstDeathButton;
            instance.FirstPauseButton = FirstPauseButton;
            instance.OptionsFirstButton = OptionsFirstButton;
            instance.OptionsClosedButton = OptionsClosedButton;
            instance.FirstWinButton = FirstWinButton;
            instance.NextLevel = NextLevel;
            Destroy(this);
        }
        LoadSettings();
        ToggleSpeedRunTimer();
        instance.buttonClick = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/MenuClick");
        instance.PauseScreen.SetActive(false);

    }

    string Times(float timeAsFloat)
    {
        int hour = (int) timeAsFloat / 3600;
        int minute = ((int) (timeAsFloat / 60)) % 60;
        int second = ((int) (timeAsFloat)) % 60;
        int centisecond = (int) (timeAsFloat * 100) % 100;
        
        string hourString = hour.ToString();
        string minuteString = minute.ToString();
        string secondString = second.ToString();
        string centisecondString = centisecond.ToString();

        if (hourString.Length <= 1)
        {
            hourString = "0" + hourString;
        }

        if (minuteString.Length <= 1)
        {
            minuteString = "0" + minuteString;
        }

        if (secondString.Length <= 1)
        {
            secondString = "0" + secondString;
        }
        if(centisecondString.Length <= 1)
        {
            centisecondString = "0" + centisecondString;
        }
        
        return hourString+ ":" + minuteString + ":" + secondString + "." + centisecondString;
        
    }

    void Update()
    {
        Timer.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Timer: " + Times(PlayerMovement.Timer);
        if (OptionsScreen.activeSelf)
        {
            Time.timeScale = 0f;
        }
        if (PlayerInputManager.instance.playerInput.actions["Submit"].WasPressedThisFrame() && Time.timeScale == 0f)  
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

    public void TogglePause()
    {
        if(!instance.PauseScreen.activeSelf)
        {
            Time.timeScale = 0;
            instance.PauseScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(instance.FirstPauseButton);

        }else{
            Time.timeScale = 1f;
            instance.PauseScreen.SetActive(false);
        }
    }

    public void QuitToMenu()
    {
        BGMManagerScript.S.currentlyPlayingSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToNextLevel()
    {
        Time.timeScale = 1f;
        //print(instance.NextLevel);
        //print(SceneManager.GetActiveScene().name);
        if (instance.NextLevel[1] != SceneManager.GetActiveScene().name[1])
        {
            BGMManagerScript.S.StopBGM(music);
            
        }
        SceneManager.LoadScene(instance.NextLevel);
    }

    public void ActivateWin()
    {
        if(!WinScreen.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(instance.FirstWinButton);
            WinScreen.SetActive(true);
        }

        if (!SaveGameData.instance.data.MapsCompleted.Contains(instance.NextLevel))
        {
            SaveGameData.instance.data.MapsCompleted.Add(instance.NextLevel);
        }
        SaveGameData.instance.Save();
        SaveGameData.instance.Load();
    }

    public void ToggleOptions()
    {
           
            if(!instance.OptionsScreen.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(instance.OptionsFirstButton);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(instance.OptionsClosedButton);
            }
            instance.OptionsScreen.SetActive(!instance.OptionsScreen.activeSelf);
    }

    public void ActivateDeath()
    {
        if(!instance.DeathScreen.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(instance.FirstDeathButton);
            instance.DeathScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        if (GameObject.FindGameObjectWithTag("Player").TryGetComponent(out PlayerSoundManager sound))
        {
            sound.Thrusting.Stop();
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
