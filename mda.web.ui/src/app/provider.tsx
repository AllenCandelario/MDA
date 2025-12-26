import type { ReactNode } from "react";
import { SignalRProvider } from "./providers/SignalRProvider";
import { ActiveAccountProvider } from "./providers/ActiveAccountProvider";

export default function AppProviders({ children }: { children: ReactNode }) {
    return ( 
        <ActiveAccountProvider>
            <SignalRProvider>
                {/* < SomeOtherProvider> */}
                    {children}
                {/* </SomeOtherProvider> */}
            </SignalRProvider>
        </ActiveAccountProvider>
    )
}