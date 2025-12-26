import { createContext, useContext, useEffect, useMemo, useState, type ReactNode } from "react";
import type { ActiveAccount } from "../../features/account/types"
import { getActiveAccount } from "../../features/account/api/accountApi";

type ActiveAccountState = {
    active: ActiveAccount | null;
    isBootStrapping: boolean;
    error: unknown;
    setActive:(a: ActiveAccount) => void;
}

const ActiveAccountContext = createContext<ActiveAccountState | null>(null);

const STORAGE_KEY = "activeAccount";

export function ActiveAccountProvider({ children }: { children: ReactNode }) {
    const [active, setActive] = useState<ActiveAccount | null>(null);
    const [isBootStrapping, setIsBootStrapping] = useState(true);
    const [error, setError] = useState<unknown>(null);


    useEffect(() => {
        let cancelled = false;

        async function bootstrap() {
            setIsBootStrapping(true);
            setError(null);

            try {
                const raw = sessionStorage.getItem(STORAGE_KEY);
                if (raw) {
                    const parsed = JSON.parse(raw) as ActiveAccount;
                    if (!cancelled) setActive(parsed);
                    return;
                }

                const acct = await getActiveAccount("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"); //TODO: hardcoded for now
                if (cancelled) return;
                setActive(acct);
                sessionStorage.setItem(STORAGE_KEY, JSON.stringify(acct));
            } catch (e) {
                if (!cancelled) setError(e);
            } finally {
                if (!cancelled) setIsBootStrapping(false);
            }
        }

        bootstrap();
        return () => {
            cancelled = true;
        };
    }, []);

    const value = useMemo(
        () => ({
            active,
            isBootStrapping,
            error,
            setActive: (a: ActiveAccount) => {
                setActive(a);
                sessionStorage.setItem(STORAGE_KEY, JSON.stringify(a));
            },
        }),
        [active, isBootStrapping, error]
    );

    return <ActiveAccountContext.Provider value={value}>{children}</ActiveAccountContext.Provider>
}

export function useActiveAccount() {
  const ctx = useContext(ActiveAccountContext);
  if (!ctx) throw new Error("useActiveAccount must be used within ActiveAccountProvider");
  return ctx;
}