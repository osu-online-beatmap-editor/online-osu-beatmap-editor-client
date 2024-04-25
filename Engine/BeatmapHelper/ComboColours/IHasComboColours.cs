using JetBrains.Annotations;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Mapping_Tools_Core.BeatmapHelper.ComboColours {
    /// <summary>
    /// Interface that indicates an object has colour colours.
    /// </summary>
    public interface IHasComboColours {
        /// <summary>
        /// Contains all the basic combo colours.
        /// </summary>
        [JetBrains.Annotations.NotNull]
        IReadOnlyList<IComboColour> ComboColours { get; }
    }
}