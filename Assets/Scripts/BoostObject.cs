using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostObject : MonoBehaviour
{
    private PlayerController playerController;
    private float boostTime =3f;
    private bool haveBoost;
    private float maxBoostTime = 3f;
    private float timer;
    private int boostStack;
    void Start()
    {       
        playerController = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") )
        {
            StartCoroutine(BoostTimer());

            //HAVEbOOST playercontrollerda alýnmalý global olmalý.

            //playerController.speed = 10f;
            //haveBoost = true;
            //boostStack++;
        }
    }
    private void Update()
    {
        //if (haveBoost)
        //{          
        //    timer += Time.deltaTime;
        //    if (timer >= maxBoostTime)
        //    {
        //        playerController.speed = 6f;
        //        Destroy(gameObject);
        //    }

        //    if (boostStack > 1)
        //    {
        //        maxBoostTime += 3;
        //        boostStack--;
        //    }
        //}

    }
    IEnumerator BoostTimer()
    {
        haveBoost = true;
        playerController.speed = 12f;
        foreach (Transform t in gameObject.transform)
        {
            t.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(boostTime);
        playerController.speed = 8f;
        Destroy(gameObject);
    }

}
