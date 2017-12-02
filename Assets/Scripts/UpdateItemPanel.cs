using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateItemPanel : MonoBehaviour {
	
	[SerializeField]
	Text itemNameID;

	[SerializeField]
	Text itemDescriptions;

	[SerializeField]
	Text stock;

	[SerializeField]
	InputField inputValue;

	[SerializeField]
	Text updateTypeText;

	HardwareItem itemData;
	DBManager db;
	UIManager.UpdateType utype;

	// Use this for initialization
	void Start () {
		db = AppManager.instance.getDB ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Show(UIManager.UpdateType _utype, HardwareItem item){

		itemData = item;
		utype = _utype;
		gameObject.SetActive (true);
		itemNameID.text = item.name + " : " + item.id;
		itemDescriptions.text = db.getCategoryName (item.category) + "\n" + db.getBrandName (item.brand) + "\n" + db.getUnitScaleName (item.unitScale) + " : " + item.scale.ToString () + "\n" + "Php " + item.price;
		stock.text = "Stock : " + item.quantity.ToString ();

		switch(utype){
		case UIManager.UpdateType.ut_add:
			updateTypeText.text = "ADD:";
			break;

		case UIManager.UpdateType.ut_remove:
			updateTypeText.text = "SELL:";
			break;

		case UIManager.UpdateType.ut_update:
			updateTypeText.text = "UPDATE:";
			break;
		default:
			break;
		}

	}

	public void cancel(){
		gameObject.SetActive (false);
	}

	public void apply(){

		bool showError = false;
		int updateValue = int.Parse (inputValue.text);
		int newValue = itemData.quantity;


			
		switch(utype){
		case UIManager.UpdateType.ut_add:
			newValue = newValue + updateValue;
			break;

		case UIManager.UpdateType.ut_remove:
			newValue = newValue - updateValue;
			if (newValue < 0)
				showError = true;
				
			break;

		case UIManager.UpdateType.ut_update:
			newValue = updateValue;
			break;
		default:
			break;
		}


		if (showError) {
			inputValue.text = "";
		} else {
			db.UpdateItemQuantity(itemData.id, newValue);
			AppManager.instance.getUIManager ().Filter ();
			gameObject.SetActive (false);
		}
		
	}


}
