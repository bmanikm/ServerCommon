﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using NuGet.Versioning;

namespace NuGet.Services.Validation
{
    public class PackageValidationMessageData
    {
        public PackageValidationMessageData(
           string packageId,
           string packageVersion,
           Guid validationTrackingId)
         : this(packageId, packageVersion, validationTrackingId, validatingType: ValidatingType.Package, deliveryCount: 0)
        {
        }

        public PackageValidationMessageData(
            string packageId,
            string packageVersion,
            Guid validationTrackingId,
            ValidatingType validatingType)
          : this(packageId, packageVersion, validationTrackingId, validatingType, deliveryCount: 0)
        {
        }

        internal PackageValidationMessageData(
            string packageId,
            string packageVersion,
            Guid validationTrackingId,
            ValidatingType validatingType,
            int deliveryCount)
        {
            if (validationTrackingId == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(validationTrackingId));
            }

            PackageId = packageId ?? throw new ArgumentNullException(nameof(packageId));
            PackageVersion = packageVersion ?? throw new ArgumentNullException(nameof(packageVersion));
            PackageNormalizedVersion = NuGetVersion.Parse(packageVersion).ToNormalizedString();
            ValidationTrackingId = validationTrackingId;
            DeliveryCount = deliveryCount;
            ValidatingType = validatingType;
        }

        public string PackageId { get; }
        public string PackageVersion { get; }
        public string PackageNormalizedVersion { get; }
        public Guid ValidationTrackingId { get; }
        public int DeliveryCount { get; }
        public ValidatingType ValidatingType { get; }
    }
}
