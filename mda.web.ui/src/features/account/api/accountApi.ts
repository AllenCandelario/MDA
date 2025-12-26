import { request } from "../../../lib/api";
import type { ActiveAccount } from "../types";


export const getActiveAccount = (accountId: string) => request<ActiveAccount>(`/Accounts/${accountId}`);