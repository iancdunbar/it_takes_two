using UnityEngine;
using System.Collections;

public class ObjectGrab : MonoBehaviour {

    ////////////////////////////////////////////////////
    // Private Variables
    ////////////////////////////////////////////////////

    private Transform target_orginal_parent;
    private Vector2 screen_touch;
    private bool grabbed;

    ////////////////////////////////////////////////////


    ////////////////////////////////////////////////////
    // Inspector Variables
    ////////////////////////////////////////////////////

    [SerializeField]
    private Transform target_object;

    ////////////////////////////////////////////////////

    ////////////////////////////////////////////////////
    // Private Functions
    ////////////////////////////////////////////////////

    private Transform detectObjectHit( )
    {
        Transform result = null;
        RaycastHit hit_info;

#if UNITY_ANDROID && !UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay( Input.GetTouch( 0 ).position );
#else
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
#endif
        if( Physics.Raycast( ray, out hit_info ) )
        {

            if( hit_info.transform.tag == "LetterChild" )
            {
                result = hit_info.transform;

            }
        }

        return result;
    }

    ////////////////////////////////////////////////////


    ////////////////////////////////////////////////////
    // Public Functions
    ////////////////////////////////////////////////////



    ////////////////////////////////////////////////////


    ////////////////////////////////////////////////////
    // Unity Messages
    ////////////////////////////////////////////////////

    void Awake( )
    {

    }

	// Use this for initialization
	void Start () 
    {
        grabbed = false;
	}
	
	// Update is called once per frame
	void Update () 
    {


#if UNITY_ANDROID && !UNITY_EDITOR
        if( Input.touchCount > 0 )
        {
            Touch curr = Input.GetTouch( 0 );
            if( curr.phase == TouchPhase.Ended )
            {

                Transform obj = detectObjectHit( );
                if( obj != null )
                    obj.GetComponent<PerspectiveLock>().ToggleObjectState( );

            }
        }
#else

        if( Input.GetMouseButtonUp( 0 ) )
        {

            Transform obj = detectObjectHit();

            if( obj != null )
            {
                obj.GetComponent<PerspectiveLock>( ).ToggleObjectState( );
            }

        }
#endif
	}

    ////////////////////////////////////////////////////
}
