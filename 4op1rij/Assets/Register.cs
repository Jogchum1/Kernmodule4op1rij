using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
	public InputField mailInput;	
	public InputField usernameInput;	
	public InputField passwordInput;

	public GameObject LoginObject;

	// Start is called before the first frame update
	public void RegisterUserButton()
	{
		StartCoroutine(RegisterUser());
	}

	IEnumerator RegisterUser()
	{
		string mail = mailInput.text;
		string username = usernameInput.text;
		string pw = passwordInput.text;
		

		using (UnityWebRequest www = UnityWebRequest.Get($"https://studenthome.hku.nl/~jogchum.hofma/NetworkingJogchum/Inserting.php?name={username}&mail={mail}&pw={pw}"))
		{
			yield return www.SendWebRequest();

			if (www.result != UnityWebRequest.Result.Success)
			{
				Debug.Log(www.error);
			}
			else
			{
				Debug.Log(www.downloadHandler.text);

				LoginObject.SetActive(true);
				gameObject.SetActive(false);
				// We're probably expexting something in return:
				//JObject json = JObject.Parse(www.downloadHandler.text);
				//int sessionID = (int)json["sessionid"];
				//Debug.Log("Login Complete!");
			}
		}

		yield return null;
	}
}
