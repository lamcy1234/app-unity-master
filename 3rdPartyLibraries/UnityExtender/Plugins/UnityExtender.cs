using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class UnityExtender : MonoBehaviour
{

    static UnityExtender _instance;

    public static UnityExtender Instance {
        get {
            if (_instance == null) {
                _instance = new GameObject ("UnityExtender", typeof(UnityExtender)).GetComponent<UnityExtender> ();
                _instance.gameObject.hideFlags = HideFlags.HideAndDontSave;
            }
            return _instance;
        }
    }
 
    public static IEnumerator Step (float T, System.Action<float> Step)
    {
        var P = 0f;
        while (P <= 1f) {
            P = Mathf.Clamp01 (P + (Time.deltaTime / T));
            Step (Mathf.SmoothStep (0, 1, P));
            yield return null;
        }
    }
 
 
 
}






