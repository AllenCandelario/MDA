export type AccountId = string;

export type PortfolioSummary = {
  accountId: AccountId;
  totalPortfolioValue: number;   // base currency
  settledCash: number;           // base currency
  excessLiquidity: number;       // base currency
  buyingPower: number;           // base currency
  investedAsset: number;         // derived (total - settled)
  dailyPnL: number;              // base currency
  unrealizedPnL: number;         // base currency
  expectedDividendsYear: number; // base currency
  dividendsPaidYtd: number;      // base currency
};

export type HoldingCategory = "Semiconductor" | "AI" | "Energy" | "ETF" | "Other";

export type Holding = {
  symbol: string;
  name: string;
  currency: string; // e.g. USD
  category: HoldingCategory;
  lastPrice: number;
  changeAbs: number;
  changePct: number; // 0.0123 => 1.23%
  week52High?: number;
  allTimeHigh?: number;
  pe?: number;
  fwdPe?: number;

  // position data
  quantity: number;
  avgPrice: number;
  costBasis: number; // quantity * avgPrice in currency
  marketValue: number; // quantity * lastPrice in currency
  marketValuePctOfAssets: number; // 0..1 vs invested asset
  unrealizedAbs: number;
  unrealizedPct: number; // 0..1

  notes?: string;
  rating?: 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10;
};

export type CategoryGoal = {
  shortTermPct?: number; // target of invested asset, 0..1
  midTermPct?: number;
  longTermPct?: number;
};

export type CategorySummary = {
  category: HoldingCategory;
  totalMarketValue: number;
  pctOfInvested: number; // 0..1
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