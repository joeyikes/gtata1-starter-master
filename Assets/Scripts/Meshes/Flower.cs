using System;
using UnityEngine;

namespace Meshes
{
    public class Flower: Surface
    {
        private Mesh flowerMesh;
        private void Start()
        {
            SetURange(0,Mathf.PI*2);
            SetVRange(0,Mathf.PI);
            SetSubDivisions(100,100);
            flowerMesh = MeshGenerator.Instance.Generate(this,"Flower");
            flowerMesh.name = "Flower";
            GetComponent<MeshFilter>().mesh = flowerMesh;
        }

        public override Vector3 GetParamFunc(float u, float v)
        {
            var r = 2 + Mathf.Sin(7 * u + 5 * v);
            var vx = r * Mathf.Cos(u) * Mathf.Sin(v);
            var vz = r * Mathf.Sin(u) * Mathf.Sin(v);
            var vy = r * Mathf.Cos(v);
            var vertex = new Vector3(vx,vy,vz);
            return vertex;
        }
    }
}