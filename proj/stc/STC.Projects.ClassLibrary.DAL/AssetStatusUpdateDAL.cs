using STC.Projects.ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DAL
{
    public class AssetStatusUpdateDAL
    {
        STCOperationalDataContext operationalContext;
        ElrocEntities EkinContext;
        public bool UpdateVitronicStatus()
        {
            try
            {
                operationalContext = new STCOperationalDataContext();
                string baseURL = ConfigurationManager.AppSettings["VitronicPath"];
                int threshold = GetThreshold();

                var ourDevices = operationalContext.Assets.Where(x => x.AssetTypeId == 3).ToList();
                var devices = Directory.GetDirectories(baseURL);

                if (devices != null && devices.Length > 0 && ourDevices != null && ourDevices.Count > 0)
                {
                    var devicesList = devices.ToList();
                    foreach (var ourDevice in ourDevices)
                    {
                        var device = devicesList.FirstOrDefault(x => x.Split('\\').Last() == ourDevice.SerialNo.Trim());

                        if (device != null)
                        {
                            DateTime lastUpdate = Directory.GetLastWriteTime(device);
                            ourDevice.LastUpdated = lastUpdate;

                            if (lastUpdate.AddHours(threshold) < DateTime.Now)
                            {
                                ourDevice.AssetStatusId = 2;
                            }
                            else
                            {
                                ourDevice.AssetStatusId = 1;
                            }
                        }
                        else
                        {
                            ourDevice.AssetStatusId = 2;
                        }

                        operationalContext.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateEkinStatus()
        {
            try
            {
                EkinContext = new ElrocEntities();
                EkinContext.Database.CommandTimeout = 5000;
                int threshold = GetThreshold();

                var ourDevices = operationalContext.Assets.Where(x => x.AssetTypeId == 2).ToList();


                string device = string.Empty;
                DateTime thresholdDate = DateTime.Now.AddHours(threshold * -1);

                var EkinList = EkinContext.elrocs.Where(x => x.date_time.Value >= thresholdDate).Select(x => x.server_no).Distinct().ToList();

                if (EkinList != null && EkinList.Count > 0 && ourDevices != null && ourDevices.Count > 0)
                {
                    foreach (var item in ourDevices)
                    {
                        item.AssetStatusId = 2;
                    }

                    foreach (var ekinDevice in EkinList)
                    {
                        device = EkinContext.servers.Where(x => x.no == ekinDevice).Select(x => x.name).FirstOrDefault();

                        if (device != null)
                        {
                            var ourdevice = ourDevices.Where(x => x.SerialNo.ToLower() == device.ToLower()).FirstOrDefault();

                            if (ourdevice != null)
                            {
                                DateTime lastUpdate = thresholdDate;
                                ourdevice.LastUpdated = lastUpdate;
                                ourdevice.AssetStatusId = 1;
                            }
                        }
                    }

                    operationalContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int GetThreshold()
        {
            try
            {
                operationalContext = new STCOperationalDataContext();

                var threshold = operationalContext.AssetStatusThresholds.Select(x => x.Threshold).FirstOrDefault();

                return threshold ?? 24;
            }
            catch
            {
                return 24;
            }
        }

        public bool UpdateThreshold(int threshold)
        {
            try
            {
                operationalContext = new STCOperationalDataContext();

                var output = operationalContext.AssetStatusThresholds.FirstOrDefault();

                if (output == null)
                {
                    operationalContext.AssetStatusThresholds.Add(new AssetStatusThreshold { Threshold = threshold });
                }
                else
                {
                    output.Threshold = threshold;
                }

                return operationalContext.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

    }
}
