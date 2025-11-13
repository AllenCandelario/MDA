// src/features/realtime/components/AccountUpdateTable.tsx
import { useAccountUpdates } from '../hooks/useAccountUpdates';

export function AccountUpdateTable() {
  const { accountUpdates, isConnected } = useAccountUpdates();

  return (
    <div className="max-w-4xl mx-auto mt-8 space-y-4">
      <div className="flex items-center justify-between">
        <h1 className="text-xl font-semibold">IBKR Account Updates</h1>
        <span
          className={`inline-flex items-center rounded-full px-3 py-1 text-xs font-medium ${
            isConnected ? 'bg-green-100 text-green-800' : 'bg-red-100 text-red-800'
          }`}
        >
          <span
            className={`mr-2 h-2 w-2 rounded-full ${
              isConnected ? 'bg-green-500' : 'bg-red-500'
            }`}
          />
          {isConnected ? 'Connected' : 'Disconnected'}
        </span>
      </div>

      <div className="overflow-hidden rounded-lg border border-gray-200 bg-white shadow-sm">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50">
            <tr>
              <th
                scope="col"
                className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-500"
              >
                Key
              </th>
              <th
                scope="col"
                className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-500"
              >
                Value
              </th>
              <th
                scope="col"
                className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-500"
              >
                Received At
              </th>
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-200 bg-white">
            {accountUpdates.length === 0 ? (
              <tr>
                <td
                  colSpan={3}
                  className="px-4 py-6 text-center text-sm text-gray-500"
                >
                  No messages received yet.
                </td>
              </tr>
            ) : (
              accountUpdates.map((msg, idx) => (
                <tr key={idx} className="hover:bg-gray-50">
                  <td className="px-4 py-2 text-sm font-mono text-gray-800">
                    {msg.key}
                  </td>
                  <td className="px-4 py-2 text-sm text-gray-800 break-all">
                    {msg.value}
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
