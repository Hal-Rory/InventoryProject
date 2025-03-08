using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    public class HeaderCard : Card
    {
        [SerializeField] protected Text _header;

        public string HeaderText
        {
            get =>
                _header != null ? _header.text : string.Empty;
            private set
            {
                if (_header == null)
                {
                    return;
                }
                _header.text = value;
            }
        }

        public void Set(string id, string header, string label = "", Sprite sprite = null)
        {
            base.Set(id, label, sprite);
            HeaderText = header;
        }

        public void SetHeader(string header)
        {
            HeaderText = header;
        }

        public void SetEmpty(string header = "", string label = "")
        {
            base.SetEmpty(label);
            HeaderText = header;
        }
    }
}