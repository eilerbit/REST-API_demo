using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DisplayController : MonoBehaviour
{
    [SerializeField] private ButtonData buttonPrefab;

    private List<ButtonData> buttons = new List<ButtonData>();
        
    public void SpawnButton(HSL color, bool updated, string id, string text)
    {        
        ButtonData button = Instantiate(buttonPrefab, transform);

        button.Initialize(color, updated, id, text);

        button.name = "Button " + id;

        buttons.Add(button);

        button.transform.SetSiblingIndex(int.Parse(id) - 1);        
    }

    public void SpawnButton(HSL color, bool updated, string id, string text, int swapListIndex)
    {
        ButtonData button = Instantiate(buttonPrefab, transform);

        button.Initialize(color, updated, id, text);

        button.name = "Button " + id;

        buttons[swapListIndex] = button;

        for (int i = 0; i < buttons.Count; i++)
        {
            if (int.Parse(id) > int.Parse(buttons[i].id.text))
                button.transform.SetSiblingIndex(buttons[i].transform.GetSiblingIndex() + 1);
        }
    }    

    public void DeleteCurrentButtons()
    {
        foreach (var buttonObject in buttons)
        {
            Destroy(buttonObject.gameObject);
        }

        buttons.Clear();
    }

    public int DeleteCurrentButtons(int id)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (int.Parse(buttons[i].id.text) == id)
            {                
                Destroy(buttons[i].gameObject);

                buttons[i] = null;
                                
                return i;                                
            }
        }
         
        return -1;
    }
}
