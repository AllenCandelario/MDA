import { useDividends } from "../hooks/useDividends";

function moneyNoSymbol(n?: number) {
  if (n == null) return "—";
  return new Intl.NumberFormat(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(n);
}

export default function DividendsTab() {
  const { items } = useDividends();

  return (
    <div className="rounded-2xl border bg-white overflow-hidden">
      <table className="min-w-full divide-y divide-gray-200">
        <thead className="bg-gray-50">
          <tr>
            <th className="px-4 py-2 text-left text-xs font-semibold text-gray-600 uppercase">Symbol</th>
            <th className="px-4 py-2 text-left text-xs font-semibold text-gray-600 uppercase">Name</th>
            <th className="px-4 py-2 text-right text-xs font-semibold text-gray-600 uppercase">Amount</th>
            <th className="px-4 py-2 text-left text-xs font-semibold text-gray-600 uppercase">Ex‑Date</th>
            <th className="px-4 py-2 text-left text-xs font-semibold text-gray-600 uppercase">Pay Date</th>
            <th className="px-4 py-2 text-left text-xs font-semibold text-gray-600 uppercase">Status</th>
          </tr>
        </thead>
        <tbody className="divide-y divide-gray-100">
          {items.map((d, idx) => (
            <tr key={idx} className="hover:bg-gray-50">
              <td className="px-4 py-2 font-medium">{d.symbol}</td>
              <td className="px-4 py-2">{d.name}</td>
              <td className="px-4 py-2 text-right tabular-nums">{moneyNoSymbol(d.amount)}</td>
              <td className="px-4 py-2">{d.exDate ?? "—"}</td>
              <td className="px-4 py-2">{d.payDate ?? "—"}</td>
              <td className="px-4 py-2">{d.status}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}