﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace NuGet.Services.ServiceBus
{
    public class TopicClientWrapper : ITopicClient
    {
        private readonly TopicClient _client;

        public TopicClientWrapper(string connectionString, string path)
        {
            _client = TopicClient.CreateFromConnectionString(connectionString, path);
        }

        public Task SendAsync(IBrokeredMessage message)
        {
            // For now, assume the only implementation is the wrapper type. We could clone over all properties
            // that the interface supports, but this is not necessary right now.
            var wrapper = message as BrokeredMessageWrapper;
            BrokeredMessage innerMessage;
            if (message != null)
            {
                innerMessage = wrapper.BrokeredMessage;
            }
            else
            {
                throw new ArgumentException(
                    $"The message must be of type {typeof(BrokeredMessageWrapper).FullName}.",
                    nameof(message));
            }

            return _client.SendAsync(innerMessage);
        }

        public Task Close()
        {
            return _client.CloseAsync();
        }
    }
}
