using UnityEngine;

namespace Meshes
{
    public class Torus: Surface
    {
        private Mesh torusMesh;
        private void Start()
        {
            
            SetURange(0,Mathf.PI*2);
            SetVRange(0,Mathf.PI*2);
            SetSubDivisions(30,30);
            torusMesh = MeshGenerator.Instance.Generate(this,"Torus");
            torusMesh.name = "Torus";
            GetComponent<MeshFilter>().mesh = torusMesh;
        }
        public override Vector3 GetParamFunc(float u, float v)
        {
            //var vertex = new Vector3(3*Mathf.Cos(newu)+Mathf.Cos(newu)*Mathf.Cos(newv),3*Mathf.Sin(newu)+Mathf.Sin(newu)*Mathf.Cos(newv),Mathf.Sin(newv));
            var vx = 3*Mathf.Cos(u)+Mathf.Cos(u)*Mathf.Cos(v);
            var vy = 3*Mathf.Sin(u)+Mathf.Sin(u)*Mathf.Cos(v);
            var vz = Mathf.Sin(v);
            var vertex = new Vector3(vx,vy,vz);
            return vertex;
        }
    }
}