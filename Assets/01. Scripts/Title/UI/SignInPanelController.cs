using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public struct SignInData
{
    public string username;
    public string password;
}

public struct SignInResult
{
    public int result;
}

public class SignInPanelController : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    public void OnClickSignIn()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            //TODO: 빈 값이 있다면 입력 요청 팝업 표시
            return;
        }

        SignInData signInData = new SignInData();
        signInData.username = username;
        signInData.password = password;
        
        StartCoroutine(Signin(signInData));
    }

    IEnumerator Signin(SignInData signInData)
    {
        string jsonData = JsonUtility.ToJson(signInData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest www = new UnityWebRequest(Constants.ServerURL + "/users/signin",
                   UnityWebRequest.kHttpVerbPOST))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {

            }
            else
            {
                var resultString = www.downloadHandler.text;
                var result = JsonUtility.FromJson<SignInResult>(resultString);

                if (result.result == 0)
                {
                    //유효하지 않은 유저 이름
                }
                else if (result.result == 1)
                {
                    //패스워드가 유효하지 않음
                }
                else if (result.result == 2)
                {
                    //로그인 성공
                }
            }
        }
    }

    public void OnClickSignUp()
    {
        
    }
}
