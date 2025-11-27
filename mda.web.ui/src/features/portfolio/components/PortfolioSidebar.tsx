import SidebarMetric from "./SidebarMetric";
import { usePortfolioSummary } from "../hooks/usePortfolioSummary";

function moneyNoSymbol(n?: number) {
  if (n == null) return "—";
  return new Intl.NumberFormat(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(n);
}

export default function PortfolioSidebar() {
  const { summary, dividendsToBePaid } = usePortfolioSummary();

  return (
    <aside className="w-full lg:w-80 xl:w-96 space-y-2">
      <div className="rounded-2xl bg-white p-4 shadow-sm space-y-1 mr-5 ">
        <SidebarMetric label="Account" value={summary?.accountId ?? "—"} />

        <SidebarMetric label="Total amount" value={moneyNoSymbol(summary?.totalPortfolioValue)} />
        <div className="mt-1 space-y-1 pl-3 border-l">
          <SidebarMetric label="Invested" value={moneyNoSymbol(summary?.investedAsset)} />
          <SidebarMetric label="Settled cash" value={moneyNoSymbol(summary?.settledCash)} />
           <div className="mt-1 space-y-1 pl-3 border-l">
            <SidebarMetric label="Excess Liquidity" value={moneyNoSymbol(summary?.excessLiquidity)} />
            <SidebarMetric label="Buying Power" value={moneyNoSymbol(summary?.buyingPower)} />
          </div>
        </div>

        <div className="pt-2" />
        <SidebarMetric label="Unrealized P&L" value={moneyNoSymbol(summary?.unrealizedPnL)} />
        <div className="mt-1 space-y-1 pl-3 border-l">
            <SidebarMetric label="Daily P&L" value={moneyNoSymbol(summary?.dailyPnL)} />
        </div>

        <div className="pt-2" />
        <SidebarMetric label="Dividends (year)" value={moneyNoSymbol(summary?.expectedDividendsYear)} />
        <div className="mt-1 space-y-1 pl-3 border-l">
          <SidebarMetric label="Dividends paid out" value={moneyNoSymbol(summary?.dividendsPaidYtd)} />
          <SidebarMetric label="Dividends to be paid out" value={moneyNoSymbol(dividendsToBePaid)} />
        </div>
      </div>
    </aside>
  );
}