using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//I don't even know what's going on in here anymore, there's so, so much shit on this page.

public class SettingsButton : MonoBehaviour
{
    public bool IsPaused = false; //is the game paused?
    public bool IsOptions = false; //is the options menu opened?
    public AudioSource Audible; //music source
    public GameObject OptionsMenu; //Options menu GUI
    public GameObject Menu;
    [HideInInspector]
    public GameObject Player; //Guess what this is?
    public float CtimeScale = 1; //Current timescale, converted tickrate.
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
    public void OpenOptions()
    {
        //Opens Options Menu
        IsOptions = true;
        OptionsMenu.SetActive(true);
        TexSlid.value = TextureQuality;
        TickSlid.value = Tickrate;
        MusSlid.value = MusicVol;
        MasSlid.value = MasterVol;
        BGTog.isOn = PrettyBackgrounds;
        Menu.SetActive(false);
}
    public void CloseOptions()
    {
        IsOptions = false;
        OptionsMenu.SetActive(false);
        Menu.SetActive(true);
    }
    public void Apply()
    {
        QualitySettings.SetQualityLevel(TextureQuality);
        AudioListener.volume = MasterVol / 100;
        Audible.volume = MusicVol / 100;
        if (PrettyBackgrounds)
        {
            PlayerPrefs.SetInt("PrettyBackgrounds", 1);
        }
        else
        {
            PlayerPrefs.SetInt("PrettyBackgrounds", 0);
        }
        Time.fixedDeltaTime = 1/Tickrate;
        Debug.Log(Tickrate);
        
        PlayerPrefs.SetFloat("Tickrate", Tickrate);
        PlayerPrefs.SetInt("TextureQuality", TextureQuality);
        PlayerPrefs.SetFloat("MusicVol", MusicVol);
        PlayerPrefs.SetFloat("MasterVol", MasterVol);
        CloseOptions();
    }
    public void SwitchToLevel(int i)
    {
        SceneManager.LoadScene(i);
    }
}
