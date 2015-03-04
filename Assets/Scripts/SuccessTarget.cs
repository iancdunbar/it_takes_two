using UnityEngine;
using System.Collections;
using Vectrosity;

// This class specifies a target area for the perspective objects
public class SuccessTarget : MonoBehaviour {

    private float SUCCESS_DIST = 25;

    [SerializeField]
    private Vector2 aspect_ratio;

    [SerializeField]
    private Rect target_screen_rect;

    private Rect target_size;

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
            points[ i ].y += target_size.height;
        }

        bool result = true;

        bool tr = false;
        bool tl = false;
        bool br = false;
        bool bl = false;

        for( int i = 0; i < points.Length; i++ )
        {
            Vector2 point = points[ i ];
            

            if( !target_size.Contains( point ) )
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

        //float w = target_screen_rect.width * Screen.width;

        float max_w = Screen.height * aspect_ratio.x / aspect_ratio.y;
        float min_w = Screen.width / 15;

        float max_h = Screen.width * aspect_ratio.y / aspect_ratio.x;
        float min_h = Screen.height / 15;

        float w = Random.RandomRange( min_w, max_w );
        float h = w * aspect_ratio.y / aspect_ratio.x / ( (float)Screen.width / (float)Screen.height ); //target_screen_rect.height * Screen.height;

        while( h > max_h || h < min_h )
        {
            Debug.Log( "Was of improper y" );
            w = Random.RandomRange( min_w, max_w );
            h = w * aspect_ratio.y / aspect_ratio.x / ( (float) Screen.width / (float) Screen.height );
        }

        float x = Random.RandomRange( 0, Screen.width - w ); //target_screen_rect.x * Screen.width;
        float y = Random.RandomRange( h, Screen.height ); //target_screen_rect.y * Screen.height;

        target_size = new Rect( x, y, w, h );


        target_lines.MakeRect( target_size );
        target_lines.Draw( );

        top_left = new Vector2( target_size.xMin, target_size.yMax );// - target_size.height );
        top_right = new Vector2( target_size.xMax, target_size.yMax );// - target_size.height );

        bottom_left = new Vector2( target_size.xMin, target_size.yMin );// - target_size.height );
        bottom_right = new Vector2( target_size.xMax, target_size.yMin );// - target_size.height );

        initialized = true;
    }

    public void Shutdown( )
    {
        solved = false;
        
        ClearLines( );

        initialized = false;
    }

}
