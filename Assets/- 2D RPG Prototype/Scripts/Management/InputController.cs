using UnityEngine;

namespace RPGPrototype.Management
{
    [System.Serializable]
    public class InputController : MonoBehaviour
    {
        public float mouseSensitivity = 1f;

        public static float ForwardBack { get; private set; }
        public static float LeftRight { get; private set; }
        public static float Horizontal { get; private set; }
        public static float Vertical { get; private set; }
        
        public static bool Weapon { get; private set; }
        public static bool WeaponClick { get; private set; }
        public static bool Block { get; private set; }
        public static bool BlockClick { get; private set; }
        public static bool Escape { get; private set; }
        public static bool Inventory { get; private set; }

        public static bool Item1 { get; private set; }
        public static bool Item2 { get; private set; }
        public static bool Item3 { get; private set; }
        public static bool Item4 { get; private set; }
        public static bool Item5 { get; private set; }
        public static bool Item6 { get; private set; }
        public static bool Item7 { get; private set; }
        public static bool Item8 { get; private set; }
        public static bool Item9 { get; private set; }
        public static bool Item0 { get; private set; }

        private static bool use;

        public static bool Use
        {
            get
            {
                bool b = use;
                use = false;
                return b;
            }
        }

        void Update()
        {
            ForwardBack = 0;
            LeftRight = 0;
            if (Input.GetKey(KeyCode.W)) ForwardBack = 1;
            if (Input.GetKey(KeyCode.S)) ForwardBack = -1;
            if (Input.GetKey(KeyCode.A)) LeftRight = -1;
            if (Input.GetKey(KeyCode.D)) LeftRight = 1;
            Horizontal = Input.GetAxis("Mouse X") * mouseSensitivity;
            Vertical = -Input.GetAxis("Mouse Y") * mouseSensitivity;

            WeaponClick = (Input.GetMouseButtonDown(0));
            BlockClick = (Input.GetMouseButtonDown(1));

            use = Input.GetKeyDown(KeyCode.E);
            Weapon = Input.GetKey(KeyCode.Mouse0);
            Block = Input.GetKey(KeyCode.Mouse1);
            Escape = Input.GetKeyDown(KeyCode.Escape);
            Inventory = Input.GetKeyDown(KeyCode.I);

            Item1 = Input.GetKeyDown(KeyCode.Alpha1);
            Item2 = Input.GetKeyDown(KeyCode.Alpha2);
            Item3 = Input.GetKeyDown(KeyCode.Alpha3);
            Item4 = Input.GetKeyDown(KeyCode.Alpha4);
            Item5 = Input.GetKeyDown(KeyCode.Alpha5);
            Item6 = Input.GetKeyDown(KeyCode.Alpha6);
            Item7 = Input.GetKeyDown(KeyCode.Alpha7);
            Item8 = Input.GetKeyDown(KeyCode.Alpha8);
            Item9 = Input.GetKeyDown(KeyCode.Alpha9);
            Item0 = Input.GetKeyDown(KeyCode.Alpha0);
        }
    }
}
