using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour
{
    // Start is called before the first frame update
    private Mesh generatedMesh;
    void Start()
    {
        generatedMesh = new Mesh();
        //changed from 8 to 30/15
        var subdivisions = new Vector2Int(30, 30);
        var vertexSize = subdivisions + new Vector2Int(1, 1);

        var vertices = new Vector3[vertexSize.x * vertexSize.y];
        var uvs = new Vector2[vertices.Length];
        for (var y = 0; y < vertexSize.y; y++)
        {
            var v = (1f / subdivisions.y) * y;
            var newv = v * (Mathf.PI * 2);

            for (var x = 0; x < vertexSize.x; x++)
            {
                var u = (1f / subdivisions.x) * x;
                var newu = u * (Mathf.PI * 2);
                var vx = 0f;
                var vy = 0f;
                //var vertex = new Vector3(u*Mathf.Cos(newv),newv,u*Mathf.Sin(newv));
                //var vertex = new Vector3(3*Mathf.Cos(newu)+Mathf.Cos(newu)*Mathf.Cos(newv),3*Mathf.Sin(newu)+Mathf.Sin(newu)*Mathf.Cos(newv),Mathf.Sin(newv));

                var r = 4 - 2 * Mathf.Cos(newu);
                if (newu > Math.PI)
                {
                    vx  = -4 * Mathf.Cos(newu) * (1 + Mathf.Sin(newu)) + r * Mathf.Cos(newv);
                    vy = -14 * Mathf.Sin(newu);
                    
                }
                else
                {
                    vx  = -4 * Mathf.Cos(newu) * (1 + Mathf.Sin(newu)) - r * Mathf.Cos(newu) * Mathf.Cos(newv);
                    vy = -14 * Mathf.Sin(newu) - r * Mathf.Sin(newu) * Mathf.Cos(newv);
                }
                var vz= r * Mathf.Sin(newv);
                
                var vertex = new Vector3(vx,vy,vz);
                var uv = new Vector2(u, v);

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
            triangles[indexer + 0] = triangleIndex;
            triangles[indexer + 1] = triangleIndex + subdivisions.x + 1;
            triangles[indexer + 2] = triangleIndex + 1;

            //triangle 2
            triangles[indexer + 3] = triangleIndex + 1;
            triangles[indexer + 4] = triangleIndex + subdivisions.x + 1;
            triangles[indexer + 5] = triangleIndex + subdivisions.x + 2;
        }

        generatedMesh.vertices = vertices;
        generatedMesh.uv = uvs;
        generatedMesh.triangles = triangles;

        generatedMesh.name = "ting";
        GetComponent<MeshFilter>().mesh = generatedMesh;
    }
}

// Update is called once per frame

