import { ArrowRight } from "lucide-react";
import type { LucideIcon } from "lucide-react";

export interface SearchResultGroupProps<T extends { id: unknown }> {
  icon: LucideIcon;
  label: string;
  items: T[];
  onSelect: (item: T) => void;
  primaryText: (item: T) => string;
  secondaryText: (item: T) => string;
}

export function SearchResultGroup<T extends { id: unknown }>({
  icon: Icon,
  label,
  items,
  onSelect,
  primaryText,
  secondaryText,
}: SearchResultGroupProps<T>) {
  if (items.length === 0) return null;

  return (
    <div className="space-y-3">
      <h3 className="text-sm font-semibold text-muted-foreground flex items-center gap-2 px-1">
        <Icon className="h-4 w-4" /> {label} ({items.length})
      </h3>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-3">
        {items.map((item) => (
          <div
            key={String(item.id)}
            onClick={() => onSelect(item)}
            className="flex items-center justify-between p-4 bg-card rounded-xl border border-transparent hover:border-border/50 hover:shadow-sm transition-all cursor-pointer group"
          >
            <div className="flex flex-col gap-0.5">
              <span className="font-medium text-foreground/90 group-hover:text-primary transition-colors">
                {primaryText(item)}
              </span>
              <span className="text-xs text-muted-foreground line-clamp-1">{secondaryText(item)}</span>
            </div>
            <ArrowRight className="h-4 w-4 text-muted-foreground opacity-0 group-hover:opacity-100 transition-opacity" />
          </div>
        ))}
      </div>
    </div>
  );
}
