using online_osu_beatmap_editor_client.components.Container;
using SFML.System;
using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components.Slider;
using System;
using online_osu_beatmap_editor_client.config;
using online_osu_beatmap_editor_client.components.Button;
using online_osu_beatmap_editor_client.components;

namespace online_osu_beatmap_editor_client.views.Editor
{
    public class EditorDetailsBar
    { 
        private UIContainer detailsBar;

        private UIText incpectorObjectType;
        private UIText incpectorTime;
        private UIText incpectorPos;

        public EditorDetailsBar()
        {
            detailsBar = new UIContainer(new Vector2i(853 * 2 - 200, 40), new Vector2i(200, 480 * 2 - 40), 10, ContainerOrientation.Vertical, StyleVariables.colorBg);

            CreateInspectorSection(detailsBar);
            CreateBackgroundDimSection(detailsBar);
            CreateDistanceSnappingSection(detailsBar);
        }
        
        private void CreateInspectorSection(UIContainer detailsBar) {
            UIContainer inspectorContainer = new UIContainer(new Vector2i(0, 0), new Vector2i(180, 0), 10, ContainerOrientation.Vertical, StyleVariables.colorBgSecondary);

            UIText incpectorHeader = new UIText("Inspector", new Vector2i(0, 0), 20);
            incpectorHeader.bold = true;

            UISpacer spacer = new UISpacer(new Vector2i(0, 0) , new Vector2i(180, 15));

            UIText incpectorObjectTypeLabel = new UIText("Object type:", new Vector2i(0, 0), 13);
            incpectorObjectType = new UIText("Hit circle", new Vector2i(0, 0));
            incpectorObjectType.bold = true;

            UIText incpectorTimeLabel = new UIText("Time:", new Vector2i(0, 0), 13);
            incpectorTime = new UIText("91,132ms", new Vector2i(0, 0));
            incpectorTime.bold = true;

            UIText incpectorPosLabel = new UIText("Position:", new Vector2i(0, 0), 13);
            incpectorPos = new UIText("X: 323, 32 Y: 125,63", new Vector2i(0, 0));
            incpectorPos.bold = true;

            inspectorContainer.AddElement(incpectorHeader);
            inspectorContainer.AddElement(spacer);
            inspectorContainer.AddElement(incpectorObjectTypeLabel);
            inspectorContainer.AddElement(incpectorObjectType);
            inspectorContainer.AddElement(incpectorTimeLabel);
            inspectorContainer.AddElement(incpectorTime);
            inspectorContainer.AddElement(incpectorPosLabel);
            inspectorContainer.AddElement(incpectorPos);

            detailsBar.AddElement(inspectorContainer);
        }
        private void CreateBackgroundDimSection(UIContainer detailsBar)
        {
            UIContainer backgroundDimContainer = new UIContainer(new Vector2i(0, 0), new Vector2i(0, 0), 10, ContainerOrientation.Vertical, StyleVariables.colorBgSecondary);

            UIText sliderLabel = new UIText("Background dim", new Vector2i(0, 0));

            UISlider slider = new UISlider(new Vector2i(0, 0), 160);
            slider.sliderValue = EditorData.backgroundDim;
            slider.ValueChanged += (sender, e) => { EditorData.backgroundDim = slider.sliderValue; };

            backgroundDimContainer.AddElement(sliderLabel);
            backgroundDimContainer.AddElement(slider);

            detailsBar.AddElement(backgroundDimContainer);
        }

        private void CreateDistanceSnappingSection(UIContainer detailsBar)
        {
            UIContainer dispanceSnappingContainer = new UIContainer(new Vector2i(0, 0), new Vector2i(0, 0), 10, ContainerOrientation.Vertical, StyleVariables.colorBgSecondary);

            UIText dispanceSnappingLabel = new UIText("Distance snapping", new Vector2i(0, 0));

            UISlider slider = new UISlider(new Vector2i(0, 0), 160);
            slider.sliderValue = EditorData.distanceSnapping;
            slider.ValueChanged += (sender, e) => { EditorData.distanceSnapping = slider.sliderValue; };

            dispanceSnappingContainer.AddElement(dispanceSnappingLabel);
            dispanceSnappingContainer.AddElement(slider);

            detailsBar.AddElement(dispanceSnappingContainer);
        }

        public BaseUIComponent GetComponent()
        {
            return detailsBar;
        }

        private void UpdateSelectedObjectInfo()
        {
            HitCircle circle = EditorData.selectedCircle;
            if (circle == null)
            {
                incpectorObjectType.label = "-";
                incpectorTime.label = "-";
                incpectorPos.label = "-";
                return;
            }

            incpectorObjectType.label = "Hit circle";
            incpectorTime.label = "332,32ms";
            incpectorPos.label = $"X: {circle.pos.X} Y: {circle.pos.Y}";
        }

        public void Update()
        {
            UpdateSelectedObjectInfo();
        }
    }
}
