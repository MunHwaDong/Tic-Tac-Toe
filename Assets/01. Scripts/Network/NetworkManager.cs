using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : Singleton<NetworkManager>
{
    public async void Request(IDataFormat data)
    {
        try
        {
            byte[] bodyRaw = DataSerialize(data.GetJsonData());

            using (UnityWebRequest www = new UnityWebRequest(Constants.ServerURL + "users/signup",
                       UnityWebRequest.kHttpVerbPOST))
            {
                www.uploadHandler = new UploadHandlerRaw(bodyRaw);
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");

                await www.SendWebRequest();
            
                if (www.result == UnityWebRequest.Result.ConnectionError ||
                    www.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log("Error : " + www.error);

                    if (www.responseCode == 409)
                    {
                        //TODO: 중복 사용자 생성 팝업 표시
                        Debug.Log("중복 사용자");
                    }
                }
                else
                {
                    var result = www.downloadHandler.text;
                    Debug.Log(result);
                
                    //TODO: 회원가입 성공 팝업 표시
                }
            }
        }
        catch (Exception e)
        {
            throw; // TODO handle exception
        }
    }
    
    private byte[] DataSerialize(string data)
    {
        return System.Text.Encoding.UTF8.GetBytes(data);
    }
}

public interface IDataFormat
{
    string GetJsonData();
}

public class RequestData : IDataFormat
{
    private string _username;
    private string _nickname;
    private string _password;

    public RequestData(string username, string nickname, string password)
    {
        _username = username;
        _nickname = nickname;
        _password = password;
    }
    
    public string GetJsonData()
    {
        return JsonUtility.ToJson(this);
    }
}