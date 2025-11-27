import type { ReactNode } from "react";
import { SignalRProvider } from "./providers/SignalRProvider";

export default function AppProviders({ children }: { children: ReactNode }) {
    return ( 
        <SignalRProvider>
        {/* < SomeOtherProvider> */}
            {children}
        {/* </SomeOtherProvider> */}
        </SignalRProvider>
    )
}