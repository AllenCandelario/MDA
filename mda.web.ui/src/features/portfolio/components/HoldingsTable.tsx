import type { Holding } from "../types";

function pct(n?: number) { return n == null ? "—" : (n * 100).toFixed(2) + "%"; }
function moneyNoSymbol(n?: number) {
  if (n == null) return "—";
  return new Intl.NumberFormat(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(n);
}

export default function HoldingsTable({ rows }: { rows: Holding[] }) {
  return (
    <div className="w-full overflow-x-auto">
      <table className="min-w-full text-sm">
        <thead className="text-left text-xs uppercase text-slate-500">
          <tr className="border-b whitespace-nowrap">
            <th className="py-2 pr-3">Stock</th>
            <th className="py-2 pr-3 text-right">Last</th>
            <th className="py-2 pr-3 text-right">Change</th>
            <th className="py-2 pr-3 text-right">52WH</th>
            <th className="py-2 pr-3 text-right">ATH</th>
            <th className="py-2 pr-3 text-right">P/E</th>
            <th className="py-2 pr-3 text-right">Fwd P/E</th>
            <th className="py-2 pr-3 text-right">Pos @ Avg / Cost</th>
            <th className="py-2 pr-3 text-right">Mkt Value</th>
            <th className="py-2 pr-3 text-right">Mkt % of Assets</th>
            <th className="py-2 pr-3 text-right">Unrealized</th>
            <th className="py-2 pr-3">Notes</th>
            <th className="py-2 pl-3 text-right">Rating</th>
          </tr>
        </thead>
        <tbody className="divide-y">
          {rows.map(r => {
            const changeClass = r.changeAbs > 0 ? "text-green-600" : r.changeAbs < 0 ? "text-red-600" : "";
            const unrealClass = r.unrealizedAbs > 0 ? "text-green-600" : r.unrealizedAbs < 0 ? "text-red-600" : "";
            const sign = r.changeAbs > 0 ? "+" : "";
            return (
              <tr key={r.symbol} className="hover:bg-slate-50">
                <td className="py-2 pr-3">
                  <div className="flex flex-col">
                    <span className="font-medium leading-tight">{r.symbol}</span>
                    <span className="text-[0.6rem] text-slate-500 leading-tight">{r.name}</span>
                  </div>
                </td>
                <td className="py-2 pr-3 text-right tabular-nums">{moneyNoSymbol(r.lastPrice)}</td>
                <td className="py-2 pr-3">
                  <div className="flex flex-col">
                    <span className={`text-right tabular-nums ${changeClass}`}>{sign}{moneyNoSymbol(r.changeAbs)}</span>
                    <span className={`text-right tabular-nums ${changeClass}`}>({pct(r.changePct)})</span>
                  </div>
                </td>



                <td className="py-2 pr-3 text-right tabular-nums">{r.week52High ?? "—"}</td>
                <td className="py-2 pr-3 text-right tabular-nums">{r.allTimeHigh ?? "—"}</td>
                <td className="py-2 pr-3 text-right tabular-nums">{r.pe ?? "—"}</td>
                <td className="py-2 pr-3 text-right tabular-nums">{r.fwdPe ?? "—"}</td>
                <td className="py-2 pr-3 text-right tabular-nums whitespace-nowrap">{r.quantity} @ {moneyNoSymbol(r.avgPrice)} / {moneyNoSymbol(r.costBasis)}</td>
                <td className="py-2 pr-3 text-right tabular-nums">{moneyNoSymbol(r.marketValue)}</td>
                <td className="py-2 pr-3 text-right tabular-nums">{pct(r.marketValuePctOfAssets)}</td>
                <td className={`py-2 pr-3 text-right tabular-nums ${unrealClass}`}>{moneyNoSymbol(r.unrealizedAbs)} ({pct(r.unrealizedPct)})</td>
                <td className="py-2 pr-3 max-w-[24rem] truncate" title={r.notes || ""}>{r.notes || "—"}</td>
                <td className="py-2 pl-3 text-right">{"★".repeat(r.rating ?? 0) || "—"}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}