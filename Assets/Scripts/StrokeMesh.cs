using UnityEngine;
using System.Collections;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class StrokeMesh : MonoBehaviour
{
    public class StrokePoint
    {
        public Vector3 centerPoint;
        public Vector3 upperPoint;
        public Vector3 lowerPoint;
        public float weight;

        public void InitStrokeData()
        {
            upperPoint = centerPoint + new Vector3(0.0f, 0.0f, weight);
            lowerPoint = centerPoint + new Vector3(0.0f, 0.0f, -weight);
        }
    }

    public StrokePoint[] m_StrokeSkeletonPointAry;

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

        StrokePoint sp0 = new StrokePoint();
        sp0.centerPoint = new Vector3(0.0f, 0.0f, 0.0f);
        sp0.weight = 0.3f;
        sp0.InitStrokeData();

        StrokePoint sp1 = new StrokePoint();
        sp1.centerPoint = new Vector3(1.0f, 0.0f, 0.0f);
        sp1.weight = 0.35f;
        sp1.InitStrokeData();

        StrokePoint sp2 = new StrokePoint();
        sp2.centerPoint = new Vector3(2.0f, 0.0f, 0.0f);
        sp2.weight = 0.3f;
        sp2.InitStrokeData();

        m_StrokeSkeletonPointAry = new StrokePoint[3] { sp0, sp1, sp2 };

        GenerateStorke();
    }

    private void GenerateStorke()
    {
        DrawStrokeBetweenTwoPoint(m_StrokeSkeletonPointAry[0], m_StrokeSkeletonPointAry[1]);
        DrawStrokeBetweenTwoPoint(m_StrokeSkeletonPointAry[1], m_StrokeSkeletonPointAry[2]);
    }
    
    private void DrawStrokeBetweenTwoPoint(StrokePoint sp0, StrokePoint sp1)
    {
        Vector3[] vertices = new Vector3[6];

        vertices[0] = sp0.centerPoint;
        vertices[1] = sp1.centerPoint;
        vertices[2] = sp0.upperPoint;
        vertices[3] = sp1.upperPoint;
        vertices[4] = sp0.lowerPoint;
        vertices[5] = sp1.lowerPoint;

        mesh.vertices = vertices;

        int[] triangles = new int[12];
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 1;
        triangles[4] = 2;
        triangles[5] = 3;
        triangles[6] = 0;
        triangles[7] = 1;
        triangles[8] = 4;
        triangles[9] = 4;
        triangles[10] = 1;
        triangles[11] = 5;
        
        mesh.triangles = triangles;
        mr.material.color = Color.blue;
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
