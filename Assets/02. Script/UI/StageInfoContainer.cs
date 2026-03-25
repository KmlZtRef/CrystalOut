using _02._Script.Stage;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class StageInfoContainer : MonoBehaviour
{
	[SerializeField] private StageDataSo stageData;
	[SerializeField] private Text titleTmp;
	[SerializeField] private Text descriptionTmp;
	[SerializeField] private Button selectButton;

	public void Initialize(StageDataSo stageData)
	{
		this.stageData = stageData;
		titleTmp.text = stageData.StageName;
		descriptionTmp.text = stageData.StageDescription;
		selectButton.onClick.AddListener(HandleButtonClick);
	}

	private void HandleButtonClick()
	{
		StageManager.Instance.LoadStage(stageData);
	}
}
