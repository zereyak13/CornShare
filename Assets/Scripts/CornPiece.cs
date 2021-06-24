using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornPiece : MonoBehaviour
{
    public Mesh[] poppedCornMeshes;
    public Material poppedCornMaterial;

    public void ChangeObjectPrefab()
    {
        StartCoroutine(DelayForPop());
    }

    public void DestroyPoppedCornGO()
    {
        StartCoroutine(DelayForDestroy());
    }
    IEnumerator DelayForDestroy()
    {
        yield return new WaitForSeconds(2.9f);
        Destroy(gameObject);
    }
    IEnumerator DelayForPop()
    {
        yield return new WaitForSeconds(0.24f);

        Mesh selectedMesh = poppedCornMeshes[Random.Range(0, poppedCornMeshes.Length)];

        gameObject.GetComponent<MeshFilter>().sharedMesh = selectedMesh;
        gameObject.GetComponent<MeshRenderer>().material = poppedCornMaterial;
    }
}
