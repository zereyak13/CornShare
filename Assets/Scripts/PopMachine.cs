using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopMachine : MonoBehaviour
{
    [SerializeField] GameObject finalObjectGO;
    [SerializeField] float popPower;

    private FinalObject finalObjectSC;
    private List<GameObject> cornPiecesforPopGO = new List<GameObject>();
    private float timer;
    private int index;
    private void Awake()
    {
        finalObjectSC = finalObjectGO.GetComponent<FinalObject>();
    }
    void Start()
    {
        timer = 0.3f;
    }

    void Update()
    {

        // Titreme eklenebilir 

        //Debug.Log(finalObjectSC.score+ "  score");
        //Debug.Log(index + "  index");
        //Debug.Log(cornPiecesforPopGO.Count + "   listcount");
        //if (index == finalObjectSC.score && cornPiecesforPopGO.Count >0)
        //{
        //    timer -= Time.deltaTime;
        //    if (timer < 0)
        //    {
        //        for (int i = 0; i < 4; i++)
        //        {
        //            if (cornPiecesforPopGO.Count > 0)
        //            {
        //                cornPiecesforPopGO[i].gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 1f), 1, Random.Range(-1, 1f)) * popPower * 10);
        //                cornPiecesforPopGO.RemoveAt(i);
        //            }
        //        }
        //        timer = 0.1f;
        //    }
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        cornPiecesforPopGO.Add( collision.gameObject);
        if(collision.gameObject.GetComponent<CornPiece>() != null)
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 1f), 1, Random.Range(-1, 1f)) * popPower, ForceMode.Impulse);
            // collision.gameObject.GetComponent<Rigidbody>().AddForce( new Vector3(Random.Range(0, 1f), 1, Random.Range(0, 1f)) * popPower);*    // Titreme versin
        }
    }
}
