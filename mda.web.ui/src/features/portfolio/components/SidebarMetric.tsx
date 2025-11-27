type MetricProps = {
  label: string;
  value?: React.ReactNode;
};

export default function SidebarMetric({ label, value }: MetricProps) {
  return (
    <div className="flex items-center justify-between">
      <div className="text-sm text-gray-600">{label}</div>
      <div className="text-sm font-medium tabular-nums">{value ?? "—"}</div>
    </div>
  );
}