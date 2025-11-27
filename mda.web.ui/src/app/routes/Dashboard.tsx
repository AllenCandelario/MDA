import ConnectionBadge from "../../components/ConnectionBadge";
import PortfolioSidebar from "../../features/portfolio/components/PortfolioSidebar";
import PortfolioTabs from "../../features/portfolio/components/PortfolioTabs";
// import { AccountUpdateTable } from "../../features/realtime/components/AccountUpdateTable"; // optional debug

export default function Dashboard() {
  return (
    <div className="min-h-dvh bg-gray-50">
      <ConnectionBadge />

      {/* empty header strip so grid starts a bit lower */}
      <header className="h-15" />

      <div className="max-w-none px-6 pb-6">
        {/* let right column grow, keep roomy gap */}
        <div className="grid grid-cols-1 lg:grid-cols-[22rem_minmax(0,1fr)] gap-8">
          <PortfolioSidebar />
          <PortfolioTabs />
        </div>
      </div>
    </div>
  );
}