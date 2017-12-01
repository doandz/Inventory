using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {


	[SerializeField]
	GameObject mainMenu;

	[SerializeField]
	GameObject accessPanel;

	[SerializeField]
	InputField itemName;

	[SerializeField]
	Dropdown categories;

	[SerializeField]
	Dropdown brands;

	[SerializeField]
	Dropdown unitScales;

	[SerializeField]
	InputField units;

	[SerializeField]
	ScrollRect itemListView;


	[SerializeField]
	GameObject loading;


	public GameObject ItemEntry;

	DBManager db;

	// Use this for initialization
	IEnumerator Start () {


		print ("+Start");

		//mainMenu.SetActive (true);
		accessPanel.SetActive (true);
		loading.SetActive (true);


		db = gameObject.GetComponent<DBManager> ();

		yield return StartCoroutine(db.load());


		loading.SetActive (false);
		resetFilterUI ();
		Filter ();

		print ("-Start");
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
		foreach (Transform child in itemListView.content.transform) {
			GameObject.Destroy(child.gameObject);
		}
		RectTransform rt = itemListView.content.GetComponent<RectTransform> ();
		rt.sizeDelta = new Vector2 (rt.sizeDelta.x, 0);

		ItemFilter filter = new ItemFilter ();

		filter.name = itemName.text;
		filter.category = categories.value;
		filter.brand = brands.value;
		filter.unitScale = unitScales.value;
		filter.scale = 0;//float.Parse(units.text);//(float)units.text;

		List<HardwareItem> list = db.getItemsWithFilter (filter);
		print ("============================================================");
		int counter = 0;
		foreach (var item in list) {


			GameObject itemClone = (GameObject) Instantiate(ItemEntry);

			itemClone.transform.SetParent(itemListView.content.transform);

			itemClone.transform.localPosition = new Vector3 (457, -20-(40 * counter), 0);
			ItemEntry itemEntry = itemClone.GetComponent<ItemEntry> ();

			itemEntry.setProperties (item.name, db.getCategoryName(item.category), db.getBrandName(item.brand), db.getUnitScaleName(item.unitScale), item.scale, item.quantity);

			print (item.name + " | " + item.category + " | " + item.brand + " | " + item.unitScale + " | " + item.scale);


			//itemListView.GetComponent<RectTransform> ().rect.height = itemListView.GetComponent<RectTransform> ().rect.height + ((40 * counter)); 

			//itemListView;

			//itemListVie

			//RectTransform rt = itemListView.content.GetComponent<RectTransform> ();
			rt.sizeDelta = new Vector2 (rt.sizeDelta.x, (40 * counter) + 50);
			counter++;
		}
	}


	public void AccessButtonClick(int p_type){
		switch (p_type) {
		case 0: // add
			break;
		case 1: // remove
			break;
		case 2: // update
			break;
		case 3: // new
			break;
		default:
			break;
		}
	
	}

	public void SetSelected(GameObject p_itemEntry){
		
	}
}
