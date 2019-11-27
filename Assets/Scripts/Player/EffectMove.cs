using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMove : MonoBehaviour
{
    public float speedX = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speedX, 0.0f, 0.0f);
    }

    private void FixedUpdate()
    {
        
    }
}
