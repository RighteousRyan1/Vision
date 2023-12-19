namespace Vision.Framework.AssetSystem;
public enum AssetState {
    /// <summary>
    ///     The asset is not present in memory.
    /// </summary>
    Unloaded,
    /// <summary>
    ///     The asset is yet to be loaded.
    /// </summary>
    OnHold,
    /// <summary>
    ///     The asset is being loaded into memory.
    /// </summary>
    Loading,
    /// <summary>
    ///     The asset is loaded into memory.
    /// </summary>
    Loaded
}
