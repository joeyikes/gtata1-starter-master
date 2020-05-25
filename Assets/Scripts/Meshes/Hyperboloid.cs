using UnityEngine;

namespace Meshes
{
    public class Hyperboloid: Surface
    {
        private Mesh hyperboloidMesh;
        private void Start()
        {
            
            SetURange((Mathf.PI*2)*-1,Mathf.PI*2);
            SetVRange((Mathf.PI*2)*-1,Mathf.PI*2);
            SetSubDivisions(30,15);
            hyperboloidMesh = MeshGenerator.Instance.Generate(this,"Hyperboloid");
            hyperboloidMesh.name = "Hyperboloid";
            GetComponent<MeshFilter>().mesh = hyperboloidMesh;
        }
        
        public override Vector3 GetParamFunc(float u, float v)
        {
            var vx = Mathf.Sqrt(Mathf.PI + (v*v))*Mathf.Cos(u);
            var vz = Mathf.Sqrt(Mathf.PI + (v*v)) * Mathf.Sin(u);
            var vy = v;
            var vertex = new Vector3(vx,vy,vz);
            return vertex;
        }
    }
}