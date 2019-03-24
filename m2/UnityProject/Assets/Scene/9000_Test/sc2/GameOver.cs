using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        StopAllCoroutines();
		StartCoroutine(work());
	}

    IEnumerator work()
    {
        var tm = GetComponent<TextMesh>();
        tm.text = string.Empty;

        yield return new WaitForSeconds(1);

        foreach(var c in "Game Over")
        {
            tm.text += new string(c,1);
            yield return new WaitForSeconds(0.3f);
        }
    }
	
}
