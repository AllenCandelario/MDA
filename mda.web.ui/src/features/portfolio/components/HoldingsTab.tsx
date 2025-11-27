import { useHoldings } from "../hooks/useHoldings";
import { usePortfolioSummary } from "../hooks/usePortfolioSummary";
import CategorySection from "./CategorySection";
import HoldingsTable from "./HoldingsTable";

export default function HoldingsTab() {
  const { summary } = usePortfolioSummary();
  const invested = summary?.investedAsset ?? 0;
  const { byCategory, categorySummaries } = useHoldings(invested);

  return (
    <div className="space-y-6">
      {categorySummaries.map(cs => (
        <section key={cs.category}>
          <CategorySection summary={cs}>
            <HoldingsTable rows={byCategory.get(cs.category) || []} />
          </CategorySection>
        </section>
      ))}
    </div>
  );
}