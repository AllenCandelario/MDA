import { useEffect, useState } from "react";
import type { PortfolioSummary } from "../types";

export function usePortfolioSummary() {
  const [summary, setSummary] = useState<PortfolioSummary | null>(null);

  useEffect(() => {
    // TODO: replace with real data source (REST/SignalR). Mock for now.
    const total = 125000;
    const settled = 30000;
    const dailyPnL = 420; // +$420 today
    const unrealized = 7850;
    const expectedDivYr = 2600;
    const paidYtd = 1900;

    const mock: PortfolioSummary = {
      accountId: "DU1234567",
      totalPortfolioValue: total,
      settledCash: settled,
      excessLiquidity: 55000,
      buyingPower: 220000,
      investedAsset: total - settled,
      dailyPnL,
      unrealizedPnL: unrealized,
      expectedDividendsYear: expectedDivYr,
      dividendsPaidYtd: paidYtd,
    };
    setSummary(mock);
  }, []);

  const dividendsToBePaid = summary
    ? summary.expectedDividendsYear - summary.dividendsPaidYtd
    : 0;

  return { summary, dividendsToBePaid };
}