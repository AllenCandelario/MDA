import type { Holding } from "../types";

function pct(n?: number) {
  if (n == null) return "—";
  return (n * 100).toFixed(2) + "%";
}

function moneyNoSymbol(n?: number) {
  if (n == null) return "—";
  return new Intl.NumberFormat(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(n);
}

type Props = { holding: Holding };

export default function StockRow({ holding }: Props) {
  const c = holding;
  const changeSign = c.changeAbs > 0 ? "+" : "";
  const changeColor = c.changeAbs > 0 ? "text-green-600" : c.changeAbs < 0 ? "text-red-600" : "text-gray-700";
  const unrealColor = c.unrealizedAbs > 0 ? "text-green-600" : c.unrealizedAbs < 0 ? "text-red-600" : "text-gray-700";

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 gap-3 py-3">
      {/* middle-left market info */}
      <div className="rounded-xl border p-3 bg-white">
        <div className="flex items-baseline justify-between">
          <div className="font-semibold">{c.symbol} <span className="text-gray-500 font-normal">{c.name}</span></div>
          <div className="text-sm text-gray-500">{c.currency}</div>
        </div>
        <div className="mt-2 grid grid-cols-2 gap-x-4 gap-y-1 text-sm">
          <div>Last</div>
          <div className="text-right font-medium tabular-nums">{moneyNoSymbol(c.lastPrice)}</div>
          <div>Change</div>
          <div className={`text-right font-medium tabular-nums ${changeColor}`}>
            {changeSign}{moneyNoSymbol(c.changeAbs)} ({pct(c.changePct)})
          </div>
          <div>52w High</div>
          <div className="text-right tabular-nums">{c.week52High ?? "—"}</div>
          <div>ATH</div>
          <div className="text-right tabular-nums">{c.allTimeHigh ?? "—"}</div>
          <div>P/E</div>
          <div className="text-right tabular-nums">{c.pe ?? "—"}</div>
          <div>Fwd P/E</div>
          <div className="text-right tabular-nums">{c.fwdPe ?? "—"}</div>
        </div>
      </div>

      {/* middle-right position/personal */}
      <div className="rounded-xl border p-3 bg-white">
        <div className="grid grid-cols-2 gap-x-4 gap-y-1 text-sm">
          <div>Position @ Avg / Cost</div>
          <div className="text-right tabular-nums">{c.quantity} @ {moneyNoSymbol(c.avgPrice)} / {moneyNoSymbol(c.costBasis)}</div>
          <div>Market Value</div>
          <div className="text-right tabular-nums">{moneyNoSymbol(c.marketValue)}</div>
          <div>Market Value % of Assets</div>
          <div className="text-right tabular-nums">{pct(c.marketValuePctOfAssets)}</div>
          <div>Unrealized P&L</div>
          <div className={`text-right tabular-nums ${unrealColor}`}>{moneyNoSymbol(c.unrealizedAbs)} ({pct(c.unrealizedPct)})</div>
          <div>Notes</div>
          <div className="text-right truncate" title={c.notes || ""}>{c.notes || "—"}</div>
          <div>Rating</div>
          <div className="text-right">{"★".repeat(c.rating ?? 0) || "—"}</div>
        </div>
      </div>
    </div>
  );
}