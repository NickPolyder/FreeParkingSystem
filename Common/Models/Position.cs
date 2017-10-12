﻿using System;

namespace FreeParkingSystem.Common.Models
{
    public class Position : IEquatable<Position>
    {
        public double Latitude { get; }

        public double Longitude { get; }

        public string Address { get; }

        public Position(double latitude, double longitude, string address)
        {
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
        }

        public override string ToString()
        {
            return $"Lat: {Latitude}, Long: {Longitude}";
        }

        public bool Equals(Position other)
        {
            return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Position position && Equals(position);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Latitude.GetHashCode() * 397) ^ Longitude.GetHashCode();
            }
        }

        public static bool operator ==(Position left, Position right)
        {
            return left?.Equals(right) ?? false;
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
    }
}
