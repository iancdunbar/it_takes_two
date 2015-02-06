using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuccessManager : MonoBehaviour {


    [SerializeField]
    private List<SuccessTarget> curr_targets;
    [SerializeField]
    private List<ScreenProjector> objects;
    [SerializeField]
    private List<Level> levels;

    private int curr_target_index = 0;
    private bool solved;

	// Use this for initialization
	void Start () 
    {
        levels[ curr_target_index ].gameObject.SetActive( true );

        curr_targets = levels[ curr_target_index ].GetSuccessTargets( );
	}
	
	// Update is called once per frame
	void Update () 
    {
        if( !solved )
        {
            bool success = true;

            foreach( SuccessTarget target in curr_targets )
            {
                if( target.Solved ) continue;

                if( !target.initialized )
                    target.Initialize( );

                foreach( ScreenProjector sp in objects )
                {

                    if( !target.CheckSuccessful( sp.GetScreenPoints( ) ) )
                    {
                        target.ChangeColor( Color.black );
                        success = false;
                    }
                    else
                    {

                        sp.GetComponent<PerspectiveLock>( ).SuccessLock( );
                        target.ChangeColor( Color.green );

                        break;
                    }

                }

                if( !target.Solved )
                    success = false;
            }



            if( success )
            {
                // Go to the next scene?
                Debug.Log( "MASTERFUL PERFORMANCE" );

                // Display a success message?
                solved = true;
                // Start the next level coroutine
                StartCoroutine( NextLevel( 3.0f ) );

            }
        }
	}

    public IEnumerator NextLevel( float delay )
    {

        yield return new WaitForSeconds( delay );

        foreach( ScreenProjector sp in objects )
        {
            if( sp.GetComponent<PerspectiveLock>( ).Solved )
                sp.GetComponent<PerspectiveLock>( ).ReleaseSuccessLock( );
        }

        levels[ curr_target_index ].ClearSuccessLines( );
        levels[ curr_target_index ].gameObject.SetActive( false );

        curr_target_index++;

        if( curr_target_index < levels.Count )
        {
            curr_targets = levels[ curr_target_index ].GetSuccessTargets( );
            solved = false;
        }
    }
}
