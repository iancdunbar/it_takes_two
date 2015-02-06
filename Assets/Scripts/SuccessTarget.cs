using UnityEngine;
using System.Collections;
using Vectrosity;

// This class specifies a target area for the perspective objects
public class SuccessTarget : MonoBehaviour {

    private float SUCCESS_DIST = 25;

    [SerializeField]
    private Rect target_screen_rect;
    private VectorLine target_lines;

    private Vector2 top_left;
    private Vector2 top_right;
    private Vector2 bottom_left;
    private Vector2 bottom_right;

    private bool printOnce = true;
    private bool solved = false;

    public bool initialized = false;

    public bool Solved { get { return solved; } }

    public void ClearLines( )
    {
        Destroy( target_lines.vectorObject );
    }

    public void ChangeColor( Color tgt_color )
    {
        target_lines.SetColor( tgt_color );
        target_lines.Draw( );
    }

    public bool CheckSuccessful( Vector3[ ] points_3d )
    {
        Vector2[ ] points = new Vector2[ points_3d.Length ];

        for( int i = 0; i < points.Length; i++ )
        {
            
            points[ i ] = points_3d[ i ];
            points[ i ].y += target_screen_rect.height;
        }

        bool result = true;

        bool tr = false;
        bool tl = false;
        bool br = false;
        bool bl = false;

        for( int i = 0; i < points.Length; i++ )
        {
            Vector2 point = points[ i ];
            

            if( !target_screen_rect.Contains( point ) )
            {
                result = false;
                break;
            }

            if( Vector2.Distance( point, top_left ) < SUCCESS_DIST )
            {
               // Debug.Log( "TL " + i );
                tl = true;
            }
            if( Vector2.Distance( point, top_right ) < SUCCESS_DIST )
            {
               // Debug.Log( "TR " + i );
                tr = true;
            }
            if( Vector2.Distance( point, bottom_left ) < SUCCESS_DIST )
            {
               // Debug.Log( "BL " + i );
                bl = true;
            }
            if( Vector2.Distance( point, bottom_right ) < SUCCESS_DIST )
            {
               // Debug.Log( "BR " + i );
                br = true;
            }

        }

        solved = result && tr && tl && br && bl;

        return solved;

    }

    public bool CheckSuccessful( Vector2[ ] points )
    {

        bool result = true;

        bool tr = false;
        bool tl = false;
        bool br = false;
        bool bl = false;

        for( int i = 0; i < points.Length; i++ )
        {
            Vector2 point = points[ i ];


            if( !target_screen_rect.Contains( point ) )
            {
                result = false;
                break;
            }

            if( Vector2.Distance( point, top_left ) < SUCCESS_DIST )
            {
                // Debug.Log( "TL " + i );
                tl = true;
            }
            if( Vector2.Distance( point, top_right ) < SUCCESS_DIST )
            {
                // Debug.Log( "TR " + i );
                tr = true;
            }
            if( Vector2.Distance( point, bottom_left ) < SUCCESS_DIST )
            {
                // Debug.Log( "BL " + i );
                bl = true;
            }
            if( Vector2.Distance( point, bottom_right ) < SUCCESS_DIST )
            {
                // Debug.Log( "BR " + i );
                br = true;
            }

        }

        return result && tr && tl && br && bl;

    }

	// Use this for initialization
    void Awake( )
    {
        if( !initialized )
            Initialize( );
        
    }

    public void Initialize( )
    {
        SUCCESS_DIST = Screen.width * 0.035f;
        Debug.Log( SUCCESS_DIST );

        target_lines = new VectorLine( "target_lines", new Vector2[ 8 ], Color.black, null, 4 );

        target_screen_rect = new Rect( Screen.width * target_screen_rect.x, Screen.height * target_screen_rect.y, Screen.width * target_screen_rect.width, Screen.height * target_screen_rect.height );

        target_lines.MakeRect( target_screen_rect );
        target_lines.Draw( );

        top_left = new Vector2( target_screen_rect.xMin, target_screen_rect.yMax );// - target_screen_rect.height );
        top_right = new Vector2( target_screen_rect.xMax, target_screen_rect.yMax );// - target_screen_rect.height );

        bottom_left = new Vector2( target_screen_rect.xMin, target_screen_rect.yMin );// - target_screen_rect.height );
        bottom_right = new Vector2( target_screen_rect.xMax, target_screen_rect.yMin );// - target_screen_rect.height );

        initialized = true;
    }

}
