﻿using System;
using System.Collections.Generic;

namespace RGB.NET.Core
{
    /// <summary>
    /// Represents a generic device provider.
    /// </summary>
    public interface IRGBDeviceProvider : IDisposable
    {
        #region Properties & Fields

        /// <summary>
        /// Indicates if the used SDK is initialized and ready to use.
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Gets a list of <see cref="IRGBDevice"/> loaded by this <see cref="IRGBDeviceProvider"/>.
        /// </summary>
        IEnumerable<IRGBDevice> Devices { get; }

        #endregion
    }
}
