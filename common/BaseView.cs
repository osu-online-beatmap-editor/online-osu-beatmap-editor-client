using System.Collections.Generic;

namespace online_osu_beatmap_editor_client.common
{
    internal class BaseView
    {
        private List<BaseUIComponent> components = new List<BaseUIComponent>();

        public void AddComponent(BaseUIComponent component)
        {
            components.Add(component);
        }

        public void Draw()
        {
            foreach (var component in components)
            {
                component.Draw();
            }
        }

        public void Update()
        {
            foreach (var component in components)
            {
                component.Update();
            }
        }
    }
}
