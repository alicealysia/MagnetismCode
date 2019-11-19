using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GenAtt : MonoBehaviour {
    [HideInInspector]
    public AudioSource ConSound; //Sound the magnets make when created
    public Vector3 StartPos; //position player stars at when dying
    public Transform Att, Att2, Att3; //attractor positions
    public LayerMask MLcast; //objects the mouse can click on
    [HideInInspector]
    public ContactFilter2D OverlapCast; //
    [HideInInspector]
    public AttractorBalance[] BalanceNodes;
    [HideInInspector]
    public GameObject ClingToObj;
    [HideInInspector]
    public Rigidbody2D ThisObj;
    public GameObject[] Hazards;
    private GameObject[] ResHazards;
    private DeviceType DT;
    private GameObject[] Atts = 
        new GameObject[3];
    private int[] CurrentAtt =
        { 0,1,2};
    private Vector2 pos2D;
    bool Tick = false;
    bool DeathTick = false;
    bool CanSloMo = true;
    public bool ResetLevel;
    
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NoSloMo")
        {
            Time.timeScale = 0.75f;
            CanSloMo = false;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "NoSloMo")
        {
            Time.timeScale = 1f;
            CanSloMo = true;
        }
    }
    void Start()
    {
        OverlapCast.layerMask = MLcast;
        DT =
            SystemInfo.deviceType;
        StartPos = transform.position;
        ThisObj = GetComponent<Rigidbody2D>();

        ResHazards = new GameObject[Hazards.Length];
        for (int i = 0; i < Hazards.Length; i++)
        {
            ResHazards[i] = GameObject.Instantiate(Hazards[i]);
        }
        foreach (GameObject RH in ResHazards)
        {
            RH.SetActive(false);
        }
    }
	
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "KillPlayer")
            Die();
        if (other.gameObject.tag == "Checkpoint")
            StartPos = transform.position;
    }

    public void RemPort()
    {
        foreach (GameObject A in Atts)
        {
            Destroy(A);
        }
        Tick = true;
    }

    void Die()
    {
        if (ResetLevel)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
        {
            foreach (GameObject A in Atts)
            {
                Destroy(A);
            }
            Tick = true;
            DeathTick = true;
            transform.position = StartPos;
            ThisObj.velocity = Vector2.zero;
            ThisObj.angularVelocity = 0;
            foreach (GameObject H in Hazards)
            {
                GameObject.Destroy(H);
            }
            for (int i = 0; i < ResHazards.Length; i++)
            {
                Hazards[i] = GameObject.Instantiate(ResHazards[i]);
                Hazards[i].SetActive(true);
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Slow Time") && CanSloMo)
        {
            Time.timeScale = 0.2f;
        }
        if (Input.GetButtonUp("Slow Time") && CanSloMo)
            Time.timeScale = 1;
        pos2D = 
            new Vector2 (transform.position.x, transform.position.y);

        if (DT == DeviceType.Desktop || DT == DeviceType.Unknown)
        {

            if (Tick)
            {
                BalanceNodes = FindObjectsOfType(typeof(AttractorBalance)) as AttractorBalance[];
                foreach (AttractorBalance AB in BalanceNodes)
                {
                    AB.ChangeMags();
                }
                Tick = false;
                if (DeathTick)
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    DeathTick = false;
            }
            if (Input.GetButtonDown("Magnet 1") || Input.GetButtonDown("Magnet 2") || Input.GetButtonDown("Magnet 3"))
            {
                Vector3 _MousePoint = 
                    Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10));
                Vector2 MousePoint = //2D mousepoint in relation to the player 
                    new Vector2 (_MousePoint.x, _MousePoint.y);
                MousePoint = MousePoint - pos2D;
                MousePoint = MousePoint.normalized;
                Collider2D RC = Physics2D.OverlapPoint(_MousePoint, MLcast);
                if (RC != null && RC.gameObject.tag == "Placeable")
                {
                    ClingToObj = RC.gameObject;
                    if (Input.GetButtonDown("Magnet 1"))
                        CreateATT(0, _MousePoint);
                    if (Input.GetButtonDown("Magnet 2"))
                        CreateATT(1, _MousePoint);
                    if (Input.GetButtonDown("Magnet 3"))
                        CreateATT(2, _MousePoint);

                }
                else
                {
                    Collider2D RCircle = Physics2D.OverlapCircle(_MousePoint, 1, MLcast);
                    if (RCircle != null && RCircle.gameObject.tag == "Placeable")
                    {
                        Vector2 NearestPoint = Vector2.zero;
                        float NearestDist = 10;
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                RaycastHit2D RCH =
                                Physics2D.Linecast(_MousePoint, _MousePoint +new Vector3(i, j), MLcast);
                                Debug.DrawLine(_MousePoint, _MousePoint + new Vector3(i, j));
                                Debug.Log("CircleHit " + RCH.collider);
                                if (RCH.collider != null && RCH.collider.gameObject.tag == "Placeable" && RCH.distance < NearestDist)
                                {
                                    ClingToObj = RCH.collider.gameObject;
                                    NearestDist = RCH.distance;
                                    NearestPoint = RCH.point;
                                }
                            }
                        }


                        if (NearestPoint != Vector2.zero)
                        {
                            if (Input.GetButtonDown("Magnet 1"))
                                CreateATT(0, NearestPoint);
                            if (Input.GetButtonDown("Magnet 2"))
                                CreateATT(1, NearestPoint);
                            if (Input.GetButtonDown("Magnet 3"))
                                CreateATT(2, NearestPoint);

                        }


                    }

                    else
                    {
                        RaycastHit2D RCH =
                        Physics2D.Raycast(pos2D, MousePoint, 1000f, MLcast);


                        if (RCH.collider != null && RCH.collider.gameObject.tag == "Placeable")
                        {
                            ClingToObj = RCH.collider.gameObject;
                            if (Input.GetButtonDown("Magnet 1"))
                                CreateATT(0, RCH.point);
                            if (Input.GetButtonDown("Magnet 2"))
                                CreateATT(1, RCH.point);
                            if (Input.GetButtonDown("Magnet 3"))
                                CreateATT(2, RCH.point);
                            

                        }
                    }
                }

                //Other OnClick effects...
            }
            if(Input.GetButtonDown("Remove Magnets"))
            {
                foreach(GameObject A in Atts)
                {
                    Destroy(A);
                }
                Tick = true;
            }
            //Other PC specific code
        }

        else
        {
            if (Tick)
            {
                BalanceNodes = FindObjectsOfType(typeof(AttractorBalance)) as AttractorBalance[];
                foreach (AttractorBalance AB in BalanceNodes)
                {
                    AB.ChangeMags();
                }
                Tick = false;
                Tick = false;
                if (DeathTick)
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                DeathTick = false;

            }
            if (Input.touchCount > 0)
            {
                foreach (Touch Tap in Input.touches)
                {
                    if (Tap.phase == TouchPhase.Ended)
                    {
                        Vector3 _MousePoint =
                        Camera.main.ScreenToWorldPoint(new Vector3(Tap.position.x, Tap.position.y, 0));
                        Vector2 MousePoint =
                            new Vector2(_MousePoint.x, _MousePoint.y);

                        Collider2D RC = Physics2D.OverlapPoint(MousePoint, 1 << 9);
                        if (RC != null && RC.gameObject.tag == "Attractor_tag")
                        {
                            Destroy(RC.gameObject);
                            Tick = true;
                        }
                        else if (RC != null && RC.gameObject.tag == "RemPort")
                        {
                            foreach (GameObject A in Atts)
                            {
                                Destroy(A);
                            }
                            Tick = true;
                        }
                        else
                        {
                            MousePoint = MousePoint - pos2D;
                            MousePoint = MousePoint.normalized;
                            RaycastHit2D RCH =
                                Physics2D.Raycast(pos2D, MousePoint, 50f, MLcast);

                            Collider2D RCX = Physics2D.OverlapPoint(MousePoint, MLcast);
                            if (RCX != null && RCX.gameObject.tag == "Placeable")
                            {
                                if (Atts[0] == null)
                                    CreateATT(0, RCH.point);
                                else if (Atts[1] == null)
                                    CreateATT(1, RCH.point);
                                else if (Atts[2] == null)
                                    CreateATT(2, RCH.point);
                                else
                                    CreateATT(CurrentAtt[0], RCH.point);
                            }

                            else if (RCH.collider != null)
                            {
                                //Play *Shock* animation
                                if (RCH.collider.gameObject.tag == "Placeable")
                                {
                                    if (Atts[0] == null)
                                        CreateATT(0, RCH.point);
                                    else if (Atts[1] == null)
                                        CreateATT(1, RCH.point);
                                    else if (Atts[2] == null)
                                        CreateATT(2, RCH.point);
                                    else
                                        CreateATT(CurrentAtt[0], RCH.point);
                                }
                            }
                        }
                    }
                }
            }
        }
	}

    void CreateATT(int ThisATT, Vector2 Location)
    {
        
        for (int i = 0; i < CurrentAtt.Length -1; i ++)
        {
            if (CurrentAtt[i] == ThisATT)
            {
                CurrentAtt[i] = CurrentAtt[i + 1];
                CurrentAtt[i + 1] = ThisATT;
            }
        }
        if (Atts[ThisATT] != null)
        {
            Atts[ThisATT].transform.position = 
                new Vector3(Location.x, Location.y, 0);
            Atts[ThisATT].transform.SetParent(ClingToObj.transform);
            ConSound = Atts[ThisATT].GetComponent<AudioSource>();
            
        }
        else
        {
            if (DT == DeviceType.Desktop)
            {
                if (ThisATT == 0)
                Atts[ThisATT] =
                (Instantiate(Att, new Vector3(Location.x, Location.y, 0), new Quaternion()) as Transform).gameObject;
                if (ThisATT == 1)
                    Atts[ThisATT] =
                    (Instantiate(Att2, new Vector3(Location.x, Location.y, 0), new Quaternion()) as Transform).gameObject;
                if (ThisATT == 2)
                    Atts[ThisATT] =
                    (Instantiate(Att3, new Vector3(Location.x, Location.y, 0), new Quaternion()) as Transform).gameObject;
                ConSound = Atts[ThisATT].GetComponent<AudioSource>();
            }
            else
            {
                Atts[ThisATT] =
                (Instantiate(Att, new Vector3(Location.x, Location.y, 0), new Quaternion()) as Transform).gameObject;
                ConSound = Atts[ThisATT].GetComponent<AudioSource>();
            }
            Atts[ThisATT].transform.SetParent(ClingToObj.transform);
            Atts[ThisATT].transform.rotation =Quaternion.identity;
        }
        BalanceNodes = FindObjectsOfType(typeof(AttractorBalance)) as AttractorBalance[];
                foreach (AttractorBalance AB in BalanceNodes)
                {
                    AB.ChangeMags();
                }
        ConSound.volume = 0.3f;
        if (Time.timeScale == 0.2f)
            ConSound.pitch = 0.07f + Random.value * 0.03f;
        else
            ConSound.pitch = 0.7f + Random.value * 0.3f;
        ConSound.Play();
    }
}
