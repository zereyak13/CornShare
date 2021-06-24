using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopCorn : MonoBehaviour
{
    public static PopCorn Instance;

    [SerializeField] private float explosionPower;

    private List<GameObject> poppedCorns = new List<GameObject>();
    private void Awake()
    {
       
        Instance = this;
    }

    private void Update()
    {
        RotatePoppedCorns();
        //if(poppedCorns.Count >0)
        //{
        //    RotatePoppedCorns();
        //}
    }
    public void PoppedCorn(GameObject poppedCornGO)//Pop the corn
    {
        //Debug.Log("PopedCorn  " + poppedCornGO.tag);
        
        //Set parent tag as Empty
        poppedCornGO.transform.parent.tag = "Empty";
        //No more parent
        poppedCornGO.transform.parent = null;
        //Gravity on for flying
        poppedCornGO.GetComponent<Rigidbody>().useGravity = true;
        ////pop to the Camera
        poppedCornGO.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(1f,1.2f), Random.Range(1.2f, 2.7f)) * explosionPower);
        //poppedCornGO.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.8f, 0.8f), Random.Range(1f, 1.2f), Random.Range(1f, 2.5f)) * explosionPower);
        //poppedCornGO.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.4f, 0.4f), 1.1f, Random.Range(2.3f, 2.7f)) * explosionPower);

        //Rotate Popped Corn
        poppedCorns.Add(poppedCornGO);
        //Remove popped Corns
        StartCoroutine(RemoveElementFromList());
        //pop to the piece.UP
        //poppedCornGO.GetComponent<Rigidbody>().AddForce((poppedCornGO.transform.up + new Vector3(0,1,Random.Range(0.2f,1))).normalized* explosionPower);

    }

    private void RotatePoppedCorns()//Rotate all popped Corns
    {
        if(poppedCorns.Count > 0)
        {
            foreach (GameObject cornGo in poppedCorns)
            {             
                if (cornGo != null) // The object of type 'GameObject' has been destroyed but you are still trying to access it.
                {
                    cornGo.transform.Rotate(1f, 1.2f, 4f * Time.deltaTime * 30, Space.Self);
                }
            }
        }             
    } 

    IEnumerator RemoveElementFromList()//
    {
       
        yield return new WaitForSeconds(3f);
        for(int i = 0; i< poppedCorns.Count; i++)
        {
            if (poppedCorns[i] == null)
            {
                poppedCorns.RemoveAt(i);
            }
        }
        //Debug.Log(poppedCorns.Count);
    }
  
}
