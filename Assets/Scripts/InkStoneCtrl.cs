using UnityEngine;
using System.Collections;

public class InkStoneCtrl : MonoBehaviour {

    private const float INK_MAX = 100f;
    private const float INK_MIN = 0f;
    public float m_fGetInkSpeed = 0.1f;
    public float m_fAddInkSpeed = 0.2f;
    public float m_fInkValue = 0;

    public MeshRenderer m_InkMeshRenderer = null;

    private void Update()
    {
        float _fColorValue = 1f - ( m_fInkValue / INK_MAX );
        m_InkMeshRenderer.material.color = new Color( _fColorValue , _fColorValue , _fColorValue );
    }


    public void AddInk()
    {
        m_fInkValue += m_fAddInkSpeed;
        m_fInkValue = Mathf.Clamp( m_fInkValue , INK_MIN , INK_MAX );
    }

    public float GetInk
    {
        get
        {
            m_fInkValue -= m_fGetInkSpeed;
            m_fInkValue = Mathf.Clamp( m_fInkValue , INK_MIN , INK_MAX );
            if (m_fInkValue > 0)
                return m_fGetInkSpeed;
            else
                return 0f;
        }
    }




}
