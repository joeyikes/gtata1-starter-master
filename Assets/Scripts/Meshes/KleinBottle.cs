using System;
using UnityEngine;

namespace Meshes
{
    public class KleinBottle: Surface
    {
        private Mesh kleinMesh;
        private void Start()
        {
            
            SetURange(0,Mathf.PI*2);
            SetVRange(0,Mathf.PI*2);
            SetSubDivisions(30,30);
            kleinMesh = MeshGenerator.Instance.Generate(this,"KleinBottle");
            kleinMesh.name = "Hyperboloid";
            GetComponent<MeshFilter>().mesh = kleinMesh;
        }

        public override Vector3 GetParamFunc(float u, float v)
        {
            var r = 4 - 2 * Mathf.Cos(u);
            var vx = 0f;
            var vy = 0f;
            if (u > Math.PI)
            {
                vx  = -4 * Mathf.Cos(u) * (1 + Mathf.Sin(u)) + r * Mathf.Cos(v);
                vy = -14 * Mathf.Sin(u);
                SetWindingOrder(true);
            }
            else
            {
                vx  = -4 * Mathf.Cos(u) * (1 + Mathf.Sin(u)) - r * Mathf.Cos(u) * Mathf.Cos(v);
                vy = -14 * Mathf.Sin(u) - r * Mathf.Sin(u) * Mathf.Cos(v);
                SetWindingOrder(false);
            }

            var vz = r * Mathf.Sin(v);
            var vertex = new Vector3(vx,vy,vz);
            return vertex;
        }
    }
}