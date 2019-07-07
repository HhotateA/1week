using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    [SerializeField] private Vector3 speedVec = new Vector3(0.0f,0.0f,-10.0f);
    [SerializeField] private Vector3[] valPos = new Vector3[4];
    [SerializeField] private Material[] valMat = new Material[4];
    [SerializeField] private AudioClip[] valSe0 = new AudioClip[0];
    [SerializeField] private AudioClip[] valSe1 = new AudioClip[0];
    [SerializeField] private AudioClip[] valSe2 = new AudioClip[0];
    [SerializeField] private AudioClip[] valSe3 = new AudioClip[0];
    [SerializeField] private float basebmp = 4.0f;
    private float bmp = 4.0f;
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private Renderer meshRender;
    private int layline;
    private float lifetime = 0.01f;
    public int layerNum = 0;
    [SerializeField] private bool setHit = false;
    [SerializeField] private bool heroHit = false;
    private float intensityVal = 1.0f;
    private float valume = 1.0f;
    [SerializeField] private float bpmTimer = 4.0f;
    [SerializeField] private float setSpeed = 0.1f;
    [SerializeField] private Vector3 setPos = new Vector3(0.0f,3.0f,0.0f);
    // Start is called before the first frame update
    void Start()
    {
        layline = Random.Range(0,4);
        rigidBody = gameObject.GetComponent<Rigidbody>();;
        audioSource = gameObject.GetComponent<AudioSource>();
        meshRender = gameObject.GetComponent<Renderer>(); 
        transform.position = valPos[layline];
        rigidBody.velocity = speedVec;
        switch(layline){
            case 0: audioSource.clip = valSe0[Random.Range(0,valSe0.Length-1)];
                    audioSource.volume = 0.25f;
                    lifetime = 0.1f;
                    break;
            case 1: audioSource.clip = valSe1[Random.Range(0,valSe1.Length-1)];
                    audioSource.volume = 0.2f;
                    lifetime = 0.01f;
                    break;
            case 2: audioSource.clip = valSe2[Random.Range(0,valSe2.Length-1)];
                    audioSource.volume = 0.3f;
                    lifetime = 0.001f;
                    break;
            case 3: audioSource.clip = valSe3[Random.Range(0,valSe3.Length-1)];
                    audioSource.volume = 0.25f;
                    lifetime = 0.05f;
                    break;
        }
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
                rigidBody.velocity = -speedVec*0.01f;
                setHit = false;
                heroHit = true;
            }
            transform.position = transform.position + setVec.normalized * setSpeed;
        }
        if(heroHit){
            valume -= Time.deltaTime*lifetime;
            // /audioSource.volume = valume;

            if(valume>0.75f){
                bmp = basebmp;
            }else if(valume>0.5f){
                bmp = basebmp * 2.0f;
            }else if(valume>0.25f){
                bmp = basebmp * 4.0f;
            }else{
                bmp = basebmp * 8.0f;
                if(valume<=0.0f)Destroy(this.gameObject);
            }

            
            bpmTimer -= Time.deltaTime;
            if(bpmTimer<0.0f){
                audioSource.Play ();
                intensityVal = 5.0f;
                bpmTimer = bmp;
            }
            meshRender.material.SetFloat("_Intensity",intensityVal);
            intensityVal = Mathf.Clamp(intensityVal-Time.deltaTime*10.0f,0.1f,5.0f);
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
            intensityVal = 0.7f;
            meshRender.material.SetFloat("_Intensity",0.1f);
            layerNum = 1;
            setPos = new Vector3( ((Time.time%bmp)/bmp-0.5f)*setPos.x, setPos.y+Random.Range(0.0f,1.0f),setPos.z);
            setHit = true;
            valume = 1.0f;
        }else if( layerName == "trig")
        {
            meshRender.material.SetFloat("_Intensity",2.0f);
            audioSource.Play ();
        }else if( layerName == "layerChanger"){
            layerNum ++;
        }
    }
}
