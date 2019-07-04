using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    [SerializeField] private Vector3 speedVec = new Vector3(0.0f,0.0f,-10.0f);
    [SerializeField] private Vector3[] valPos = new Vector3[4];
    [SerializeField] private Material[] valMat = new Material[4];
    [SerializeField] private AudioClip[] valSe = new AudioClip[0];
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private Renderer meshRender;
    private int layline;
    // Start is called before the first frame update
    void Start()
    {
        layline = Random.Range(0,3);
        rigidBody = gameObject.GetComponent<Rigidbody>();;
        audioSource = gameObject.GetComponent<AudioSource>();
        meshRender = gameObject.GetComponent<Renderer>(); 
        transform.position = valPos[layline];
        rigidBody.velocity = speedVec;
        audioSource.clip = valSe[Random.Range(0,valSe.Length-1)];
        meshRender.material = valMat[layline];
    }

    // Update is called once per frame
    void Update()
    {
        //rigidBody.velocity = speedVec;
        
    }

    void OnTriggerEnter(Collider other)
    {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);

        if( layerName == "eraser")
        {
            Destroy(gameObject);
        }else if( layerName == "hero")
        {
            Debug.Log("hit");
            audioSource.Play ();
        }else if( layerName == "trig")
        {
            audioSource.Play ();
        }
    }
}
