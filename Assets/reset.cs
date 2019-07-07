using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset : MonoBehaviour
{
    Collider m_Collider;
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<Collider>();
        m_Collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey (KeyCode.R)) {
            reseter();
        }
        if(timer>0.0f){
            m_Collider.enabled = true;
        }else{
            m_Collider.enabled = false;
        }
        timer -= Time.deltaTime;
        
    }

    public void reseter(){
        timer = 1.0f;
    }
}
