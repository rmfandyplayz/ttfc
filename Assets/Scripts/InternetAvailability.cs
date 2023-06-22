using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


//this script checks whether there is internet connection. will be used for many different purposes.

/// <summary>
/// the function ur lookin for is probably <b>IsConnectedToInternet()</b>
/// </summary>
public class InternetAvailability : MonoBehaviour
{
    /// <summary>
    /// Returns true if device is connected to internet, returns false otherwise.
    /// </summary>
    /// <returns></returns>
    public static bool IsConnectedToInternet()
    {
        NetworkReachability networkReachability = Application.internetReachability;
        if(networkReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Not connected to internet");
            return false;
        }
        else
        {
            Debug.Log("Connected to internet");
            return true;
        }
    }
}
