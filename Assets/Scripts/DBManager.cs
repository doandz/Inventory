using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public struct ItemFilter {
	public string name;
	public int category;
	public int brand;
	public int unitScale;
	public float scale;
}

public struct HardwareItem {
	public int id;
	public string name;
	public int category;
	public int brand;
	public int unitScale;
	public float scale;
	public float price;
	public int quantity;
};  

public class DBManager : MonoBehaviour {


	SqliteDatabase sqlDB;

	public DataTable brands;
	public DataTable categories;
	public DataTable unit_scales;

	public List<HardwareItem> items;// = new List<HardwareItem> ();

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



	public string getCategoryName(int p_id){
		for (int i = 0; i < categories.Rows.Count; i++) {
			DataRow row = categories.Rows[i];
			if (p_id == (int)row ["id"])
				return (string)row ["name"];
		}
		return "unknown category";
	}

	public string getBrandName(int p_id){
		for (int i = 0; i < brands.Rows.Count; i++) {
			DataRow row = brands.Rows[i];
			if (p_id == (int)row ["id"])
				return (string)row ["name"];
		}

		return "unknown brand";
	}

	public string getUnitScaleName(int p_id){
		for (int i = 0; i < unit_scales.Rows.Count; i++) {
			DataRow row = unit_scales.Rows[i];
			if (p_id == (int)row ["id"])
				return (string)row ["name"];
		}
		return "unknown unit scale";
	}

	public IEnumerator load(){

		print ("+load");
		items = new List<HardwareItem> ();
		var result = sqlDB.ExecuteQuery("SELECT * FROM items");

		for(int i = 0; i < result.Rows.Count; i++){
			var row = result.Rows[i];

			HardwareItem item = new HardwareItem ();
			item.id = (int)row["id"];
			item.name = (string)row ["name"];
			item.category = (int)row ["category_id"];
			item.brand = (int)row ["brand_id"];
			item.unitScale = (int)row ["unit_id"];
			item.scale =  float.Parse((string)row ["scale"]);
			item.price = (int)row ["price"];
			item.quantity = (int)row ["quantity"];



			items.Add (item);


			//string _logData = "id = " + (int)row ["id"] + " | name = " + (string)row ["name"] + " | category = " + \
			//getCategoryName ((int)row ["category_id"]) + " | brand = " + getBrandName ((int)row ["brand_id"]) + " | unit_scale = " + getUnitScaleName ((int)row ["unit_id"]) + " : " + (string)row ["scale"] + " | price = " + (int)row ["price"] + " | quantity = " + (int)row ["quantity"];

			//print (_logData);
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


		yield return new WaitForSeconds(1);
		print ("-load");

	}

	public List<HardwareItem> getItemsWithFilter(ItemFilter filter){

		List<HardwareItem> retVal = new List<HardwareItem> ();

		foreach (var item in items) {
			
			if (filter.name.Length > 0 && filter.name != item.name)
				continue;
			

			if (filter.category > 0 && filter.category != item.category)
				continue;
			

			if (filter.brand > 0 && filter.brand != item.brand)
				continue;
			

			if (filter.unitScale > 0 && filter.unitScale != item.unitScale)
				continue;
			

			if (filter.scale > 0 && filter.scale != item.scale)
				continue;

			retVal.Add (item);
		}

		return retVal;
	}

	public HardwareItem getItem(int itemID){


		foreach (var item in items) {
			if (item.id == itemID)
				return item;
		}

		HardwareItem didNotFound = new HardwareItem();
		didNotFound.id = 0;
		return didNotFound;
	}

	public HardwareItem getItem(string itemName){
		foreach (var item in items) {
			if (item.name.ToLower() == itemName.ToLower())
				return item;
		}

		HardwareItem didNotFound = new HardwareItem();
		didNotFound.id = 0;
		return didNotFound;
	}

	public void UpdateItemQuantity(int itemID, int quantity){
		string query = "UPDATE items SET quantity = " + quantity.ToString() + " WHERE ID = " + itemID.ToString () + ";";
		sqlDB.ExecuteNonQuery(query);
	}

	public void AddNewItem(HardwareItem newItem){
		string query = "INSERT INTO items (name, category_id, brand_id, unit_id, scale, price, quantity) VALUES (\"" + newItem.name + "\" , " + newItem.category + " , " + newItem.brand + " , " + newItem.unitScale + " , " + newItem.scale + " , " + newItem.price + " , " + newItem.quantity + " );";
		print (query);
		sqlDB.ExecuteNonQuery(query);
	}

}


