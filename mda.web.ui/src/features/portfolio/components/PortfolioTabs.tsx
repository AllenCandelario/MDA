import { useState } from "react";
import HoldingsTab from "./HoldingsTab";
import DividendsTab from "./DividendsTab";

const TABS = [
  { id: "holdings", label: "Holdings", node: <HoldingsTab /> },
  { id: "dividends", label: "Dividends", node: <DividendsTab /> },
];

export default function PortfolioTabs() {
  const [active, setActive] = useState("holdings");

  return (
    <div className="space-y-4">
      <div className="flex items-center gap-6">
        {TABS.map(t => (
          <button
            key={t.id}
            onClick={() => setActive(t.id)}
            className={`pb-1 text-sm font-medium text-slate-700 hover:text-slate-900 border-b-2 ${
              active === t.id ? "border-blue-600 text-slate-900" : "border-transparent"
            }`}
          >
            {t.label}
          </button>
        ))}
      </div>
      <div className="border-b border-slate-200" />

      <div>{TABS.find(t => t.id === active)?.node}</div>
    </div>
  );
}