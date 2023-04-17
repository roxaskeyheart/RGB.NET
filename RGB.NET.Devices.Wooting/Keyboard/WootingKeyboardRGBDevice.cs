﻿using System;
using System.Collections.Generic;
using RGB.NET.Core;
using RGB.NET.Devices.Wooting.Generic;
using RGB.NET.Devices.Wooting.Native;

namespace RGB.NET.Devices.Wooting.Keyboard;

/// <inheritdoc cref="WootingRGBDevice{TDeviceInfo}" />
/// <summary>
/// Represents a Wooting keyboard.
/// </summary>
public class WootingKeyboardRGBDevice : WootingRGBDevice<WootingKeyboardRGBDeviceInfo>, IKeyboard
{
    #region Properties & Fields

    IKeyboardDeviceInfo IKeyboard.DeviceInfo => DeviceInfo;

    #endregion

    #region Constructors

    /// <inheritdoc />
    /// <summary>
    /// Initializes a new instance of the <see cref="T:RGB.NET.Devices.Wooting.Keyboard.WootingKeyboardRGBDevice" /> class.
    /// </summary>
    /// <param name="info">The specific information provided by Wooting for the keyboard</param>
    /// <param name="updateTrigger">The update trigger used to update this device.</param>
    internal WootingKeyboardRGBDevice(WootingKeyboardRGBDeviceInfo info, IUpdateQueue updateQueue)
        : base(info, updateQueue)
    {
        InitializeLayout();
    }

    #endregion

    #region Methods

    private void InitializeLayout()
    {
        Dictionary<LedId, (int row, int column)> mapping = WootingKeyboardLedMappings.Mapping[DeviceInfo.WootingDeviceType];

        foreach (KeyValuePair<LedId, (int row, int column)> led in mapping)
            AddLed(led.Key, new Point(led.Value.column * 19, led.Value.row * 19), new Size(19, 19));
    }

    /// <inheritdoc />
    protected override object GetLedCustomData(LedId ledId) => WootingKeyboardLedMappings.Mapping[DeviceInfo.WootingDeviceType][ledId];

    public override void Dispose()
    {
        _WootingSDK.SelectDevice(DeviceInfo.WootingDeviceIndex);
        _WootingSDK.Reset();

        base.Dispose();

        GC.SuppressFinalize(this);
    }

    #endregion
}