import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { env } from '../../config/env';

export function createHubConnection(): HubConnection {
  return new HubConnectionBuilder()
    .withUrl(env.signalRHubUrl)
    .withAutomaticReconnect()
    .configureLogging(LogLevel.Information)
    .build();
}