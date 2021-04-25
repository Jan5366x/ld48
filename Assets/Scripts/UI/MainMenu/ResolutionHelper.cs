using UnityEngine;

namespace UI.MainMenu
{
    public static class ResolutionHelper
    {
        public const float DefaultRatio = 1.7f;
        public const float DefaultPixelHeight = 1080;

        public static void ScaleByAspect(this GameObject gameObject, Camera camera)
        {
            gameObject.ScaleByAspect(camera.aspect);
        }
        
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
                scaleWidth = camera.pixelWidth / (float)minWidth;
            }
            
            gameObject.transform.localScale = new Vector3(1, 1, 1) * (byPixelHeight * scaleWidth);
        }

        public static void ScalePositionByAspect(this GameObject gameObject, float camerAspect)
        {
            var position = gameObject.transform.position;
            
            position =  new Vector3(
                position.x,
                position.y * camerAspect / DefaultRatio,
                position.z);
            
            gameObject.transform.position = position;
        }
    }
}