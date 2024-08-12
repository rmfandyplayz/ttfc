using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Simple utility script to check for internet availability
/// 
/// Written by Andy (@rmfz)
/// </summary>
public class CheckWifi : MonoBehaviour
{
	public static bool connectedToInternet;
	
	void Start()
	{
		NetworkReachability networkReachability = Application.internetReachability;
		if (networkReachability == NetworkReachability.NotReachable)
		{
			Debug.Log("Not connected to internet");
			connectedToInternet = false;
		}
		else
		{
			Debug.Log("Connected to internet");
			connectedToInternet = true;
		}
	}
}
