using Cysharp.Threading.Tasks;
using SimpleJSON;
using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static InputHandler;

public class DataLoader : MonoBehaviour, IIdChecker, IEffectTrigger
{
    [SerializeField] private string URL;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private DisplayController displayController;
    [SerializeField] private GlitchFilter filter;

    public Action OnClick { get ; set ; }
        
    private void Awake()
    {
        inputHandler.Initialize(this);
        filter.Initialize(this);
    }

    public void Create()
    {
        OnClick?.Invoke();

        AddItem().Forget();
    }

    public void DeleteItem()
    {
        OnClick?.Invoke();

        inputHandler.Enable(DeleteOneItem);                
    }

    public void UpdateItem()
    {
        OnClick?.Invoke();

        inputHandler.Enable(UpdateOneItem);                
    }

    public void Refresh()
    {
        OnClick?.Invoke();

        inputHandler.Enable(FetchItems, refresh: true);
    }

    private async UniTaskVoid AddItem()
    {                
        string json = JsonUtility.ToJson("");

        using (UnityWebRequest request = UnityWebRequest.Post(URL, json, "application/json"))
        {
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {
                string output = request.downloadHandler.text;

                Debug.Log(output);
            }
        } 
    }
        
    public async UniTaskVoid DeleteOneItem(int id)
    {
        string json = JsonUtility.ToJson("");
        string myURL = URL + "/" + id.ToString();
        using (UnityWebRequest request = UnityWebRequest.Delete(myURL))
        {
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {
                string output = request.responseCode.ToString();

                Debug.Log(output);
            }
        }
    }

    public async UniTaskVoid UpdateOneItem(int id)
    {
        string myURL = URL + "/" + id.ToString();
                
        var data = new UpdatedData(true);
        
        var json = JsonUtility.ToJson(data);        

        Debug.Log(json); 
      
        using (UnityWebRequest request = UnityWebRequest.Put(myURL, json))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {
                string output = request.downloadHandler.text;

                Debug.Log(output);
            }
        }
    }

    private async UniTaskVoid FetchItems(int id)
    {
        string myURL = "";

        if (id == 0) myURL = URL;
        else myURL = URL + "/" + id.ToString();

        using (UnityWebRequest request = UnityWebRequest.Get(myURL))
        {
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {                
                string json = request.downloadHandler.text;

                SimpleJSON.JSONNode buttons = SimpleJSON.JSON.Parse(json);
                                
                if (id == 0)
                {
                    displayController.DeleteCurrentButtons();

                    for (int i = 0; i < buttons.Count; i++)
                    {
                        displayController.SpawnButton(getHSL(buttons[i]["color"].ToString()), buttons[i]["updated"], buttons[i]["id"], buttons[i]["text"]);
                    }
                }

                else
                {
                    int swapIndex = displayController.DeleteCurrentButtons(id);

                    if(swapIndex >= 0) displayController.SpawnButton(getHSL(buttons["color"].ToString()), buttons["updated"], buttons["id"], buttons["text"], swapIndex);
                }
                    
            }
        }
    }

    private HSL getHSL(string rString)
    {        
        char[] splitChars = {',', '[', ']', ' '};

        string[] temp = rString.Split(splitChars);
                
        float H = float.Parse(temp[1], new CultureInfo("en-US").NumberFormat);
        float S = float.Parse(temp[2], new CultureInfo("en-US").NumberFormat);
        float L = float.Parse(temp[3], new CultureInfo("en-US").NumberFormat);

        HSL rValue = new HSL(H, S, L);
        
        return rValue;
    }

    public async UniTask<bool> CheckId(int id)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            await request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {                
                string json = request.downloadHandler.text;
                
                SimpleJSON.JSONNode buttons = SimpleJSON.JSON.Parse(json);
                
                for (int i = 0; i < buttons.Count; i++)
                {
                    if (int.Parse(buttons[i]["id"]) == id) return true;
                }                
            }

            return false;
        }
    }    
    
}






