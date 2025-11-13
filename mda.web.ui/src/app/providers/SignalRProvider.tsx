import { HubConnection, HubConnectionState } from "@microsoft/signalr";
import { createContext, useContext, useEffect, useMemo, useState, type ReactNode } from "react";
import { createHubConnection } from "../../lib/signalr/createHubConnection";

type SignalRContextValue = {
    connection: HubConnection | null;
    isConnected: boolean;
}

const SignalRContext = createContext<SignalRContextValue | undefined>(undefined);

export function SignalRProvider( { children }: { children: ReactNode }) {
    const [connection] = useState<HubConnection>(() => createHubConnection());
    const [isConnected, setIsConnected] = useState(false);


    useEffect(() => {
        let isDisposed = false;

        connection.onclose(() => {
            if (!isDisposed) {
            setIsConnected(false);
            }
        });
        
        startConnection(isDisposed);

        return () => {
            isDisposed = true;
            if ( connection.state === HubConnectionState.Connected || connection.state === HubConnectionState.Reconnecting) {
                connection
                    .stop()
                    .catch(err =>
                        console.error('Error stopping SignalR connection', err),
                    );
            }

            connection.onclose(null as any); // Optional: detach onclose to avoid accumulating handlers
        }; 
    }, [connection])

    async function startConnection(isDisposed: boolean) {
        if (!connection) return;
        
        if (connection.state !== HubConnectionState.Disconnected) return;

         try {
            await connection.start();
            if (!isDisposed) {
            setIsConnected(true);
            }
        } catch (err) {
            console.error('SignalR connection error', err);
            if (!isDisposed) {
            setIsConnected(false);
            }
        }
    }

    const value = useMemo(
        () => ({ connection, isConnected }),
        [connection, isConnected],
    );

    return <SignalRContext.Provider value={value}>{children}</SignalRContext.Provider>
}

export function useSignalRContext() : SignalRContextValue {
    const ctx  = useContext(SignalRContext);
    if (!ctx) {
        throw new Error('useSignalRContext must be used within a SignalRProvider');
    }
    return ctx;
}