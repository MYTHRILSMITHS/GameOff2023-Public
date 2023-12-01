using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using System;
using Unity.Services.Core.Environments;
using TMPro;

public class UGSManager : MonoBehaviour
{
    [SerializeField] private string CurrentEnvironment = "development";
    [SerializeField] private TextMeshProUGUI text;

    private async void Awake()
    {
        await InitializeUnityServices();
        await SignInAnonymously();
    }

    async Task InitializeUnityServices()
    {
        // set environment and initalize unity services
        try
        {
            var initOptions = new InitializationOptions();
            initOptions.SetEnvironmentName(CurrentEnvironment);
            await UnityServices.InitializeAsync(initOptions);
            Debug.Log("Initialized");
        }
        catch (Exception e)
        {
            Debug.Log("Unity Services initialize exception: " + e);
        }
    }

    // If a session token is cached on the SDK, then the SignInAnonymouslyAsync() method recovers the
    // existing credentials of the cached user, regardless whether they signed in anonymously or through
    // a platform account. If there is no user sign-in information, this method creates a new anonymous user.
    async Task SignInAnonymously()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            await AuthenticationService.Instance.GetPlayerNameAsync();
            Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId);
            text.SetText(AuthenticationService.Instance.PlayerName);
        }
        catch (Exception e)
        {
            Debug.Log("Sign in exception: " + e);
            text.SetText("Error, Can't Sign In.");
        }
    }
}
