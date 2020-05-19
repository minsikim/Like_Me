using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPlayer : MonoBehaviour
{
    public InputField displayNameInput, userNameInput, passwordInput;
    public void onRegisterPlayerButtonClick()
    {
        new GameSparks.Api.Requests.RegistrationRequest()
            .SetDisplayName(displayNameInput.text)
            .SetPassword(passwordInput.text)
            .SetUserName(userNameInput.text)
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Auth Success: "+response.AuthToken);
                }
                else
                {
                    Debug.Log("Auth Failed: " + response.Errors.JSON.ToString());
                }
            }
            );
    }
}
