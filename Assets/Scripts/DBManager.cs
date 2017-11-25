using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DBManager : MonoBehaviour {


	SqliteDatabase sqlDB;

	public Text _name;
	public Text _number;

	void Awake() 
	{
		string dbPath = System.IO.Path.Combine (Application.persistentDataPath, "game.db");
		var dbTemplatePath = System.IO.Path.Combine(Application.streamingAssetsPath, "default.db");

		if (!System.IO.File.Exists(dbPath)) {
			// game database does not exists, copy default db as template
			if (Application.platform == RuntimePlatform.Android)
			{
				// Must use WWW for streaming asset
				WWW reader = new WWW(dbTemplatePath);
				while ( !reader.isDone) {}
				System.IO.File.WriteAllBytes(dbPath, reader.bytes);
			} else {
				System.IO.File.Copy(dbTemplatePath, dbPath, true);
			}       
		}
		print (dbPath);
		sqlDB = new SqliteDatabase(dbPath);
	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void click(){
		var result = sqlDB.ExecuteQuery("SELECT * FROM example");

		for(int i = 0; i < result.Rows.Count; i++){
			var row = result.Rows[i];
			print("name=" + (string)row["name"]);
			print("dummy=" + (int)row["dummy"]);
		}

	

	}

	public void add(){
		string query = "INSERT INTO example (name, dummy) VALUES (\'" + _name.text + "\'," + _number.text + ");";
		print (query);

		sqlDB.ExecuteNonQuery(query);
	}
}
