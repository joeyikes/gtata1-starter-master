using UnityEngine;

namespace Meshes
{
    public class TorusScript: MonoBehaviour
    {
        private Mesh torusMesh;
        private void Start()
        {
            Surface torus = new Torus();
            torus.SetURange(0,Mathf.PI*2);
            torus.SetVRange(0,Mathf.PI*2);
            torus.SetSubDivisions(30,30);
            torusMesh = MeshGenerator.Instance.Generate(torus,"Torus");
            torusMesh.name = "Torus";
            GetComponent<MeshFilter>().mesh = torusMesh;
        }
    }
}