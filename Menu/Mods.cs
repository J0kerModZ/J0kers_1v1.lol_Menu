using Il2Cpp;
using Il2CppAssets.Scripts;
using Il2CppPhoton.Pun;
using UnityEngine;
using Object = UnityEngine.Object;

namespace J0kers1v1LOLMenu.Menu
{
    internal class Mods : MonoBehaviour
    {
        #region Player
        public static void Flight()
        {
            if (!PhotonNetwork.InRoom) return;

            if (Input.GetKey(KeyCode.W))
            {
                PlayerController.NDCIACGBFEG.transform.position += Camera.main.transform.forward * Time.deltaTime * 15f;
                PlayerController.NDCIACGBFEG.GetComponent<Rigidbody>().velocity = Vector3.zero;
                PlayerController.NDCIACGBFEG.GetComponent<Rigidbody>().useGravity = false;
            }
            else
            {
                PlayerController.NDCIACGBFEG.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        public static void SpeedBoost()
        {
            if (PhotonNetwork.InRoom && PlayerController.NDCIACGBFEG != null)
            {
                PlayerController.NDCIACGBFEG.NLJKDAPCAAB = 5f;
            }
        }

        public static void DisableSpeedBoost()
        {
            if (PhotonNetwork.InRoom && PlayerController.NDCIACGBFEG != null)
            {
                PlayerController.NDCIACGBFEG.NLJKDAPCAAB = 1.5f;
            }
        }

        public static void SpinBot()
        {
            if (PhotonNetwork.InRoom && PlayerController.NDCIACGBFEG != null)
            {
                PlayerController.NDCIACGBFEG.GetComponentInChildren<SkinPack>().gameObject.transform.Rotate(0f, 360f * Time.deltaTime * 10f, 0f);
            }
        }

        public static void GodMode()
        {
            if(PhotonNetwork.InRoom && PlayerController.NDCIACGBFEG != null)
            {
                PlayerController.NDCIACGBFEG.LHBMILEKFID.HealPlayer(69420);
            }
        }

        #endregion

        #region Visual

        #region Box ESP
        public static void BoxESP()
        {
            if (!PhotonNetwork.InRoom) return;

            UpdateCachedPlayers();

            foreach (PlayerController player in cachedPlayers)
            {
                if (player == null || player.IsMine()) continue;

                Transform espBox = player.transform.Find("ESPBox");
                if (espBox == null)
                {
                    GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    box.name = "ESPBox";
                    box.transform.localScale = new Vector3(0.5f, 0.8f, 0.5f);

                    Vector3 targetHeadPosition = player.ADCPPBLNAMK;
                    box.transform.SetParent(player.transform, false);
                    box.transform.position = targetHeadPosition;

                    Renderer renderer = box.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material = new Material(Shader.Find("GUI/Text Shader"));
                        renderer.material.color = new Color(0, 1, 0, 0.5f);
                    }

                    Collider collider = box.GetComponent<Collider>();
                    if (collider != null) collider.enabled = false;
                }
                else
                {
                    Renderer renderer = espBox.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = Color.Lerp(Color.cyan, Color.green, Mathf.PingPong(Time.time / 2f, 1f));
                    }
                }
            }
        }

        public static void DestoryBoxes()
        {
            GameObject box = GameObject.Find("ESPBox");
            Object.Destroy(box);
        }
        #endregion

        #region Frame ESP

        // Could Have Used Line Renders But They Are Broke Af!
        public static void frameESP()
        {
            if (!PhotonNetwork.InRoom) return;

            UpdateCachedPlayers();

            foreach (PlayerController player in cachedPlayers)
            {
                if (player == null || player.IsMine()) continue;

                Vector3 playerHeadPosition = player.ADCPPBLNAMK;

                GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                box.transform.position = playerHeadPosition;
                UnityEngine.Object.Destroy(box.GetComponent<BoxCollider>());
                box.transform.localScale = new Vector3(0.5f, 0.5f, 0f);

                Vector3 directionToCamera = Camera.main.transform.position - box.transform.position;
                directionToCamera.y = 0;

                if (directionToCamera != Vector3.zero)
                {
                    Quaternion rotation = Quaternion.LookRotation(directionToCamera);
                    box.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
                }

                box.GetComponent<Renderer>().enabled = false;

                CreateOutlineBox(playerHeadPosition + (box.transform.up * 0.25f), box.transform.rotation, new Vector3(0.5f, 0.05f, 0f), box);
                CreateOutlineBox(playerHeadPosition + (box.transform.up * -1.5f), box.transform.rotation, new Vector3(0.55f, 0.05f, 0f), box);
                CreateOutlineBox(playerHeadPosition + (box.transform.right * 0.25f + box.transform.up * -0.625f), box.transform.rotation, new Vector3(0.05f, 1.75f, 0f), box);
                CreateOutlineBox(playerHeadPosition + (box.transform.right * -0.25f + box.transform.up * -0.625f), box.transform.rotation, new Vector3(0.05f, 1.75f, 0f), box);
            }
        }


        private static void CreateOutlineBox(Vector3 position, Quaternion rotation, Vector3 scale, GameObject parentBox)
        {
            GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
            box.transform.position = position;
            UnityEngine.Object.Destroy(box.GetComponent<BoxCollider>());
            box.transform.localScale = scale;
            box.transform.rotation = rotation;
            box.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
            box.GetComponent<Renderer>().material.color = Color.Lerp(Color.cyan, Color.green, Mathf.PingPong(Time.time / 2f, 1f));

            UnityEngine.Object.Destroy(box, 0.02f);
        }
        #endregion

        #region Body ESP
        public static void BodyESP()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (PlayerController playerObjs in Object.FindObjectsOfType<PlayerController>())
                {
                    if (playerObjs != null)
                    {
                        if (playerObjs.gameObject.GetComponentInChildren<SkinnedMeshRenderer>())
                        {
                            playerObjs.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.shader = Shader.Find("GUI/Text Shader");
                            playerObjs.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.Lerp(Color.cyan, Color.green, Mathf.PingPong(Time.time / 2f, 1f));
                        }
                    }
                }
            }
        }

        public static void DisableBodyESP()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (PlayerController playerObjs in Object.FindObjectsOfType<PlayerController>())
                {
                    if (playerObjs != null)
                    {
                        if (playerObjs.gameObject.GetComponentInChildren<SkinnedMeshRenderer>())
                        {
                            playerObjs.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.shader = Shader.Find("PlayerToonShader");
                            playerObjs.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
                        }
                    }
                }
            }
        }
        #endregion

        #region Bone ESP

        public static HumanBodyBones[] playersBody { get; } = 
        {
            HumanBodyBones.Head,
            HumanBodyBones.Neck,
            HumanBodyBones.UpperChest,

            HumanBodyBones.LeftShoulder,
            HumanBodyBones.LeftUpperArm,
            HumanBodyBones.LeftLowerArm,
            HumanBodyBones.LeftHand,

            HumanBodyBones.RightShoulder,
            HumanBodyBones.RightUpperArm,
            HumanBodyBones.RightLowerArm,
            HumanBodyBones.RightHand,

            HumanBodyBones.LeftFoot,
            HumanBodyBones.LeftLowerLeg,
            HumanBodyBones.LeftUpperLeg,

            HumanBodyBones.RightFoot,
            HumanBodyBones.RightLowerLeg,
            HumanBodyBones.RightUpperLeg,
        };

        public static void BoneESP()
        {
            if (!PhotonNetwork.InRoom) return;

            UpdateCachedPlayers();

            foreach (PlayerController player in cachedPlayers)
            {
                if (player == null ||player.IsMine()) continue;

                Animator animator = player.gameObject.GetComponentInChildren<Animator>();
                if (animator == null) continue;

                foreach (HumanBodyBones humanBone in playersBody)
                {
                    Transform boneTransform = animator.GetBoneTransform(humanBone);

                    if (boneTransform != null)
                    {
                        LineRenderer lineRenderer = boneTransform.gameObject.GetComponent<LineRenderer>();
                        if (lineRenderer == null)
                        {
                            lineRenderer = boneTransform.gameObject.AddComponent<LineRenderer>();
                            lineRenderer.material = new Material(Shader.Find("GUI/Text Shader"));
                            lineRenderer.startWidth = 0.025f;
                            lineRenderer.endWidth = 0.025f;
                        }

                        if (boneTransform.parent != null)
                        {
                            lineRenderer.positionCount = 2;
                            lineRenderer.SetPosition(0, boneTransform.position);
                            lineRenderer.SetPosition(1, boneTransform.parent.position);
                            lineRenderer.material.color = Color.Lerp(Color.cyan, Color.green, Mathf.PingPong(Time.time / 2f, 1f));
                        }
                        Destroy(lineRenderer, 0.02f);
                    }
                }
            }
        }

        #endregion

        #region Tracers
        public static void Tracers()
        {
            if (!PhotonNetwork.InRoom) return;

            UpdateCachedPlayers();

            foreach (PlayerController players in cachedPlayers)
            {
                if (players != null && !players.IsMine())
                {
                    Vector3 playerHeadPosition = players.ADCPPBLNAMK;

                    GameObject gameObject = new GameObject("PlayerLine");
                    LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
                    Color Color = Color.Lerp(Color.cyan, Color.green, Mathf.PingPong(Time.time / 2f, 1f));
                    lineRenderer.startColor = Color;
                    lineRenderer.endColor = Color;
                    lineRenderer.startWidth = 0.01f;
                    lineRenderer.endWidth = 0.01f;
                    lineRenderer.positionCount = 2;
                    lineRenderer.useWorldSpace = true;
                    lineRenderer.SetPosition(0, PlayerController.NDCIACGBFEG.KDMDNHKDILI);
                    lineRenderer.SetPosition(1, playerHeadPosition);
                    lineRenderer.material.shader = Shader.Find("GUI/Text Shader");
                    UnityEngine.Object.Destroy(lineRenderer, 0.02f);
                    UnityEngine.Object.Destroy(gameObject, 0.02f);
                }
            }
        }

        #endregion

        #endregion

        #region Weapons
        public static void AimBot()
        {
            if (PhotonNetwork.InRoom)
            {
                if (Input.GetMouseButton(0))
                {
                    UpdateCachedPlayers();

                    PlayerController closestTarget = null;
                    float closestDistance = float.MaxValue;
                    Vector3 cameraPosition = CameraManager.DNNBNONIHBG.MainCamera.transform.position;

                    foreach (PlayerController target in cachedPlayers)
                    {
                        if (target.IsMine()) continue;

                        Vector3 targetHeadPosition = target.ADCPPBLNAMK;
                        float distance = Vector3.Distance(cameraPosition, targetHeadPosition);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestTarget = target;
                        }
                    }

                    if (closestTarget != null)
                    {
                        Vector3 targetPosition = closestTarget.ADCPPBLNAMK;
                        CameraManager.DNNBNONIHBG.MainCamera.transform.LookAt(targetPosition);
                    }
                }
            }
        }

        public static void InfAmmo()
        {
            if (PhotonNetwork.InRoom && PlayerController.NDCIACGBFEG.FKEKCIKNFPN.CBACMLEFBIG != null)
            {
                PlayerController.NDCIACGBFEG.FKEKCIKNFPN.CBACMLEFBIG.SetCurrentAmmoAmount(1);
                PlayerController.NDCIACGBFEG.FKEKCIKNFPN.CBACMLEFBIG.SetCurrentMagazineAmount(2);
            }
        }

        public static void RapidFire()
        {
            if (PhotonNetwork.InRoom && PlayerController.NDCIACGBFEG.FKEKCIKNFPN != null && Input.GetMouseButton(0))
            {
                PlayerController.NDCIACGBFEG.FKEKCIKNFPN.ApplyFireRateMultiplier(100);
            }
        }

        public static void InstaKill()
        {
            if (PhotonNetwork.InRoom && PlayerController.NDCIACGBFEG.FKEKCIKNFPN != null)
            {
                PlayerController.NDCIACGBFEG.FKEKCIKNFPN.AddLevelsToAllWeapons(2147483641);
                PlayerController.NDCIACGBFEG.FKEKCIKNFPN.RefreshWeaponsDisplay();
            }
        }
        #endregion

        #region Server
        public static void SpawnPlayer()
        {
            if (!PhotonNetwork.InRoom) return;

            PhotonNetwork.Instantiate("PolyPlayer", PlayerController.NDCIACGBFEG.ADCPPBLNAMK, Quaternion.identity);
        }

        public static void SpawnMaterial()
        {
            if (!PhotonNetwork.InRoom) return;

            PhotonNetwork.Instantiate("BuildingMaterialPickupable", PlayerController.NDCIACGBFEG.ADCPPBLNAMK, Quaternion.identity);
        }

        public static void SpawnAmmo()
        {
            if (!PhotonNetwork.InRoom) return;

            PhotonNetwork.Instantiate("AmmoPickupable", PlayerController.NDCIACGBFEG.ADCPPBLNAMK, Quaternion.identity);
        }

        public static void SpawnShadowClone()
        {
            if (!PhotonNetwork.InRoom) return;

            PhotonNetwork.Instantiate("ShadowClone", PlayerController.NDCIACGBFEG.ADCPPBLNAMK, Quaternion.identity);
        }

        public static void DestoryAllBuilds()
        {
            if (!PhotonNetwork.InRoom) return;

            BuildingNetworkController.Instance.KillAllBuildings(true);
        }

        public static void OpenAllBoxes()
        {
            if (!PhotonNetwork.InRoom) return;

            foreach (SupplyCrate crates in Object.FindObjectsOfType<SupplyCrate>())
            {
                crates.OpenCrate(PlayerController.NDCIACGBFEG);
            }
        }

        public static void EndGame()
        {
            var il2cppList = new Il2CppSystem.Collections.Generic.List<int>();
            for (int i = 1; i <= 5; i++)
            {
                il2cppList.Add(i);
            }
            GameManager.Instance.EndGame(false, false, il2cppList);
        }
        #endregion

        #region Other
        private static PlayerController[] cachedPlayers;
        private static float nextCacheTime;

        private static void UpdateCachedPlayers()
        {
            if (Time.time > nextCacheTime)
            {
                cachedPlayers = Object.FindObjectsOfType<PlayerController>();
                nextCacheTime = Time.time + 1f;
            }
        }
        #endregion
    }
}