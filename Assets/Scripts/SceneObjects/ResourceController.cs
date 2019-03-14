using System.Threading.Tasks;
using DataModel;
using Network;
using UnityEngine;
using UnityEngine.UI;

namespace SceneObjects
{   
    public class ResourceController : MonoBehaviour
    {
        public ResourceType type;
        [SerializeField] Text valueLabel;
        [SerializeField] Button postButton;
        [SerializeField] InputField valueField;

        private void Start()
        {
            postButton.onClick.AddListener(PostValue);
        }

        public Task<ValueOrError<Resource>> UpdateState()
        {
            return NetworkProvider.GetResource(type);
        }

        private async void PostValue()
        {
            var val = valueField.text;
            if (string.IsNullOrEmpty(val))
            {
                UiController.Instance.ShowInfo("Field can not be empty");
                return;
            }
      
            var result = await NetworkProvider.PostResource(new Resource { 
                Type = type, 
                UserId = UiController.Instance.currentUser, 
                Value = int.Parse(val)});

            if (result.IsError)
            {
                UiController.Instance.ShowInfo(result.ErrorMessage);
                return;
            }
            
            SetValue(result.Value.Value);
        }

        public void SetValue(float value)
        {
            valueLabel.text = ((int) value).ToString();
        }
    }
}
