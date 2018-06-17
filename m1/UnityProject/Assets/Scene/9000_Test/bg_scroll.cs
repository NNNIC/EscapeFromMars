using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_scroll : MonoBehaviour {

    public float m_diff;
    float m_yoffset = 0;

    Renderer m_rend;

	// Use this for initialization
	void Start () {
		m_rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        m_yoffset += m_diff;
        if (m_yoffset > 1) m_yoffset -= 1;
        m_rend.material.SetTextureOffset("_MainTex", Vector2.up * m_yoffset);
	}
}
