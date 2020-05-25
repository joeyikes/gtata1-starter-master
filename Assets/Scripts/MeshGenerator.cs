using System;
using System.Collections;
using System.Collections.Generic;
using Meshes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class MeshGenerator : MonoBehaviour
{
    //SINGLETON
    private static MeshGenerator instance;
    public static MeshGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                Initialize();
            }

            return instance;
        }
        private set { instance = value; }
    }
    private static void Initialize()
    {
        var gameObjects = FindObjectsOfType<MeshGenerator>();
        if (gameObjects.Length < 1)
        {
            CreateInstance();
        }
        else if(gameObjects.Length == 1)
        {
            instance = gameObjects[0];
        }
        else
        {
            Debug.LogWarning("more than one Instance! Assuming first");
            instance = gameObjects[0];
        }
    }
    private static void CreateInstance()
    {
        var host = new GameObject();
        Instance = host.AddComponent<MeshGenerator>();
        // prevent accidental scene saving
        host.hideFlags = HideFlags.DontSaveInEditor;
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    //creates a Mesh from a surface 
    public Mesh Generate(Surface surface, String name)
    {
        Mesh returnMesh = new Mesh();
      
        var subdivisions = new Vector2Int(surface.GetSubX(),surface.GetSubY());
        var vertexSize = subdivisions + new Vector2Int(1, 1);
        
        var vertices = new Vector3[vertexSize.x*vertexSize.y];
        var uvs = new Vector2[vertices.Length];
        for (var y = 0; y < vertexSize.y; y++)
        {
            var v = (1f / subdivisions.y) * y;
            var newv = surface.GetNewV(v);
            
            for (var x = 0; x < vertexSize.x; x++)
            {
                var u = (1f / subdivisions.x) * x;
                var newu = surface.GetNewU(u);
                
                
                var uv = new Vector2(u,v);

                var arrayIndex = x + y * vertexSize.x;


                vertices[arrayIndex] = surface.GetParamFunc(newu,newv);
                uvs[arrayIndex] = uv;
            }
        }
        
        //triangles
        var triangles = new int[subdivisions.x * subdivisions.y * 6];

        for (var i = 0; i < subdivisions.x * subdivisions.y; i += 1)
        {
            var triangleIndex = (i % subdivisions.x) + (i / subdivisions.x) * vertexSize.x;
            var indexer = i * 6;
            var windingorderNum = 0;
            var windingorderNumTwo = 1;
            var windingorderNumThr = 3;
            var windingorderNumFo = 4;
            if (surface.IsClockWiseWindingOrder())
            {
                windingorderNum = 1;
                windingorderNumTwo = 0;
                windingorderNumThr = 4;
                windingorderNumFo = 3;
            }

            //triangle 1
            triangles[indexer + windingorderNum] = triangleIndex;
            triangles[indexer + windingorderNumTwo] = triangleIndex + subdivisions.x + 1;
            triangles[indexer + 2] = triangleIndex+1;
            
            //triangle 2
            triangles[indexer + windingorderNumThr] = triangleIndex+1;
            triangles[indexer + windingorderNumFo] = triangleIndex+subdivisions.x+1;
            triangles[indexer + 5] = triangleIndex+subdivisions.x+2;
        }

        
        returnMesh.vertices = vertices;
        returnMesh.uv = uvs;
        returnMesh.triangles = triangles;
        var path = "Assets/Meshes/" + name + ".asset";
        if (!File.Exists(path))
        { 
            AssetDatabase.CreateAsset(returnMesh,path);
        }
        return returnMesh;
    }
    
}
