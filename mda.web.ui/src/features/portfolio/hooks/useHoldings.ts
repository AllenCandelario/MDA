import { useEffect, useMemo, useState } from "react";
import type { Holding, CategorySummary, HoldingCategory, Holdings } from "../types";
import { getFullHoldingDetails } from "../api/portfolioApi";
import { useActiveAccount } from "../../../app/providers/ActiveAccountProvider";

const CATEGORY_GOALS: Record<HoldingCategory, { shortTermPct?: number; midTermPct?: number; longTermPct?: number }> = {
  Semiconductor: { shortTermPct: 0.2375, midTermPct: 0.22, longTermPct: 0.20 },
  AI: {},
  Energy: {},
  ETF: {},
  Other: {},
  Uncategorized: {}
};

export function useHoldings(investedAsset: number) {
  const { active, isBootStrapping } = useActiveAccount();
  const [holdings, setHoldings] = useState<Holding[]>([]);

  const [error, setError] = useState<unknown>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (isBootStrapping || !active) return;
    load();
  }, [active, isBootStrapping]);

  const load = async () => {
      setLoading(true);
      setError(false);
  
      try {
        if (active?.id == null || active.id == undefined) {
          return;
        }
        const holdingsResponse = await getFullHoldingDetails(active.id)
        setHoldings(holdingsResponse.holdings);
      } catch (err) {
        setError(err);
      } finally {
        setLoading(false);
      }
  }

  const byCategory = useMemo(() => {
    const map = new Map<string, Holding[]>();
    for (const h of holdings) {
      const arr = map.get(h.category) ?? [];
      arr.push(h);
      map.set(h.category, arr);
    }
    return map;
  }, [holdings]);

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

  return { holdings, byCategory, categorySummaries };
}