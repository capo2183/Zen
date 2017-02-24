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
    public float   IGNORE_FORCE     = 0.015f;
    public float   CLAMP_FORCE      = 1f;
    public float   INK_MAX          = 10f;
    public float   INK_MIN          = 0f;
    #endregion
    public Transform m_Pen = null;
    private Color m_InkColor = new Color(0,0,0);

    private float m_fHorizantalForce    = 0f;   //左右
    private float m_fDepthForce         = 0f;   //前後
    private float m_fVerticalForce      = 0f;   //上下垂直

    private float m_fHorizontal         = 0f;   //左右
    private float m_fDepth              = 0f;   //前後
    private float m_fVertical           = 0f;   //上下垂直

    public float m_fInk                = 1f;
    public  float GetInk{ get{ return m_fInk; }}

    public MeshRenderer[] m_HairMeshRenderer    = null;

	// Update is called once per frame
	void Update () 
    {
        ProcessInput();
        UpdateInk();
        ChangeHairColor();
        Move();
    }

    private void Move()
    {
        m_Pen.Translate(new Vector3 ( m_fHorizantalForce , m_fVerticalForce ,m_fDepthForce ) * ForceFix);        
    }        

    private void AddInk( float _fValue )
    {
        m_fInk += _fValue;
        m_fInk = Mathf.Clamp( m_fInk , INK_MIN , INK_MAX );
    }

    private void UpdateInk()
    {
        float _fInk = 1f - (m_fInk / INK_MAX );
        m_InkColor = new Color( _fInk , _fInk , _fInk );
    }

    private void ChangeHairColor()
    {
        for(int i = 0 ; i < m_HairMeshRenderer.Length ; i++ )
        {
            m_HairMeshRenderer[i].material.color =  m_InkColor * (7 - (6 - i));
        }
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

        IgnoreForce();
        ClampForce();

    }
        
    private void IgnoreForce()
    {
        if (Mathf.Abs( m_fDepthForce ) < IGNORE_FORCE) m_fDepthForce = 0f;
        if (Mathf.Abs( m_fHorizantalForce ) < IGNORE_FORCE) m_fHorizantalForce = 0f;
        if (Mathf.Abs( m_fVerticalForce ) < IGNORE_FORCE) m_fVerticalForce = 0f;
    }

    private void ClampForce()
    {
        m_fDepthForce       = Mathf.Clamp(m_fDepthForce ,       - CLAMP_FORCE , CLAMP_FORCE);
        m_fHorizantalForce  = Mathf.Clamp(m_fHorizantalForce,   - CLAMP_FORCE , CLAMP_FORCE);
        m_fVerticalForce    = Mathf.Clamp(m_fVerticalForce ,    - CLAMP_FORCE , CLAMP_FORCE);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0,0,400,50)," Vertical   = "    + m_fVerticalForce);
        GUI.Label(new Rect(0,25,400,50)," m_fDepthForce = "  + m_fDepthForce);
        GUI.Label(new Rect(0,50,400,50)," Horizantal = "  + m_fHorizantalForce);
    }

    private void OnCollisionStay(Collision _other)
    {
        if (_other.gameObject.tag == "Inkstone")
        {            
            Debug.LogError("GetINK");
            if (m_fInk < INK_MAX)
            {
                this.AddInk( _other.gameObject.GetComponent<InkStoneCtrl>().GetInk );
            }
        }
    }
}
