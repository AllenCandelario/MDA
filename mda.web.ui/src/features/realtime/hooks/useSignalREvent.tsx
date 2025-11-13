// src/features/realtime/hooks/useSignalREvent.ts
import { useEffect } from 'react';
import { useSignalRContext } from '../../../app/providers/SignalRProvider';

export function useSignalREvent<T = any>(eventName: string, handler: (payload: T) => void) {
  const { connection } = useSignalRContext();

  useEffect(() => {
    if (!connection) return;

    connection.on(eventName, handler);

    return () => {
      connection.off(eventName, handler);
    };
  }, [connection, eventName, handler]);
}
