﻿using OpenRGB.NET;
using RGB.NET.Core;

namespace RGB.NET.Devices.OpenRGB;

/// <inheritdoc />
public class OpenRGBZoneDevice : AbstractOpenRGBDevice<OpenRGBDeviceInfo>
{
    #region Properties & Fields

    private readonly int _initialLed;
    private readonly Zone _zone;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenRGBZoneDevice"/> class.
    /// </summary>
    /// <param name="info">The information provided by OpenRGB</param>
    /// <param name="initialLed">The ledId of the first led in the device that belongs to this zone.</param>
    /// <param name="zone">The Zone information provided by OpenRGB.</param>
    /// <param name="updateQueue">The queue used to update this zone.</param>
    public OpenRGBZoneDevice(OpenRGBDeviceInfo info, int initialLed, Zone zone, IUpdateQueue updateQueue)
        : base(info, updateQueue)
    {
        _initialLed = initialLed;
        _zone = zone;

        InitializeLayout();
    }

    #endregion

    #region Methods

    private void InitializeLayout()
    {
        Size ledSize = new(19);
        const int LED_SPACING = 20;
        LedId initialId = Helper.GetInitialLedIdForDeviceType(DeviceInfo.DeviceType);

        if (_zone.Type == ZoneType.Matrix)
        {
            for (int row = 0; row < _zone.MatrixMap!.Height; row++)
            {
                for (int column = 0; column < _zone.MatrixMap!.Width; column++)
                {
                    uint index = _zone.MatrixMap!.Matrix[row, column];

                    //will be max value if the position does not have an associated key
                    if (index == uint.MaxValue)
                        continue;

                    LedId ledId = LedMappings.DEFAULT.TryGetValue(DeviceInfo.OpenRGBDevice.Leds[_initialLed + index].Name, out LedId id)
                                      ? id
                                      : initialId++;

                    while (AddLed(ledId, new Point(LED_SPACING * column, LED_SPACING * row), ledSize, _initialLed + (int)index) == null)
                        ledId = initialId++;
                }
            }
        }
        else
        {
            for (int i = 0; i < _zone.LedCount; i++)
            {
                LedId ledId = initialId++;

                while (AddLed(ledId, new Point(LED_SPACING * i, 0), ledSize, _initialLed + i) == null)
                    ledId = initialId++;
            }
        }
    }

    #endregion
}