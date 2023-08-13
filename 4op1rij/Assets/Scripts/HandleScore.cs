using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class HandleScore : MonoBehaviour
{

	public void StartScoreInsert()
	{
		StartCoroutine(ScoreInsert());
	}
	IEnumerator ScoreInsert()
	{
		int id = 1;
		string password = "Steen";

		using (UnityWebRequest www = UnityWebRequest.Get($"https://studenthome.hku.nl/~jogchum.hofma/NetworkingJogchum/Insert_Score.php"))
		{
			yield return www.SendWebRequest();

			if (www.result != UnityWebRequest.Result.Success)
			{
				Debug.Log(www.error);
			}
			else
			{
				Debug.Log(www.downloadHandler.text);

			}
		}

		yield return null;
	}

}
