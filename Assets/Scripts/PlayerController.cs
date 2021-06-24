using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2;

    [SerializeField] private int pixelDistToDetect;

    private Animator playerAnimator;
    private Vector3 startPos;
    private bool mouseDown;
    private bool mouseinstantDown;
    //Smoot Movement
    private bool aPressed;
    private bool dPressed;
    //Mobile Inputs
    private float xAxis;
    private int pixelDistToDetectMob = 20;//serialize to test   
    private Vector2 startPosMob;   
    private bool fingerDown;
    private Camera cam;
    private Vector3 targetPos;
    private float camSpeed = 6f;
    private float swipeTimer;

    Vector3 targetPosA;
    Vector3 targetPosD;
    void Start()
    {
        cam = Camera.main;

        playerAnimator = GetComponent<Animator>();
    }
 
    void Update()
    {
        //Run or idle
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed = 8f;
            playerAnimator.SetBool("isRunning", true);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            speed = 0;
            playerAnimator.SetBool("isRunning", false);
        }


        //SmoothMovementWithKeyboard();

        //MouseSwipeControl();
        //MobileSwipeControl();
        //SmoothMovementCall();

        //MouseTouchControl();
        //MouseSwipeControl();
        //;
        MouseSwipeControl1();// Baþlangýçtan itibaren swipe mekanýzmasý saðlar
        MouseInstantSwipeControl(); // Dash atarak swipe saðlar. 
        

    }

    #region PrefabricMovements
    private void SmoothMovementWithKeyboard()
    {
        Vector3 dir = Vector3.forward * speed;
        //Vectoral movement
        transform.Translate(dir * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetPosA = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
            aPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            targetPosD = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
            dPressed = true;
        }
        SmoothMovementCall();
    }


    #region Mouse Control

    private void MouseInstantSwipeControl()
    {
        Vector3 dir = Vector3.forward * speed;
        //Vectoral movement
        transform.Translate(dir * Time.deltaTime);

        //Mouse swipe 
        if (mouseDown == false && Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            mouseinstantDown = true;
            swipeTimer = 0.05f;
        }
        if (mouseinstantDown)
        {
            //Timer for swipe movement
            swipeTimer -= Time.deltaTime;
            if (swipeTimer <= 0)
            {
                mouseinstantDown = false;
                Debug.Log("test" + mouseDown);

            }
            //Swipe Movements
            if (Input.mousePosition.x >= startPos.x + pixelDistToDetect && transform.position.x < 1f)
            {
                mouseinstantDown = false;
                xAxis = 0.9f;
                Debug.Log("Swipe right");
                dPressed = true;
            }
            else if (Input.mousePosition.x <= startPos.x - pixelDistToDetect && transform.position.x > -1f)
            {
                mouseinstantDown = false;
                Debug.Log("Swipe Left");
                xAxis = -0.9f;
                aPressed = true;

            }
            //MouseDown default false.
            if (Input.GetMouseButtonUp(0))
            {
                mouseinstantDown = false;
            }
        }
        SmoothMovementCall();
    }

    private void MouseSwipeControl1() //Baþlangýç konumundan farklý ise
    {
        Vector3 dir = Vector3.forward * speed;
        //Vectoral movement
        transform.Translate(dir * Time.deltaTime);

        //Mouse swipe 
        if (mouseDown == false && Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            mouseDown = true;
        }
        if (mouseDown)
        {          
            //Swipe Movements
            if(Input.mousePosition.x > startPos.x+1  && transform.position.x < 1f)
            {
                //Debug.Log("dif" + ( Input.mousePosition.x - startPos.x));
                Debug.Log("Swipe right");
                targetPosA = new Vector3(0.99f, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosA, 0.05f);

                startPos = Input.mousePosition;

            }
            else if (Input.mousePosition.x < startPos.x-1 && transform.position.x > -1f)
            {
                //Debug.Log("difeksi" + (Input.mousePosition.x - startPos.x));
                Debug.Log("Swipe left");
                targetPosD = new Vector3(-0.99f, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosD, 0.05f);

                startPos = Input.mousePosition;
            }
            //MouseDown default false.
            if (Input.GetMouseButtonUp(0))
            {
                mouseDown = false;
            }
        }
    }

    private void MouseTouchControl()//Dokunulan konuma gider.
    {
        if (Input.GetMouseButton(0))
        {
            if(transform.position.x <= 1 && transform.position.x >= -1)
            {
                float distance = transform.position.z - Camera.main.transform.position.z;
                targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                targetPos = cam.ScreenToWorldPoint(targetPos);

                Vector3 followXonly = new Vector3(targetPos.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, followXonly, camSpeed * Time.deltaTime);
            }
            if (transform.position.x > 1)
                transform.position = new Vector3(1, transform.position.y, transform.position.z);
            else if (transform.position.x < -1)
                transform.position = new Vector3(-1, transform.position.y, transform.position.z);
        }
    }
    #endregion


    #region MobileControl
    private void MobileSwipeControl()
    {
        Vector3 dir = Vector3.forward * speed;
        //Vectoral movement
        transform.Translate(dir * Time.deltaTime);

        if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            startPosMob = Input.touches[0].position;
            fingerDown = true;
        }
        if (fingerDown)
        {
            //Timer for swipe movement
            swipeTimer -= Time.deltaTime;
            if (swipeTimer <= 0)
            {
                fingerDown = false;
                Debug.Log("test" + mouseDown);

            }
            //finger down default = false
            if (Input.touchCount == 0)
            {
                fingerDown = false;
            }
            else
            {
                if (Input.touches[0].position.x >= startPosMob.x + pixelDistToDetectMob && transform.position.x < 0.51f)
                {
                    Debug.Log("pos" + transform.position.x);
                    fingerDown = false;
                    xAxis = transform.position.x + 0.5f;
                    dPressed = true;
                    Debug.Log("Swipe right");
                    //Sharp
                    //Vector3 vec = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                    //transform.position = vec;

                }
                else if (Input.touches[0].position.x <= startPosMob.x - pixelDistToDetectMob && transform.position.x > -0.51f)
                {
                    fingerDown = false;
                    xAxis = transform.position.x - 0.5f;
                    aPressed = true;
                    Debug.Log("Swipe Left");
                    //Sharp
                    //Vector3 vec = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                    //transform.position = vec;
                }
            }
        }
    }
    private void MobileTouchControl()
    {
        if (Input.touchCount > 0 )
        {
            if (transform.position.x <= 1 && transform.position.x >= -1)
            {
                float distance = transform.position.z - Camera.main.transform.position.z;
                targetPos = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, distance);
                targetPos = cam.ScreenToWorldPoint(targetPos);

                Vector3 followXonly = new Vector3(targetPos.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, followXonly, camSpeed * Time.deltaTime);
            }
            if (transform.position.x > 1)
                transform.position = new Vector3(1, transform.position.y, transform.position.z);
            else if (transform.position.x < -1)
                transform.position = new Vector3(-1, transform.position.y, transform.position.z);
        }
    }

    #endregion
    private void SmoothMovementCall()
    {
        if(aPressed)
        {
            targetPosA = new Vector3(xAxis, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosA, 0.1f);
            if (transform.position.x <= targetPosA.x)
            {
                aPressed = false;
            }
        }
        else if(dPressed )
        {
            targetPosD = new Vector3(xAxis, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosD, 0.1f);
            if (transform.position.x >= targetPosD.x)
                dPressed = false;
        }             
    }

    private void KeyboardControl()
    {        Vector3 dir = Vector3.forward * speed;
        //Vectoral movement
        transform.Translate(dir * Time.deltaTime);

        //Horizontal Movement     
        float inputHor = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3 vec = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            transform.position = vec;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 vec = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            transform.position = vec;
        }
    }

   
    #endregion
}
