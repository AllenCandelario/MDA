export type ActiveAccount = {
  id: string;     // internal GUID (string in TS)
  userId: string;
  ibkrAccountId: string;  // "U1234567"
  baseCurrency?: string;
  accountType?: string;
};