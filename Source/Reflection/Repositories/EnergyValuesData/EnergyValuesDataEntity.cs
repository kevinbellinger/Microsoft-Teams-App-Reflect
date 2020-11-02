// -----------------------------------------------------------------------
// <copyright file="QuestionsDataEntity.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Reflection.Repositories.EnergyData
{
    using System;
    using Microsoft.Azure.Cosmos.Table;

    /// <summary>
    /// QuestionsDataEntity.
    /// </summary>
    public class EnergyValuesDataEntity : TableEntity
    {
        /// <summary>
        /// Gets or sets ConfidenceDataID.
        /// </summary>
        public Guid? EnergyDataID { get; set; }

        /// <summary>
        /// Gets or sets Value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets QuestionCreatedDate.
        /// </summary>
        public DateTime EnergyDataCreatedDate { get; set; }

        /// <summary>
        /// Gets or sets IsDefaultFlag.
        /// </summary>
        public bool IsDefaultFlag { get; set; }

        /// <summary>
        /// Gets or sets CreatedBy.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets CreatedByEmail.
        /// </summary>
        public string CreatedByEmail { get; set; }

    }
}
