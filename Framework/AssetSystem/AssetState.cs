namespace Vision.Framework.AssetSystem;

/// <summary>
///     Describes the state of an <see cref="Asset{T}"/>
/// </summary>
public enum AssetState : byte {
    /// <summary>
    ///     The <see cref="Asset{T}"/> is not present in memory.
    /// </summary>
    Unloaded,

    /// <summary>
    ///     The <see cref="Asset{T}"/> is yet to be loaded.
    /// </summary>
    OnHold,

    /// <summary>
    ///     The <see cref="Asset{T}"/> is being loaded into memory.
    /// </summary>
    Loading,

    /// <summary>
    ///     The <see cref="Asset{T}"/> is loaded into memory.
    /// </summary>
    Loaded
}