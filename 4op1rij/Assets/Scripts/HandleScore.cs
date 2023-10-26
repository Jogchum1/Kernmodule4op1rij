using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class HandleScore : MonoBehaviour
{
	public int score;

	public void StartScoreInsert(int scoreAmount)
	{
		score = scoreAmount;
		StartCoroutine(ScoreInsert());
	}
	IEnumerator ScoreInsert()
	{
		int tmpScore = score;
		Debug.Log(tmpScore + " is temp score");
		int userID = PlayerPrefs.GetInt("userID");
		Debug.Log(userID + " is this my userID?");
		using (UnityWebRequest www = UnityWebRequest.Get($"https://studenthome.hku.nl/~jogchum.hofma/NetworkingJogchum/Insert_Score.php?score={tmpScore}&user_id={userID}"))
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
