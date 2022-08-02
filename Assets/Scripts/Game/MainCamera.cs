
using ECS;
using Game.Input;
using MVC;
using MVC.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public static class MainCamera
    {
        public static Camera camera;

        public static void Init()
        {
            camera = Camera.main;
        }
    }
}
