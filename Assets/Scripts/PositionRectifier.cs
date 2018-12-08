using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRectifier : MonoBehaviour {
    public bool facingTop = true;
    public bool facingRight = false;
    public bool facingDown = false;
    public bool facingLeft = false;
    private float rectification = 10.0f;

    public Vector3 getRectifiedPosition(){
        return this.transform.position + new Vector3((facingRight ? rectification : 0.0f) + (facingLeft ? -rectification : 0.0f), 0.0f, (facingTop ? rectification : 0.0f) + (facingDown ? -rectification : 0.0f));
    }
}
