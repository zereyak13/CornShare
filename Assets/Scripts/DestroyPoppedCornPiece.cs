using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPoppedCornPiece : MonoBehaviour
{
    public void DestroyPoppedCornGO()
    {
        StartCoroutine(AddDelay());
    }
    IEnumerator AddDelay()
    {
        yield return new WaitForSeconds(2.9f);
        Destroy(gameObject);
    }
}
