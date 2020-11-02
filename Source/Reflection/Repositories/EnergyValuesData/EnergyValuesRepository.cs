// -----------------------------------------------------------------------
// <copyright file="EnergyValuesRepository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Reflection.Repositories.EnergyData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.ApplicationInsights;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// EnergyData Repository.
    /// </summary>
    public class EnergyValuesRepository : BaseRepository<EnergyValuesDataEntity>
    {
        private TelemetryClient _telemetry;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnergyDataRepository"/> class.
        /// </summary>
        /// <param name="configuration">Represents the application configuration.</param>
        /// <param name="telemetry">telemetry.</param>
        /// <param name="isFromAzureFunction">Flag to show if created from Azure Function.</param>
        public EnergyValuesRepository(IConfiguration configuration, TelemetryClient telemetry, bool isFromAzureFunction = false)
            : base(
                configuration,
                PartitionKeyNames.EnergyDataTable.TableName,
                PartitionKeyNames.EnergyDataTable.EnergyDataPartition,
                isFromAzureFunction)
        {
            _telemetry = telemetry;
        }

        /// <summary>
        /// Get the energy data.
        /// </summary>
        /// <param name="userEmail">userEmail.</param>
        public async Task<List<EnergyValuesDataEntity>> GetAllEnergyDataForUser(string userEmail)
        {
            _telemetry.TrackEvent("GetAllEnergyDataForUser");

            try
            {
                var allRows = await this.GetAllAsync(PartitionKeyNames.EnergyDataTable.TableName);
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
        /// Get scores By EnergyDataID.
        /// </summary>
        /// <param name="EnergyDataID">edID.</param>
        /// <returns>EnergyDataEntity.</returns>
        public async Task<List<EnergyValuesDataEntity>> GetEnergyDataByFocusDataID(Guid? EnergyDataID)
        {
            _telemetry.TrackEvent("GetEnergyDataByEnergyDataID");

            try
            {
                var allRows = await this.GetAllAsync(PartitionKeyNames.EnergyDataTable.TableName);
                var result = allRows.Where(d => d.IsDefaultFlag == true || d.EnergyDataID == EnergyDataID);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                return null;
            }
        }

        /// <summary>
        /// Get All Energy Data.
        /// </summary>
        /// <param name="EnergyDataID">edID.</param>
        /// <returns>EnergyDataEntity.</returns>
        public async Task<List<EnergyValuesDataEntity>> GetAllEnergyData(List<Guid?> EnergyDataID)
        {
            _telemetry.TrackEvent("GetAllEnergyData");
            try
            {
                var allRows = await this.GetAllAsync(PartitionKeyNames.EnergyDataTable.TableName);
                List<EnergyValuesDataEntity> result = allRows.Where(c => EnergyDataID.Contains(c.EnergyDataID)).ToList();
                return result ?? null;
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Energy Data.
        /// </summary>
        /// <param name="EnergyDataID">EnergyDataID.</param>
        /// <returns>Bool.</returns>
        public async Task<EnergyValuesDataEntity> GetEnergyData(Guid? EnergyDataID)
        {
            _telemetry.TrackEvent("GetEnergyData");
            try
            {
                var allRows = await this.GetAllAsync(PartitionKeyNames.EnergyDataTable.TableName);
                EnergyValuesDataEntity result = allRows.Where(c => c.EnergyDataID == EnergyDataID).FirstOrDefault();
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
