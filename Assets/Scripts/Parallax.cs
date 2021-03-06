﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    private float length, startpos;

    public GameObject cam;
    public Transform platform;
    public float parralaxEffect;

	// Use this for initialization
	void Start () {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
		
	}
	
	// Update is called once per frame
	void Update () {

        float dist = (cam.transform.position.x * parralaxEffect);

        transform.position = new Vector3(startpos + dist,
                                         platform.transform.position.y, 
                                         transform.position.z);
	}
}
