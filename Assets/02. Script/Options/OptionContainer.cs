using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using SceneManager = UnityUtilities.SceneManagements.SceneManager;
using Text = TMPro.TextMeshProUGUI;

namespace _02._Script.Options
{
    public class OptionContainer : MonoBehaviour
    {
        [SerializeField] private Transform contents;
        [SerializeField] private OptionSelectButton buttonPrf;
        [SerializeField] private Button backButton; 
        [SerializeField] private Text titleText;
        [SerializeField] private string containerName;
        [SerializeField] private string parentName;
        [SerializeField] private float closedXPos = 720;
        public event Action<string> OnOpenContainer;
        public event Action<string> OnOpenPanel;
    
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
            foreach (OptionNameId d in optionData.datas)
            {
                var btn = Instantiate(buttonPrf, contents);
                btn.Initialize(d.Id, d.Name, d.OnClickAction);
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

        private async void SelectOption(string option, OptionNameId.OnClickActionEnum action)
        {
            try
            {
                Close();

                switch (action)
                {
                    case OptionNameId.OnClickActionEnum.ToOtherCategory:
                        OnOpenContainer?.Invoke(option);
                        break;
                    case OptionNameId.OnClickActionEnum.LoadScene:
                        await SceneManager.Instance.LoadSceneAsync(option);
                        break;
                    case OptionNameId.OnClickActionEnum.OpenPanel:
                        OnOpenPanel?.Invoke(option);
                        break;
                    default:
                        Debug.LogWarning("Unexpected Enum");
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[OptionContainer] Exception : {e.Message}");
            }
        }

        private void GoBack()
        {
            SelectOption(parentName, OptionNameId.OnClickActionEnum.ToOtherCategory);
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
}
