using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public struct SignUpData
{
    public string username;
    public string password;
    public string nickname;
}

public class SignUpPanelController : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField nicknameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField confirmPasswordInputField;
    
    public void OnClickConfirmButton()
    {
        var username = usernameInputField.text;
        var nickname = nicknameInputField.text;
        var password = passwordInputField.text;
        var confirmPassword = confirmPasswordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(password) ||
            string.IsNullOrEmpty(confirmPassword))
        {
            //TODO: 입력값이 비어있음을 알리는 팝업창 표시
            return;
        }

        if (password.Equals(confirmPassword))
        {
            SignUpData signUpData = new SignUpData();
            signUpData.username = username;
            signUpData.password = password;
            signUpData.nickname = nickname;
            
            //서버로 SignUpData 전달하면서 회원가입 진행
            StartCoroutine(Signup(signUpData));
        }
    }

    IEnumerator Signup(SignUpData signupData)
    {
        string jsonStr = JsonUtility.ToJson(signupData);
        
        //서버로 전달하기 위해 byte타입으로 직렬화
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonStr);
        
        //범위를 벗어나면 객체 사용 종료 (리소스 정리)
        //내 로컬 컴퓨터를 서버로 만든다 -> 내 로컬 컴퓨터의 users폴더에 signup에 요청을 보낸다
        using (UnityWebRequest www = new UnityWebRequest(Constants.ServerURL + "/users/signup",
                   UnityWebRequest.kHttpVerbPOST))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            
            yield return www.SendWebRequest();

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

    public void OnClickCancelButton()
    {
        
    }
}
