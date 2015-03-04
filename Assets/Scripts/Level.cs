using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour {

    public List<SuccessTarget> GetSuccessTargets( )
    {
        List<SuccessTarget> result = new List<SuccessTarget>( );

        foreach( Transform child in transform )
        {

            
            result.Add( child.GetComponent<SuccessTarget>( ) );
        }

        return result;
    }

    public void ClearSuccessLines( )
    {
        foreach( SuccessTarget st in GetSuccessTargets( ) )
        {
            st.Shutdown( );
        }
    }

}
