using System.Threading.Tasks;
using DataModel;
using Network;
using UnityEngine;
using UnityEngine.UI;

namespace SceneObjects
{
    public class UiController : MonoBehaviour
    {
        public static UiController Instance;
        [SerializeField] GameObject loadingBar;
        [SerializeField] Text infoLabel;
        [SerializeField] private InputField loginInput;
        [SerializeField] private InputField passwordInput;
        [SerializeField] private Button loginButton;
        [SerializeField] private Button createUserButton;
        [SerializeField] private InputField loginCreateInput;
        [SerializeField] private InputField passwordCreateInput;
        

        private void Awake()
        {
            Instance = this;
            loginInput.onValueChanged.AddListener(login => loginButton.interactable = !string.IsNullOrEmpty(login));
            loginButton.onClick.AddListener(Login);
            createUserButton.onClick.AddListener(CreateUser);
        }

        private async void Login()
        {
            LoadingBarEnable(true);
            var credentials = new Credentials {userId = loginInput.text, Password = passwordInput.text};
            var res = await NetworkProvider.Login(credentials);

            LoadingBarEnable(false);
            if (res.IsError)
            {
                ShowInfo(res.ErrorMessage);                  
                return;
            }

            switch (res.Value.Status)
            {
                case Status.Ok:
                    loginButton.transform.parent.gameObject.SetActive(false);
                    break;
                case Status.IncorrectPassword:
                    ShowInfo("Incorrect password");   
                    break;
                case Status.UnknownLogin:
                    ShowInfo("There is no user with this name"); 
                    break;
            }
        }

        private async void CreateUser()
        {
            LoadingBarEnable(true);
            
            var credentials = new Credentials {userId = loginCreateInput.text, Password = passwordCreateInput.text};
            var res = await NetworkProvider.CreateUser(credentials);
            LoadingBarEnable(false);
            
            if (res.IsError)
            {
                ShowInfo(res.ErrorMessage);                  
                return;
            }
            
            switch (res.Value.Status)
            {
                case Status.Ok:
                    createUserButton.transform.parent.gameObject.SetActive(false);
                    break;
                case Status.LogInInUse:
                    ShowInfo("This name is already used");   
                    break;
            }       
        }

        public void LoadingBarEnable(bool isEnable)
        {
            loadingBar.SetActive(isEnable);
        }

        public async void ShowInfo(string info)
        {
            infoLabel.gameObject.SetActive(true);
            infoLabel.text = info;
            await Task.Delay(3000);
            infoLabel.gameObject.SetActive(false);
        }
    }
}
