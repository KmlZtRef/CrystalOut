using System.Collections.Generic;
using System.Linq;
using _02._Script.EventParams;
using GameManagements;
using UnityEngine;

namespace _02._Script.Options
{
    public class OptionSelector : MonoBehaviour
    {
        [SerializeField] private OptionContainer containerPrf;
        [SerializeField] private Transform contents;
        [SerializeField] private OptionData[] options;
    
        private List<OptionContainer> _containers = new List<OptionContainer>();
 
        private void Start()
        {
            bool isFirst = true;
            foreach (OptionData option in options)
            {
                var container = Instantiate(containerPrf, contents);
                container.Initialize(option);
                container.OnOpenContainer += ChangeCategory;
                container.OnOpenPanel += OpenPanel;
                if (isFirst) container.Open();
                else container.Close();
            
                isFirst = false;
            
                _containers.Add(container);
            }
            
            MessageBus.Subscribe<RequestChangeCategory>(ChangeCategory);
        }

        private void ChangeCategory(RequestChangeCategory param)
        {
            ChangeCategory(param.CategoryName);
        }

        private void ChangeCategory(string optionName)
        {
            var container = _containers.FirstOrDefault(c => c.ContainerName == optionName);
            if (container != null)
                container.Open();
            else
                Debug.LogWarning("Container not found");
        }

        private void OpenPanel(string panelName)
        {
            MessageBus.Publish(new RequestSetPanelActive() { PanelName = panelName, Active = true});
        }
    }
}
