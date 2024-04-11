using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Simple utility script to check for internet availability
/// Written by Andy (@rmfz)
/// </summary>
public class InternetAvailability : MonoBehaviour
{
    /// <summary>
    /// Returns true if device is connected to the internet.
    /// Returns false otherwise
    /// </summary>
    /// <returns></returns>
    public static bool IsConnectedToInternet()
    {
        NetworkReachability networkReachability = Application.internetReachability;
        if (networkReachability == NetworkReachability.NotReachable)
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
