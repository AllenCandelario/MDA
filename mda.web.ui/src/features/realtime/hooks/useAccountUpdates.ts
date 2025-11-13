// src/features/realtime/hooks/useAccountUpdates.ts
import { useState } from 'react';
import { useSignalRContext } from '../../../app/providers/SignalRProvider';
import { useSignalREvent } from './useSignalREvent';
import type { AccountUpdateMessage } from '../types';

export function useAccountUpdates() {
  const [accountUpdates, setAccountUpdates] = useState<AccountUpdateMessage[]>([]);
  const { isConnected } = useSignalRContext();

  useSignalREvent<any>('accountUpdate', payload => {
    const key = payload?.key ?? 'accountUpdate';
    const value =
      typeof payload?.value === 'string'
        ? payload.value
        : JSON.stringify(payload?.value);

    setAccountUpdates(prev => [
      { key, value, receivedAt: new Date().toISOString() },
      ...prev,
    ]);
  });

  return { accountUpdates, isConnected };
}
