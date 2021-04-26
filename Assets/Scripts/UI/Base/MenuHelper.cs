using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace UI.Base
{
    public static class MenuHelper
    {
        /// <summary>
        /// Der Standard Ausgang ist Full HD. Von dort aus wird rauf oder runter gerechnet.
        /// </summary>
        private const float DefaultAspectRatio = 1.7f;
        
        /// <summary>
        /// Wird fast nicht verwendet. Findet nur einsatz bei sehr kleinen Bildschirmen (z.B. 800x600).
        /// </summary>
        private const float DefaultPixelHeight = 1080;

        /// <summary>
        /// Skaliert das Ziel UI Element auf die aktuell verwendete Bildschirm verhältnis.
        /// </summary>
        /// <param name="gameObject">Instanze muss von ein Typ von GameObject sein.</param>
        /// <param name="aspectRatio">Uebergabe des Bildschirmverhaeltnis.</param>
        public static void ScaleByAspect(this GameObject gameObject, float aspectRatio)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1) * aspectRatio / DefaultAspectRatio;
        }

        /// <summary>
        /// Skaliert das Ziel UI Element auf die aktuell verwendete Bildschirm verhältnis und Bildhoehe.
        /// Wird der mindest Breite unterschritten, wird zusaetzlich die Breite fuer die berechnung
        /// der Skalierung mit eingezogen.
        /// </summary>
        /// <param name="gameObject">Instanze muss von ein Typ von GameObject sein.</param>
        /// <param name="camera">Verwendet Hoehe und Breite aus der Kamera, um die skalierung zu berechnen.</param>
        /// <param name="minWidth">Mindest Pixel Anzahl der Bildschirmauflösung Breite.</param>
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

        /// <summary>
        /// Ruft aus dem Element die Abstrakte Methode 'Show' auf aus dem BaseUiElement.
        /// </summary>
        /// <param name="menus">Liste verwendete UI Bereiche.</param>
        /// <typeparam name="TMenu">Die Instanze muss von BaseUiElement erben.</typeparam>
        public static void Show<TMenu>(this IEnumerable<BaseUiElement> menus) where TMenu : BaseUiElement
        {
            GetMenu<TMenu>(menus).Show();
        }

        /// <summary>
        /// Ruft aus dem Element die Abstrakte Methode 'Hide' auf aus dem BaseUiElement.
        /// </summary>
        /// <param name="menus">Liste verwendete UI Bereiche.</param>
        /// <typeparam name="TMenu">Die Instanze muss von BaseUiElement erben.</typeparam>
        [UsedImplicitly]
        public static void Hide<TMenu>(this IEnumerable<BaseUiElement> menus) where TMenu : BaseUiElement
        {
            GetMenu<TMenu>(menus).Hide();
        }

        /// <summary>
        /// Ruft das Element aus, das als Typ angegeben wurde und gibt diese zuruek.
        /// </summary>
        /// <param name="menus">Liste verwendete UI Bereiche.</param>
        /// <typeparam name="TMenu">Die Instanze muss von BaseUiElement erben.</typeparam>
        /// <returns>Gibt die Instanze zuruek.</returns>
        private static TMenu GetMenu<TMenu>(IEnumerable<BaseUiElement> menus) where TMenu : BaseUiElement
        {
            return (TMenu) menus.First(f => f.GetType() == typeof(TMenu));
        }
    }
}