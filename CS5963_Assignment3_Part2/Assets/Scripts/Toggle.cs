using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour {


    void Start()
    {
        gameObject.GetComponent<BloomEffect>().enabled = false;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            if (gameObject.GetComponent<DepthOfFieldEffect>().enabled == true)
            {
                gameObject.GetComponent<DepthOfFieldEffect>().enabled = false;
                gameObject.GetComponent<BloomEffect>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<DepthOfFieldEffect>().enabled = true;
                gameObject.GetComponent<BloomEffect>().enabled = false;
            }
        }
    }
}
