using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] private float minTime = 0.0f;
    [SerializeField] private float maxTime = 5.0f;
    [SerializeField] private GameObject plefabSample;
    private float intervalTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        intervalTime = Random.Range(minTime,maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        intervalTime -= Time.deltaTime;
        if(intervalTime<0.0f){
            Instantiate (plefabSample,transform);
            intervalTime = Random.Range(minTime,maxTime);
        }
    }
}
