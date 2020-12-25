using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect_smoke : MonoBehaviour {

    public int m_line = 10;
    public int m_num  = 10;
    public float m_diff = 10;
    public GameObject m_template;
    public List<   List<GameObject> > m_list;

	// Use this for initialization
	void Start () {
        m_list= new List<List<GameObject>>();

        for(var l = 0; l < m_line; l++)
        { 
            var list = new List<GameObject>();
            var dg = 360f / m_num;
	    
            for(var i = 0; i < m_num; i++)
            {
                var g = Instantiate(m_template);
                g.name = "su_"+ i.ToString("000");
                g.transform.parent = transform;
                g.transform.localEulerAngles = Vector3.up * (dg * i + m_diff * l);
                g.gameObject.SetActive(false);
                list.Add(g);
            }

            m_list.Add(list);
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
        foreach(var l in m_list)
        {
            l.ForEach(g=>g.SetActive(false));
        }
    }

    IEnumerator Kick_co()
    {
        foreach(var list in m_list)
        {
            list.ForEach(g=>g.SetActive(true));
            yield return new WaitForSeconds(0.2f);
        }
    }


}
