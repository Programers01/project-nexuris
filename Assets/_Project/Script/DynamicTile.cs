using UnityEngine;
using UnityEngine.Tilemaps;

namespace Nexuris.Core
{
    [CreateAssetMenu(fileName = "New Dynamic Tile", menuName = "Nexuris/Tiles/Dynamic Tile")]
    public class DynamicTile : Tile
    {
        public string runtimeTexturePath;
        public ASSET_CATEGORY tileCategory;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);

            if (string.IsNullOrEmpty(runtimeTexturePath))
            {
                tileData.sprite = AssetLoader.LoadSprite("", tileCategory);
                return;
            }

            tileData.sprite = AssetLoader.LoadSprite(runtimeTexturePath, tileCategory);
        }
    }
}
