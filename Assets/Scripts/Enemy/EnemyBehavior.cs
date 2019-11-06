using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class EnemyBehavior : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#if UNITY_EDITOR
    void OnGUI()
    {

    }
    private void OnDrawGizmosSelected()
    {
        bool spriteFaceLeft = true;
        float viewFov = 300;
        float viewDirection = 0;
        float viewDistance = 10;
        //draw the cone of view
        Vector3 forward = spriteFaceLeft ? Vector2.left : Vector2.right;
        forward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirection : viewDirection) * forward;

        if (GetComponent<SpriteRenderer>().flipX) forward.x = -forward.x;

        Vector3 endpoint = transform.position + (Quaternion.Euler(0, 0, viewFov * 0.5f) * forward);

        Handles.color = new Color(0, 1.0f, 0, 0.2f);
        Handles.DrawSolidArc(transform.position, -Vector3.forward, (endpoint - transform.position).normalized, viewFov, viewDistance);

        //Draw attack range
        //Handles.color = new Color(1.0f, 0, 0, 0.1f);
        //Handles.DrawSolidDisc(transform.position, Vector3.back, meleeRange);
    }

#endif
}
