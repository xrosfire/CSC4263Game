﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayPhase
{
    public class PlayPhaseManager : MonoBehaviour
    {
        #region Fields

        public GameManager gameManager;
        [Header("Play Level Prefabs")]
        public GameObject playerPrefab;
        public EnemyPrefabs enemyPrefabs;
        public EnvironmentPrefabs envPrefabs;

        private GameObject playPhaseContainer;
        private PlayEnvironment env;

        #endregion

        #region Initialization

        private void Start()
        {
            if (gameManager == null)
            {
                gameManager = FindObjectOfType<GameManager>();
            }
            Init();
        }

        private void Init()
        {
            if (this.gameManager == null)
            {
                Logger.Debug("PlayPhaseManager: No GameManager object found.");
            }

            playPhaseContainer = new GameObject("Play Phase Root");
            env = new PlayEnvironment(this, playPhaseContainer.transform);
        }

        #endregion

        [ContextMenu("Start Play Phase")]
        public void StartPhase()
        {
            GenerateLevel();
            SpawnEnemies();
            SpawnPlayer();

            // Activate the Play Phase root & make all Play Phase GameObjects it visible.
            playPhaseContainer.SetActive(true);
        }

        protected void GenerateLevel()
        {
            Logger.Debug("GenerateLevel");
            env.Generate();
        }

        protected void SpawnEnemies()
        {
            Logger.Debug("SpawnEnemies");
        }

        protected void SpawnPlayer()
        {
            Logger.Debug("SpawnPlayer");
        }

        [ContextMenu("End Play Phase")]
        public void EndPhase()
        {
            Logger.Debug("Exit Play Phase");
            playPhaseContainer.SetActive(false);
        }
    }

    [System.Serializable]
    public class EnemyPrefabs
    {
        public GameObject gruntPrefab;
        public GameObject bossPrefab;
    }

    [System.Serializable]
    public class EnvironmentPrefabs
    {
        #region Fields

        [Header("Environment Prefabs")]
        [Space]

        [SerializeField]
        [Tooltip("List of prefabs to instantiate at Play Phase initialization.")]
        private GameObject[] levelBasePrefabs;

        [Header("Tile Center Prefabs")]

        [SerializeField]
        [Tooltip("Prefab to be instantiated at the center of every placed tile. \n" + 
                 "List of variations of this prefab. ")]
        private GameObject[] centerFloorPrefabVariations;

        [Header("Border Prefabs")]

        [SerializeField]
        [Tooltip("Prefab to be instantiated at the North/South border of 2 tiles connected by a path/door. \n" +
                 "List of variations of this prefab. ")]
        private GameObject[] NSAlleyFloorPrefabVariations;

        [SerializeField]
        [Tooltip("Prefab to be instantiated at the North/South border of 2 tiles connected by a path/door. \n" +
                 "List of variations of this prefab. ")]
        private GameObject[] NSOpenFloorPrefabVariations;

        [SerializeField]
        [Tooltip("Prefab to be instantiated at the East/West border of 2 tiles connected by a path/door. \n" +
                 "List of variations of this prefab. ")]
        private GameObject[] EWAlleyFloorPrefabVariations;

        [SerializeField]
        [Tooltip("Prefab to be instantiated at the East/West border of 2 tiles connected by a path/door. \n" +
                 "List of variations of this prefab. ")]
        private GameObject[] EWOpenFloorPrefabVariations;

        [Header("Empty Adjacent Tile Prefabs")]

        [SerializeField]
        [Tooltip("Prefab to be instantiated at empty spaces next to edge tiles. \n" +
                 "List of variations of this prefab. ")]
        private GameObject[] nullTilePrefabVariations;

        [Header("Tile Corner Prefabs")]

        [SerializeField]
        [Tooltip("Prefab to be instantiated in the middle of 4 open tiles. \n" +
                 "List of variations of this prefab. ")]
        private GameObject[] openCornerPrefabVariations;

        [SerializeField]
        [Tooltip("Edge/corner building prefabs to be instantiated between placed tiles. \n" +
                 "List of variations of building types. ")]
        private BuildingPrefabs[] buildingPrefabVariations;

        #endregion

        public GameObject[] LevelBasePrefabs {
            get
            {
                return levelBasePrefabs;
            }
        }

        public GameObject CenterFloorPrefab
        {
            get
            {
                return Utility.GetOneOf(ref centerFloorPrefabVariations);
            }
        }

        public GameObject NSAlleyFloorPrefab
        {
            get
            {
                return Utility.GetOneOf(ref NSAlleyFloorPrefabVariations);
            }
        }

        public GameObject NSOpenFloorPrefab
        {
            get
            {
                return Utility.GetOneOf(ref NSOpenFloorPrefabVariations);
            }
        }

        public GameObject EWAlleyFloorPrefab
        {
            get
            {
                return Utility.GetOneOf(ref EWAlleyFloorPrefabVariations);
            }
        }

        public GameObject EWOpenFloorPrefab
        {
            get
            {
                return Utility.GetOneOf(ref EWOpenFloorPrefabVariations);
            }
        }

        public GameObject NullTilePrefab
        {
            get
            {
                return Utility.GetOneOf(ref nullTilePrefabVariations);
            }
        }

        public GameObject OpenCornerPrefab
        {
            get
            {
                return Utility.GetOneOf(ref openCornerPrefabVariations);
            }
        }

        public BuildingPrefabs BuildingPrefabs
        {
            get
            {
                return Utility.GetOneOf(ref buildingPrefabVariations);
            }
        }
    }

    [System.Serializable]
    public struct BuildingPrefabs
    {
        public GameObject[] centerRoofPrefabVariations;
        public GameObject[] openWallPrefabVariations;
        public GameObject[] pathWallPrefabVariations;
        public GameObject[] closedWallPrefabVariations;

        public GameObject CenterRoofPrefab
        {
            get
            {
                return Utility.GetOneOf(ref centerRoofPrefabVariations);
            }
        }

        public GameObject OpenWallPrefab
        {
            get
            {
                return Utility.GetOneOf(ref openWallPrefabVariations);
            }
        }

        public GameObject PathWallPrefab
        {
            get
            {
                return Utility.GetOneOf(ref pathWallPrefabVariations);
            }
        }

        public GameObject ClosedWallPrefab
        {
            get
            {
                return Utility.GetOneOf(ref closedWallPrefabVariations);
            }
        }
    }
}