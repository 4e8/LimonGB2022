using UnityEngine;

namespace Game
{
    public class GuiHpBar : MonoBehaviour
    {
        public float value = 1;
        [SerializeField] Texture texture;
        float alpha = 1;
        private void OnGUI()
        {
            alpha = GUI.HorizontalSlider(new Rect(10, 50, 100, 100), alpha, 0, 1);
            GUI.color = new Color(1, 1, 1, alpha);

            GUI.Box(new Rect(8, 8, 304, 34), "");
            //GUI.Box(new Rect(10, 10, value * 100, 30), texture);

            GUI.DrawTexture(new Rect(10, 10, value*300, 30), texture, ScaleMode.StretchToFill, true, 10.0F);

        }
        public void SetValue(float v)
        {
            value = v;
        }
        private void Start()
        {
            SetValue(value);
        }
    }
}
