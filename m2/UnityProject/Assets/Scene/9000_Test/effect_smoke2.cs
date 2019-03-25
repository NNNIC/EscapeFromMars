using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect_smoke2 : MonoBehaviour {

    public int m_num  = 10;
    public GameObject m_template;
    public  List<GameObject>  m_list;

	// Use this for initialization
	void Start () {
        m_list= new List<GameObject>();

        var dg = 360f / m_num;
	    
        for(var i = 0; i < m_num; i++)
        {
            var g = Instantiate(m_template);
            g.name = "su_"+ i.ToString("000");
            g.transform.parent = transform;
            g.transform.localEulerAngles = Vector3.up * (dg * i );
            g.gameObject.SetActive(false);
            m_list.Add(g);
        }

	}

    [ContextMenu("Kick")]
    public void Kick()
    {
        StartCoroutine(Kick_co());
    }

    [ContextMenu("Stop")]
    public void Stop()
    {
        m_list.ForEach(g=>g.SetActive(false));
    }

    IEnumerator Kick_co()
    {
        foreach(var g in m_list)
        {
            g.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }


}
