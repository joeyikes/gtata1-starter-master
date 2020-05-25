using System;
using UnityEngine;

namespace Meshes
{
    public class FlowerScript : MonoBehaviour
    {
        private Mesh flowerMesh;
        private void Start()
        {
            Surface flower = new Flower();
            flower.SetURange(0,Mathf.PI*2);
            flower.SetVRange(0,Mathf.PI);
            flower.SetSubDivisions(100,100);
            flowerMesh = MeshGenerator.Instance.Generate(flower,"Flower");
            flowerMesh.name = "Flower";
            GetComponent<MeshFilter>().mesh = flowerMesh;
        }
    }
}