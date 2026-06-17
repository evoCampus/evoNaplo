import { Search } from "lucide-react";
import { Input, Checkbox } from "@evonaplo/ui-library";

export interface CheckboxListItem {
  id: string;
  primaryText: string;
  secondaryText?: string;
}

interface SearchableCheckboxListProps {
  title: string;
  isEditing: boolean;
  searchValue: string;
  onSearchChange: (value: string) => void;
  items: CheckboxListItem[];
  selectedIds: string[];
  onToggle: (id: string) => void;
  emptyMessage: string;
}

export function SearchableCheckboxList({
  title,
  isEditing,
  searchValue,
  onSearchChange,
  items,
  selectedIds,
  onToggle,
  emptyMessage,
}: SearchableCheckboxListProps) {
  const filteredItems = items
    .filter(
      (item) =>
        item.primaryText?.toLowerCase().includes(searchValue.toLowerCase()) ||
        item.secondaryText?.toLowerCase().includes(searchValue.toLowerCase())
    )
    .sort((a, b) => {
      const aChecked = selectedIds.includes(a.id);
      const bChecked = selectedIds.includes(b.id);
      if (aChecked !== bChecked) return aChecked ? -1 : 1;
      return a.primaryText.localeCompare(b.primaryText);
    });

  return (
    <div className="flex flex-col gap-2 pb-4 border-t border-foreground/5 pt-4">
      <div className="flex items-center justify-between">
        <h3 className="text-sm font-semibold text-muted-foreground">{title}</h3>
        {isEditing && (
          <div className="relative w-48">
            <Search className="absolute left-2.5 top-1/2 -translate-y-1/2 w-3.5 h-3.5 text-muted-foreground pointer-events-none" />
            <Input
              type="text"
              placeholder="Search..."
              value={searchValue}
              onChange={(e: React.ChangeEvent<HTMLInputElement>) => onSearchChange(e.target.value)}
              className="pl-7 h-7 text-xs bg-background/50 border-border/30 focus-visible:ring-2 focus-visible:ring-ring/30 focus-visible:border-ring/30"
            />
          </div>
        )}
      </div>
      <div className="max-h-36 overflow-y-auto border border-border/30 rounded-xl p-3 bg-background/30 grid gap-2 mt-1">
        {filteredItems.map((item) => {
          const isChecked = selectedIds.includes(item.id);
          return (
            <div
              key={item.id}
              className={`flex items-center gap-3 py-1 px-2 rounded-lg hover:bg-foreground/5 transition-colors ${
                isEditing ? "cursor-pointer" : "cursor-default"
              }`}
              onClick={() => isEditing && onToggle(item.id)}
            >
              <Checkbox
                checked={isChecked}
                disabled={!isEditing}
                className="pointer-events-none"
              />
              <div className="flex flex-col">
                <span className="text-sm font-medium text-foreground">
                  {item.primaryText}
                </span>
                {item.secondaryText && (
                  <span className="text-xs text-muted-foreground line-clamp-1">
                    {item.secondaryText}
                  </span>
                )}
              </div>
            </div>
          );
        })}
        {filteredItems.length === 0 && (
          <span className="text-xs text-muted-foreground text-center py-2">
            {emptyMessage}
          </span>
        )}
      </div>
    </div>
  );
}
