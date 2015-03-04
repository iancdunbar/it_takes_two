using UnityEngine;
using System.Collections;

public class SimpleGameController : MonoBehaviour {

    private static SimpleGameController _instance;

    private bool menu_active = false;

    [SerializeField]
    private GameObject _playbutton;

    [SerializeField]
    private GameObject _aboutpanel;

    [SerializeField]
    private GameObject _aboutbutton;

    [SerializeField]
    private GameObject _titlelabel;

    [SerializeField]
    private GameObject _menubutton;

    [SerializeField]
    private GameObject _resetbutton;

    [SerializeField]
    private GameObject _returnbutton;

    public static SimpleGameController Get 
    { 
        get
        {
            if( _instance == null )
            {
                _instance = GameObject.FindObjectOfType<SimpleGameController>( );

                if( _instance == null )
                {
                    GameObject sgc = new GameObject( );
                    _instance = sgc.AddComponent<SimpleGameController>( );
                }
            }

            return _instance;
        }
    }

    void Awake( )
    {
        if( _instance == null )
            _instance = this;
        else if( _instance != this )
            Destroy( this.gameObject );

        DontDestroyOnLoad( this );
    }

    public void OnClickStartGame( )
    {

        Application.LoadLevel( 1 );

        NGUITools.SetActive( _playbutton, false );
        NGUITools.SetActive( _aboutbutton, false );
        NGUITools.SetActive( _aboutpanel, false );
        NGUITools.SetActive( _titlelabel, false );
        NGUITools.SetActive( _menubutton, true );
    }

    public void OnClickMenu( )
    {
        // Toggle the current menu buttons
        TweenPosition tp_return = _returnbutton.GetComponent<TweenPosition>( );
        TweenPosition tp_reset = _resetbutton.GetComponent<TweenPosition>( );

        if( menu_active )
        {
            // Then hide the menu
            //tp_return.AddOnFinished( on_return_del );
            tp_return.PlayReverse( );

            //tp_reset.AddOnFinished( on_reset_del );
            tp_reset.PlayReverse( );
        }
        else
        {
            _returnbutton.SetActive( true );
            _resetbutton.SetActive( true );

            tp_return.PlayForward( );
            tp_reset.PlayForward( );
        }

        menu_active = !menu_active;
    }

    public void OnClickReset( )
    {
        Tango.PoseProvider.ResetMotionTracking( );
    }

    public void OnClickReturn( )
    {
        // This will hide the menu
        OnClickMenu( );

        NGUITools.SetActive( _playbutton, true );
        NGUITools.SetActive( _aboutbutton, true );
        NGUITools.SetActive( _aboutpanel, false );
        NGUITools.SetActive( _titlelabel, true );
        NGUITools.SetActive( _menubutton, false );

        Application.LoadLevel( 0 );
    }

    public void OnClickAbout( )
    {
        NGUITools.SetActive( _aboutpanel, !_aboutpanel.activeSelf );
    }

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        if( Input.GetKeyDown( KeyCode.Escape ) )
        {
            Application.Quit( );
        }
	}
}
