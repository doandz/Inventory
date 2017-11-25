using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DBManager : MonoBehaviour {


	SqliteDatabase sqlDB;

	DataTable brands;
	DataTable categories;
	DataTable unit_scales;

	void Awake() 
	{
		string dbPath = System.IO.Path.Combine (Application.persistentDataPath, "game.db");
		var dbTemplatePath = System.IO.Path.Combine(Application.streamingAssetsPath, "inventory.db");

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

		brands = sqlDB.ExecuteQuery("SELECT * FROM brands");
		categories = sqlDB.ExecuteQuery("SELECT * FROM categories");
		unit_scales = sqlDB.ExecuteQuery("SELECT * FROM unit_scale");
	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}



	string getCategoryName(int p_id){
		for (int i = 0; i < categories.Rows.Count; i++) {
			DataRow row = categories.Rows[i];
			if (p_id == (int)row ["id"])
				return (string)row ["name"];
		}
		return "unknown category";
	}

	string getBrandName(int p_id){
		for (int i = 0; i < brands.Rows.Count; i++) {
			DataRow row = brands.Rows[i];
			if (p_id == (int)row ["id"])
				return (string)row ["name"];
		}

		return "unknown brand";
	}

	string getUnitScaleName(int p_id){
		for (int i = 0; i < unit_scales.Rows.Count; i++) {
			DataRow row = unit_scales.Rows[i];
			if (p_id == (int)row ["id"])
				return (string)row ["name"];
		}
		return "unknown unit scale";
	}


	public void click(){

		print ("CLICK");
		var result = sqlDB.ExecuteQuery("SELECT * FROM items");

		for(int i = 0; i < result.Rows.Count; i++){
			var row = result.Rows[i];

			string _logData = "id = " + (int)row ["id"] + " | name = " + (string)row ["name"] + " | category = " + getCategoryName ((int)row ["category_id"]) + " | brand = " + getBrandName ((int)row ["brand_id"]) + " | unit_scale = " + getUnitScaleName ((int)row ["unit_id"]) + " : " + (string)row ["scale"] + " | price = " + (int)row ["price"] + " | quantity = " + (int)row ["quantity"];

			print (_logData);
			/*
			print("id=" + (int)row["id"]);
			print("name=" + (string)row["name"]);
			print("category=" + getCategoryName((int)row["category_id"]));
			print("brand=" + getBrandName((int)row["brand_id"]));
			print("unit_scale=" + getUnitScaleName((int)row["unit_id"]) + ":" + (string)row["scale"] );

			print("price=" + (int)row["price"]);
			print("quantity=" + (int)row["quantity"]);

			print ("===============================");
			*/
		}

	

	}

}
