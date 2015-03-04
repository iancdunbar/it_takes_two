using UnityEngine;
using System.Collections;

public class SimpleRotate : MonoBehaviour {

    [SerializeField]
    private float rotation_amount;

	// Update is called once per frame
	void Update () 
    {
        transform.Rotate( Vector3.up, rotation_amount * Time.deltaTime );
	}
}
