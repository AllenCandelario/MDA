import { useEffect, useState } from "react";
import type { PortfolioSummary } from "../types";
import { getPortfolioSummaryByAccountId } from "../api/portfolioApi";
import { useActiveAccount } from "../../../app/providers/ActiveAccountProvider";

export function usePortfolioSummary() {
  const { active, isBootStrapping } = useActiveAccount();
  const [summary, setSummary] = useState<PortfolioSummary | null>(null);

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
      const portfolioSummaryResponse = await getPortfolioSummaryByAccountId(active.id)
      setSummary(portfolioSummaryResponse);
    } catch (err) {
      setError(err);
    } finally {
      setLoading(false);
    }
  }

  return { summary, loading, error };
}