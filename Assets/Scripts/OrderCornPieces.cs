using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCornPieces : MonoBehaviour
{
    public static OrderCornPieces Instance;

    public Transform[] spine;
    public Transform[] spine1;
    public Transform[] spine2;
    public Transform[] spine3;
    public Transform[] spine4;

    [SerializeField] private GameObject cornPieceGO;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Initially build corn pieces on the Corn.
        SetCornPieces();
    }

    void SetCornPieces()
    {
        //Use CallSetCornPiece for all Spine Positions 
        CallSetCornPieces(spine);
        CallSetCornPieces(spine1);
        CallSetCornPieces(spine2);
        CallSetCornPieces(spine3);
        CallSetCornPieces(spine4);
    }
    void CallSetCornPieces(Transform[] cornPiecePos)
    {
        foreach (Transform cornPieceTF in cornPiecePos)
        {
            //Create new corn piece 
            GameObject curCornPieceGO = Instantiate(cornPieceGO);
            //Set Pos ant Rot
            curCornPieceGO.transform.position = cornPieceTF.position;
            curCornPieceGO.transform.rotation = cornPieceTF.rotation;

            //Set Parent
            curCornPieceGO.transform.parent = cornPieceTF;

        }
    }
}
