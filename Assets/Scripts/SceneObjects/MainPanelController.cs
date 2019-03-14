using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Network;
using UnityEngine;
using UnityEngine.UI;

namespace SceneObjects
{
    public class MainPanelController : MonoBehaviour
    {
        [SerializeField] private Button showRankingButton;
        [SerializeField] private Button closeRankingButton;
        [SerializeField] private RankUserItem rankUserItemPrefab;
        [SerializeField] private GameObject scorePanel;
        [SerializeField] private GameObject scoreListHolder;
        private ResourceController[] resources;
        private List<RankUserItem> listItems = new List<RankUserItem>();
        private bool isRunning;

        private void Awake()
        {
            resources = GetComponentsInChildren<ResourceController>();
            showRankingButton.onClick.AddListener(ShowRanking);
            closeRankingButton.onClick.AddListener(CloseRanking);
        }

        private void OnEnable()
        {
            isRunning = true;
            StartStateUpdating();
        }

        private void OnDisable()
        {
            isRunning = false;
        }

        private async void StartStateUpdating()
        {
            while (isRunning)
            {
                UpdateResources();
                await Task.Delay(5000);
            }
        }

        async void UpdateResources()
        {
            UiController.Instance.LoadingBarEnable(true);
            var result = await Task.WhenAll(resources.Select(controller => controller.UpdateState()));
            UiController.Instance.LoadingBarEnable(false);
            if (result.Any(res => res.IsError))
            {
                var errorText = result.First(res => res.IsError).ErrorMessage;
                UiController.Instance.ShowInfo($"Can't update resources - {errorText}");
                return;
            }

            for (var i = 0; i < resources.Length; i++)
            {
                resources[i].SetValue(result[i].Value.Value);
            }
        }

        async void ShowRanking()
        {
            UiController.Instance.LoadingBarEnable(true);
            var userList = await NetworkProvider.GetRanking();
            UiController.Instance.LoadingBarEnable(false);
            if (userList.IsError)
            {
                UiController.Instance.ShowInfo(userList.ErrorMessage);
                return;
            }

            userList.Value.ForEach(user =>
            {
                var item = Instantiate(rankUserItemPrefab, scoreListHolder.transform);
                item.SetValue(user);
                listItems.Add(item);
            });
            scorePanel.SetActive(true);
        }

        void CloseRanking()
        {
            listItems.ForEach(item => Destroy(item.gameObject));
            listItems.Clear();
            scorePanel.SetActive(false);
        }

    }
}
