using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GameSparks.Api.Requests;

public class AuthenticatePlayer : MonoBehaviour
{
    public InputField displayNameInput, userNameInput, passwordInput;

    [SerializeField]
    private Text currUserDisplayName;
    [SerializeField]
    private Text authState;

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
        Debug.Log("onAuthorizePlayerNickNameButtonClick");
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

    public void onGoogleLoginButtonClick()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            // 연결된 게임스파크 서버 응용 프로그램과 토큰을 교환
            .RequestServerAuthCode(false)
            // 해당유저의 이메일 주소를 요청
            // 이게 추가되면 어플리케이션이 처음 실행될 때 동의창이 열림
            .RequestEmail()
            .Build();

        //초기화
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(success =>
        {
            Debug.Log("Google SignIn Successful: " + success);
            if(success == false)
            {
                return;
            }
            Debug.Log("GetServerAuthCode - " + PlayGamesPlatform.Instance.GetServerAuthCode());
            Debug.Log("GetIdToken - " + PlayGamesPlatform.Instance.GetIdToken());
            Debug.Log("Email - " + ((PlayGamesLocalUser)Social.localUser).Email);
            Debug.Log("GoogleId - " + Social.localUser.id);
            Debug.Log("UserName - " + Social.localUser.userName);
            Debug.Log("UserName - " + PlayGamesPlatform.Instance.GetUserDisplayName());

            GameSpaekGoogleLoginConnect();

        });
    }
    // 게임스파크와 구글로그인을 연동시키는 함수
    void GameSpaekGoogleLoginConnect()
    {
        // 유저 이름
        string displayName = PlayGamesPlatform.Instance.GetUserDisplayName();
        // 인증 코드
        string AuthCode = PlayGamesPlatform.Instance.GetServerAuthCode();

        new GooglePlayConnectRequest()
            .SetRedirectUri("http://www.gamesparks.com/oauth2callback")
            .SetCode(AuthCode)
            .SetDisplayName(displayName)
            .Send((googleplayAuthResponse) =>
            {
                if (!googleplayAuthResponse.HasErrors)
                {
                    authState.text = "구글, 게임스파크 계정 로그인 연동 완료";
                    currUserDisplayName.text = googleplayAuthResponse.DisplayName;
                }
                else
                {
                    authState.text = googleplayAuthResponse.Errors.JSON;
                    currUserDisplayName.text = "실패";
                }
            });
    }

}
