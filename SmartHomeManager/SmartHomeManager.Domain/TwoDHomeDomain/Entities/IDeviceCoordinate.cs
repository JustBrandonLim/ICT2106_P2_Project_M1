﻿namespace SmartHomeManager.Domain.TwoDHomeDomain.Entities;

public interface IDeviceCoordinate
{
    public Guid DeviceCoordinateId { get; set; }
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Guid DeviceId { get; set; }
}