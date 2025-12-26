import { request } from "../../../lib/api";
import type { PortfolioSummary, Holdings } from "../types";

export const getPortfolioSummaryByAccountId = (accountId: string) => request<PortfolioSummary>(`/Accounts/${accountId}/summary`);

export const getFullHoldingDetails = (accountId: string) => request<Holdings>(`/Holdings/${accountId}`)