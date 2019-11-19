using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SwitchStage : MonoBehaviour {
    public int ThisLvl;
    private int CurrentLevel;
    private int CurrentLevels;
    private int LVLreq;
    public bool SwitchCond;
    public int levelRequirement;
    public 
	// Use this for initialization
    void Start()
    {
        CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        for (int i = 0; i <= GameObject.FindObjectsOfType<SwitchStage>().Length + 1; i ++)
        {
            LVLreq += PlayerPrefs.GetInt(i.ToString());
        }
    }
	void OnTriggerEnter2D(Collider2D Col2d)
    {
        if (SwitchCond && Col2d.gameObject.tag == "Contravert")
        {
            PlayerPrefs.SetInt(CurrentLevel.ToString(), 1);
            SceneManager.LoadScene("Level select");

        }
        Debug.Log(Col2d);
    }
    void OnMouseDown()
    {
        Debug.Log(LVLreq);
        if (!SwitchCond && LVLreq >= levelRequirement)
            SceneManager.LoadScene(ThisLvl +1);
    }
    void Update()
    {
        if (!SwitchCond)
        {
            if (LVLreq < levelRequirement)
                GetComponent<SpriteRenderer>().color = Color.clear;
            else if (PlayerPrefs.GetInt(ThisLvl.ToString()) == 0)
                GetComponent<SpriteRenderer>().color = Color.white;
            else
                GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            transform.Rotate(Vector3.forward, -360 * Time.deltaTime);
        }
    }
}
