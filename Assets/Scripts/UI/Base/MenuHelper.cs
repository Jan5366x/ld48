using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Base
{
    public static class MenuHelper
    {
        public const float DefaultRatio = 1.7f;
        private const float DefaultPixelHeight = 1080;


        public static void ScaleByAspect(this GameObject gameObject, float cameraAspect)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1) * cameraAspect / DefaultRatio;
        }

        public static void ScaleByAspectAndPixelHeight(this GameObject gameObject, Camera camera, int minWidth)
        {
            var byPixelHeight = camera.pixelHeight / DefaultPixelHeight;
            Debug.Log($"Pixel Height: {byPixelHeight}");

            float scaleWidth = 1;

            if (camera.pixelWidth < minWidth)
            {
                scaleWidth = camera.pixelWidth / (float) minWidth;
            }

            gameObject.transform.localScale = new Vector3(1, 1, 1) * (byPixelHeight * scaleWidth);
        }

        public static void Show<TMenu>(this List<BaseMenu> menus) where TMenu : BaseMenu
        {
            if (TryMenu<TMenu>(menus, out var menu))
            {
                menu.Show();
            }
        }

        public static void Hide<TMenu>(this IEnumerable<BaseMenu> menus) where TMenu : BaseMenu
        {
            if (TryMenu<TMenu>(menus, out var menu))
            {
                menu.Hide();
            }
        }

        private static bool TryMenu<TMenu>(IEnumerable<BaseMenu> menus, out TMenu menu) where TMenu : BaseMenu
        {
            menu = (TMenu) menus.FirstOrDefault(f => f.GetType() == typeof(TMenu));

            if (menu != null)
            {
                return true;
            }

            Debug.Log($"menu type not found {typeof(TMenu)}");
            return false;
        }
    }
}