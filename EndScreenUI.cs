/*
	End Screen UI code architectire for Night Terrors

	@Author Mark Toledo
*/

//This class creates all the TableViewController for each player
public class EndScreenManager : MonoBehaviour {
	//PlayerDataTracker is the class used to store every players raw analytics
	[SerializeField]
	List<PlayerDataTracker> pdtList = new List<PlayerDataTracker>();
	//Prefab used to create data
	[SerializeField]
	TableViewController tvcPrefab;
	void OnEnable()
	{
		InitializeTableViews();
		CreateTableViews();
	}
	void InitializeTableViews(){
		for(int i = 0; i < pdtList.Count; i++){
			TableModel tb = new TableModel();

			PropertyHandler[] phArr = {
				new PropertyHandler("Kills", (float)(pdtList[i].iSploderKillCount + pdtList[i].iSnufferKillCount + pdtList[i].iSnatcherKillCount)),
				new PropertyHandler("Flashlight",pdtList[i].fFlashLightDmg),
				new PropertyHandler("Hammer",pdtList[i].fHammerDmg),
				new PropertyHandler("Hugs", (float)pdtList[i].iHugAmount),
				new PropertyHandler("Lamps",(float)pdtList[i].iTrapAmount)
			};
			tb.phPropertiesArr = phArr;
			tb.sName = csPlayerSO[i].sName;
			tb.cColor = csPlayerSO[i].colColour;

			CreateTableViews(tb);
		}
	}
	//function used to create the TableViewController
	void CreateTableViews(TableModel tbParam){
		TableViewController tvc = Instantiate(tvcPrefab.gameObject,transform.position,transform.rotation).GetComponent<TableViewController>();
		tvc.transform.SetParent(transform);
		tvc.transform.localScale = Vector3.one;
		tvc.Initialize(tbParam);
	}
}
//TableViewController is a class used to display analytics data in a list like view
public class TableViewController : MonoBehaviour {
	[SerializeField]
	PropertyCell pcCell;

	[SerializeField]
	Text txtName;

	//Acts as a constructor to create the TableView
	public void Initialize(TableModel paramTB){
		txtName.text = paramTB.sName;
		txtName.color = paramTB.cColor;
		foreach(PropertyHandler handler in paramTB.phPropertiesArr){
			PropertyCell cell = Instantiate(pcCell,transform.position,transform.rotation,transform);
			cell.transform.localScale = Vector3.one;
			cell.CellInitialize(handler);
		}
	}
}
//TabelModel is a class to organize data for the TableViewController to be able to easily display it
public class TableModel
{
	public PropertyHandler[] phPropertiesArr;
	public string sName;
	public Color cColor;
}
//PropertyHandler is a model used to store the name and value of a specific data of a player
//This will be injected into the PropertyCell to be displayed
public class PropertyHandler {
	public string sName;
	public float fValue;

	public PropertyHandler(string paramName, float paramValue){
		sName = sParam;
		fValue = fParam;
	}
}
//PropertyCell is a view to display one analytic data in it.
public class PropertyCell : MonoBehaviour {
	public Text txtProperty;

	//acts as a constructor to setup the propertycell
	public void CellInitialize(PropertyHandler handlerParam){
		txtProperty.text = handlerParam.sName + ": " + CleanUpNumber(handlerParam.fValue);
	}
	//Rounds of the number and adds a "K" it is over 1000. Used to make numbers more readable
	private string CleanUpNumber(float fparam){
		string sNumber = "";
		float roundedNum = Mathf.Round(fparam);
		float newNum = roundedNum / 1000;
		sNumber = newNum + "K";
		if(newNum < 1 ){
			sNumber = roundedNum.ToString();
		}
		return sNumber;
	}
}
