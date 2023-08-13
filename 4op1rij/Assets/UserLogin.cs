using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UserLogin : MonoBehaviour
{
	public InputField usernameInput;
	public InputField passwordInput;

	public GameObject registerObj;

    // Start is called before the first frame update
    public void UserLoginButton()
    {
		StartCoroutine(LoginUser());
    }

	public void GoToRegisterButton()
    {
		registerObj.SetActive(true);
		gameObject.SetActive(false);
    }

	IEnumerator LoginUser()
	{
		string userName = usernameInput.text;
		string password = passwordInput.text;

		using (UnityWebRequest www = UnityWebRequest.Get($"https://studenthome.hku.nl/~jogchum.hofma/NetworkingJogchum/user_login.php?name={userName}&pw={password}"))
		{
			yield return www.SendWebRequest();

			if (www.result != UnityWebRequest.Result.Success)
			{
				Debug.Log(www.error);
			}
			else
			{
				Debug.Log(www.downloadHandler.text);
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
				// We're probably expexting something in return:
				//JObject json = JObject.Parse(www.downloadHandler.text);
				//int sessionID = (int)json["sessionid"];
				//Debug.Log("Login Complete!");
			}
		}

		yield return null;
	}
	
    
}
