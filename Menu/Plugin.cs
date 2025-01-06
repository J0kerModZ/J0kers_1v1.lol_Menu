using J0kers1v1LOLMenu.Menu;
using MelonLoader;
using UnityEngine;

namespace J0kers1v1LOLMenu
{
    public class Plugin : MelonMod
    {
        #region Variables

        #region Menu variables
        private Rect menuRect = new Rect(50, 50, 300, 350);
        static bool shouldEnableGUI, shouldESP, shouldWireFrameESP, shouldBoneESP, shouldWireBodyESP, shouldTracers, shouldAimBot, shouldInfAmmo, shouldRapidFire, shouldFlight, shouldSpeed, shouldGod, shouldSpinBot,shouldSpawnPlayer, shouldSpawnShadowClone, shouldSpawnMats, shouldSpawnAmmo;
        #endregion

        #region Tab variables
        private string[] tabNames = { "Visual", "Player", "Weapon", "Server" };
        private int selectedTab = 0;
        #endregion

        #region Color variables
        private Color rgbColor = Color.red;
        private float hue = 0, GuiEnableDelay;
        #endregion

        #endregion

        #region Mod Menu
        public override void OnGUI()
        {
            if (shouldEnableGUI)
            {
                GUI.Box(menuRect, GUIContent.none, GetBackgroundStyle());
                GUILayout.BeginArea(menuRect);

                GUILayout.Space(10);

                GUILayout.Label("J0ker Menu", GetTitleStyle(Color.white));

                GUILayout.BeginHorizontal();
                for (int i = 0; i < tabNames.Length; i++)
                {
                    if (GUILayout.Button(tabNames[i], GetTabStyle(i)))
                    {
                        selectedTab = i;
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                switch (selectedTab)
                {
                    case 0: // Visual Tab
                        DisplayVisualMods();
                        break;
                    case 1: // Player Tab
                        DisplayPlayerMods();
                        break;
                    case 2: // Weapon Tab
                        DisplayWeaponMods();
                        break;
                    case 3: // Server Tab
                        DisplayServerMods();
                        break;
                }

                GUILayout.Space(20);

                GUILayout.Label($"j0kermodz.lol\nMenu Visibility: Tab\nMouse Visibility: Alt", GetTitleStyle(rgbColor));

                GUILayout.EndArea();

                UpdateRGBColor();
            }
        }

        public override void OnUpdate()
        {
            if (Input.GetKey(KeyCode.Tab) && Time.time > GuiEnableDelay)
            {
                GuiEnableDelay = Time.time + 1f;
                shouldEnableGUI = !shouldEnableGUI;
            }

            EnabledMods();
        }

        [System.Obsolete]
        public override void OnApplicationStart()
        {
            shouldEnableGUI = true;
        }
        #endregion

        #region Active Mods

        static void EnabledMods() // If You Make A Mod That Needs To Repeat Add It To Here!
        {
            #region Visual
            if (shouldESP)
            {
                Mods.BoxESP();
            }
            else
            {
                Mods.DestoryBoxes();
            }

            if (shouldWireFrameESP)
            {
                Mods.frameESP();
            }

            if (shouldWireBodyESP)
            {
                Mods.BodyESP();
            }
            else
            {
                Mods.DisableBodyESP();
            }

            if (shouldBoneESP)
            {
                Mods.BoneESP();
            }

            if (shouldTracers)
            {
                Mods.Tracers();
            }
            #endregion

            #region Player
            if (shouldFlight)
            {
                Mods.Flight();
            }

            if (shouldSpeed)
            {
                Mods.SpeedBoost();
            }
            else
            {
                Mods.DisableSpeedBoost();
            }

            if (shouldSpinBot)
            {
                Mods.SpinBot();
            }

            if (shouldGod)
            {
                Mods.GodMode();
            }
            #endregion

            #region Weapon

            if (shouldAimBot)
            {
                Mods.AimBot();
            }
            
            if (shouldInfAmmo)
            {
                Mods.InfAmmo();
            }

            if (shouldRapidFire)
            {
                Mods.RapidFire();
            }
            #endregion

            #region Server
            if (shouldSpawnPlayer)
            {
                Mods.SpawnPlayer();
            }

            if (shouldSpawnShadowClone)
            {
                Mods.SpawnShadowClone();
            }

            if (shouldSpawnMats)
            {
                Mods.SpawnMaterial();
            }

            if (shouldSpawnAmmo)
            {
                Mods.SpawnAmmo();
            }
            #endregion
        }

        #endregion

        #region Tabs
        private void DisplayVisualMods()
        {
            if (GUILayout.Button("Box ESP", GetButtonStyle(shouldESP)))
            {
                shouldESP = !shouldESP;
            }

            if (GUILayout.Button("Wire Frame ESP", GetButtonStyle(shouldWireFrameESP)))
            {
                shouldWireFrameESP = !shouldWireFrameESP;
            }

            if (GUILayout.Button("Body ESP", GetButtonStyle(shouldWireBodyESP)))
            {
                shouldWireBodyESP = !shouldWireBodyESP;
            }

            if (GUILayout.Button("Bone ESP", GetButtonStyle(shouldBoneESP)))
            {
                shouldBoneESP = !shouldBoneESP;
            }

            if (GUILayout.Button("Tracers", GetButtonStyle(shouldTracers)))
            {
                shouldTracers = !shouldTracers;
            }
        }

        private void DisplayPlayerMods()
        {
            if (GUILayout.Button("Fly", GetButtonStyle(shouldFlight)))
            {
                shouldFlight = !shouldFlight;
            }

            if (GUILayout.Button("Speed Boost", GetButtonStyle(shouldSpeed)))
            {
                shouldSpeed = !shouldSpeed;
            }

            if (GUILayout.Button("Spin Bot", GetButtonStyle(shouldSpinBot)))
            {
                shouldSpinBot = !shouldSpinBot;
            }

            if (GUILayout.Button("God Mode", GetButtonStyle(shouldGod)))
            {
                shouldGod = !shouldGod;
            }
        }

        private void DisplayWeaponMods()
        {
            if (GUILayout.Button("Aim Bot", GetButtonStyle(shouldAimBot)))
            {
                shouldAimBot = !shouldAimBot;
            }

            if (GUILayout.Button("Infinite Ammo", GetButtonStyle(shouldInfAmmo)))
            {
                shouldInfAmmo = !shouldInfAmmo;
            }

            if (GUILayout.Button("Rapid Fire", GetButtonStyle(shouldRapidFire)))
            {
                shouldRapidFire = !shouldRapidFire;
            }

            if (GUILayout.Button("Weapon Level Max", GetButtonStyle(false)))
            {
                Mods.InstaKill();
            }
        }

        private void DisplayServerMods()
        {
            if (GUILayout.Button("Spawn Materials", GetButtonStyle(shouldSpawnMats)))
            {
                shouldSpawnMats = !shouldSpawnMats;
            }

            if (GUILayout.Button("Spawn Ammo", GetButtonStyle(shouldSpawnAmmo)))
            {
                shouldSpawnAmmo = !shouldSpawnAmmo;
            }

            if (GUILayout.Button("Spawn Player", GetButtonStyle(shouldSpawnPlayer)))
            {
                shouldSpawnPlayer = !shouldSpawnPlayer;
            }

            if (GUILayout.Button("Spawn Shadow Clone", GetButtonStyle(shouldSpawnShadowClone)))
            {
                shouldSpawnShadowClone = !shouldSpawnShadowClone;
            }

            if (GUILayout.Button("Destroy All Build", GetButtonStyle(false)))
            {
                Mods.DestoryAllBuilds();
            }

            if (GUILayout.Button("Open All Loot Boxes", GetButtonStyle(false)))
            {
                Mods.OpenAllBoxes();
            }

            if (GUILayout.Button("Win Game", GetButtonStyle(false)))
            {
                Mods.EndGame();
            }
        }
        #endregion

        #region Menu Style
        private GUIStyle GetBackgroundStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.normal.background = MakeTex(2, 2, new Color(0.05f, 0.05f, 0.05f, 0.95f));
            return style;
        }

        private GUIStyle GetTitleStyle(Color color)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 18;
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = color;
            return style;
        }

        private GUIStyle GetButtonStyle(bool isActive)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.background = MakeTex(2, 2, new Color(0.2f, 0.2f, 0.2f, 1f));
            style.normal.textColor = isActive ? Color.green : Color.white;

            style.hover.background = MakeTex(2, 2, new Color(0.3f, 0.3f, 0.3f, 1f));
            style.hover.textColor = isActive ? Color.green : Color.white;

            style.active.background = MakeTex(2, 2, new Color(0.1f, 0.1f, 0.1f, 1f));
            style.active.textColor = isActive ? Color.green : Color.white;

            style.fontSize = 14;
            style.alignment = TextAnchor.MiddleCenter;
            return style;
        }

        private GUIStyle GetTabStyle(int tabIndex)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.background = MakeTex(2, 2, new Color(0.2f, 0.2f, 0.2f, 1f));
            style.fontSize = 14;
            style.alignment = TextAnchor.MiddleCenter;

            style.hover.background = MakeTex(2, 2, new Color(0.3f, 0.3f, 0.3f, 1f));
            style.hover.textColor = Color.white;

            style.active.background = MakeTex(2, 2, new Color(0.1f, 0.1f, 0.1f, 1f));
            style.active.textColor = Color.green;

            if (selectedTab == tabIndex)
            {
                style.normal.textColor = Color.green;
            }
            else
            {
                style.normal.textColor = Color.white;
            }

            return style;
        }


        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; i++) pix[i] = col;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        private void UpdateRGBColor()
        {
            hue += Time.deltaTime * 0.5f; // Speed of RGB
            if (hue > 1) hue -= 1;
            rgbColor = Color.HSVToRGB(hue, 1f, 1f);
        }
        #endregion
    }
}
