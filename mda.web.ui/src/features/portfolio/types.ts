

export type PortfolioSummary = {
  ibkrAccountId: string;
  baseCurrency: string;
  accountType: string;
  lastUpdatedUtc: string; 
  totalPortfolioValue: number;   // base currency
  settledCash: number;           // base currency
  excessLiquidity: number;       // base currency
  buyingPower: number;           // base currency
  investedAsset: number;         // derived (total - settled)
  dailyPnL: number;              // base currency
  unrealizedPnl: number;         // base currency
  // expectedDividendsYear: number; // base currency
  // dividendsPaidYtd: number;      // base currency
};

export type HoldingCategory = "Semiconductor" | "AI" | "Energy" | "ETF" | "Other" | "Uncategorized";

export type Holdings = { holdings: Holding[]; }

export type Holding = {
  symbol: string;
  name: string;
  currency: string; // e.g. USD
  category: HoldingCategory;
  lastPrice: number | null;
  changeAbs: number | null;
  changePct: number | null; // 0.0123 => 1.23%
  week52High: number | null;
  allTimeHigh: number | null;
  pe: number | null;
  fwdPe: number | null;

  // position data
  quantity: number;
  avgPrice: number;
  costBasis: number; // quantity * avgPrice in currency
  marketValue: number; // quantity * lastPrice in currency
  marketValuePctOfAssets: number; // 0 to 1 vs invested asset
  unrealizedAbs: number;
  unrealizedPct: number; // 0 to 1

  notes: string | null;
  rating?: number | null;
};

export type CategoryGoal = {
  shortTermPct?: number; // target of invested asset, 0 to 1
  midTermPct?: number;
  longTermPct?: number;
};

export type CategorySummary = {
  category: HoldingCategory;
  totalMarketValue: number;
  pctOfInvested: number; // 0 to 1
  goals?: CategoryGoal;
};

export type DividendItem = {
  symbol: string;
  name: string;
  currency: string;
  amount: number; // per‑payment or forecasted
  payDate?: string; // ISO
  exDate?: string;  // ISO
  status: "Paid" | "Scheduled" | "Forecast";
};