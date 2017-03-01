using Microsoft.AspNetCore.Mvc;
using CloudantDotNet.Common;
using CloundantDotNet.Models;

namespace CloudantDotNet.Controllers
{
    public class HomeController : Controller
    {
    	private readonly string GateType = "GW_GatewayDeviceType";
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Application()
        {
            ApiClient client = new ApiClient(ConfigurationManager.AppSettings["ApiKey"], ConfigurationManager.AppSettings["ApiToken"]);
            List<DeviceList> devicelist = new List<DeviceList>();
            var arrayList = client.GetAllDevices();

            foreach (var result in arrayList["results"])
            {
                DeviceList device = new DeviceList();

                //for (int i = 0; i < result.Count; i++)
                {
                    //var deviceCredentails = result[i];
                    device.Device = result["deviceId"];
                    device.DeviceType = result["typeId"];
                    if (device.DeviceType.Equals(GateType))
                    {
                        device.Class = "Gateway";
                    }
                    else
                    {
                        device.Class = "Device";
                    }
                }
                devicelist.Add(device);
            }

            //devicelist.Add(new DeviceList { Class = "1", Connected = true, Device = "deviceId", DeviceType = "Type" });
            //devicelist.Add(new DeviceList { Class = "12", Connected = false, Device = "deviceId", DeviceType = "Type" });

            return View(devicelist);
        }
    }
}