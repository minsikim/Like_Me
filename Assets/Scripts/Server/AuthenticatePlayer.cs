using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthenticatePlayer : MonoBehaviour
{
    public InputField displayNameInput, userNameInput, passwordInput;


    // 계정이름과 비밀번호로 로그인
    public void onAuthorizePlayerButtonClick()
    {
        new GameSparks.Api.Requests.AuthenticationRequest()
            .SetUserName(userNameInput.text)
            .SetPassword(passwordInput.text)
            .Send((response) => {
                if (!response.HasErrors)
                {
                    Debug.Log("Authentication Successful: " + response.UserId + ", " + response.ToString());
                }
                else
                {
                    Debug.Log("Authentication Fail: " + response.Errors.JSON.ToString());
                }
            });
    }

    // DisplayName 으로 로그인
    public void onAuthorizePlayerNickNameButtonClick()
    {
        new GameSparks.Api.Requests.DeviceAuthenticationRequest()
            .SetDisplayName(displayNameInput.text)
            .Send((response) => {
                if (!response.HasErrors)
                {
                    Debug.Log("Authentication Successful: " + response.UserId+", "+response.ToString());
                }
                else
                {
                    Debug.Log("Authentication Fail: " + response.Errors.JSON.ToString());
                }
            });
    }
}
