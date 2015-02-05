using UnityEngine;
using System.Collections;

public class PerspectiveLock : MonoBehaviour {

    ////////////////////////////////////////////////////
    // Private Variables
    ////////////////////////////////////////////////////

    private Quaternion angle_diff;
    private Quaternion lock_angle;
    private Transform original_parent;
    private Vector3 lock_scale;
    private Vector3 forward_offset;
    private float initial_distance;
    private bool locked;
    private bool grabbed;

    ////////////////////////////////////////////////////


    ////////////////////////////////////////////////////
    // Inspector Variables
    ////////////////////////////////////////////////////

    [SerializeField]
    private Transform lock_target;

    ////////////////////////////////////////////////////


    ////////////////////////////////////////////////////
    // Public Funtions
    ////////////////////////////////////////////////////

    public bool Locked { get { return locked; } }

    public void LockObjectPerspective( )
    {
        
        angle_diff = Quaternion.Inverse( transform.rotation ) * lock_target.rotation;
        forward_offset = transform.forward - lock_target.forward;
        lock_angle = transform.rotation;

        initial_distance = Vector3.Magnitude( transform.position - lock_target.position );
        lock_scale = transform.localScale;

        Debug.Log( lock_scale );

        locked = true;
    }

    public void ToggleGrabObject( )
    {
        if( grabbed )
        {
            renderer.material.color = Color.red;

            transform.parent = original_parent;

            grabbed = false;
        }
        else
        {
            renderer.material.color = Color.blue;

            original_parent = transform.parent;

            transform.parent = lock_target;

            grabbed = true;
        }

    }

    public void ToggleObjectLocked( )
    {
        if( locked )
        {
            renderer.material.color = Color.red;
            locked = false;
        }
        else
        {
            renderer.material.color = Color.yellow;
            LockObjectPerspective( );
        }
    }

    public void ToggleObjectState( )
    {
        if( grabbed )
        {
            ToggleGrabObject( );
        }
        else
        {
            ToggleObjectLocked( );

            if( !locked )
                ToggleGrabObject( );
        }
    }

    ////////////////////////////////////////////////////


    ////////////////////////////////////////////////////
    // Unity Messages
    ////////////////////////////////////////////////////

	// Use this for initialization
	void Start () 
    {

        locked = false;

	}
	
	// Update is called once per frame
	void Update () 
    {

        if( locked )
        {
            // Rotate the Object so it is facing the target
            //transform.rotation = Quaternion.Inverse( lock_angle ) * lock_target.rotation;

            Vector3 forward_off = lock_target.forward + forward_offset;

            transform.rotation = Quaternion.LookRotation( new Vector3( forward_off.x, forward_off.y, forward_off.z ), transform.up );

            // Scale the object to that it remains the same size
            float curr_distance = Vector3.Magnitude( transform.position - lock_target.position );

            float scale_ratio = curr_distance / initial_distance;
            Debug.Log( scale_ratio );

            transform.localScale = lock_scale * scale_ratio;
        }

	}

    ////////////////////////////////////////////////////
}
