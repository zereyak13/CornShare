using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FinalObject : MonoBehaviour
{
    public int score;
    public bool inFinalZone;

    //Cam Handle
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private Transform vCamTargetPos;
    //Final Corn Handle
    [SerializeField] private Transform instantiatePos;
    [SerializeField] private GameObject cornPiece;
    
    private PlayerController playerController;
    private Animator playerAnimator;
    private float timer;
    private int index;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        timer = 0.2f;
    }
    void Update()
    {
        if(inFinalZone && playerController.speed != 0)
        {
            playerController.speed = 0;          
        }
        if (inFinalZone)
        {
            CreateCornPiecesOnMachine();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            CalculateCornPieceOnSpine(OrderCornPieces.Instance.spine4);
            CalculateCornPieceOnSpine(OrderCornPieces.Instance.spine);
            CalculateCornPieceOnSpine(OrderCornPieces.Instance.spine1);
            CalculateCornPieceOnSpine(OrderCornPieces.Instance.spine2);
            CalculateCornPieceOnSpine(OrderCornPieces.Instance.spine3);
            Debug.Log(score);
      
            playerController.speed = 0;
            inFinalZone = true;
            playerAnimator.SetBool("isRunning", false);

            HandleCamMovement();
            //Next leveli animastondan sonra ortaya çýkar;
            //SceneManagement.Instance.LoadNextNevel();
        }
    }

    private void CalculateCornPieceOnSpine(Transform[] spine)
    {
        foreach (Transform cornParent in spine)
        {
            if (cornParent.transform.gameObject.tag != "Empty")//if there is corn part as a child.
            {
                score++;
            }
        }
    }

    private void CreateCornPiecesOnMachine()
    {
        timer -= Time.deltaTime;

        if (timer < 0 && index<score)
        {
            for(int i =0; i < 4; i++)
            {
                if(index < score)
                {
                    Vector3 offset = new Vector3(i/6, 0, 0);
                    GameObject cornPieceGO = Instantiate(cornPiece , instantiatePos.position + offset, Quaternion.identity);
                    //cornPieceGO.AddComponent<Rigidbody>();
                    cornPieceGO.GetComponent<Rigidbody>().mass += 10f;
                    cornPieceGO.GetComponent<Rigidbody>().useGravity = true;
                    cornPieceGO.GetComponent<BoxCollider>().isTrigger = false;
                    index++;
                }               
            }
            timer = 0.1f;
        }

    }

    private void HandleCamMovement()
    {
        vCam.Follow = vCamTargetPos;
        vCam.LookAt = vCamTargetPos;
        vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 2.5f, -10f);
        vCam.GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset = new Vector3(0, -1, 0);
        //2.50f, 15.5
    }


}
