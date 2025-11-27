import { useState } from "react";

type CollapseProps = {
    title: React.ReactNode;
    children: React.ReactNode;
    defaultOpen?: boolean; 
    displayShowHideWords?: boolean;
}

export function Collapse({ title, children, defaultOpen = false, displayShowHideWords = true }: CollapseProps) {
  const [open, setOpen] = useState(defaultOpen);

  return (
    <div className="border rounded-lg">
      <button
        type="button"
        onClick={() => setOpen(o => !o)}
        className="w-full flex items-center justify-between px-3 py-2 text-left"
      >
        <div className="flex-1 min-w-0">{title}</div>
        {displayShowHideWords && (
          <span className="ml-3 shrink-0 text-xs text-gray-500">{open ? "Hide" : "Show"}</span>
        )}
      </button>
      {open && <div className="border-t px-3 py-2">{children}</div>}
    </div>
  );
}