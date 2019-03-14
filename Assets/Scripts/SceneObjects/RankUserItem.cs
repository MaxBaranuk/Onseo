using DataModel;
using UnityEngine;
using UnityEngine.UI;

namespace SceneObjects
{
    public class RankUserItem : MonoBehaviour
    {
        [SerializeField] private Text nameField;
        [SerializeField] private Text valueField;
        
        public void SetValue(RankUser user)
        {
            nameField.text = user.name;
            valueField.text = user.rank.ToString();
        }
    }
}
