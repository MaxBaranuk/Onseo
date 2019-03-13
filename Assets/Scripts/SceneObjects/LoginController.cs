using System;
using DataModel;
using Network;
using UnityEngine;
using UnityEngine.UI;

namespace SceneObjects
{
//     public class LoginController : MonoBehaviour
//     {
//          [SerializeField] private InputField loginInput;
//          [SerializeField] private InputField passwordInput;
//          [SerializeField] private Button loginButton;
//          [SerializeField] private Button createUserButton;
//
//          private void Awake()
//          {
//               loginInput.onValueChanged.AddListener(login => loginButton.interactable = !string.IsNullOrEmpty(login));
//               loginButton.onClick.AddListener(Login);
//          }
//
//          private async void Login()
//          {
//               UiController.Instance.LoadingBarEnable(true);
//               var credentials = new Credentials {userId = loginInput.text, Password = passwordInput.text};
//               var res = await NetworkProvider.Login(credentials);
//
//               UiController.Instance.LoadingBarEnable(false);
//               if (res.IsError)
//               {
//                    UiController.Instance.ShowInfo(res.ErrorMessage);                  
//                    return;
//               }
//
//               switch (res.Value.Status)
//               {
//                    case Status.Ok:
//                         gameObject.SetActive(false);
//                         break;
//                    case Status.IncorrectPassword:
//                         UiController.Instance.ShowInfo("Incorrect password");   
//                         break;
//                    case Status.UnknownLogin:
//                         UiController.Instance.ShowInfo("There is no user with this name"); 
//                         break;
//               }
//          }
//     }
}
