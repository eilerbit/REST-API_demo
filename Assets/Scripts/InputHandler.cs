using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button refreshAllButton;

    private IIdChecker checker;

    public delegate UniTaskVoid onClick(int id);

    private onClick callback;

    public void Initialize(IIdChecker checker)
    {
        this.checker = checker;        
    }

    public void Enable(onClick callback, bool refresh = false)
    {
        if(refresh) refreshAllButton.gameObject.SetActive(true);

        else refreshAllButton.gameObject.SetActive(false);

        gameObject.SetActive(true);

        this.callback = callback;

        inputField.text = "";
        text.text = "";
    }

    public async void ValidateInput()
    {
        string input = inputField.text;

        if (int.TryParse(input, out int result))
        {            
            if (await checker.CheckId(result))
            {                
                callback(result).Forget();
                gameObject.SetActive(false);
            }

            else text.text = $"{result} is not a valid Id";
        }

        else
        {
            text.text = $"{input} is not a valid Input, please use integers";
        }
    }

    public void RefreshAll()
    {
        callback(0).Forget();
        gameObject.SetActive(false);
    }
}
