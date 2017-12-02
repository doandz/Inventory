using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour {
	
	public static AppManager instance = null;

	[SerializeField]
	DBManager db;

	[SerializeField]
	UIManager ui;

	void Awake()
	{
		//Check if instance already exists
		if (instance == null) //if not, set instance to this
			instance = this;
		
		//If instance already exists and it's not this:
		else if (instance != this) //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		//Get a component reference to the attached BoardManager script
		//boardScript = GetComponent<BoardManager>();

		//Call the InitGame function to initialize the first level 
		InitGame();
	}

	public UIManager getUIManager(){
		return ui;
	}

	public DBManager getDB(){
		return db;
	}

	void InitGame(){
	
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
