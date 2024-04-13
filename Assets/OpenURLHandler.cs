using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURLHandler : MonoBehaviour
{
    private WebViewObject webViewObject;

    void Start()
    {
        webViewObject = GetComponent<WebViewObject>();
    }

    public void OpenURL(string url)
    {
        // Check if the WebViewObject is available
        if (webViewObject != null)
        {
            // Open the URL in the WebViewObject
            webViewObject.LoadURL(url);

            // Show the WebViewObject
            webViewObject.SetVisibility(true);
        }
        else
        {
            Debug.LogError("WebViewObject component not found.");
        }
    }
}
