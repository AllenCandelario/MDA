import { useEffect, useState } from "react";
import type { DividendItem } from "../types";

export function useDividends() {
  const [items, setItems] = useState<DividendItem[]>([]);

  useEffect(() => {
    // TODO: replace with real data.
    setItems([
      { symbol: "VTI", name: "Vanguard Total Stock Market ETF", currency: "USD", amount: 145.23, payDate: "2025-12-28", exDate: "2025-12-20", status: "Scheduled" },
      { symbol: "XOM", name: "Exxon Mobil", currency: "USD", amount: 48.00, payDate: "2025-11-30", exDate: "2025-11-15", status: "Paid" },
    ]);
  }, []);

  return { items };
}