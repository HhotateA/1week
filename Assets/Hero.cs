using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private int val = 0;
    [SerializeField] private Vector3[] valPos = new Vector3[4];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = valPos[val];
    }

    public void rightBot(){
        val = Mathf.Clamp(val+1,0,3);
    }
    public void leftBpt(){
        val = Mathf.Clamp(val-1,0,3);
    }
}
