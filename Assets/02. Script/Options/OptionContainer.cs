using System;
using _02._Script.Options;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
using SceneManager = UnityUtility.SceneManagements.SceneManager;

public class OptionContainer : MonoBehaviour
{
    [SerializeField] private Transform contents;
    [SerializeField] private OptionSelectButton buttonPrf;
    [SerializeField] private Button backButton; 
    [SerializeField] private Text titleText;
    [SerializeField] private bool loadSceneOnClick = false;
    [SerializeField] private string containerName;
    [SerializeField] private string parentName;
    [SerializeField] private float closedXPos = 720;
    public event Action<string> OnSelect;
    
    private RectTransform _rectTransform;
    
    public string ContainerName => containerName;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(OptionData optionData)
    {
        this.containerName = optionData.optionName;
        this.parentName = optionData.parentName;
        this.loadSceneOnClick = optionData.loadSceneOnClick;
        foreach (OptionNameId d in optionData.datas)
        {
            var btn = Instantiate(buttonPrf, contents);
            btn.Initialize(d.Id, d.Name);
            btn.OnClick += SelectOption;
        }

        if (string.IsNullOrEmpty(optionData.parentName))
        {
            backButton.gameObject.SetActive(false);
        }
        else
        {
            backButton.onClick.AddListener(GoBack);
        }
        
        titleText.text = optionData.optionName;
    }

    private void OnDestroy()
    {
        _rectTransform.DOKill();
    }

    private void SelectOption(string option)
    {
        SelectOption(option, false);
    }

    private void SelectOption(string option, bool dontLoadScene)
    {
        Debug.Log($"Selected: {option}");
        Close();
        if (loadSceneOnClick && !dontLoadScene)
        {
            SceneManager.Instance.LoadSceneAsync(option);
        }
        else
        {
            OnSelect?.Invoke(option);
        }
    }

    private void GoBack()
    {
        SelectOption(parentName, true);
    }

    public void Close()
    {
        _rectTransform.DOKill();
        _rectTransform.DOAnchorPosX(closedXPos, 0.5f).SetEase(Ease.OutElastic);
    }

    public void Open()
    {
        _rectTransform.DOKill();
        _rectTransform.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutElastic);
    }
}
