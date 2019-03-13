using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    int DNALength = 2;
    public DNA dna;
    public GameObject eyes;
    bool seeWall = true;
    Vector3 startPosition;
    public float distanceTravelled = 0f;
    bool alive = true;


    public void Init()
    {
        // initialize DNA
        // 0 forward
        // 1 turn

        dna = new DNA(DNALength, 360);
        startPosition = this.transform.position;

    }
    public void setDNA(DNA nDna)
    {
        startPosition = this.transform.position;
        dna = nDna;
    }

    private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "dead")
        {
            alive = false;
            distanceTravelled = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (!alive) return;

        seeWall = false;
        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 0.5f, Color.red);
        RaycastHit hit;
        if (Physics.SphereCast(eyes.transform.position, 0.1f , eyes.transform.forward,out hit, 0.5f))
        {
            if (hit.collider.gameObject.tag == "wall")
            {
                seeWall = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!alive) return;

        //read dna
        float h = 0;
        float v = dna.GetGene(0);

        if(seeWall)
        {
            h = dna.GetGene(1);
        }

        this.transform.Translate(0, 0, v * 0.0005f);
        this.transform.Rotate(0, h, 0);
        distanceTravelled = Vector3.Distance(startPosition, this.transform.position);
    }
}
