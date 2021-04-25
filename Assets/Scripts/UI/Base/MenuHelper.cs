using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Base
{
    public static class MenuHelper
    {
        private const float DefaultRatio = 1.7f;
        private const float DefaultPixelHeight = 1080;

        public static void ScaleByAspect(this GameObject gameObject, float cameraAspect)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1) * cameraAspect / DefaultRatio;
        }

        public static void ScaleByAspectAndPixelHeightAndMinWidth(this GameObject gameObject, Camera camera, int minWidth)
        {
            var byPixelHeight = camera.pixelHeight / DefaultPixelHeight;

            float scaleWidth = 1;

            if (camera.pixelWidth < minWidth)
            {
                scaleWidth = camera.pixelWidth / (float) minWidth;
            }

            gameObject.transform.localScale = new Vector3(1, 1, 1) * (byPixelHeight * scaleWidth);
        }

        public static void Show<TMenu>(this List<BaseMenu> menus) where TMenu : BaseMenu
        {
            GetMenu<TMenu>(menus).Show();
        }

        public static void Hide<TMenu>(this IEnumerable<BaseMenu> menus) where TMenu : BaseMenu
        {
            GetMenu<TMenu>(menus).Hide();
        }

        private static TMenu GetMenu<TMenu>(IEnumerable<BaseMenu> menus) where TMenu : BaseMenu
        {
            return (TMenu) menus.First(f => f.GetType() == typeof(TMenu));
        }
    }
}