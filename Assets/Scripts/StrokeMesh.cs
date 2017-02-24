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
        sp0.weight = 0.1f;
        sp0.InitStrokeData();

        StrokePoint sp1 = new StrokePoint();
        sp1.centerPoint = new Vector3(1.0f, 0.0f, 0.0f);
        sp1.weight = 0.35f;
        sp1.InitStrokeData();

        StrokePoint sp2 = new StrokePoint();
        sp2.centerPoint = new Vector3(2.0f, 0.0f, 0.0f);
        sp2.weight = 0.4f;
        sp2.InitStrokeData();
        StrokePoint sp3 = new StrokePoint();
        sp3.centerPoint = new Vector3(3.0f, 0.0f, 0.0f);
        sp3.weight = 0.45f;
        sp3.InitStrokeData();

        StrokePoint sp4 = new StrokePoint();
        sp4.centerPoint = new Vector3(4.0f, 0.0f, 0.0f);
        sp4.weight = 0.35f;
        sp4.InitStrokeData();

        StrokePoint sp5 = new StrokePoint();
        sp5.centerPoint = new Vector3(5.0f, 0.0f, 0.0f);
        sp5.weight = 0.1f;
        sp5.InitStrokeData();

        StrokePoint sp6 = new StrokePoint();
        sp6.centerPoint = new Vector3(0.0f, 0.0f, 0.0f);
        sp6.weight = 0.3f;
        sp6.InitStrokeData();

        StrokePoint sp7 = new StrokePoint();
        sp7.centerPoint = new Vector3(1.0f, 0.0f, 0.0f);
        sp7.weight = 0.35f;
        sp7.InitStrokeData();

        StrokePoint sp8 = new StrokePoint();
        sp8.centerPoint = new Vector3(2.0f, 0.0f, 0.0f);
        sp8.weight = 0.4f;
        sp8.InitStrokeData();
        StrokePoint sp9 = new StrokePoint();
        sp9.centerPoint = new Vector3(3.0f, 0.0f, 0.0f);
        sp9.weight = 0.45f;
        sp9.InitStrokeData();
        

        m_StrokeSkeletonPointAry = new StrokePoint[10] { sp0, sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8, sp9 };

        GenerateStorke(m_StrokeSkeletonPointAry);
    }

    private void GenerateStorke(StrokePoint[] spAry)
    {
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
