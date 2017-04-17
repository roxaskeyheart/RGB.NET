﻿using System;
using RGB.NET.Core;
using RGB.NET.Devices.CoolerMaster.Helper;

namespace RGB.NET.Devices.CoolerMaster
{
    /// <summary>
    /// Represents a generic information for a Corsair-<see cref="IRGBDevice"/>.
    /// </summary>
    public class CoolerMasterRGBDeviceInfo : IRGBDeviceInfo
    {
        #region Properties & Fields

        /// <inheritdoc />
        public RGBDeviceType DeviceType { get; }

        /// <inheritdoc />
        public string Manufacturer => "Cooler Master";

        /// <inheritdoc />
        public string Model { get; }

        /// <inheritdoc />
        public Uri Image { get; protected set; }

        /// <inheritdoc />
        public RGBDeviceLighting Lighting => RGBDeviceLighting.Key;

        /// <summary>
        /// Gets the <see cref="CoolerMasterDevicesIndexes"/> of the <see cref="CoolerMasterRGBDevice"/>.
        /// </summary>
        public CoolerMasterDevicesIndexes DeviceIndex { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Internal constructor of managed <see cref="CoolerMasterRGBDeviceInfo"/>.
        /// </summary>
        /// <param name="deviceType">The type of the <see cref="IRGBDevice"/>.</param>
        /// <param name="deviceIndex">The <see cref="CoolerMasterDevicesIndexes"/> of the <see cref="IRGBDevice"/>.</param>
        internal CoolerMasterRGBDeviceInfo(RGBDeviceType deviceType, CoolerMasterDevicesIndexes deviceIndex)
        {
            this.DeviceType = deviceType;
            this.DeviceIndex = deviceIndex;

            Model = deviceIndex.GetDescription();
        }

        #endregion
    }
}