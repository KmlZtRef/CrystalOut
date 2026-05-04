using System;
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

	public event Action<StageDataSo> OnButtonClick; 

	public void Initialize(StageDataSo stageData)
	{
		this.stageData = stageData;
		titleTmp.text = stageData.StageName;
		descriptionTmp.text = stageData.StageDescription;
		selectButton.onClick.AddListener(HandleButtonClick);
	}

	private void HandleButtonClick()
	{
		OnButtonClick?.Invoke(stageData);
		
		// Legacy:
		// StageManager.Instance.LoadStage(stageData);
	}
}
