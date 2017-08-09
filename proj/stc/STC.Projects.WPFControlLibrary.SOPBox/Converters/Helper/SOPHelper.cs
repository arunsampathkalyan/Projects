using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace STC.Projects.WPFControlLibrary.MapControl.Helper
{
    public static class SOPHelper
    {
        public static string GetAssetImageUrl(AssetTypesEnum AssetType, AssetStatusEnum AssetStatus)
        {

            switch (AssetType)
            {
                case AssetTypesEnum.DOTcounters:
                    switch (AssetStatus)
                    {
                        case AssetStatusEnum.NotWorking:
                            return @"Images/icons/Dot_counters_not_working.png";

                        case AssetStatusEnum.UnderMaintenance:
                            return @"Images/icons/Dot_counters_maint.png";

                        case AssetStatusEnum.Working:
                            return @"Images/icons/Dot_counters.png";
                    }
                    break;

                case AssetTypesEnum.EkinRedLightCamera:
                    switch (AssetStatus)
                    {
                        case AssetStatusEnum.NotWorking:
                            return @"Images/icons/ekin_red_light_camera_not_working.png";

                        case AssetStatusEnum.UnderMaintenance:
                            return @"Images/icons/ekin_red_light_camera_maint.png";

                        case AssetStatusEnum.Working:
                            return @"Images/icons/ekin_red_light_camera.png";
                    }
                    break;

                case AssetTypesEnum.SmartTowers:
                    switch (AssetStatus)
                    {
                        case AssetStatusEnum.NotWorking:
                            return @"Images/icons/s_tower_cctv_camera_not_working.png";

                        case AssetStatusEnum.UnderMaintenance:
                            return @"Images/icons/s_tower_cctv_camera_maint.png";

                        case AssetStatusEnum.Working:
                            return @"Images/icons/s_tower_cctv_camera.png";
                    }
                    break;

                case AssetTypesEnum.SpeedGuns:
                    switch (AssetStatus)
                    {
                        case AssetStatusEnum.NotWorking:
                            return @"Images/icons/speed_guns_not_working.png";

                        case AssetStatusEnum.UnderMaintenance:
                            return @"Images/icons/speed_guns_maint.png";

                        case AssetStatusEnum.Working:
                            return @"Images/icons/speed_guns.png";
                    }
                    break;

                case AssetTypesEnum.VitronicMobileRadars:
                    switch (AssetStatus)
                    {
                        case AssetStatusEnum.NotWorking:
                            return @"Images/icons/assets-not-work.png";

                        case AssetStatusEnum.UnderMaintenance:
                            return @"Images/icons/assets-not-work.png";

                        case AssetStatusEnum.Working:
                            return @"Images/icons/assets-marker.png";
                    }
                    break;

                case AssetTypesEnum.VitronicRadars:
                    switch (AssetStatus)
                    {
                        case AssetStatusEnum.NotWorking:
                            return @"Images/icons/vitronic_radars_not_working.png";

                        case AssetStatusEnum.UnderMaintenance:
                            return @"Images/icons/vitronic_radars_maint.png";

                        case AssetStatusEnum.Working:
                            return @"Images/icons/vitronic_radars.png";
                    }
                    break;
            }

            return "";
        }

        public static T FindDescendant<T>(DependencyObject obj) where T : DependencyObject
        {
            // Check if this object is the specified type
            if (obj is T)
                return obj as T;

            // Check for children
            int childrenCount = VisualTreeHelper.GetChildrenCount(obj);
            if (childrenCount < 1)
                return null;

            // First check all the children
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is T)
                    return child as T;
            }

            // Then check the childrens children
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = FindDescendant<T>(VisualTreeHelper.GetChild(obj, i));
                if (child != null && child is T)
                    return child as T;
            }

            return null;
        }
    }
}
