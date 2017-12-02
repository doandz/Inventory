using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewItemPanel : MonoBehaviour {

	[SerializeField]
	InputField _name;
	[SerializeField]
	Dropdown _category;
	[SerializeField]
	Dropdown _brand;
	[SerializeField]
	Dropdown _unitScale;
	[SerializeField]
	InputField _scale;
	[SerializeField]
	InputField _price;
	[SerializeField]
	InputField _quantity;


	[SerializeField]
	DBManager db;
	[SerializeField]
	UIManager ui;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Show(){
		gameObject.SetActive (true);

		//--reset categories
		_category.ClearOptions ();
		DataTable categoriesData = db.categories;

		List<string> categoryNames = new List<string>();
		//categoryNames.Add ("unset");
		for (int i = 0; i < categoriesData.Rows.Count; i++) {
			var row = categoriesData.Rows[i];

			categoryNames.Add ((string)row["name"]);
		}
		_category.AddOptions (categoryNames);

		//--reset brands
		_brand.ClearOptions ();
		DataTable brandsData = db.brands;

		List<string> brandNames = new List<string>();
		//brandNames.Add ("unset");
		for (int i = 0; i < brandsData.Rows.Count; i++) {
			var row = brandsData.Rows[i];
			brandNames.Add ((string)row["name"]);
		}
		_brand.AddOptions (brandNames);


		//--reset unitScale
		_unitScale.ClearOptions ();
		DataTable unitScaleData = db.unit_scales;

		List<string> unitScaleNames = new List<string>();
		//unitScaleNames.Add ("unset");
		for (int i = 0; i < unitScaleData.Rows.Count; i++) {
			var row = unitScaleData.Rows[i];
			unitScaleNames.Add ((string)row["name"]);
		}
		_unitScale.AddOptions (unitScaleNames);

		_name.text = "";
		_scale.text = "0";
		_price.text = "0";
		_quantity.text = "0";
		_category.value = 0;
		_brand.value = 0;
		_unitScale.value = 0;
	}

	public void cancel(){
		gameObject.SetActive (false);
	}

	public void create(){
		if (_name.text.Length == 0 || db.getItem(_name.text).id != 0) {
			print ("error");
			return;
		}

		HardwareItem item = new HardwareItem ();
		item.category = _category.value + 1;
		item.brand = _brand.value + 1;
		item.unitScale = _unitScale.value + 1;

		item.name = _name.text;
		item.scale = float.Parse(_scale.text);
		item.price = float.Parse(_price.text);
		item.quantity = int.Parse(_quantity.text);


		db.AddNewItem (item);

		ui.refreshList ();
		gameObject.SetActive (false);


	}
}
