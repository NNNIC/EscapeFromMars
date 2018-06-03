using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorList : MonoBehaviour {
    
    [System.Serializable]
    public class Item { public string name; public Color color;  }

    public List<Item> m_colors;

    public static ColorList m_colorList;
    public static Color GetColor(string name)
    {
        if (m_colorList==null)
        {
            var prefab = (GameObject)Resources.Load("Game/ColorList");
            m_colorList = prefab.GetComponent<ColorList>();
        }

        foreach(var i in m_colorList.m_colors)
        {
            if (i.name == name)
            {
                return i.color;
            }
        }

        return Color.magenta;
    }
}
