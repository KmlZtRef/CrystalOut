using System;
using System.Collections.Generic;
using System.Linq;
using _02._Script.Options;
using UnityEngine;

public class OptionSelector : MonoBehaviour
{
    [SerializeField] private OptionContainer containerPrf;
    [SerializeField] private Transform contents;
    [SerializeField] private OptionData[] options;
    
    private List<OptionContainer> containers = new List<OptionContainer>();
 
    private void Start()
    {
        bool isFirst = true;
        foreach (OptionData option in options)
        {
            var container = Instantiate(containerPrf, contents);
            container.Initialize(option);
            container.OnSelect += OpenContainer;
            if (isFirst) container.Open();
            else container.Close();
            
            isFirst = false;
            
            containers.Add(container);
        }
    }

    private void OpenContainer(string optionName)
    {
        var container = containers.FirstOrDefault(c => c.ContainerName == optionName);
        if (container != null)
            container.Open();
        else
            Debug.Log("Container not found");
    }
}
