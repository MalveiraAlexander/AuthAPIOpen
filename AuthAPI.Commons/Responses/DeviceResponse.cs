using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Commons.Responses
{
    public class DeviceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isApp { get; set; }
        public string PublicIp { get; set; }
    }
}
