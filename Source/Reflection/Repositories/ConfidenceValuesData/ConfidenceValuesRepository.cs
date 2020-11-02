// -----------------------------------------------------------------------
// <copyright file="ConfidenceDataRepository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Reflection.Repositories.ConfidenceData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.ApplicationInsights;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// ConfidenceData Repository.
    /// </summary>
    public class ConfidenceValuesRepository : BaseRepository<ConfidenceValuesDataEntity>
    {
        private TelemetryClient _telemetry;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfidenceDataRepository"/> class.
        /// </summary>
        /// <param name="configuration">Represents the application configuration.</param>
        /// <param name="telemetry">telemetry.</param>
        /// <param name="isFromAzureFunction">Flag to show if created from Azure Function.</param>
        public ConfidenceValuesRepository(IConfiguration configuration, TelemetryClient telemetry, bool isFromAzureFunction = false)
            : base(
                configuration,
                PartitionKeyNames.ConfidenceDataTable.TableName,
                PartitionKeyNames.ConfidenceDataTable.ConfidenceDataPartition,
                isFromAzureFunction)
        {
            _telemetry = telemetry;
        }

        /// <summary>
        /// Get the confidence data.
        /// </summary>
        /// <param name="userEmail">userEmail.</param>
        public async Task<List<ConfidenceValuesDataEntity>> GetAllConfidenceDataForUser(string userEmail)
        {
            _telemetry.TrackEvent("GetAllConfidenceDataForUser");

            try
            {
                var allRows = await this.GetAllAsync(PartitionKeyNames.ConfidenceDataTable.TableName);
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
        /// Get scores By ConfidenceDataID.
        /// </summary>
        /// <param name="ConfidenceDataID">qID.</param>
        /// <returns>ConfidenceDataEntity.</returns>
        public async Task<List<ConfidenceValuesDataEntity>> GetConfidenceDataByConfidenceDataID(Guid? ConfidenceDataID)
        {
            _telemetry.TrackEvent("GetConfidenceDataByConfidenceDataID");

            try
            {
                var allRows = await this.GetAllAsync(PartitionKeyNames.ConfidenceDataTable.TableName);
                var result = allRows.Where(d => d.IsDefaultFlag == true || d.ConfidenceDataID == ConfidenceDataID);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                return null;
            }
        }

        /// <summary>
        /// Get All Confidence Data.
        /// </summary>
        /// <param name="ConfidenceDataID">cdID.</param>
        /// <returns>ConfidenceDataEntity.</returns>
        public async Task<List<ConfidenceValuesDataEntity>> GetAllConfidenceData(List<Guid?> ConfidenceDataID)
        {
            _telemetry.TrackEvent("GetAllConfidenceData");
            try
            {
                var allRows = await this.GetAllAsync(PartitionKeyNames.ConfidenceDataTable.TableName);
                List<ConfidenceValuesDataEntity> result = allRows.Where(c => ConfidenceDataID.Contains(c.ConfidenceDataID)).ToList();
                return result ?? null;
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Confidence Data.
        /// </summary>
        /// <param name="ConfidenceDataID">ConfidenceDataID.</param>
        /// <returns>Bool.</returns>
        public async Task<ConfidenceValuesDataEntity> GetConfidenceData(Guid? ConfidenceDataID)
        {
            _telemetry.TrackEvent("GetConfidenceData");
            try
            {
                var allRows = await this.GetAllAsync(PartitionKeyNames.ConfidenceDataTable.TableName);
                ConfidenceValuesDataEntity result = allRows.Where(c => c.ConfidenceDataID == ConfidenceDataID).FirstOrDefault();
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
