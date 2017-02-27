using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InkOnPaper : MonoBehaviour {
    public GameObject InkPrefab;
    private List<GameObject> InkList;

    public void Start()
    {
        InkList = new List<GameObject>();
    }

    public int CreateStroke()
    {
        GameObject ink = Instantiate(InkPrefab);
        ink.transform.localPosition = Vector3.zero;
        ink.transform.localScale = Vector3.one;
        ink.transform.parent = this.transform;
        InkList.Add(ink);
        
        return InkList.Count-1;
    }

    public void AddVertex(int id, Vector3 point, float weight)
    {
        InkList[id].GetComponent<StrokeMesh>().AddSkeletonVertex(point, weight);
    }
}
