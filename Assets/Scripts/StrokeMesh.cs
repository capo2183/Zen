using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class StrokeMesh : MonoBehaviour
{
    [System.Serializable]
    public class StrokePoint
    {
        public Vector3 centerPoint;
        public float weight;
        public Vector3 upperPoint;
        public Vector3 lowerPoint;
    }

    private List<StrokePoint> m_StrokeSkeletonPointList;
    private StrokePoint[] m_StrokeSkeletonPointAry;

    private Mesh mesh;
    private MeshRenderer mr;
    private MeshFilter mf;

    // Use this for initialization
    void Awake()
    {
        mr = this.GetComponent<MeshRenderer>();
        mf = this.GetComponent<MeshFilter>();

        mf.mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        m_StrokeSkeletonPointList = new List<StrokePoint>();
    }

    public void AddSkeletonVertex(Vector3 _targetPoint, float _weight)
    {
        StrokePoint sp = new StrokePoint();
        sp.centerPoint = _targetPoint;
        sp.weight = _weight;
        m_StrokeSkeletonPointList.Add(sp);
    }

    public void Update()
    {
        m_StrokeSkeletonPointAry = m_StrokeSkeletonPointList.ToArray();
        GenerateStorke(m_StrokeSkeletonPointAry);
    }

    private void GenerateStorke(StrokePoint[] spAry)
    {
        CalStrokeUpperAndLowerPoint(spAry);

        Vector3[] vertices = new Vector3[spAry.Length * 3];
        for(int i=0; i<spAry.Length; i++)
        {
            vertices[3*i]     = spAry[i].centerPoint;
            vertices[3*i + 1] = spAry[i].upperPoint;
            vertices[3*i + 2] = spAry[i].lowerPoint;
        }        
        mesh.vertices = vertices;
        
        int[] triangles = new int[(spAry.Length - 1) * 12];
        for (int i = 0; i < spAry.Length-1; i++)
        {
            triangles[12*i]        = 3*i + 0;
            triangles[12*i + 1]    = 3*i + 1;
            triangles[12*i + 2]    = 3*i + 3;
            triangles[12*i + 3]    = 3*i + 1;
            triangles[12*i + 4]    = 3*i + 4;
            triangles[12*i + 5]    = 3*i + 3;
            triangles[12*i + 6]    = 3*i + 2;
            triangles[12*i + 7]    = 3*i + 0;
            triangles[12*i + 8]    = 3*i + 3;
            triangles[12*i + 9]    = 3*i + 2;
            triangles[12*i + 10]   = 3*i + 3;
            triangles[12 * i + 11] = 3*i + 5;
        }        
        
        mesh.triangles = triangles;
        mr.material.color = Color.blue;
    }

    private void CalStrokeUpperAndLowerPoint(StrokePoint[] spAry)
    {
        Vector3 widthVec = new Vector3(0.0f, 0.0f, 1.0f);
        Vector3 forward = new Vector3(1.0f, 0.0f, 0.0f);
        Vector3 upward = new Vector3(0.0f, 1.0f, 0.0f);

        // The first of the point
        forward = spAry[1].centerPoint - spAry[0].centerPoint;
        widthVec = Vector3.Cross(upward, forward).normalized;
        spAry[0].upperPoint = spAry[0].centerPoint - spAry[0].weight * widthVec;
        spAry[0].lowerPoint = spAry[0].centerPoint + spAry[0].weight * widthVec;

        for (int i=1; i<spAry.Length-1; i++)
        {
            StrokePoint prevPoint = spAry[i - 1];
            StrokePoint currPoint = spAry[i];
            StrokePoint nextPoint = spAry[i + 1];
            forward = nextPoint.centerPoint - prevPoint.centerPoint;

            widthVec = Vector3.Cross(upward, forward).normalized;
            spAry[i].upperPoint = currPoint.centerPoint - currPoint.weight * widthVec;
            spAry[i].lowerPoint = currPoint.centerPoint + currPoint.weight * widthVec;
        }

        // The last of the point
        spAry[spAry.Length-1].upperPoint = spAry[spAry.Length-1].centerPoint - spAry[spAry.Length-1].weight * widthVec;
        spAry[spAry.Length-1].lowerPoint = spAry[spAry.Length-1].centerPoint + spAry[spAry.Length-1].weight * widthVec;
    }

    public void OnDrawGizmos()
    {
        if (m_StrokeSkeletonPointAry == null)
            return;

        Gizmos.color = Color.red;
        for (int i = 0; i < m_StrokeSkeletonPointAry.Length; i++)
        {
            Gizmos.DrawSphere(m_StrokeSkeletonPointAry[i].centerPoint, 0.1f);
        }
    }
}
