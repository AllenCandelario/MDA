import { useEffect, useMemo, useState } from "react";
import type { Holding, CategorySummary, HoldingCategory } from "../types";

const CATEGORY_GOALS: Record<HoldingCategory, { shortTermPct?: number; midTermPct?: number; longTermPct?: number }> = {
  Semiconductor: { shortTermPct: 0.2375, midTermPct: 0.22, longTermPct: 0.20 },
  AI: {},
  Energy: {},
  ETF: {},
  Other: {},
};

export function useHoldings(investedAsset: number) {
  const [holdings, setHoldings] = useState<Holding[]>([]);

  useEffect(() => {
    // TODO: replace with real source. Mock holdings for now.
    const h: Holding[] = [
      {
        symbol: "NVDA",
        name: "NVIDIA Corp",
        currency: "USD",
        category: "Semiconductor",
        lastPrice: 120.12,
        changeAbs: 2.35,
        changePct: 0.0201,
        week52High: 135.2,
        allTimeHigh: 150.0,
        pe: 70.5,
        fwdPe: 40.2,
        quantity: 50,
        avgPrice: 95.0,
        costBasis: 50 * 95,
        marketValue: 50 * 120.12,
        marketValuePctOfAssets: 0, // set below when investedAsset is known
        unrealizedAbs: 50 * (120.12 - 95),
        unrealizedPct: (120.12 - 95) / 95,
        notes: "Core AI position",
        rating: 5,
      },
      {
        symbol: "AMD",
        name: "Advanced Micro Devices",
        currency: "USD",
        category: "Semiconductor",
        lastPrice: 65.33,
        changeAbs: -0.41,
        changePct: -0.0062,
        week52High: 77.9,
        allTimeHigh: 90.0,
        pe: 45.1,
        fwdPe: 31.5,
        quantity: 80,
        avgPrice: 58.0,
        costBasis: 80 * 58,
        marketValue: 80 * 65.33,
        marketValuePctOfAssets: 0, // set below
        unrealizedAbs: 80 * (65.33 - 58),
        unrealizedPct: (65.33 - 58) / 58,
        notes: "",
        rating: 4,
      },
    ];
    setHoldings(h);
  }, []);

  const enriched = useMemo(() => {
    if (!investedAsset || investedAsset <= 0) return holdings;
    return holdings.map(h => ({
      ...h,
      marketValuePctOfAssets: h.marketValue / investedAsset,
    }));
  }, [holdings, investedAsset]);

  const byCategory = useMemo(() => {
    const map = new Map<string, Holding[]>();
    for (const h of enriched) {
      const arr = map.get(h.category) ?? [];
      arr.push(h);
      map.set(h.category, arr);
    }
    return map;
  }, [enriched]);

  const categorySummaries: CategorySummary[] = useMemo(() => {
    const out: CategorySummary[] = [];
    for (const [cat, arr] of byCategory) {
      const total = arr.reduce((s, h) => s + h.marketValue, 0);
      const pct = investedAsset > 0 ? total / investedAsset : 0;
      const goals = CATEGORY_GOALS[cat as HoldingCategory];
      out.push({ category: cat as HoldingCategory, totalMarketValue: total, pctOfInvested: pct, goals });
    }
    return out;
  }, [byCategory, investedAsset]);

  return { holdings: enriched, byCategory, categorySummaries };
}