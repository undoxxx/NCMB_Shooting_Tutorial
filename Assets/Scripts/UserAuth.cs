using UnityEngine;
using System.Collections;
using NCMB;
using System.Collections.Generic;

public class UserAuth : MonoBehaviour {
	private string currentPlayerName;

	public void logIn(string id, string pw){
		NCMBUser.LogInAsync(id, pw, (NCMBException e) => {
			if(e==null){
				currentPlayerName = id;
			}
		});
	}

	public void signUp(string id, string mail, string pw){
		NCMBUser user = new NCMBUser();
		user.UserName = id;
		user.Email = mail;
		user.Password = pw;
		user.SignUpAsync ((NCMBException e) => {
			if(e==null){
				currentPlayerName = id;
			}
		});
	}

	public void logOut(){
		NCMBUser.LogOutAsync ((NCMBException e) => {
			if (e == null) {
				currentPlayerName = null;
			}
		});
	}

	public string currentPlayer(){
		return currentPlayerName;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// シングルトン化する ------------------------
	
	private UserAuth instance = null;
	void Awake ()
	{
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
			
			string name = gameObject.name;
			gameObject.name = name + "(Singleton)";
			
			GameObject duplicater = GameObject.Find (name);
			if (duplicater != null) {
				Destroy (gameObject);
			} else {
				gameObject.name = name;
			}
		} else {
			Destroy (gameObject);
		}
	}
}
