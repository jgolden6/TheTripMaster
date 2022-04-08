using System;
using System.Net;
using System.Xml.Linq;

namespace TheTripMasterLibrary.Model
{
    public class AddressValidation
    {
        public static bool ValidateAddressField(string field)
        {
            field ??= "";
            return field.Length > 0 && field.Length <= 128;
        }

        public static bool ValidateZipCode(string zip)
        {
            zip ??= "";
            int n;
            return zip.Length > 0 && zip.Length <= 5 && int.TryParse(zip, out n);
        }

        public static bool ValidateAddress(string streetAddress, string city, string state, string zipCode)
        {
            if (GetLatitudeLongitude(streetAddress, city, state, zipCode) == null)
            {
                return false;
            }

            return true;
        }


        public static Coordinates GetLatitudeLongitude(string streetAddress, string city, string state, string zipCode)
        {
            string address = streetAddress + "," + city + "," + state + "," + zipCode;

            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&address={0}&sensor=false", Uri.EscapeDataString(address), "AIzaSyA0pLPlpOeYoxCtsmyKVTlYKo8r-PKQwgU");

            WebRequest request = WebRequest.Create(requestUri);
            WebResponse response = request.GetResponse();
            XDocument xdoc = XDocument.Load(response.GetResponseStream());
            XElement result = xdoc.Element("GeocodeResponse").Element("result");

            if (result == null)
            {
                return null;
            }

            XElement locationElement = result.Element("geometry").Element("location");
            XElement latitude = locationElement.Element("lat");
            XElement longitude = locationElement.Element("lng");

            return new Coordinates
            {
                Latitude = Convert.ToDouble(latitude.Value),
                Longitude = Convert.ToDouble(longitude.Value)
            };
        }
    }
}
