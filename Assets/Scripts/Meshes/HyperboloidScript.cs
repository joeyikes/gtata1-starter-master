using System.Collections;
using System.Collections.Generic;
using Meshes;
using UnityEngine;

public class HyperboloidScript : MonoBehaviour
{
    private Mesh hyperboloidMesh;
    // Start is called before the first frame update
    void Start()
    {
        Surface hyper = new Hyperboloid();
        hyper.SetURange((Mathf.PI*2)*-1,Mathf.PI*2);
        hyper.SetVRange((Mathf.PI*2)*-1,Mathf.PI*2);
        hyper.SetSubDivisions(30,15);
        hyperboloidMesh = MeshGenerator.Instance.Generate(hyper,"Hyperboloid");
        hyperboloidMesh.name = "Hyperboloid";
        GetComponent<MeshFilter>().mesh = hyperboloidMesh;
    }
}
