using UnityEngine;
using System.Collections.Generic;
using Vectrosity;

public enum Direction { TOP = 0, RIGHT, BOTTOM, LEFT }

public class ScreenProjector : MonoBehaviour {

    ////////////////////////////////////////////////////
    // Private Variables
    ////////////////////////////////////////////////////

    private Mesh box_mesh;
    private Camera main_camera;
    private BoxCollider box_collider;
    private VectorLine screen_projection;
    private VectorLine target_lines;
    private Rect target_rect;
    private bool succeeded;

    ////////////////////////////////////////////////////


    ////////////////////////////////////////////////////
    //  Inspector Variables
    ////////////////////////////////////////////////////

    [SerializeField]
    private SuccessTarget tgt;
    [SerializeField]
    private SuccessTarget tgt2;
    [SerializeField]
    private SuccessTarget tgt3;

    ////////////////////////////////////////////////////

    ////////////////////////////////////////////////////
    // Unity Messages
    ////////////////////////////////////////////////////

    void Awake( )
    {
        succeeded = false;
        target_rect = new Rect( Screen.width / 2 - 30, Screen.height / 2 - 100, 60, 200 );
        main_camera = Camera.main;
        box_collider = GetComponent<BoxCollider>( );
        box_mesh = GetComponent<MeshFilter>( ).mesh;

        Vector2[] test_points = new Vector2[ ] { new Vector2( 200, 500), new Vector2( 250, 550), new Vector2( 250, 550), new Vector2( 550, 50 ), new Vector2( 550, 50), new Vector2( 500, 0), new Vector2( 500, 0 ), new Vector2( 200, 500 ) };  

        screen_projection = new VectorLine( "source_rect", new Vector2[16], Color.black, null, 4 );

        screen_projection.Draw( );

        //target_lines = new VectorLine( "target_rect", new Vector2[ 8 ], Color.red, null, 4 );

       // target_lines.MakeRect( target_rect );
       // target_lines.Draw( );
    }

    public Vector3[] GetScreenPoints( )
    {
        Vector3[] mesh_points = box_mesh.vertices;

        for( int i = 0; i < mesh_points.Length; i++ )
        {
            mesh_points[ i ] = transform.TransformPoint( mesh_points[ i ] );
            mesh_points[ i ] = main_camera.WorldToScreenPoint( mesh_points[ i ] );
        }

        Vector3[] main_points = new Vector3[ 8 ];

        for( int i = 0; i < 8; i++ )
        {
            main_points[ i ] = mesh_points[ i ];
        }

        return main_points;

    }


    ////////////////////////////////////////////////////
}
