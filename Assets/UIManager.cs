using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject accessPanel;
	public InputField itemName;
	public Dropdown categories;
	public Dropdown brands;
	public Dropdown unitScales;
	public InputField units;

	DBManager db;

	// Use this for initialization
	void Start () {

		mainMenu.SetActive (true);
		accessPanel.SetActive (false);


		db = gameObject.GetComponent<DBManager> ();
		db.click ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//--------

	void resetFilterUI(){

		itemName.text = "";
		//--reset categories
		categories.ClearOptions ();
		DataTable categoriesData = db.categories;

		List<string> categoryNames = new List<string>();
		categoryNames.Add ("unset");
		for (int i = 0; i < categoriesData.Rows.Count; i++) {
			var row = categoriesData.Rows[i];

			categoryNames.Add ((string)row["name"]);
		}
		categories.AddOptions (categoryNames);

		//--reset brands
		brands.ClearOptions ();
		DataTable brandsData = db.brands;

		List<string> brandNames = new List<string>();
		brandNames.Add ("unset");
		for (int i = 0; i < brandsData.Rows.Count; i++) {
			var row = brandsData.Rows[i];

			brandNames.Add ((string)row["name"]);
		}
		brands.AddOptions (brandNames);


		//--reset unitScale
		unitScales.ClearOptions ();
		DataTable unitScaleData = db.unit_scales;

		List<string> unitScaleNames = new List<string>();
		unitScaleNames.Add ("unset");
		for (int i = 0; i < unitScaleData.Rows.Count; i++) {
			var row = unitScaleData.Rows[i];
			unitScaleNames.Add ((string)row["name"]);
		}
		unitScales.AddOptions (unitScaleNames);

		units.text = "";
	}





	//------- menus

	public void MenuView(){
		mainMenu.SetActive (false);
		accessPanel.SetActive (true);
		resetFilterUI ();
	}

	public void MenuAdd(){
		mainMenu.SetActive (false);
		accessPanel.SetActive (true);
		resetFilterUI ();
	
	}

	public void MenuRemove(){
		mainMenu.SetActive (false);
		accessPanel.SetActive (true);
		resetFilterUI ();
	}

	public void MenuAdmin(){
	
	}

	public void Category(bool p_add){
	
	}

	public void Brand(bool p_add){
	
	}

	public void UnitScale(bool p_add){
		
	}

	public void ExitToMainMenu(){
		mainMenu.SetActive (true);
	}

	public void Filter(){
		ItemFilter filter = new ItemFilter ();

		filter.name = itemName.text;
		filter.category = categories.value;
		filter.brand = brands.value;
		filter.unitScale = unitScales.value;
		filter.scale = 0;//float.Parse(units.text);//(float)units.text;

		List<HardwareItem> list = db.getItemsWithFilter (filter);
		print ("============================================================");
		foreach (var item in list) {
			print (item.name + " | " + item.category + " | " + item.brand + " | " + item.unitScale + " | " + item.scale);
		
		}
	}
}
