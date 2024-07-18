using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    //Coroutine
    private IEnumerator coroutine;
    //FunctionSwitch
    [SerializeField] private int currentFunction = 0;
    //Animation Components
    //public Animator anim;
    [SerializeField]
    private Animator anim;
    private string animationName = "";
    
    //UI GameObjects
    //[SerializeField] 
    private GameObject hello;
    private GameObject startTour;
    private GameObject moveStart;
    private GameObject towardsLRC;
    private GameObject towardsCafe;
    public static PlayerController1 Instance;
    
    //Audio
    public AudioClip audioDIC;
    public AudioClip audioBitwise;
    public AudioClip walking;
    public AudioClip audioLRC;
    public AudioClip audioAskCafe;
    public AudioSource audioSource;
    //private AudioSource audioSource;
     private Camera cam;

    //movement
    public bool movePlayerforward = false, stair = false;//movePlayerbackward = false
    GameObject BitwiseCollider = null;
    GameObject LRCcollider = null;
    public GameObject Player;
    [SerializeField] private GameObject TargetObject;
    private GameObject stairs0 , stairs1 ,stairs2,stairs3 ,stairs4 , stairs5, stairs6, stairs7, stairs8, stairs9;
    [SerializeField] private float playerspeed = 1f;
    private float dist = 0f;
    private float waitTime = 0f;
    public void Awake()
    {
        CreateInstance();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        startTour = GameObject.FindGameObjectWithTag("StartTour");
        moveStart = GameObject.FindGameObjectWithTag("moveStart");
        towardsLRC = GameObject.FindGameObjectWithTag("towardsLRC");
        towardsCafe = GameObject.FindGameObjectWithTag("towardsCafe");
        
        anim = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
        hello = GameObject.FindGameObjectWithTag("Hello");

        //stairs
        // Destination = GameObject.FindGameObjectWithTag("StairCase");
        BitwiseCollider = GameObject.FindGameObjectWithTag("Bitwise");
        LRCcollider = GameObject.FindGameObjectWithTag("LRC");
        stairs0 = GameObject.FindGameObjectWithTag("StairCase");
        stairs1 = GameObject.FindGameObjectWithTag("2");
        stairs2 = GameObject.FindGameObjectWithTag("3");
        stairs3 = GameObject.FindGameObjectWithTag("4");
        stairs4 = GameObject.FindGameObjectWithTag("5");
        stairs5 = GameObject.FindGameObjectWithTag("6");
        stairs6 = GameObject.FindGameObjectWithTag("7");
        stairs7 = GameObject.FindGameObjectWithTag("8");
        stairs8 = GameObject.FindGameObjectWithTag("9");
    }
    public void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        
        // Ensure the Animator component is not null

       /* if (hello == null)
        {
            Debug.LogError("GameObject with tag 'Hello' not found.");
        }

        if (startTour == null)
        {
            Debug.LogError("GameObject with tag 'StartTour' not found.");
        }*/

    }


    // Update is called once per frame
    void Update()
    {


        
        //stopPlayer();

        // Ensure the Animator component is not null
        if (anim == null)
        {
            Debug.LogError("Animator component not found on the GameObject.");
        }

            if(movePlayerforward)
            {
                //transform.position += Vector3.forward * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, TargetObject.transform.position,  Time.deltaTime);

            //transform.position = Vector3.MoveTowards(transform.position, TargetObject.transform.position,playerspeed*Time.deltaTime );
        }
        /*    if (movePlayerbackward)
            {
                transform.position += Vector3.back * Time.deltaTime;
                //transform.position = Vector3.MoveTowards(transform.position, TargetObject.transform.position,playerspeed*Time.deltaTime );
            }*/
        if (stair)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetObject.transform.position, playerspeed * Time.deltaTime);
        }

    }
    void IdleAnim()
    {
        animationName = "Idle";
        PlayAnimation(animationName);
        audioSource.Pause();
    }
    void SkipData()
    {
        currentFunction = 0;
        // StopCoroutine(coroutine);
        StopAllCoroutines();
        IdleAnim();
    }
    public void SkipTo()
    {
        if(currentFunction == 1)
        {
            currentFunction = 0;
            StopCoroutine(coroutine);
            IdleAnim();
            moveUI();
        }
        else if(currentFunction == 2)
        {
            currentFunction = 0;
            StopCoroutine(coroutine);
            IdleAnim();
            LRCUI();
        }
        else if ( currentFunction == 3)
        {
            askCafe();
            // StopCoroutine(coroutine);
            StopAllCoroutines();
        }
        else if (currentFunction == 4)
        {
            SkipData();
            towardsCafe.SetActive(true);
        }
    }
    public void HiAnim()
    {
        startTour.SetActive(false);
        moveStart.SetActive(false);
        towardsLRC.SetActive(false);
        towardsCafe.SetActive(false);
        animationName = "Hello";
        Debug.Log("controll here");
        PlayAnimation(animationName);
        waitTime = 6f;
        hello.SetActive(false);
        StartCoroutine(waitAndSwitch(6));
       
    }
    IEnumerator waitAndSwitch(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        startTour.SetActive(true);
     
    }

    public void startTourAnim()
    {
        currentFunction = 1;
        animationName = "Talk";
        PlayAnimationAudio(animationName, audioDIC);
        startTour.SetActive(false);
        coroutine = moveFirst(47f);
        StartCoroutine(coroutine);
    }
    IEnumerator moveFirst(float delay) {
        yield return new WaitForSeconds(delay);
        moveUI();
        animationName = "Idle";
        PlayAnimation(animationName);
    }
    void moveUI()
    {
        moveStart.SetActive(true);
    }
    public void StartMovement()
    {
        transform.Rotate(0, 93, 0);
        TargetObject = BitwiseCollider;
        movePlayerforward = true;
        audioSource.loop = true;
        animationName = "Walk";
        PlayAnimationAudio(animationName, walking);
        moveStart.SetActive(false);
    }
    void PlayAnimation(string animationName)
    {
        // Check if the Animator component is not null
        if (anim != null)
        {
            // Trigger the specified animation using a trigger parameter
            // anim.SetTrigger(animationName);
            anim.Play(animationName);

        }
        else
        {
            Debug.LogError("Animator component is null. Make sure it is attached to the GameObject.");
        }
    }
    void PlayAnimationAudio(string animationName, AudioClip audioClip)
    {
        // Check if the Animator component is not null
        if (anim != null && audioClip != null)
        {
            // Trigger the specified animation using a trigger parameter
            // anim.SetTrigger(animationName);
            anim.Play(animationName);
            audioSource.clip = audioClip;
            audioSource.Play();

        }
        else
        {
            Debug.LogError("Animator component is null. Make sure it is attached to the GameObject.");
        }
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Bitwise"))
        {
            transform.Rotate(0, -180, 0);
            movePlayerforward = false;
            audioSource.loop = false;
            other.enabled = false;
            TalkBitwise();
        }
        else if (other.CompareTag("LRC"))
        {
            movePlayerforward = false;
            audioSource.loop = false;
            TalkLRC();
            other.enabled = false;
        }
        // Movement onstairs
        else if (other.CompareTag("StairCase") && stair == true)
        {
            transform.Rotate(0, 90, 0);
            other.enabled = false;
            TargetObject = stairs1;
            playerspeed = .5f;
        }
        else if (other.CompareTag("2") && stair==true)
        {
            transform.Rotate(0, -90, 0);
            TargetObject = stairs2;
            other.enabled = false;
            
        }
        else if (other.CompareTag("3") && stair == true)
        {
            transform.Rotate(0, -90, 0);
            TargetObject = stairs3;
            other.enabled = false;
            
        }
        else if (other.CompareTag("4") && stair == true)
        {
            transform.Rotate(0, -90, 0);
            other.enabled = false;
            TargetObject = stairs4;
        }
        else if (other.CompareTag("5") && stair == true)
        {
            transform.Rotate(0, -90, 0);
            TargetObject = stairs5;
            other.enabled = false;
        }
        else if (other.CompareTag("6") && stair == true)
        {
            TargetObject = stairs6;
            other.enabled = false;
        }
        else if (other.CompareTag("7") && stair == true)
        {
            TargetObject = stairs7;
            other.enabled = false;
        }
        else if (other.CompareTag("8") && stair == true)
        {
            TargetObject = stairs8;
            other.enabled = false;
        }
        else if(other.CompareTag("9") && stair == true)
        {
            animationName = "Idle";
            PlayAnimation(animationName);
            audioSource.loop = false;
            stair = false;
        }



    }
    public void TalkBitwise()
    {
        currentFunction = 2;
       
        Debug.Log("transform.Rotate(0, -180, 0)");
        animationName = "Talk";
        PlayAnimationAudio(animationName, audioBitwise);
        coroutine = movetowardsLRC(24.5f);
        StartCoroutine(coroutine);   
    }
    IEnumerator movetowardsLRC(float delay)
    {
        yield return new WaitForSeconds(delay);
        LRCUI();
        IdleAnim();
    }
    void LRCUI()
    {
        towardsLRC.SetActive(true);
    }
    public void moveLRC()
    {
        transform.Rotate(0, 180, 0);
        TargetObject = LRCcollider;
        movePlayerforward= true;
        animationName = "Walk";
        PlayAnimationAudio(animationName, walking);
        towardsLRC.SetActive(false);
        
    }
    public void TalkLRC()
    {
        currentFunction = 3;
        transform.Rotate(0, 180, 0);
        animationName = "Talk";
        PlayAnimationAudio(animationName, audioLRC);
        coroutine = askToMoveToCafe(22f);
        StartCoroutine(coroutine);
    }
    IEnumerator askToMoveToCafe(float delay)
    {
        yield return new WaitForSeconds(delay);
        askCafe();
    }
    void askCafe()
    {
        currentFunction = 4;
        animationName = "Talk";
        PlayAnimationAudio(animationName, audioAskCafe);
        coroutine = moveToCafe(18f);
        StartCoroutine(coroutine);
    }
    IEnumerator moveToCafe(float delay)
    {
        yield return new WaitForSeconds(delay);
        //towardsCafe.SetActive(true);
        CafeUI();
        animationName = "Idle";
        PlayAnimation(animationName);
    }
    void CafeUI()
    {
        towardsCafe.SetActive(true);
    }
    public void moveCafe()
    {
        //movePlayerbackward= true;
        stair = true;
        TargetObject = stairs0;
        audioSource.loop = true;
        animationName = "Walk";
        PlayAnimationAudio(animationName, walking);
        towardsCafe.SetActive(false);
       // Destination=
    }
    /*void stopPlayer()
    {
        cam = Camera.main;
        dist = Vector3.Distance(Player.transform.position, cam.transform.position);
        if (dist >= 3f && movePlayer == true)
        {
            transform.Rotate(0, 180, 0);
            movePlayer = false;
            animationName = "Idle";
            PlayAnimation(animationName);
        }
        else if (dist<3f && movePlayer == false) 
        {
            transform.Rotate(0, -180, 0);
            movePlayer = true;
            animationName = "Walk";
            PlayAnimation(animationName);
        }
    }*/
}
