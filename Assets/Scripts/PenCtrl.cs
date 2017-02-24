using UnityEngine;
using System.Collections;

public class PenCtrl : MonoBehaviour {
	
    #region keycode
    private const KeyCode FORWARD   = KeyCode.W;
    private const KeyCode BACKWARD  = KeyCode.S;
    private const KeyCode LEFT      = KeyCode.A;
    private const KeyCode RIGHT     = KeyCode.D;
    private const KeyCode UP        = KeyCode.UpArrow;
    private const KeyCode DOWN      = KeyCode.DownArrow;
    private const float   ForceFix  = 0.1f;
    #endregion

    public Transform m_Pen = null;

    private float m_fHorizantalForce    = 0f;   //左右
    private float m_fDepthForce         = 0f;   //前後
    private float m_fVerticalForce      = 0f;   //上下垂直

    private float m_fHorizontal    = 0f;   //左右
    private float m_fDepth         = 0f;   //前後
    private float m_fVertical      = 0f;   //上下垂直

	// Update is called once per frame
	void Update () {
        ProcessInput();
        m_Pen.Translate(new Vector3 ( m_fHorizantalForce , m_fVerticalForce ,m_fDepthForce ) * ForceFix);
	}

    private void ProcessInput()
    {
        if (Input.GetKey(FORWARD))
        {
            m_fDepthForce += Input.GetAxis("Vertical") * Time.deltaTime;
        }
        else if (Input.GetKey(BACKWARD))
        {
            m_fDepthForce += Input.GetAxis("Vertical") * Time.deltaTime;
        }

        if (Input.GetKey(LEFT))
        {
            m_fHorizantalForce += Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        else if (Input.GetKey(RIGHT))
        {
            m_fHorizantalForce += Input.GetAxis("Horizontal") * Time.deltaTime;
        }

        if (Input.GetKey(UP))
        {
            m_fVerticalForce += Input.GetAxis("Depth") * Time.deltaTime;
        }
        else if (Input.GetKey(DOWN))
        {
            m_fVerticalForce += Input.GetAxis("Depth") * Time.deltaTime;
        }            
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0,0,400,50)," Vertical   = "    + m_fVerticalForce);
        GUI.Label(new Rect(0,25,400,50)," m_fDepthForce = "  + m_fDepthForce);
        GUI.Label(new Rect(0,50,400,50)," Horizantal = "  + m_fHorizantalForce);
    }
}
