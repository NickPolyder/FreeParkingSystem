using System;
using System.Collections.Generic;
using System.Text;

namespace FreeParkingSystem.Common.Models
{
    public interface IParkingSpace : IBaseModel
    {
        /// <summary>
        /// Describes the parking place
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The type of the Parking.
        /// </summary>
        IParkingType ParkingType { get; set; }

        /// <summary>
        /// The Position on the Map.
        /// </summary>
        Position Position { get; set; }

        int Floors { get; set; }

        /// <summary>
        /// The number of available park positions
        /// </summary>
        int ParkSpaces { get; set; }

        /// <summary>
        /// It contains park spaces for people with disabilities.
        /// </summary>
        bool HasSpecialParkSpaces { get; set; }

        /// <summary>
        /// The max height for a vehicle that can enter.
        /// </summary>
        double? MaxHeightOfVehicle { get; set; }

        /// <summary>
        /// the width that the park spot has.
        /// </summary>
        double? WidthOfParkSpot { get; set; }
    }
}
