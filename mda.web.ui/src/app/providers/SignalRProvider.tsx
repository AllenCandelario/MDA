import { HubConnection } from "@microsoft/signalr";
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
        const conn = connection;

        conn
            .start()
            .then(() => setIsConnected(true))
            .catch(err => {
                console.error('SignalR connection error', err);
                setIsConnected(false);
            });

        conn.onclose(() => setIsConnected(false));

        return () => {
            conn
                .stop()
                .catch(err => {
                    console.error('Error stopping Signal', err)
                })
        }   
    }, [connection])

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

