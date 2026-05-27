import { useState } from "react";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
  Button,
  Input,
  Checkbox,
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@evonaplo/ui-library";
import { SlidersHorizontal, X, RotateCcw } from "lucide-react";

export type FilterField<T> = {
  key: keyof T;
  label: string;
  type: "text" | "boolean" | "select";
  options?: { label: string; value: string | number | boolean }[];
};

interface AdminFilterProps<T> {
  fields: FilterField<T>[];
  onFilterChange: (filters: Partial<Record<keyof T, unknown>>) => void;
  currentFilters: Partial<Record<keyof T, unknown>>;
}

export function AdminFilter<T>({
  fields,
  onFilterChange,
  currentFilters,
}: AdminFilterProps<T>) {
  const [isOpen, setIsOpen] = useState(false);

  const handleValueChange = (key: keyof T, value: unknown) => {
    const newValue = value === "all" ? undefined : value;

    const newFilters = { ...currentFilters };
    if (newValue === undefined || newValue === "") {
      delete newFilters[key];
    } else {
      newFilters[key] = newValue;
    }

    onFilterChange(newFilters);
  };

  const clearFilters = () => {
    onFilterChange({});
  };

  const activeCount = Object.keys(currentFilters).length;

  return (
    <Popover open={isOpen} onOpenChange={setIsOpen}>
      <PopoverTrigger asChild>
        <button className="flex items-center gap-2 text-muted-foreground hover:text-foreground transition-colors relative group cursor-pointer">
          <span className="text-sm font-medium">Filters</span>
          <SlidersHorizontal className="w-4 h-4" />
          {activeCount > 0 && (
            <span className="absolute -top-2 -right-2 bg-primary text-primary-foreground text-[10px] font-bold min-w-4 h-4 px-1 rounded-full flex items-center justify-center animate-in zoom-in duration-200">
              {activeCount}
            </span>
          )}
        </button>
      </PopoverTrigger>
      <PopoverContent className="w-80 p-4 bg-secondary border border-border/40 shadow-xl rounded-2xl" align="end">
        <div className="flex items-center justify-between mb-4">
          <h4 className="font-semibold text-foreground">Filter Records</h4>
          <div className="flex items-center gap-1">
            {activeCount > 0 && (
              <Button
                variant="ghost"
                size="sm"
                onClick={clearFilters}
                className="h-8 px-2 text-[11px] text-muted-foreground hover:text-foreground hover:bg-transparent"
              >
                <RotateCcw className="w-3 h-3 mr-1" />
                Reset
              </Button>
            )}
            <Button
              variant="ghost"
              size="icon"
              onClick={() => setIsOpen(false)}
              className="h-8 w-8 rounded-full hover:bg-foreground/5"
            >
              <X className="w-4 h-4" />
            </Button>
          </div>
        </div>

        <div className="space-y-4">
          {fields.map((field) => (
            <div key={String(field.key)} className="space-y-1.5">
              <label className="text-[11px] font-semibold uppercase tracking-wider text-muted-foreground/70 px-1">
                {field.label}
              </label>

              {field.type === "text" && (
                <Input
                  placeholder={`Search by ${field.label.toLowerCase()}...`}
                  value={currentFilters[field.key] || ""}
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) => handleValueChange(field.key, e.target.value)}
                  className="bg-background border-none rounded-xl h-10 text-sm focus-visible:ring-1 focus-visible:ring-primary shadow-sm"
                />
              )}

              {field.type === "boolean" && (
                <div
                  className="flex items-center gap-2 bg-background rounded-xl p-2 px-3 h-10 cursor-pointer hover:bg-background/80 transition-colors shadow-sm"
                  onClick={() => handleValueChange(field.key, currentFilters[field.key] === true ? undefined : true)}
                >
                  <Checkbox
                    id={`filter-${String(field.key)}`}
                    checked={currentFilters[field.key] === true}
                    onCheckedChange={(checked: React.ChangeEvent<HTMLInputElement>) => handleValueChange(field.key, checked.target.checked ? true : undefined)}
                  />
                  <span className="text-sm text-foreground select-none font-medium">{field.label}</span>
                </div>
              )}

              {field.type === "select" && (
                <Select
                  value={String(currentFilters[field.key] ?? "all")}
                  onValueChange={(val: React.ChangeEvent<HTMLSelectElement>) => handleValueChange(field.key, val.target.value)}
                >
                  <SelectTrigger className="bg-background border-none rounded-xl h-10 text-sm focus:ring-1 focus:ring-primary shadow-sm">
                    <SelectValue placeholder="All" />
                  </SelectTrigger>
                  <SelectContent className="bg-secondary border-border/40 shadow-lg rounded-xl">
                    <SelectItem value="all">All</SelectItem>
                    {field.options?.map((opt) => (
                      <SelectItem key={String(opt.value)} value={String(opt.value)}>
                        {opt.label}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              )}
            </div>
          ))}
        </div>
      </PopoverContent>
    </Popover>
  );
}
