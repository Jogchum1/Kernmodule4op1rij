using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class ServerLogin : MonoBehaviour
{
	public Text serverStatus;

	void Start()
	{
		StartCoroutine(LoginServer());
	}
	IEnumerator LoginServer()
	{
		int id = 1;
		string password = "Steen";

		using (UnityWebRequest www = UnityWebRequest.Get($"https://studenthome.hku.nl/~jogchum.hofma/NetworkingJogchum/server_login.php?id={id}&pw={password}"))
		{
			yield return www.SendWebRequest();

			if (www.result != UnityWebRequest.Result.Success)
			{
				Debug.Log(www.error);
			}
			else
			{
				Debug.Log(www.downloadHandler.text);

				serverStatus.text = "Server status: Logged In!";
			}
		}

		yield return null;
	}

}
