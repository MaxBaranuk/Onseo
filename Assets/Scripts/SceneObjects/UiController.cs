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
        [SerializeField] GameObject mainPanel;
        [SerializeField] GameObject loginPanel;
        [SerializeField] GameObject loadingBar;
        [SerializeField] Text infoLabel;
        [SerializeField] private InputField loginInput;
        [SerializeField] private InputField passwordInput;
        [SerializeField] private Button loginButton;
        [SerializeField] private Button createUserButton;
        [SerializeField] private InputField loginCreateInput;
        [SerializeField] private InputField passwordCreateInput;
        [SerializeField] private Button logOutButton;

        public string currentUser;
        private void Awake()
        {
            Instance = this;
            mainPanel.SetActive(false);
            loginPanel.SetActive(true);
            loginInput.onValueChanged.AddListener(login => loginButton.interactable = !string.IsNullOrEmpty(login));
            loginButton.onClick.AddListener(Login);
            createUserButton.onClick.AddListener(CreateUser);
            logOutButton.onClick.AddListener(LogOut);
            
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
                    currentUser = res.Value.userId;
                    loginPanel.SetActive(false);
                    mainPanel.SetActive(true);                    
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

        private async void LogOut()
        {
            LoadingBarEnable(true);
            var credentials = new Credentials {userId = loginInput.text, Password = passwordInput.text, Status = Status.LogOut};
            var res = await NetworkProvider.Login(credentials);
            LoadingBarEnable(false);
            
            if (res.IsError)
            {
                ShowInfo(res.ErrorMessage);                  
                return;
            }
            
            loginPanel.SetActive(true);
            mainPanel.SetActive(false);
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
