using UnityEngine;

namespace UI.MainMenu
{
    public static class ResolutionHelper
    {
        public const float DefaultRatio = 1.7f;

        public static void ScaleByAspect(this GameObject gameObject, float cameraAspect)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1) * cameraAspect / DefaultRatio;
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