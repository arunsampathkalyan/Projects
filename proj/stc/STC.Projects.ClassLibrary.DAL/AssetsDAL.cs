using System;
using System.Collections.Generic;
using System.Linq;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System.Text;
using System.Data.Entity.Spatial;
using STC.Projects.ClassLibrary.Common;

namespace STC.Projects.ClassLibrary.DAL
{
    public class AssetsDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();
        //private CDSAssetsDataContext cdsAssetsDataContext = new CDSAssetsDataContext();

        public AssetsViewDTO GetAssetBySerial(string AssetSerial)
        {
            try
            {
                var lstAssets = operationalDataContext.AssetsViews
                    .Where(Asset => Asset.SerialNo == AssetSerial)
                    .Select(Asset => new AssetsViewDTO
                    {
                        ItemId = Asset.Id,
                        OriginalIdent = Asset.Id.ToString(),
                        ItemName = Asset.Name,
                        SerialNo = Asset.SerialNo,
                        ItemCategoryId = Asset.AssetTypeId,
                        ItemCategoryName = Asset.AssetType,
                        ItemStatusId = Asset.AssetStatusId,
                        ItemStatusName = Asset.AssetStatus,
                        Longitude = Asset.Longitude,
                        Latitude = Asset.Latitude,
                        LocationCode = Asset.LocationCode,
                        LinkId = Asset.LinkId,
                        FromNodeId = Asset.FromNodeId,
                        CurrentValue = Asset.CurrentValue,
                        AreaId = Asset.AreaId ?? 0,
                        AreaName = Asset.AreaName,
                        AssetsDetails = Asset.AssetsDetailsViews.Select(AV => new AssetsDetailsViewDTO
                        {
                            Id = AV.Id,
                            Name = AV.Name,
                            SerialNo = AV.SerialNo
                        }).ToList()
                    }).FirstOrDefault();

                return lstAssets;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public string GetAssetLocation(int assetId, bool isArabic)
        {
            try
            {
                var asset = operationalDataContext.Assets.FirstOrDefault(x => x.Id == assetId);
                if (asset != null)
                    return isArabic ? asset.LocationArabic : asset.Location;
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        public string GetAssetSerialNumber(long assetId)
        {
            try
            {
                var asset = operationalDataContext.Assets.FirstOrDefault(x => x.Id == assetId);
                if (asset != null)
                    return asset.SerialNo;
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        public List<AssetsViewDTO> GetAssetsList(int? AssetStatusId, int? AssetTypeId)
        {
            try
            {
                var lstAssets = operationalDataContext.AssetsViews
                    .Where(Asset => (AssetStatusId == null || Asset.AssetStatusId == AssetStatusId)
                                && (AssetTypeId == null || Asset.AssetTypeId == AssetTypeId))
                    .Select(Asset => new AssetsViewDTO
                    {
                        ItemId = Asset.Id,
                        OriginalIdent = Asset.Id.ToString(),
                        ItemName = Asset.Name,
                        SerialNo = Asset.SerialNo,
                        ItemCategoryId = Asset.AssetTypeId,
                        ItemCategoryName = Asset.AssetType,
                        ItemStatusId = Asset.AssetStatusId,
                        ItemStatusName = Asset.AssetStatus,
                        Longitude = Asset.Longitude,
                        Latitude = Asset.Latitude,
                        LocationCode = Asset.LocationCode,
                        LinkId = Asset.LinkId,
                        FromNodeId = Asset.FromNodeId,
                        CurrentValue = Asset.CurrentValue,
                        AreaId = Asset.AreaId ?? 0,
                        AreaName = Asset.AreaName,
                        AssetsDetails = Asset.AssetsDetailsViews.Select(AV => new AssetsDetailsViewDTO
                        {
                            Id = AV.Id,
                            Name = AV.Name,
                            SerialNo = AV.SerialNo
                        }).ToList()
                    }).ToList();

                return lstAssets;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public List<AssetsViewDTO> GetAssetsListBySource(int? AssetStatusId, int? AssetTypeId, int? sourceId)
        {
            try
            {
                var lstAssets = operationalDataContext.AssetsViews
                    .Where(Asset => (AssetStatusId == null || Asset.AssetStatusId == AssetStatusId)
                                && (AssetTypeId == null || Asset.AssetTypeId == AssetTypeId)
                                && (sourceId == null || Asset.SourceId == sourceId))
                    .Select(Asset => new AssetsViewDTO
                    {
                        ItemId = Asset.Id,
                        OriginalIdent = Asset.Id.ToString(),
                        ItemName = Asset.Name,
                        SerialNo = Asset.SerialNo,
                        ItemCategoryId = Asset.AssetTypeId,
                        ItemCategoryName = Asset.AssetType,
                        ItemStatusId = Asset.AssetStatusId,
                        ItemStatusName = Asset.AssetStatus,
                        Longitude = Asset.Longitude,
                        Latitude = Asset.Latitude,
                        LocationCode = Asset.LocationCode,
                        LinkId = Asset.LinkId,
                        CurrentValue = Asset.CurrentValue,
                        AreaId = Asset.AreaId ?? 0,
                        AreaName = Asset.AreaName,
                        AssetsDetails = Asset.AssetsDetailsViews.Select(AV => new AssetsDetailsViewDTO
                        {
                            Id = AV.Id,
                            Name = AV.Name,
                            SerialNo = AV.SerialNo
                        }).ToList()
                    }).ToList();

                return lstAssets;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
            }

            return null;
        }

        public List<AssetLastStatusDTO> GetAssetsLastStatusList(bool? IsNoticed)
        {
            try
            {
                var lstAssets = operationalDataContext.AssetLastStatusViews.Where(Asset => IsNoticed == null || Asset.IsNoticed == IsNoticed)
                    .Select(Asset => new AssetLastStatusDTO
                    {
                        AssetCode = Asset.AssetCode,
                        AssetId = Asset.AssetId,
                        AssetLastStatusId = Asset.AssetLastStatusId,
                        AssetStatusId = Asset.AssetStatusId,
                        AssetStatusName = Asset.AssetStatusName,
                        DateChanged = Asset.DateChanged,
                        IsNoticed = Asset.IsNoticed,
                        AssetTypeId = Asset.AssetTypeId,
                        AssetTypeName = Asset.AssetTypeName
                    }).ToList();

                return lstAssets;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public bool UpdateAssetValue(long AssetId, string NewValue)
        {
            var asset = operationalDataContext.Assets.FirstOrDefault(x => x.Id == AssetId);
            if (asset == null)
                return false;
            asset.CurrentValue = NewValue;
            return operationalDataContext.SaveChanges() > 0;
        }
        public List<AssetsViewDTO> GetNearByTowersByLatLon(double longitude, double latitude, double radius)
        {
            try
            {
                var pointString = string.Format(
                "POINT({0} {1})",
                longitude.ToString(),
                latitude.ToString());
                DbGeography dbGeography = DbGeography.FromText(pointString).Buffer(radius);

                var lstAssets = (from av in operationalDataContext.AssetsViews
                                 join at in operationalDataContext.AssetTypeDIMViews on av.AssetTypeId equals at.AssetTypeId
                                 where at.AssetTypeCode == "SmartTowersCode" && av.GeoLocation.Intersects(dbGeography)
                                 select new AssetsViewDTO()
                                 {
                                     Latitude = av.Latitude,
                                     Longitude = av.Longitude,
                                     ItemId = av.Id,
                                     ItemCategoryId = av.AssetTypeId,
                                     ItemCategoryName = av.AssetType,
                                     ItemImage = "",
                                     ItemName = av.Name,
                                     ItemStatusId = av.AssetStatusId,
                                     ItemStatusName = av.AssetStatus,
                                     SerialNo = av.SerialNo,
                                     LocationCode = av.LocationCode,
                                     CurrentValue = av.CurrentValue
                                 }).ToList();

                return lstAssets;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
        public List<AssetsViewDTO> GetNearByRadarsByLatLon(double longitude, double latitude, double radius)
        {
            try
            {
                var pointString = string.Format(
                "POINT({0} {1})",
                longitude.ToString(),
                latitude.ToString());
                DbGeography dbGeography = DbGeography.FromText(pointString).Buffer(radius);

                var lstAssets = (from av in operationalDataContext.AssetsViews
                                 join at in operationalDataContext.AssetTypeDIMViews on av.AssetTypeId equals at.AssetTypeId
                                 where at.AssetTypeCode == "VitronicRadarsCode" && av.GeoLocation.Intersects(dbGeography)
                                 select new AssetsViewDTO()
                                 {
                                     Latitude = av.Latitude,
                                     Longitude = av.Longitude,
                                     ItemId = av.Id,
                                     ItemCategoryId = av.AssetTypeId,
                                     ItemCategoryName = av.AssetType,
                                     ItemImage = "",
                                     ItemName = av.Name,
                                     ItemStatusId = av.AssetStatusId,
                                     ItemStatusName = av.AssetStatus,
                                     SerialNo = av.SerialNo,
                                     LocationCode = av.LocationCode,
                                     CurrentValue = av.CurrentValue
                                 }).ToList();

                return lstAssets;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
        public List<AssetsViewDTO> GetNearByCamerasByLatLon(double longitude, double latitude, double radius)
        {
            try
            {
                var pointString = string.Format(
                "POINT({0} {1})",
                longitude.ToString(),
                latitude.ToString());
                DbGeography dbGeography = DbGeography.FromText(pointString).Buffer(radius);

                var lstAssets = (from av in operationalDataContext.AssetsViews
                                 join at in operationalDataContext.AssetTypeDIMViews on av.AssetTypeId equals at.AssetTypeId
                                 where (at.AssetTypeCode == "EkinRedLightCameraCode" || at.AssetTypeCode == "VitronicRadarsCode") && av.GeoLocation.Intersects(dbGeography)
                                 select new AssetsViewDTO()
                                 {
                                     Latitude = av.Latitude,
                                     Longitude = av.Longitude,
                                     ItemId = av.Id,
                                     ItemCategoryId = av.AssetTypeId,
                                     ItemCategoryName = av.AssetType,
                                     ItemImage = "",
                                     ItemName = av.Name,
                                     ItemStatusId = av.AssetStatusId,
                                     ItemStatusName = av.AssetStatus,
                                     SerialNo = av.SerialNo,
                                     LocationCode = av.LocationCode,
                                     CurrentValue = av.CurrentValue
                                 }).ToList();

                return lstAssets;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
        public List<AssetsDetailsViewDTO> GetAllTowerCameras(long TowerId)
        {
            try
            {
                var lstAssets = operationalDataContext.AssetsDetailsViews
                    .Where(Asset => Asset.AssetId == TowerId)
                    .Select(Asset => new AssetsDetailsViewDTO
                    {
                        Id = Asset.Id,
                        Name = Asset.Name,
                        SerialNo = Asset.SerialNo,
                        CameraURL = Asset.CameraURL
                    }).ToList();

                return lstAssets;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public AssetsViewDTO GetAssetDataById(int AssetId)
        {
            try
            {
                return operationalDataContext.AssetsViews
                    .Where(Asset => Asset.Id == AssetId)
                    .Select(Asset => new AssetsViewDTO
                    {
                        ItemId = Asset.Id,
                        OriginalIdent = Asset.Id.ToString(),
                        ItemName = Asset.Name,
                        SerialNo = Asset.SerialNo,
                        ItemCategoryId = Asset.AssetTypeId,
                        ItemCategoryName = Asset.AssetType,
                        ItemStatusId = Asset.AssetStatusId,
                        ItemStatusName = Asset.AssetStatus,
                        Longitude = Asset.Longitude,
                        Latitude = Asset.Latitude,
                        LocationCode = Asset.LocationCode,
                        LinkId = Asset.LinkId,
                        AreaId = Asset.AreaId ?? 0,
                        AreaName = Asset.AreaName,
                        CurrentValue = Asset.CurrentValue
                    }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);

                return null;
            }
        }
    }
}
