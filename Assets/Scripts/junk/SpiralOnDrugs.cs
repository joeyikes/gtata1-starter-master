using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralOnDrugs : MonoBehaviour
{
   private Mesh generatedMesh;
    // Start is called before the first frame update
    void Start()
    {
        generatedMesh = new Mesh();
        var subdivisions = new Vector2Int(50,50);
        var vertexSize = subdivisions + new Vector2Int(1, 1);
        
        var vertices = new Vector3[vertexSize.x*vertexSize.y];
        var uvs = new Vector2[vertices.Length];
        
        for (var y = 0; y < vertexSize.y; y++)
        {
            var v = (1f / subdivisions.y) * y;
            var newv = v * (Mathf.PI);
            
            for (var x = 0; x < vertexSize.x; x++)
            {
                var u = (1f / subdivisions.x) * x;
                var newu = u *  (Mathf.PI*2);

                var r = 2 + Mathf.Sin(7 * newu + 5 * newv);
                var vx = r * Mathf.Cos(newu) * Mathf.Sin(newv);
                var vz = r * Mathf.Sin(newu) * Mathf.Sin(newv);
                var vy = r * Mathf.Cos(newv);

                var vertex = new Vector3(vx,vy,vz);
                    
                var uv = new Vector2(u,v);

                var arrayIndex = x + y * vertexSize.x;


                vertices[arrayIndex] = vertex;
                uvs[arrayIndex] = uv;
            }
        }
        
        //triangles
        var triangles = new int[subdivisions.x * subdivisions.y * 6];

        for (var i = 0; i < subdivisions.x * subdivisions.y; i += 1)
        {
            var triangleIndex = (i % subdivisions.x) + (i / subdivisions.x) * vertexSize.x;
            var indexer = i * 6;

            //triangle 1
            triangles[indexer + 1] = triangleIndex;
            triangles[indexer + 0] = triangleIndex + subdivisions.x + 1;
            triangles[indexer + 2] = triangleIndex+1;
            
            //triangle 2
            triangles[indexer + 4] = triangleIndex+1;
            triangles[indexer + 3] = triangleIndex+subdivisions.x+1;
            triangles[indexer + 5] = triangleIndex+subdivisions.x+2;
        }

        generatedMesh.vertices = vertices;
        generatedMesh.uv = uvs;
        generatedMesh.triangles = triangles;

        generatedMesh.name = "ting";
        GetComponent<MeshFilter>().mesh = generatedMesh;
    }
}
