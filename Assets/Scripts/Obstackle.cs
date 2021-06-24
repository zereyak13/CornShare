using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstackle : MonoBehaviour
{
    [SerializeField] private int obstackleEffect;

    private GameObject poppedCornGO;
    private float minDistance = 100f;
    private float distance;
    private CornPiece cornPiece;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            for(int i =0; i<obstackleEffect; i++)
            {
                poppedCornGO = null;
                //Set min distance as 100 at the beginnig of every calculation
                minDistance = 100f;
                //Call Calculate Distance for all Spine parts
                CalculateDistance(OrderCornPieces.Instance.spine4);
                CalculateDistance(OrderCornPieces.Instance.spine);
                CalculateDistance(OrderCornPieces.Instance.spine1);
                CalculateDistance(OrderCornPieces.Instance.spine2);
                CalculateDistance(OrderCornPieces.Instance.spine3);


                //if there is no corn to pop Game Over
                if (poppedCornGO == null) //GAME OVER
                {
                    //Debug.Log("No more piece");
                    SceneManagement.Instance.GameOver();
                }


                //Pop selected cornPiece
                if (poppedCornGO != null) 
                {
                    //Pop the selected cornGO
                    PopCorn.Instance.PoppedCorn(poppedCornGO);

                    //Set corn mesh as Popped one
                    cornPiece = poppedCornGO.GetComponent<CornPiece>();
                    cornPiece.ChangeObjectPrefab();
                    //Destroy poppedCorns
                    cornPiece.DestroyPoppedCornGO();
                }
            }       
        }
    }
    public void CalculateDistance(Transform [] emptyPosArray)//Find The closest corn part to he obstackle
    {
        foreach (Transform cornParent in emptyPosArray)
        {
            if(cornParent.transform.gameObject.tag != "Empty")//if there is corn part as a child.
            {
                //Debug.Log(cornParent.tag);
                distance = (cornParent.transform.position - gameObject.transform.position).magnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    poppedCornGO = cornParent.transform.GetChild(0).gameObject;//Closest cornPiece to POP 
                }
            }
        }
    }
}
