using System.Diagnostics;
using UnityEngine;

namespace Nexuris.Core
{
    ///Broad initial categorisation of game assets to select the correct fallback

    public enum ASSET_CATEGORY
    {
        Ground,
        Obstacle,
        Interactable,
        Entity,
        UI
    }

    public static class AssetLoader
    {
        private const string BASE_FALLBACK_PATH = "System_Defaults/Sprites/";
        /// attempts to load sprite from "Materials/Sprites"
        /// Reverts to a category specific texture if target is missing
        /// 
        /// resourcePath : path relative to Resources folder
        /// category : category of the asset being loaded

        public static Sprite LoadSprite(string resourcePath, ASSET_CATEGORY category)
        {
            //trying to load the asset from the path provided and using it immidietly
            Sprite tagetSprite = Resources.Load<Sprite>(resourcePath);

            if (tagetSprite != null)
            {
                return tagetSprite;
            }

            //fallback if sprite is missing
            UnityEngine.Debug.LogWarning($"[AssetLoader] Missing asset at 'Resources/{resourcePath}'. Using default {category} sprite");

            string fallbackPath = GetFallbackPath(category);
            Sprite fallbackSprite = Resources.Load<Sprite>(fallbackPath);

            if (fallbackSprite == null)
            {
                UnityEngine.Debug.LogError($"[AssetLoader] Fallback assets missing!!!");
            }

            return fallbackSprite;
        }

        private static string GetFallbackPath(ASSET_CATEGORY category)
        {
            switch (category)
            {
                case ASSET_CATEGORY.Ground:
                    return BASE_FALLBACK_PATH + "Default_Missing_Ground_Sprite";
                case ASSET_CATEGORY.Obstacle:
                    return BASE_FALLBACK_PATH + "Default_Missing_Obstacle_Sprite";
                case ASSET_CATEGORY.Interactable:
                    return BASE_FALLBACK_PATH + "Default_Missing_Interactable_Sprite";
                case ASSET_CATEGORY.Entity:
                    return BASE_FALLBACK_PATH + "Default_Missing_Entity_Sprite";
                case ASSET_CATEGORY.UI:
                    return BASE_FALLBACK_PATH + "Default_Missing_UI_Sprite";
                default:
                    return BASE_FALLBACK_PATH + "Default_Missing_UI_Sprite";
            }
        }
    }
}