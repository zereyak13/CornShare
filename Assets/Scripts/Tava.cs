using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tava : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.5f,0,0 * Time.deltaTime,Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Tava sesi çýkar.
        Destroy(gameObject);
    }
}
