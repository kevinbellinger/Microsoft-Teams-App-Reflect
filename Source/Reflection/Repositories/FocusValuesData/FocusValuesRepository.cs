// -----------------------------------------------------------------------
// <copyright file="FocusDataRepository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Reflection.Repositories.FocusData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.ApplicationInsights;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// FocusData Repository.
    /// </summary>
    public class FocusValuesRepository : BaseRepository<FocusValuesDataEntity>
    {
        private TelemetryClient _telemetry;

        /// <summary>
        /// Initializes a new instance of the <see cref="FocusDataRepository"/> class.
        /// </summary>
        /// <param name="configuration">Represents the application configuration.</param>
        /// <param name="telemetry">telemetry.</param>
        /// <param name="isFromAzureFunction">Flag to show if created from Azure Function.</param>
        public FocusValuesRepository(IConfiguration configuration, TelemetryClient telemetry, bool isFromAzureFunction = false)
            : base(
                configuration,
                PartitionKeyNames.FocusDataTable.TableName,
                PartitionKeyNames.FocusDataTable.FocusDataPartition,
                isFromAzureFunction)
        {
            _telemetry = telemetry;
        }

        /// <summary>
        /// Get the focus data.
        /// </summary>
        /// <param name="userEmail">userEmail.</param>
        public async Task<List<FocusValuesDataEntity>> GetAllFocusDataForUser(string userEmail)
        {
            _telemetry.TrackEvent("GetAllFocusDataForUser");

            try
            {
                var allRows = await this.GetAllAsync(PartitionKeyNames.FocusDataTable.TableName);
                var result = allRows.Where(d => d.IsDefaultFlag == true || d.CreatedByEmail == userEmail);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                return null;
            }
        }

        /// <summary>
        /// Get scores By FocusDataID.
        /// </summary>
        /// <param name="FocusDataID">qID.</param>
        /// <returns>FocusDataEntity.</returns>
        public async Task<List<FocusValuesDataEntity>> GetFocusDataByFocusDataID(Guid? FocusDataID)
        {
            _telemetry.TrackEvent("GetFocusDataByFocusDataID");

            try
            {
                var allRows = await this.GetAllAsync(PartitionKeyNames.FocusDataTable.TableName);
                var result = allRows.Where(d => d.IsDefaultFlag == true || d.FocusDataID == FocusDataID);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                return null;
            }
        }

        /// <summary>
        /// Get All Focus Data.
        /// </summary>
        /// <param name="FocusDataID">fdID.</param>
        /// <returns>ConfidenceDataEntity.</returns>
        public async Task<List<FocusValuesDataEntity>> GetAllFocusData(List<Guid?> FocusDataID)
        {
            _telemetry.TrackEvent("GetAllFocusData");
            try
            {
                var allRows = await this.GetAllAsync(PartitionKeyNames.FocusDataTable.TableName);
                List<FocusValuesDataEntity> result = allRows.Where(c => FocusDataID.Contains(c.FocusDataID)).ToList();
                return result ?? null;
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Focus Data.
        /// </summary>
        /// <param name="FocusDataID">FocusDataID.</param>
        /// <returns>Bool.</returns>
        public async Task<FocusValuesDataEntity> GetFocusData(Guid? FocusDataID)
        {
            _telemetry.TrackEvent("GetFocusData");
            try
            {
                var allRows = await this.GetAllAsync(PartitionKeyNames.FocusDataTable.TableName);
                FocusValuesDataEntity result = allRows.Where(c => c.FocusDataID == FocusDataID).FirstOrDefault();
                return result ?? null;
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                return null;
            }
        }
    }
}
