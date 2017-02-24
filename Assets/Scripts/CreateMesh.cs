﻿using UnityEngine;
using System.Collections;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CreateMesh : MonoBehaviour {
    public int xSize;
    public int ySize;
    
    private Mesh mesh;
    private Vector3[] vertices;
    private MeshRenderer mr;
    private MeshFilter mf;

    // Use this for initialization
    void Awake () {
        mr = this.GetComponent<MeshRenderer>();
        mf = this.GetComponent<MeshFilter>();

        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        mf.mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                yield return new WaitForSeconds(0.05f);
            }
        }

        mesh.vertices = vertices;

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = xSize + 1;
        triangles[2] = 1;
        triangles[3] = 1;
        triangles[4] = xSize + 1;
        triangles[5] = xSize + 2;

        mesh.triangles = triangles;
        mr.material.color = Color.blue;
    }

    public void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
