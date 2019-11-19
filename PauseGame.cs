using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//I don't even know what's going on in here anymore, there's so, so much shit on this page.

public class PauseGame : MonoBehaviour
{
    public bool IsPaused = false; //is the game paused?
    public bool IsOptions = false; //is the options menu opened?
    public AudioSource Audible; //music source
    public GameObject BGcamera; //background camera
    public GameObject BGimage; //image that is substituted for the background camera. Should I disable the background objects as well? maybe...
    public GameObject PauseMenu; //Pause menu GUI
    public GameObject OptionsMenu; //Options menu GUI
    [HideInInspector]
    public CanvasGroup PauseAlpha; //Changing the .alpha changes the pause menu alpha
    [HideInInspector]
    public GameObject Player; //Guess what this is?
    public float CtimeScale = 1; //Current timescale, converted tickrate.
    [HideInInspector]
    public AudioListener Audacity;
    [HideInInspector]
    public int TextureQuality = 0; //overall game quality
    [HideInInspector]
    public float MusicVol = 1;
    [HideInInspector]
    public float MasterVol = 1;
    [HideInInspector]
    public float Tickrate = 60;
    [HideInInspector]
    public bool PrettyBackgrounds = false;
    public Slider TexSlid;
    public Slider TickSlid;
    public Slider MusSlid;
    public Slider MasSlid;
    public Toggle BGTog;

    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PauseAlpha = GetComponentInChildren<CanvasGroup>();
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        if (PlayerPrefs.GetInt("FirstPlay",0) != 1)
        {
            int QualityLvl = QualitySettings.GetQualityLevel();
            TextureQuality = QualityLvl;
            Tickrate = 250 * (QualityLvl + 1);
            MusicVol = 50;
            MasterVol = 50;
            if (QualityLvl > 1)
            {
                PrettyBackgrounds = true;
                PlayerPrefs.SetInt("PrettyBackgrounds", 1);
            }
            PlayerPrefs.SetFloat("Tickrate", Tickrate);
            PlayerPrefs.SetInt("TextureQuality", QualityLvl);
            PlayerPrefs.SetFloat("MusicVol", MusicVol);
            PlayerPrefs.SetFloat("MasterVol", MasterVol);
            PlayerPrefs.SetInt("FirstPlay", 1);
        }
        else
        {
            Tickrate = PlayerPrefs.GetFloat("Tickrate",60);
            TextureQuality = PlayerPrefs.GetInt("TextureQuality",1);
            MusicVol = PlayerPrefs.GetFloat("MusicVol",50);
            MasterVol = PlayerPrefs.GetFloat("MasterVol",50);
            int PBG = PlayerPrefs.GetInt("PrettyBackgrounds", 0);
            if (PBG == 1)
                PrettyBackgrounds = true;
            else
                PrettyBackgrounds = false;
        }
    }
    public void AdjustTQ(float TQ)
    {
        TextureQuality = (int)TQ;
    }
    public void AdjustMusV(float MV)
    {
        MusicVol = MV;
    }
    public void AdjustMasV(float TQ)
    {
        MusicVol = TQ;
    }
    public void AdjustPB(bool PB)
    {
        PrettyBackgrounds = PB;
    }
    public void AdjustTick(float tick)
    {
        Tickrate = tick;
    }
    void Update ()
    {
        if (PauseMenu.activeSelf && PauseAlpha.alpha < 1)
        {
            PauseAlpha.alpha += 0.1f;
        }

        if (Input.GetButtonDown("Pause"))
        {
            if (IsPaused)
                Continue();
            else
                OpenPause();
        }
        if (Input.GetButtonDown("Slow Time") && CtimeScale == 1)
        {
            CtimeScale = 0.2f;
        }
        else if (Input.GetButtonUp("Slow Time") && CtimeScale == 0.2f)
        {
            CtimeScale = 1f;
        }
        
	}
    public void OpenPause()
    {
        //open Pause Menu
        PauseMenu.SetActive(true);
        IsPaused = true;
        if (Player != null)
            Player.SetActive(false);

    }
    public void Continue()
    {
        //close Pause Menu
        PauseMenu.SetActive(false);
        IsPaused = false;
        PauseAlpha.alpha = 0;
        if (Player != null)
            Player.SetActive(true);
    }
    public void Restart()
    {
        IsPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Player.SetActive(true);

    }
    public void Map()
    {
        IsPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Level select");
        Player.SetActive(true);

    }
    public void OpenOptions()
    {
        //Opens Options Menu
        IsOptions = true;
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(true);
        TexSlid.value = TextureQuality;
        TickSlid.value = Tickrate;
        MusSlid.value = MusicVol;
        MasSlid.value = MasterVol;
        BGTog.isOn = PrettyBackgrounds;
}
    public void CloseOptions()
    {
        IsOptions = false;
        PauseMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }
    public void Apply()
    {
       
        QualitySettings.SetQualityLevel(TextureQuality);
        AudioListener.volume = MasterVol / 100;
        Audible.volume = MusicVol / 100;
        if (PrettyBackgrounds)
        {
            if (BGcamera != null)
                BGcamera.SetActive(true);
            BGimage.SetActive(false);
            PlayerPrefs.SetInt("PrettyBackgrounds", 1);
        }
        else
        {
            if (BGcamera != null)
                BGcamera.SetActive(false);
            BGimage.SetActive(true);
            PlayerPrefs.SetInt("PrettyBackgrounds", 0);
        }
        Time.fixedDeltaTime = 1/Tickrate;
        Debug.Log(Tickrate);
        CloseOptions();
        PlayerPrefs.SetFloat("Tickrate", Tickrate);
        PlayerPrefs.SetInt("TextureQuality", TextureQuality);
        PlayerPrefs.SetFloat("MusicVol", MusicVol);
        PlayerPrefs.SetFloat("MasterVol", MasterVol);
    }
}
