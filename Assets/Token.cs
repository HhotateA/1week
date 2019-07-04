using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    [SerializeField] private Vector3 speedVec = new Vector3(0.0f,0.0f,-10.0f);
    [SerializeField] private Vector3[] valPos = new Vector3[4];
    [SerializeField] private Material[] valMat = new Material[4];
    [SerializeField] private AudioClip[] valSe = new AudioClip[0];
    [SerializeField] private float bmp = 4.0f;
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private Renderer meshRender;
    private int layline;
    [SerializeField] private bool setHit = false;
    [SerializeField] private float setSpeed = 0.1f;
    [SerializeField] private Vector3 setPos = new Vector3(0.0f,3.0f,0.0f);
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
        if(setHit){
            Vector3 setVec = setPos - transform.position;
            if(setVec.magnitude<setSpeed){
                transform.position = setPos;
                setHit = false;
            }
            transform.position = transform.position + setVec.normalized * setSpeed;
        }
        
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
            rigidBody.velocity = Vector3.zero;
            audioSource.Play ();
            setPos = new Vector3( ((Time.time%bmp)/bmp-0.5f)*setPos.x, setPos.y,setPos.z);
            setHit = true;
        }else if( layerName == "trig")
        {
            audioSource.Play ();
        }
    }
}
