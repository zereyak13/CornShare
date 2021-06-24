using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePieces : MonoBehaviour
{
    [SerializeField]private GameObject cornPieceGO;
    [SerializeField] private int foodEffect;


    private void Update()
    {
        transform.Rotate(0, 0.2f, 0 * Time.deltaTime, Space.World);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Call Build Method for all spine parts. 
            BuildEmptyParts(OrderCornPieces.Instance.spine);
            BuildEmptyParts(OrderCornPieces.Instance.spine1);
            BuildEmptyParts(OrderCornPieces.Instance.spine2);
            BuildEmptyParts(OrderCornPieces.Instance.spine3);
            BuildEmptyParts(OrderCornPieces.Instance.spine4);
        }
        //Food alma sesi
        Destroy(gameObject);
        //
    }
    void BuildEmptyParts(Transform[] emptyPosArray)//Boþ Konumlara mýsýr ekle.
    {
        foreach (Transform cornParent in emptyPosArray)
        {
            if (cornParent.gameObject.tag == "Empty" && foodEffect >0)//is empty ?
            {
                //Create cornPiece.
                GameObject curCornPieceGO = Instantiate(cornPieceGO);
                //Set Positions and rotations.
                curCornPieceGO.transform.position = cornParent.position;
                curCornPieceGO.transform.rotation = cornParent.rotation;
                //Set parent object.
                curCornPieceGO.transform.parent = cornParent;
                //Set parent tag Empty or full.
                cornParent.tag = "Full";
                //
                foodEffect--;
            }
        }
    }

    //Patlayan mýsýrlarý ortadan kaldýr...
}
