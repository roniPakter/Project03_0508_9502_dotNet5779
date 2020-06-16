using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Windows.Data;

namespace PLWPF
{
    class VehicleTypeToUserStringConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "";
            switch ((VehicleType)value)
            {
                case VehicleType.TwoWheeled:
                    result = "Two Wheeled";
                    break;
                case VehicleType.Private:
                    result = "Private";
                    break;
                case VehicleType.LightTrack:
                    result = "Light Track";
                    break;
                case VehicleType.HeavyTrack:
                    result = "Heavy Track";
                    break;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return value;
            //if (value == null)
            //{
            //    return null;
            //}
            //VehicleType result = VehicleType.Private;
            //switch ((string)value)
            //{
            //    case "Two Wheeled":
            //        result = VehicleType.TwoWheeled;
            //        break;
            //    case "Private":
            //        result = VehicleType.Private;
            //        break;
            //    case "Light Track":
            //        result = VehicleType.LightTrack;
            //        break;
            //    case "Heavy Track":
            //        result = VehicleType.HeavyTrack;
            //        break;
            //}
            //return result;
        }
    }
}
