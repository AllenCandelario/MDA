import { Collapse } from "../../../components/Collapse";
import type { CategorySummary, HoldingCategory } from "../types";

function pct(n: number) {
  return (n * 100).toFixed(2) + "%";
}

type Props = {
  summary: CategorySummary;
  children: React.ReactNode;
};

export default function CategorySection({ summary, children }: Props) {
  const { category, pctOfInvested, goals } = summary;

  const title = (
    <div className="flex w-full items-center justify-between">
      <div className="font-semibold">{category as HoldingCategory}</div>
      <div className="ml-6 flex items-center gap-3 text-xs text-gray-600">
        <span className="font-medium whitespace-nowrap">{pct(pctOfInvested)} of invested</span>
        {goals && (
          <span className="hidden sm:inline text-gray-500 whitespace-nowrap">
            Goals: {goals.shortTermPct != null ? pct(goals.shortTermPct) : "—"} / {goals.midTermPct != null ? pct(goals.midTermPct) : "—"} / {goals.longTermPct != null ? pct(goals.longTermPct) : "—"}
          </span>
        )}
      </div>
    </div>
  );

  return (
    <Collapse title={title} defaultOpen displayShowHideWords={false}>
      {children}
    </Collapse>
  );
}